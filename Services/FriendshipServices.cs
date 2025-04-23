using fullstack_backend.Models;
using fullstack_backend.Context;
using Microsoft.EntityFrameworkCore;

namespace fullstack_backend.Services
{
    public class FriendshipServices
    {
        private readonly DataContext _context;

        public FriendshipServices(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFriend(int userId, int friendId)
        {
            var existingFriendship = await _context.Friendships
                .FirstOrDefaultAsync(f => (f.UserId == userId && f.FriendId == friendId) || (f.UserId == friendId && f.FriendId == userId));

            if (existingFriendship != null) return false;

            var friendship = new FriendshipModel
            {
                UserId = userId,
                FriendId = friendId,
                IsConfirmed = true 
            };

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AreFriends(int userId, int friendId)
        {
            return await _context.Friendships
                   .AnyAsync(f => ((f.UserId == userId && f.FriendId == friendId) || (f.UserId == friendId && f.FriendId == userId)) && f.IsConfirmed);
        }

        public async Task<bool> RemoveFriend(int userId, int friendId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.UserId == userId && f.FriendId == friendId) ||
                    (f.UserId == friendId && f.FriendId == userId));

            if (friendship == null) return false;

            _context.Friendships.Remove(friendship);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<UserModel>> GetFriends(int userId)
        {
            var friends = await _context.Friendships
                .Where(f => (f.UserId == userId || f.FriendId == userId) && f.IsConfirmed)
                .Include(f => f.User)
                .Include(f => f.Friend)
                .ToListAsync();

            return friends.Select(f =>
                f.UserId == userId ? f.Friend : f.User
            ).ToList();
        }

    }
}