using EcoNest.Domain.Enums;

namespace EcoNest.Application.DTOs.Maintenance;

public record MaintenanceResponse(
    int Id,
    int CabinId,
    string CabinName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    MaintenanceStatus Status
);

public record CreateMaintenanceRequest(
    int CabinId,
    string Description,
    DateTime StartDate,
    DateTime EndDate
);

public record UpdateMaintenanceRequest(
    string? Description,
    DateTime? StartDate,
    DateTime? EndDate,
    MaintenanceStatus? Status
);