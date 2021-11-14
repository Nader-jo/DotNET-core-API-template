using ApiTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infrastructure.Database
{
    public class ApiTemplateDbContext : DbContext
    {
        public ApiTemplateDbContext(DbContextOptions<ApiTemplateDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}