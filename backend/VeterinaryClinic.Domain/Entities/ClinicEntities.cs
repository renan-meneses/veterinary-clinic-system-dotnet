using VeterinaryClinic.Domain.Common;

namespace VeterinaryClinic.Domain.Entities;

public class ClinicEnvironment : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid EnvironmentType { get; set; }
    public int? Capacity { get; set; }
    public Guid Status { get; set; }
    public string? Observations { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
    public ICollection<Hospitalization> Hospitalizations { get; set; } = new List<Hospitalization>();
}

public class PetshopAttendance : BaseEntity
{
    public Guid TutorId { get; set; }
    public Guid AnimalId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid? EmployeeId { get; set; }
    public Guid? EnvironmentId { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExpectedDeliveryTime { get; set; }
    public DateTime? ActualDeliveryTime { get; set; }
    public Guid Status { get; set; }
    public string? Observations { get; set; }

    public Tutor Tutor { get; set; } = null!;
    public Animal Animal { get; set; } = null!;
    public Service Service { get; set; } = null!;
    public User? Employee { get; set; }
    public ClinicEnvironment? Environment { get; set; }
}

public class QueueAttendance : BaseEntity
{
    public Guid TutorId { get; set; }
    public Guid AnimalId { get; set; }
    public Guid? VeterinarianId { get; set; }
    public Guid? OfficeId { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime? ScheduledTime { get; set; }
    public Guid AttendanceType { get; set; }
    public Guid Status { get; set; }
    public int Priority { get; set; }
    public string? Observations { get; set; }

    public Tutor Tutor { get; set; } = null!;
    public Animal Animal { get; set; } = null!;
    public User? Veterinarian { get; set; }
    public ClinicEnvironment? Office { get; set; }
}