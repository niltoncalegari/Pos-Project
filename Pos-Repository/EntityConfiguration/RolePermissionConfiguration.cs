using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pos_Domain.Entities;
using Pos_Domain.Enums;

namespace Pos_Repository.EntityConfiguration;

internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");
        builder.Property(t => t.Id).IsRequired();
        builder.HasKey(p => p.Id);

        builder.Property(urp => urp.Role)
            .HasMaxLength(50);

        builder.Property(urp => urp.Permissions)
            .HasConversion(v => string.Join(",", v),
                           v => v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(e => Enum.Parse<PermissionEnum>(e)).ToList());
    }
}
