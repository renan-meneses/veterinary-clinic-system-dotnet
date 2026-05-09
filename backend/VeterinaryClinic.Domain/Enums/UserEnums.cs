namespace VeterinaryClinic.Domain.Enums;

public enum UserType
{
    Administrator = 1,
    Attendant = 2,
    Veterinarian = 3,
    PetshopEmployee = 4,
    Financial = 5,
    Tutor = 6
}

public enum UserStatus
{
    Active = 1,
    Inactive = 2,
    Blocked = 3
}