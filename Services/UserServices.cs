using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using fullstack_backend.Context;
using fullstack_backend.Models;
using fullstack_backend.Models.DTOS;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace fullstack_backend.Services
{
    public class UserServices
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public UserServices(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _config = config;
        }

        public async Task<bool> CreateUser(UserDTO newUser)
        {
            if (await DoesUserExist(newUser.Username!, newUser.Email!)) return false;

            UserModel userToAdd = new();

            PasswordDTO encryptedPassword = HashPassword(newUser.Password!);

            userToAdd.Hash = encryptedPassword.Hash;
            userToAdd.Salt = encryptedPassword.Salt;
            userToAdd.Username = newUser.Username;
            userToAdd.Email = newUser.Email;
            userToAdd.DateOfBirth = newUser.DateOfBirth;

            await _dataContext.User.AddAsync(userToAdd);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        private static PasswordDTO HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(64);

            string salt = Convert.ToBase64String(saltBytes);
            string hash;

            using (var derivedBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256))
            {
                hash = Convert.ToBase64String(derivedBytes.GetBytes(32));
            }

            return new PasswordDTO
            {
                Salt = salt,
                Hash = hash
            };
        }

        private async Task<bool> DoesUserExist(string username, string email)
        {
            return await _dataContext.User.FirstOrDefaultAsync(user => user.Username == username || user.Email == email) != null;
        }

        // public async Task<string> Login(UserLoginDTO user)
        // {
        //     UserModel currentUser = await GetUserByUserNameOrEmail(user.EmailOrUsername!, user.EmailOrUsername!);

        //     if (currentUser == null) return null;

        //     if (!VerifyPassword(user.Password!, currentUser.Salt!, currentUser.Hash!)) return null;

        //     return GenerateJWToken(new List<Claim>());
        // }

        public async Task<string?> Login(UserLoginDTO user)
{
    if (string.IsNullOrWhiteSpace(user.EmailOrUsername) || string.IsNullOrWhiteSpace(user.Password))
    {
        return null;
    }

    var currentUser = await GetUserByUserNameOrEmail(user.EmailOrUsername, user.EmailOrUsername);

    if (currentUser == null) return null;

    if (!VerifyPassword(user.Password, currentUser.Salt!, currentUser.Hash!)) return null;

    return GenerateJWToken(new List<Claim>());
}


        private string GenerateJWToken(List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://fullstackwebapp-bxcja2evd2hef3b9.westus-01.azurewebsites.net/",
                audience: "https://fullstackwebapp-bxcja2evd2hef3b9.westus-01.azurewebsites.net/",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private async Task<UserModel> GetUserByUserNameOrEmail(string username, string email)
        {
            return (await _dataContext.User.FirstOrDefaultAsync(user => user.Username == username || user.Email == email))!;
        }

        private bool VerifyPassword(string password, string salt, string hash)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            string checkHash;

            using (var derivedBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256))
            {
                checkHash = Convert.ToBase64String(derivedBytes.GetBytes(32));
                return hash == checkHash;
            }
        }

        public async Task<UserInfoDTO> GetUserInfoByEmailOrUsername(string emailOrUsername)
        {
            var currentUser = await _dataContext.User.FirstOrDefaultAsync(user => user.Username == emailOrUsername || user.Email == emailOrUsername);

            UserInfoDTO user = new();

            user.Id = currentUser!.Id;
            user.Username = currentUser.Username;
            user.Email = currentUser.Email;
            user.DateOfBirth = currentUser.DateOfBirth;
            user.PhoneNumber = currentUser.PhoneNumber;
            user.ProfilePicture = currentUser.ProfilePicture;

            return user;
        }

        public async Task<UserModel> GetUserByEmailOrUsername(string emailOrUsername)
        {
            return await _dataContext.User.FirstOrDefaultAsync(user => user.Username == emailOrUsername || user.Email == emailOrUsername);
        }

        public async Task<bool> UpdateUserInfo(int userId, UpdateUserDTO updatedUser)
        {
            var user = await _dataContext.User.FindAsync(userId);
            if (user == null) return false;

                if (!string.IsNullOrWhiteSpace(updatedUser.Username)) user.Username = updatedUser.Username;
                if (!string.IsNullOrWhiteSpace(updatedUser.Email)) user.Email = updatedUser.Email;
                if (!string.IsNullOrWhiteSpace(updatedUser.DateOfBirth)) user.DateOfBirth = updatedUser.DateOfBirth;
                if (!string.IsNullOrWhiteSpace(updatedUser.PhoneNumber)) user.PhoneNumber = updatedUser.PhoneNumber;
                if (!string.IsNullOrWhiteSpace(updatedUser.ProfilePicture)) user.ProfilePicture = updatedUser.ProfilePicture;

                _dataContext.User.Update(user);
                return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}