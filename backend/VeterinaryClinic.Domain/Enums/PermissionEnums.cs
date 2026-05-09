namespace VeterinaryClinic.Domain.Enums;

public enum ModuleType
{
    Users = 1,
    Tutors = 2,
    Animals = 3,
    Vaccines = 4,
    Consultations = 5,
    Hospitalizations = 6,
    Petshop = 7,
    Finance = 8,
    Sales = 9,
    Products = 10,
    Services = 11,
    Office = 12,
    ClinicStructure = 13,
    AttendanceQueue = 14,
    Reports = 15,
    Settings = 16
}

public enum PermissionAction
{
    View = 1,
    Create = 2,
    Edit = 3,
    Delete = 4,
    Export = 5,
    Manage = 6
}