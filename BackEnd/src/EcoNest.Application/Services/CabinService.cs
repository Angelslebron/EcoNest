using EcoNest.Application.DTOs.Cabin;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using EcoNest.Domain.Exceptions;
using EcoNest.Application.Interfaces;

namespace EcoNest.Application.Services;

public class CabinService(ICabinRepository cabinRepository)
{
    private readonly ICabinRepository _cabinRepository = cabinRepository;

    public async Task<IEnumerable<CabinResponse>> GetAllAsync()
    {
        var cabin = await _cabinRepository.GetAllAsync();
        return cabin.Select(MapToResponse);
    }

    public async Task<CabinResponse> GetByIdAsync(int id)
    {
        var cabin = await _cabinRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Cabin), id);

        return MapToResponse(cabin);
    }

    public async Task<IEnumerable<CabinResponse>> GetAvailableAsync(DateTime checkIn, DateTime checkOut)
    {
        if (checkOut <= checkIn)
            throw new BusinessRuleException("Check-out date must be after check-in date.");

        var cabin = await _cabinRepository.GetAvailableAsync(checkIn, checkOut);
        return cabin.Select(MapToResponse);
    }

    public async Task<CabinResponse> CreateAsync(CreateCabinRequest request)
    {
        var cabin = new Cabin
        {
            Name = request.Name,
            Location = request.Location,
            Capacity = request.Capacity,
            Description = request.Description,
            State = CabinStatus.Available
        };

        var created = await _cabinRepository.AddAsync(cabin);
        return MapToResponse(created);
    }

    public async Task<CabinResponse> UpdateAsync(int id, UpdateCabinRequest request)
    {
        var cabin = await _cabinRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Cabin), id);

        if (request.Name is not null) cabin.Name = request.Name;
        if (request.Location is not null) cabin.Location = request.Location;
        if (request.Capacity is not null) cabin.Capacity = request.Capacity.Value;
        if (request.Description is not null) cabin.Description = request.Description;
        if (request.State is not null) cabin.State = request.State.Value;

        cabin.UpdatedAt = DateTime.UtcNow;

        await _cabinRepository.UpdateAsync(cabin);
        return MapToResponse(cabin);
    }

    public async Task DeleteAsync(int id)
    {
        var cabin = await _cabinRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Cabin), id);

        cabin.IsActive = false;
        cabin.UpdatedAt = DateTime.UtcNow;

        await _cabinRepository.UpdateAsync(cabin);
    }

    private static CabinResponse MapToResponse(Cabin c) =>
        new(c.Id, c.Name, c.Location, c.Capacity, c.Description, c.State);
}