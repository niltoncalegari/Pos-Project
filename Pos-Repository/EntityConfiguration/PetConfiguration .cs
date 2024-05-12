using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pos_Domain.Entities;

namespace Pos_Repository.EntityConfiguration;

internal class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("Pets");

        // Configuring properties
        builder.Property(p => p.Color).IsRequired();
        builder.Property(p => p.Breed).IsRequired();
        builder.Property(p => p.Age).IsRequired();
        builder.Property(p => p.Medicines).IsRequired(false);
        builder.Property(p => p.Vaccines).IsRequired(false);
        builder.Property(p => p.Location).IsRequired();
        builder.Property(p => p.Behavior).IsRequired();
        builder.Property(p => p.TutorId);
        builder.HasOne(p => p.Tutor)
               .WithMany(t => t.Pets)
               .HasForeignKey(p => p.TutorId);

        builder.HasIndex(p => p.Id).IsUnique();

        builder.Property(p => p.Id)
               .HasComputedColumnSql("IIF([Gender] = 1, (1 + 2 * (SELECT COUNT(*) FROM Pets WHERE Gender = 1)), 2 * (SELECT COUNT(*) FROM Pets WHERE Gender = 0))");
    }
}
