using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pos_Domain.Entities;

namespace Pos_Repository.EntityConfiguration;

internal class TutorConfiguration : IEntityTypeConfiguration<Tutor>
{
    public void Configure(EntityTypeBuilder<Tutor> builder)
    {
        builder.ToTable("Tutors");
        builder.Property(t => t.Id).IsRequired();
        builder.HasKey(p => p.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.PhoneNumber).IsRequired().HasMaxLength(15);
        builder.Property(t => t.Email).HasMaxLength(50);
        builder.Property(t => t.Address).HasMaxLength(200);
    }
}
