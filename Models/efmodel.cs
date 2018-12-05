using Microsoft.EntityFrameworkCore;
using System;
namespace quizartsocial_backend.Models
{
    public class SocialContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public SocialContext(DbContextOptions<SocialContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>().HasMany(n => n.posts).WithOne().HasForeignKey(c => c.topicId);
            modelBuilder.Entity<Post>().HasMany(n => n.comments).WithOne().HasForeignKey(c => c.postId);
            modelBuilder.Entity<User>().HasMany(n => n.posts).WithOne().HasForeignKey(c => c.userId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(n => n.comments).WithOne().HasForeignKey(c => c.userId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// {
//    optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=QuizRTSocialDb;Trusted_Connection=True;");
// }