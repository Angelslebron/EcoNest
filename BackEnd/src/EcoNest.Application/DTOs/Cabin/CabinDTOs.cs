using EcoNest.Domain.Enums;

namespace EcoNest.Application.DTOs.Cabin;

public record CabinResponse(
    int Id,
    string Name,
    string Location,
    int Capacity,
    string Description,
    CabinStatus Status
);

public record CreateCabinRequest(
    string Name,
    string Location,
    int Capacity,
    string Description
);

public record UpdateCabinRequest(
    string? Name,
    string? Location,
    int? Capacity,
    string? Description,
    CabinStatus? Status
);

