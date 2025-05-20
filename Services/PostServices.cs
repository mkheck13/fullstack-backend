using fullstack_backend.Context;
using fullstack_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace fullstack_backend.Services
{
    public class PostServices
    {
        private readonly DataContext _dataContext;

        public PostServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<PostModel>> GetAllPosts()
        {
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<List<PostModel>> GetPostsByUserId(int id)
        {
            return await _dataContext.Posts.Where(post => post.UserId == id).ToListAsync();
        }

        public async Task<PostModel> GetPostById(int id)
        {
            return (await _dataContext.Posts.FindAsync(id))!;
        }

        public async Task<List<PostModel>> GetPostsByDate(string date)
        {
            return await _dataContext.Posts.Where(posts => posts.DateCreated == date).ToListAsync();
        }

        public async Task<bool> CreatePost(PostModel post)
        {
            await _dataContext.Posts.AddAsync(post);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdatePost(PostModel post)
        {
            var postToUpdate = await GetPostById(post.Id);

            if (postToUpdate == null) return false;


            postToUpdate.Username = post.Username;
            postToUpdate.TrueName = post.TrueName;
            postToUpdate.ProfilePicture = post.ProfilePicture;
            postToUpdate.DateCreated = post.DateCreated;
            postToUpdate.Description = post.Description;
            postToUpdate.Stat = post.Stat;
            postToUpdate.Sport = post.Sport;
            postToUpdate.IsPublished = post.IsPublished;
            postToUpdate.IsDeleted = post.IsDeleted;

            _dataContext.Posts.Update(postToUpdate);
            return await _dataContext.SaveChangesAsync() != 0;
        }
    }
}