namespace VeterinaryClinic.Domain.Events;

public interface IDomainEvent
{
    Guid EventId => Guid.NewGuid();
    DateTime OccurredAt => DateTime.UtcNow;
}

public class AnimalCreatedEvent : IDomainEvent
{
    public Guid AnimalId { get; set; }
    public Guid TutorId { get; set; }
    public string AnimalName { get; set; } = string.Empty;
}

public class VaccineAppliedEvent : IDomainEvent
{
    public Guid VaccineId { get; set; }
    public Guid AnimalId { get; set; }
    public string VaccineName { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
}

public class ConsultationScheduledEvent : IDomainEvent
{
    public Guid ConsultationId { get; set; }
    public Guid AnimalId { get; set; }
    public Guid TutorId { get; set; }
    public DateTime ScheduledAt { get; set; }
}

public class SaleCompletedEvent : IDomainEvent
{
    public Guid SaleId { get; set; }
    public Guid TutorId { get; set; }
    public decimal TotalAmount { get; set; }
}