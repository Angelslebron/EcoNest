namespace EcoNest.Application.DTOs.Observation;

public record ObservationResponse(
    int Id,
    int ReservationId,
    int? CabinId,
    string Comment
);

public record CreateObservationRequest(
    int ReservationId,
    int? CabinId,
    string Comment
);