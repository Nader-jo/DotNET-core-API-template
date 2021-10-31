using ApiTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTemplate.Api.Database.Configuration
{
    internal class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(t => t.Id)
                .ValueGeneratedNever();
            
            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(t => t.Role)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(t => t.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.ToTable("users", "users");
        }
        
    }
}