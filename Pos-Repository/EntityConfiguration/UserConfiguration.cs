using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos_Domain.Entities;

namespace Pos_Repository.EntityConfiguration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(t => t.Id).IsRequired();
        builder.HasKey(p => p.Id);
        builder.Property(u => u.Username)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(u => u.Password)
            .HasMaxLength(100)
            .IsRequired(); // Assuming encrypted passwords are stored.
        builder.HasMany(u => u.RolePermissions)
            .WithMany(urp => urp.Users);
            
    }
}
