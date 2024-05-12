using Pos_Domain.Enums;

namespace Pos_Domain.Entities;

public sealed class Pet
{
    public int Id { get; set; }
    public PetTypeEnum Type { get; set; }
    public PetGenderEnum Gender { get; set; }
    public string Color { get; set; }
    public string Breed { get; set; }
    public DateTime Age { get; set; }
    public string Medicines { get; set; }
    public string Vaccines { get; set; }
    public string Location { get; set; }
    public string TutorName { get; set; }
    public byte Photo { get; set; }
    public BehaviorEnum Behavior { get; set; }

    // Navigation property for the Tutor
    public int? TutorId { get; set; }
    public Tutor Tutor { get; set; }
}
