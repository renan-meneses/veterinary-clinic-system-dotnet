using VeterinaryClinic.Domain.Common;

namespace VeterinaryClinic.Domain.Entities;

public class Tutor : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Cpf { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }

    public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public ICollection<FinancialTransaction> FinancialTransactions { get; set; } = new List<FinancialTransaction>();
}

public class Animal : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid Species { get; set; }
    public string? Breed { get; set; }
    public Guid Sex { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? ApproximateAge { get; set; }
    public decimal? Weight { get; set; }
    public string? Color { get; set; }
    public string? Microchip { get; set; }
    public string? Observations { get; set; }
    public string? PhotoUrl { get; set; }
    public Guid Status { get; set; }

    public ICollection<AnimalTutor> AnimalTutors { get; set; } = new List<AnimalTutor>();
    public ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
    public ICollection<Hospitalization> Hospitalizations { get; set; } = new List<Hospitalization>();
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public ICollection<PetshopAttendance> PetshopAttendances { get; set; } = new List<PetshopAttendance>();
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}

public class AnimalTutor : BaseEntity
{
    public Guid AnimalId { get; set; }
    public Guid TutorId { get; set; }
    public bool IsOwner { get; set; } = true;
    public bool IsPrimary { get; set; } = true;

    public Animal Animal { get; set; } = null!;
    public Tutor Tutor { get; set; } = null!;
}

public class Vaccine : BaseEntity
{
    public Guid AnimalId { get; set; }
    public Guid TutorId { get; set; }
    public string VaccineName { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
    public DateTime? NextDoseDate { get; set; }
    public string? Batch { get; set; }
    public string? Manufacturer { get; set; }
    public Guid? VeterinarianId { get; set; }
    public string? Observations { get; set; }
    public Guid Status { get; set; }

    public Animal Animal { get; set; } = null!;
    public Tutor Tutor { get; set; } = null!;
    public User? Veterinarian { get; set; }
}

public class Consultation : BaseEntity
{
    public Guid AnimalId { get; set; }
    public Guid TutorId { get; set; }
    public Guid? VeterinarianId { get; set; }
    public Guid? OfficeId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public string? Reason { get; set; }
    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }
    public string? RequestedExams { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Guid Status { get; set; }
    public string? Notes { get; set; }

    public Animal Animal { get; set; } = null!;
    public Tutor Tutor { get; set; } = null!;
    public User? Veterinarian { get; set; }
    public ClinicEnvironment? Office { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}

public class Hospitalization : BaseEntity
{
    public Guid AnimalId { get; set; }
    public Guid TutorId { get; set; }
    public Guid? ResponsibleVeterinarianId { get; set; }
    public Guid? EnvironmentId { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExpectedDischargeDate { get; set; }
    public DateTime? ActualDischargeDate { get; set; }
    public string? Reason { get; set; }
    public string? Evolution { get; set; }
    public string? Medications { get; set; }
    public string? Feeding { get; set; }
    public string? Exams { get; set; }
    public Guid Status { get; set; }
    public string? Notes { get; set; }

    public Animal Animal { get; set; } = null!;
    public Tutor Tutor { get; set; } = null!;
    public User? ResponsibleVeterinarian { get; set; }
    public ClinicEnvironment? Environment { get; set; }
}

public class MedicalRecord : BaseEntity
{
    public Guid? AnimalId { get; set; }
    public Guid? ConsultationId { get; set; }
    public Guid? TutorId { get; set; }
    public Guid? VeterinarianId { get; set; }
    public DateTime RecordDate { get; set; }
    public string? Anamnesis { get; set; }
    public string? Symptoms { get; set; }
    public string? PhysicalExam { get; set; }
    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }
    public string? Observations { get; set; }
    public string? ExamResults { get; set; }

    public Animal? Animal { get; set; }
    public Consultation? Consultation { get; set; }
    public Tutor? Tutor { get; set; }
    public User? Veterinarian { get; set; }
}