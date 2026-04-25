
namespace EcoNest.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3
    }

    public enum PaymentMethod
    {
        CreditCard = 1,
        DebitCard = 2,
        Cash = 3,
        Online = 4
    }
}
