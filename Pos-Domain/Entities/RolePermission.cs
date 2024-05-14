using Pos_Domain.Enums;

namespace Pos_Domain.Entities;

public sealed class RolePermission
{
    public int Id { get; set; }
    public string Role { get; set; }
    public ICollection<PermissionEnum> Permissions { get; set; }
    public ICollection<User> Users { get; set; }
}
