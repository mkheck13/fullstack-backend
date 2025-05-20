using fullstack_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace fullstack_backend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<UserModel> User { get; set; }
        public DbSet<FriendshipModel> Friendships { get; set; }
        public DbSet<PostModel> Posts { get; set;}
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<ConversationModel> Conversations { get; set; }
        public DbSet<MatchModel> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Friendship relationships
            modelBuilder.Entity<FriendshipModel>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendshipModel>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message-Conversation relationship (one-to-many)
            modelBuilder.Entity<ConversationModel>()
                .HasMany(c => c.Messages)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);  // If a conversation is deleted, delete its messages

            modelBuilder.Entity<MessageModel>()
                .HasOne<ConversationModel>() // Defining that each message belongs to a conversation
                .WithMany(c => c.Messages) // A conversation can have many messages
                .HasForeignKey(m => m.ConversationId)  // Linking message to the conversation
                .OnDelete(DeleteBehavior.Cascade);  // Delete messages when the conversation is deleted

            base.OnModelCreating(modelBuilder);
        }
    }
}