using Microsoft.AspNetCore.Identity;

namespace VeterinaryClinic.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Cpf { get; set; }
    public string? Address { get; set; }
    public Guid UserType { get; set; }
    public Guid? RoleId { get; set; }
    public Guid Status { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public DateTime? BlockedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ApplicationRole : IdentityRole<Guid>
{
    public string Description { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ApplicationUserRole : IdentityUserRole<Guid>
{
}

public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
}

public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
}

public class ApplicationUserToken : IdentityUserToken<Guid>
{
}