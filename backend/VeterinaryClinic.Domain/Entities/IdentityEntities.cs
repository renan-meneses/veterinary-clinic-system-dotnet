using VeterinaryClinic.Domain.Common;

namespace VeterinaryClinic.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Cpf { get; set; }
    public string? Address { get; set; }
    public Guid UserType { get; set; }
    public Guid? RoleId { get; set; }
    public Guid Status { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime? LastLoginAt { get; set; }
    public DateTime? BlockedAt { get; set; }

    public Role? Role { get; set; }
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<User> Users { get; set; } = new List<User>();
}

public class Permission : BaseEntity
{
    public string Module { get; set; } = string.Empty;
    public Guid ModuleType { get; set; }
    public Guid Action { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}

public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }

    public Role Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}

public class UserPermission : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }
    public bool IsGranted { get; set; } = true;

    public User User { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevokedByIp { get; set; }
    public string? ReplacedByToken { get; set; }

    public User User { get; set; } = null!;
}

public class AuditLog : BaseEntity
{
    public Guid UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }

    public User? User { get; set; }
}