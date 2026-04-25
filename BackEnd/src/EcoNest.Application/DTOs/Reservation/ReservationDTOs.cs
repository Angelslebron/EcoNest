using EcoNest.Domain.Enums;

namespace EcoNest.Application.DTOs.Reservation;

public record ReservationResponse(
    int Id,
    int CabinId,
    string CabinName,
    int GuestId,
    string GuestFullName,
    int SeasonId,
    string SeasonName,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    ReservationStatus Status,
    decimal CostPerNight,
    decimal TotalCost
);

public record CreateReservationRequest(
    int CabinId,
    int GuestId,
    int SeasonId,
    DateTime CheckInDate,
    DateTime CheckOutDate
);

public record UpdateReservationRequest(
    DateTime? CheckInDate,
    DateTime? CheckOutDate,
    ReservationStatus? Status
);
