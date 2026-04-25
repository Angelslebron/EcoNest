namespace EcoNest.Application.DTOs.Season;

public record SeasonResponse(
    int Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    decimal PriceMultiplier
);

public record CreateSeasonRequest(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    decimal PriceMultiplier
);

public record UpdateSeasonRequest(
    string? Name,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal? PriceMultiplier
);