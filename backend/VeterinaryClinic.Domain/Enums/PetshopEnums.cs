namespace VeterinaryClinic.Domain.Enums;

public enum PetshopAttendanceStatus
{
    Scheduled = 1,
    Waiting = 2,
    InProgress = 3,
    BathStarted = 4,
    GroomingStarted = 5,
    Finishing = 6,
    ReadyForPickup = 7,
    Delivered = 8,
    Cancelled = 9
}