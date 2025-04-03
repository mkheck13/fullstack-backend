using Microsoft.EntityFrameworkCore;
using fullstack_backend.Models;

namespace fullstack_backend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base (options)
        {
        }
        public DbSet<UserModel> User { get; set;}
    }
}