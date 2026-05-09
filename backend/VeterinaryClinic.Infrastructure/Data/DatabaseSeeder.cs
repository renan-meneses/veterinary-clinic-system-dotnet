using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Enums;

namespace VeterinaryClinic.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        await context.Database.MigrateAsync();

        await SeedRolesAsync(roleManager);
        await SeedPermissionsAsync(context);
        await SeedRolePermissionsAsync(context);
        await SeedAdminUserAsync(userManager);
        await SeedPaymentMethodsAsync(context);
        await SeedServiceCategoriesAsync(context);
    }

    private static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
    {
        var roles = new[]
        {
            new ApplicationRole { Name = "Administrator", Description = "Administrador do sistema com acesso total", IsDefault = false, IsActive = true },
            new ApplicationRole { Name = "Veterinarian", Description = "Veterinário responsável por consultas", IsDefault = false, IsActive = true },
            new ApplicationRole { Name = "Attendant", Description = "Atendente da clínica", IsDefault = false, IsActive = true },
            new ApplicationRole { Name = "PetshopEmployee", Description = "Funcionário do petshop", IsDefault = false, IsActive = true },
            new ApplicationRole { Name = "Financial", Description = "Responsável pelo financeiro", IsDefault = false, IsActive = true },
            new ApplicationRole { Name = "Tutor", Description = "Tutor de animais", IsDefault = true, IsActive = true },
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name!))
            {
                await roleManager.CreateAsync(role);
            }
        }
    }

    private static async Task SeedPermissionsAsync(ApplicationDbContext context)
    {
        if (await context.Permissions.AnyAsync())
            return;

        var modules = new[]
        {
            (ModuleType.Users, "Usuários"),
            (ModuleType.Tutors, "Tutores"),
            (ModuleType.Animals, "Animais"),
            (ModuleType.Vaccines, "Vacinas"),
            (ModuleType.Consultations, "Consultas"),
            (ModuleType.Hospitalizations, "Internações"),
            (ModuleType.Petshop, "Petshop"),
            (ModuleType.Finance, "Financeiro"),
            (ModuleType.Sales, "Vendas"),
            (ModuleType.Products, "Produtos"),
            (ModuleType.Services, "Serviços"),
            (ModuleType.Office, "Consultório"),
            (ModuleType.ClinicStructure, "Estrutura"),
            (ModuleType.AttendanceQueue, "Fila de Atendimento"),
            (ModuleType.Reports, "Relatórios"),
            (ModuleType.Settings, "Configurações"),
        };

        var permissions = new List<Permission>();

        foreach (var (moduleType, moduleName) in modules)
        {
            foreach (PermissionAction action in Enum.GetValues(typeof(PermissionAction)))
            {
                permissions.Add(new Permission
                {
                    Module = moduleName,
                    ModuleType = moduleType,
                    Action = action,
                    Description = $"{GetActionName(action)} {moduleName}"
                });
            }
        }

        await context.Permissions.AddRangeAsync(permissions);
        await context.SaveChangesAsync();
    }

    private static async Task SeedRolePermissionsAsync(ApplicationDbContext context)
    {
        if (await context.RolePermissions.AnyAsync())
            return;

        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Administrator");
        if (adminRole == null) return;

        var allPermissions = await context.Permissions.ToListAsync();
        var rolePermissions = allPermissions.Select(p => new RolePermission
        {
            RoleId = adminRole.Id,
            PermissionId = p.Id
        }).ToList();

        await context.RolePermissions.AddRangeAsync(rolePermissions);
        await context.SaveChangesAsync();
    }

    private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        if (await userManager.Users.AnyAsync(u => u.Email == "admin@vetclinic.com"))
            return;

        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Administrator");
        if (adminRole == null) return;

        var adminUser = new ApplicationUser
        {
            UserName = "admin@vetclinic.com",
            Email = "admin@vetclinic.com",
            Name = "Administrador",
            EmailConfirmed = true,
            UserType = UserType.Administrator,
            RoleId = adminRole.Id,
            Status = UserStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await userManager.CreateAsync(adminUser, "Admin@123456");
        await userManager.AddToRoleAsync(adminUser, "Administrator");
    }

    private static async Task SeedPaymentMethodsAsync(ApplicationDbContext context)
    {
        // Seeds will be implemented through enums
    }

    private static async Task SeedServiceCategoriesAsync(ApplicationDbContext context)
    {
        // Seeds will be implemented through enums
    }

    private static string GetActionName(PermissionAction action) => action switch
    {
        PermissionAction.View => "Visualizar",
        PermissionAction.Create => "Criar",
        PermissionAction.Edit => "Editar",
        PermissionAction.Delete => "Excluir",
        PermissionAction.Export => "Exportar",
        PermissionAction.Manage => "Gerenciar",
        _ => action.ToString()
    };
}