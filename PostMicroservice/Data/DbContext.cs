using Microsoft.EntityFrameworkCore;
using PostMicroservice.Models;

namespace PostMicroservice.Data
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasKey(u => u.Id);
            modelBuilder.Entity<Post>().Property(u => u.Id).ValueGeneratedOnAdd();
        }
    }
}
