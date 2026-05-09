namespace VeterinaryClinic.Domain.Enums;

public enum ClinicEnvironmentType
{
    Office = 1,
    VaccineRoom = 2,
    Hospitalization = 3,
    Petshop = 4,
    BathAndGrooming = 5,
    Reception = 6,
    Storage = 7,
    Other = 99
}

public enum ClinicEnvironmentStatus
{
    Available = 1,
    Occupied = 2,
    UnderMaintenance = 3,
    Inactive = 4
}