using EcoNest.Domain.Enums;
using Stripe;

namespace EcoNest.Application.DTOs.Payment;

public record PaymentResponse(
    int Id,
    int ReservationId,
    DateTime PaymentDate,
    PaymentStatus Status,
    decimal Amount,
    PaymentMethod PaymentMethod
);

public record CreatePaymentRequest(
    int ReservationId,
    decimal Amount,
    PaymentMethod PaymentMethod
);
