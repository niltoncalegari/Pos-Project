using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pos_Domain.Entities;

namespace Pos_Repository.EntityConfiguration;

internal class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("Pets");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

        builder.Property(p => p.Type).IsRequired();
        builder.Property(p => p.Gender).IsRequired();
        builder.Property(p => p.Color).IsRequired();
        builder.Property(p => p.Breed).IsRequired();
        builder.Property(p => p.Age).IsRequired();
        builder.Property(p => p.Medicines).IsRequired(false);
        builder.Property(p => p.Vaccines).IsRequired(false);
        builder.Property(p => p.Location).IsRequired();
        builder.Property(p => p.TutorName).IsRequired();
        builder.Property(p => p.Photo).IsRequired();
        builder.Property(p => p.Behavior).IsRequired();
        builder.Property(p => p.TutorId).IsRequired();

        builder.HasOne(p => p.Tutor)
            .WithMany(t => t.Pets)
            .HasForeignKey(p => p.TutorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
