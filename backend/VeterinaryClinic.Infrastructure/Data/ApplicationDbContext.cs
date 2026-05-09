using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public DbSet<Tutor> Tutors => Set<Tutor>();
    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<AnimalTutor> AnimalTutors => Set<AnimalTutor>();
    public DbSet<Vaccine> Vaccines => Set<Vaccine>();
    public DbSet<Consultation> Consultations => Set<Consultation>();
    public DbSet<Hospitalization> Hospitalizations => Set<Hospitalization>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<FinancialTransaction> FinancialTransactions => Set<FinancialTransaction>();

    public DbSet<ClinicEnvironment> ClinicEnvironments => Set<ClinicEnvironment>();
    public DbSet<PetshopAttendance> PetshopAttendances => Set<PetshopAttendance>();
    public DbSet<QueueAttendance> QueueAttendances => Set<QueueAttendance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureIdentityEntities(modelBuilder);
        ConfigureClinicalEntities(modelBuilder);
        ConfigureBusinessEntities(modelBuilder);
        ConfigureClinicEntities(modelBuilder);

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.Action).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Entity).HasMaxLength(100).IsRequired();
        });
    }

    private void ConfigureIdentityEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Cpf).HasMaxLength(14);
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("permissions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Module).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("role_permissions");
            entity.HasKey(e => new { e.RoleId, e.PermissionId });
            entity.HasOne(e => e.Role).WithMany(r => r.RolePermissions).HasForeignKey(e => e.RoleId);
            entity.HasOne(e => e.Permission).WithMany(p => p.RolePermissions).HasForeignKey(e => e.PermissionId);
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.ToTable("user_permissions");
            entity.HasKey(e => new { e.UserId, e.PermissionId });
            entity.HasOne(e => e.User).WithMany(u => u.UserPermissions).HasForeignKey(e => e.UserId);
            entity.HasOne(e => e.Permission).WithMany(p => p.UserPermissions).HasForeignKey(e => e.PermissionId);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("refresh_tokens");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Token).HasMaxLength(500).IsRequired();
            entity.HasIndex(e => e.Token).IsUnique();
            entity.HasOne(e => e.User).WithMany(u => u.RefreshTokens).HasForeignKey(e => e.UserId);
        });
    }

    private void ConfigureClinicalEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.ToTable("tutors");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
            entity.HasIndex(e => e.Email);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Cpf).HasMaxLength(14);
            entity.HasIndex(e => e.Cpf);
        });

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.ToTable("animals");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.Name);
        });

        modelBuilder.Entity<AnimalTutor>(entity =>
        {
            entity.ToTable("animal_tutors");
            entity.HasKey(e => new { e.AnimalId, e.TutorId });
            entity.HasOne(e => e.Animal).WithMany(a => a.AnimalTutors).HasForeignKey(e => e.AnimalId);
            entity.HasOne(e => e.Tutor).WithMany(t => t.Animals).HasForeignKey(e => e.TutorId);
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.ToTable("vaccines");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.VaccineName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Batch).HasMaxLength(50);
            entity.Property(e => e.Manufacturer).HasMaxLength(200);
            entity.HasOne(e => e.Animal).WithMany(a => a.Vaccines).HasForeignKey(e => e.AnimalId);
            entity.HasOne(e => e.Tutor).WithMany().HasForeignKey(e => e.TutorId);
        });

        modelBuilder.Entity<Consultation>(entity =>
        {
            entity.ToTable("consultations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reason).HasMaxLength(1000);
            entity.Property(e => e.Diagnosis).HasMaxLength(2000);
            entity.HasOne(e => e.Animal).WithMany(a => a.Consultations).HasForeignKey(e => e.AnimalId);
            entity.HasOne(e => e.Tutor).WithMany().HasForeignKey(e => e.TutorId);
        });

        modelBuilder.Entity<Hospitalization>(entity =>
        {
            entity.ToTable("hospitalizations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reason).HasMaxLength(2000);
            entity.HasOne(e => e.Animal).WithMany(a => a.Hospitalizations).HasForeignKey(e => e.AnimalId);
            entity.HasOne(e => e.Tutor).WithMany().HasForeignKey(e => e.TutorId);
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.ToTable("medical_records");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Animal).WithMany(a => a.MedicalRecords).HasForeignKey(e => e.AnimalId);
            entity.HasOne(e => e.Consultation).WithMany(c => c.MedicalRecords).HasForeignKey(e => e.ConsultationId);
        });
    }

    private void ConfigureBusinessEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Sku).HasMaxLength(50);
            entity.HasIndex(e => e.Sku);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("services");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("sales");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Tutor).WithMany(t => t.Sales).HasForeignKey(e => e.TutorId);
            entity.HasOne(e => e.Animal).WithMany(a => a.Sales).HasForeignKey(e => e.AnimalId);
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.ToTable("sale_items");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Sale).WithMany(s => s.Items).HasForeignKey(e => e.SaleId);
            entity.HasOne(e => e.Product).WithMany(p => p.SaleItems).HasForeignKey(e => e.ProductId);
            entity.HasOne(e => e.Service).WithMany(s => s.SaleItems).HasForeignKey(e => e.ServiceId);
        });

        modelBuilder.Entity<FinancialTransaction>(entity =>
        {
            entity.ToTable("financial_transactions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).HasMaxLength(500).IsRequired();
            entity.HasOne(e => e.Tutor).WithMany(t => t.FinancialTransactions).HasForeignKey(e => e.TutorId);
            entity.HasOne(e => e.Sale).WithMany().HasForeignKey(e => e.SaleId);
        });
    }

    private void ConfigureClinicEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClinicEnvironment>(entity =>
        {
            entity.ToTable("clinic_environments");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<PetshopAttendance>(entity =>
        {
            entity.ToTable("petshop_attendances");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Tutor).WithMany().HasForeignKey(e => e.TutorId);
            entity.HasOne(e => e.Animal).WithMany(a => a.PetshopAttendances).HasForeignKey(e => e.AnimalId);
            entity.HasOne(e => e.Service).WithMany(s => s.PetshopAttendances).HasForeignKey(e => e.ServiceId);
        });

        modelBuilder.Entity<QueueAttendance>(entity =>
        {
            entity.ToTable("queue_attendances");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Tutor).WithMany().HasForeignKey(e => e.TutorId);
            entity.HasOne(e => e.Animal).WithMany().HasForeignKey(e => e.AnimalId);
        });
    }
}