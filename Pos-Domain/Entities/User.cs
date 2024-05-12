namespace Pos_Domain.Entities;

public sealed class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<UserRolePermission> UserRolePermissions { get; set; }
}
