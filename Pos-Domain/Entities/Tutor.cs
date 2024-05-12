namespace Pos_Domain.Entities;

public sealed class Tutor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    // Navigation property for the Pets
    public ICollection<Pet> Pets { get; set; }
}
