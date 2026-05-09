namespace VeterinaryClinic.Domain.Enums;

public enum TransactionType
{
    Entry = 1,
    Exit = 2
}

public enum TransactionStatus
{
    Paid = 1,
    Pending = 2,
    Cancelled = 3
}

public enum PaymentMethod
{
    Cash = 1,
    CreditCard = 2,
    DebitCard = 3,
    Pix = 4,
    BankTransfer = 5,
    Check = 6
}

public enum TransactionCategory
{
    Consultation = 1,
    Vaccine = 2,
    Hospitalization = 3,
    Medication = 4,
    ProductSale = 5,
    ServiceSale = 6,
    Petshop = 7,
    Salary = 8,
    Rent = 9,
    Utilities = 10,
    Supplies = 11,
    Other = 99
}