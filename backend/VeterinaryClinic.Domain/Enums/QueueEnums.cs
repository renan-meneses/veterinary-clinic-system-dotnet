namespace VeterinaryClinic.Domain.Enums;

public enum QueueAttendanceStatus
{
    Waiting = 1,
    Called = 2,
    InProgress = 3,
    Finished = 4,
    Cancelled = 5
}

public enum QueueAttendanceType
{
    Consultation = 1,
    Vaccine = 2,
    Return = 3,
    Emergency = 4,
    Petshop = 5,
    Hospitalization = 6
}