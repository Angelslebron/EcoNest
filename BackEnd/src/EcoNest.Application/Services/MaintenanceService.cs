using EcoNest.Application.DTOs.Maintenance;
using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using EcoNest.Domain.Exceptions;

namespace EcoNest.Application.Services;

public class MaintenanceService(IMaintenanceRepository maintenanceRepository, ICabinRepository cabinRepository)
{
    private readonly IMaintenanceRepository _maintenanceRepository = maintenanceRepository;
    private readonly ICabinRepository _cabinRepository = cabinRepository;

    public async Task<IEnumerable<MaintenanceResponse>> GetAllAsync()
    {
        var items = await _maintenanceRepository.GetAllAsync();
        return items.Select(MapToResponse);
    }

    public async Task<MaintenanceResponse> GetByIdAsync(int id)
    {
        var item = await _maintenanceRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Maintenance), id);
        return MapToResponse(item);
    }

    public async Task<IEnumerable<MaintenanceResponse>> GetByCabinAsync(int cabinId)
    {
        var items = await _maintenanceRepository.GetByCabinAsync(cabinId);
        return items.Select(MapToResponse);
    }

    public async Task<MaintenanceResponse> CreateAsync(CreateMaintenanceRequest request)
    {
        var cabin = await _cabinRepository.GetByIdAsync(request.CabinId)
            ?? throw new NotFoundException(nameof(Cabin), request.CabinId);

        if (request.EndDate <= request.StartDate)
            throw new BusinessRuleException("End date must be after start date.");

        var maintenance = new Maintenance
        {
            CabinId = request.CabinId,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = MaintenanceStatus.Scheduled
        };

        // Set cabin to maintenance status
        cabin.State = CabinStatus.Maintenance;
        await _cabinRepository.UpdateAsync(cabin);

        var created = await _maintenanceRepository.AddAsync(maintenance);
        created.Cabin = cabin;
        return MapToResponse(created);
    }

    public async Task<MaintenanceResponse> UpdateAsync(int id, UpdateMaintenanceRequest request)
    {
        var item = await _maintenanceRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Maintenance), id);

        if (request.Description is not null) item.Description = request.Description;
        if (request.StartDate is not null) item.StartDate = request.StartDate.Value;
        if (request.EndDate is not null) item.EndDate = request.EndDate.Value;
        if (request.Status is not null)
        {
            item.Status = request.Status.Value;
            // If completed, free the cabin
            if (request.Status == MaintenanceStatus.Completed)
            {
                var cabin = await _cabinRepository.GetByIdAsync(item.CabinId);
                if (cabin is not null)
                {
                    cabin.State = CabinStatus.Available;
                    await _cabinRepository.UpdateAsync(cabin);
                }
            }
        }
        item.UpdatedAt = DateTime.UtcNow;

        await _maintenanceRepository.UpdateAsync(item);
        return MapToResponse(item);
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _maintenanceRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Maintenance), id);
        await _maintenanceRepository.DeleteAsync(item);
    }

    private static MaintenanceResponse MapToResponse(Maintenance m) =>
        new(m.Id, m.CabinId, m.Cabin?.Name ?? string.Empty, m.Description, m.StartDate, m.EndDate, m.Status);
}