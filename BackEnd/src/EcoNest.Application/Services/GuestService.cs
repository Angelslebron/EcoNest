using EcoNest.Application.DTOs.Guest;
using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Exceptions;
using OpenQA.Selenium.BiDi.Network;

namespace EcoNest.Application.Services;

public class GuestService(IGuestRepository guestRepository)
{
    private readonly IGuestRepository _guestRepository = guestRepository;

    public async Task<IEnumerable<GuestResponse>> GetAllAsync()
    {
        var guests = await _guestRepository.GetAllAsync();
        return guests.Select(MapToResponse);
    }

    public async Task<GuestResponse> GetByIdAsync(int id)
    {
        var guest = await _guestRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Guest), id);
        return MapToResponse(guest);
    }


    public async Task<IEnumerable<GuestResponse>> SearchAsync(string term)
    {
        var guests = await _guestRepository.SearchAsync(term);
        return guests.Select(MapToResponse);
    }

    public async Task<GuestResponse> CreateAsync(CreateGuestRequest request)
    {
        // Description - Business rule: cedula must be unique
        var existing = await _guestRepository.GetByDocumentIdAsync(request.DocumentId);
        if (existing is not null)
            throw new BusinessRuleException($"A guest with cedula '{request.DocumentId}' already exists.");

        var guest = new Guest
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            DocumentId = request.DocumentId,
            Address = request.Address,
            Nationality = request.Nationality
        };

        var created = await _guestRepository.AddAsync(guest);
        return MapToResponse(created);
    }

    public async Task<GuestResponse> UpdateAsync(int id, UpdateGuestRequest request)
    {
        var guest = await _guestRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Guest), id);

        if (request.Name is not null) guest.Name = request.Name;
        if (request.Surname is not null) guest.Surname = request.Surname;
        if (request.Email is not null) guest.Email = request.Email;
        if (request.PhoneNumber is not null) guest.PhoneNumber = request.PhoneNumber;
        if (request.DocumentId is not null) guest.DocumentId = request.DocumentId;
        if (request.Address is not null) guest.Address = request.Address;
        if (request.Nationality is not null) guest.Nationality = request.Nationality;
        guest.UpdatedAt = DateTime.UtcNow;

        await _guestRepository.UpdateAsync(guest);
        return MapToResponse(guest);
    }

    public async Task DeleteAsync(int id)
    {
        var guest = await _guestRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Guest), id);
        await _guestRepository.DeleteAsync(guest);
    }

    private static GuestResponse MapToResponse(Guest g) =>
        new(g.Id, g.Name, g.Surname, g.Email, g.PhoneNumber, g.DocumentId, g.Address, g.Nationality);
}
