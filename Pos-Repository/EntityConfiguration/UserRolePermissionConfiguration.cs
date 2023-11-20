using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pos_Domain.Entities;

namespace Pos_Repository.EntityConfiguration;

internal class UserRolePermissionConfiguration : IEntityTypeConfiguration<UserRolePermission>
{
    public void Configure(EntityTypeBuilder<UserRolePermission> builder)
    {
        builder.ToTable("UserRolePermissions");

        builder.Property(urp => urp.Role)
            .HasMaxLength(50);

        builder.Property(urp => urp.Permissions)
            .HasMaxLength(500);
    }
}
