using VeterinaryClinic.Domain.Common;

namespace VeterinaryClinic.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public decimal SalePrice { get; set; }
    public decimal? CostPrice { get; set; }
    public int StockQuantity { get; set; }
    public int? MinStockQuantity { get; set; }
    public string? Supplier { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}

public class Service : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? EstimatedDurationMinutes { get; set; }
    public Guid ServiceType { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    public ICollection<PetshopAttendance> PetshopAttendances { get; set; } = new List<PetshopAttendance>();
}

public class Sale : BaseEntity
{
    public Guid TutorId { get; set; }
    public Guid? AnimalId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal? Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid PaymentMethod { get; set; }
    public Guid Status { get; set; }
    public DateTime SaleDate { get; set; }
    public string? Notes { get; set; }

    public Tutor Tutor { get; set; } = null!;
    public Animal? Animal { get; set; }
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    public ICollection<FinancialTransaction> FinancialTransactions { get; set; } = new List<FinancialTransaction>();
}

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public Sale Sale { get; set; } = null!;
    public Product? Product { get; set; }
    public Service? Service { get; set; }
}

public class FinancialTransaction : BaseEntity
{
    public Guid TransactionType { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public Guid Category { get; set; }
    public DateTime TransactionDate { get; set; }
    public Guid PaymentMethod { get; set; }
    public string? Origin { get; set; }
    public Guid Status { get; set; }
    public Guid? SaleId { get; set; }
    public Guid? ConsultationId { get; set; }
    public Guid? HospitalizationId { get; set; }
    public Guid? PetshopAttendanceId { get; set; }
    public Guid TutorId { get; set; }

    public Sale? Sale { get; set; }
    public Consultation? Consultation { get; set; }
    public Hospitalization? Hospitalization { get; set; }
    public PetshopAttendance? PetshopAttendance { get; set; }
    public Tutor Tutor { get; set; } = null!;
}