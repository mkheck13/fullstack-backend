using fullstack_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace fullstack_backend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options) { }

        public DbSet<UserModel> User { get; set; }
        public DbSet<MessageModel> Message { get; set; }
    }
}
