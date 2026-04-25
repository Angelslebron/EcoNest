namespace EcoNest.Application.DTOs.Service;

public record ServiceResponse(
    int Id,
    string Name,
    string Description,
    decimal Price
);

public record CreateServiceRequest(
    string Name,
    string Description,
    decimal Price
);

public record UpdateServiceRequest(
    string? Name,
    string? Description,
    decimal? Price
);