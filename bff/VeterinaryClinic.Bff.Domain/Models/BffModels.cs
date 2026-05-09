namespace VeterinaryClinic.Bff.Domain.Models;

public class NavigationMenuDto
{
    public List<NavigationItemDto> Items { get; set; } = new();
}

public class NavigationItemDto
{
    public string Path { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<NavigationItemDto>? Children { get; set; }
}

public class DashboardDto
{
    public int TotalAnimals { get; set; }
    public int TodayConsultations { get; set; }
    public int ActiveHospitalizations { get; set; }
    public int PetshopAttendancesInProgress { get; set; }
    public int UpcomingVaccines { get; set; }
    public decimal TodaySalesTotal { get; set; }
    public decimal FinancialBalance { get; set; }
    public int WaitingPatients { get; set; }
    public List<SaleSummaryDto> RecentSales { get; set; } = new();
    public List<VaccineAlertDto> UpcomingVaccineList { get; set; } = new();
}

public class SaleSummaryDto
{
    public Guid Id { get; set; }
    public string TutorName { get; set; } = string.Empty;
    public string AnimalName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public DateTime SaleDate { get; set; }
}

public class VaccineAlertDto
{
    public Guid Id { get; set; }
    public string AnimalName { get; set; } = string.Empty;
    public string VaccineName { get; set; } = string.Empty;
    public DateTime NextDoseDate { get; set; }
}

public class FinanceDashboardDto
{
    public decimal TotalEntries { get; set; }
    public decimal TotalExits { get; set; }
    public decimal Balance { get; set; }
    public List<ChartDataDto> EntryExitChart { get; set; } = new();
    public List<ChartDataDto> CategoryChart { get; set; } = new();
    public List<TransactionDto> RecentTransactions { get; set; } = new();
    public decimal AccountsPayable { get; set; }
    public decimal AccountsReceivable { get; set; }
}

public class ChartDataDto
{
    public string Label { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public class TransactionDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public Guid Type { get; set; }
    public DateTime Date { get; set; }
}

public class QueueListDto
{
    public List<QueueItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

public class QueueItemDto
{
    public Guid Id { get; set; }
    public string AnimalName { get; set; } = string.Empty;
    public string TutorName { get; set; } = string.Empty;
    public string? VeterinarianName { get; set; }
    public string? OfficeName { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime? ScheduledTime { get; set; }
    public Guid Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public Guid Type { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public int Priority { get; set; }
}

public class PetshopBoardDto
{
    public List<PetshopBoardItemDto> Waiting { get; set; } = new();
    public List<PetshopBoardItemDto> InProgress { get; set; } = new();
    public List<PetshopBoardItemDto> Ready { get; set; } = new();
}

public class PetshopBoardItemDto
{
    public Guid Id { get; set; }
    public string AnimalName { get; set; } = string.Empty;
    public string TutorName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime EntryTime { get; set; }
    public DateTime? ExpectedDelivery { get; set; }
    public Guid Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
}

public class TutorDashboardDto
{
    public int TotalAnimals { get; set; }
    public int UpcomingVaccines { get; set; }
    public int ScheduledConsultations { get; set; }
    public int ActiveHospitalizations { get; set; }
    public List<VaccineDto> UpcomingVaccineList { get; set; } = new();
    public List<ConsultationDto> UpcomingConsultations { get; set; } = new();
}

public class AnimalDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid Species { get; set; }
    public string? Breed { get; set; }
    public Guid Sex { get; set; }
    public DateTime? BirthDate { get; set; }
    public decimal? Weight { get; set; }
    public string? Color { get; set; }
    public string? PhotoUrl { get; set; }
    public Guid Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class VaccineDto
{
    public Guid Id { get; set; }
    public string VaccineName { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
    public DateTime? NextDoseDate { get; set; }
    public Guid Status { get; set; }
}

public class ConsultationDto
{
    public Guid Id { get; set; }
    public DateTime ScheduledAt { get; set; }
    public string? Reason { get; set; }
    public Guid Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
}

public class HospitalizationDto
{
    public Guid Id { get; set; }
    public string AnimalName { get; set; } = string.Empty;
    public DateTime EntryDate { get; set; }
    public DateTime? ExpectedDischarge { get; set; }
    public Guid Status { get; set; }
}

public class PetshopAttendanceDto
{
    public Guid Id { get; set; }
    public string AnimalName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public DateTime EntryTime { get; set; }
    public DateTime? ActualDeliveryTime { get; set; }
    public Guid Status { get; set; }
}

public class AnimalCompleteHistoryDto
{
    public AnimalDto Animal { get; set; } = null!;
    public List<VaccineDto> Vaccines { get; set; } = new();
    public List<ConsultationDto> Consultations { get; set; } = new();
    public List<HospitalizationDto> Hospitalizations { get; set; } = new();
    public List<MedicalRecordDto> MedicalRecords { get; set; } = new();
    public List<PetshopAttendanceDto> PetshopAttendances { get; set; } = new();
}

public class MedicalRecordDto
{
    public Guid Id { get; set; }
    public DateTime RecordDate { get; set; }
    public string? Anamnesis { get; set; }
    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }
}