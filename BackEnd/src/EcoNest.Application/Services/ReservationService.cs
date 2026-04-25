using EcoNest.Application.DTOs.Reservation;
using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Enums;
using EcoNest.Domain.Exceptions;

namespace EcoNest.Application.Services;

public class ReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICabinRepository _cabinRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly ISeasonRepository _seasonRepository;

    public ReservationService(
        IReservationRepository reservationRepository,
        ICabinRepository cabinRepository,
        IGuestRepository guestRepository,
        ISeasonRepository seasonRepository)
    {
        _reservationRepository = reservationRepository;
        _cabinRepository = cabinRepository;
        _guestRepository = guestRepository;
        _seasonRepository = seasonRepository;
    }

    public async Task<IEnumerable<ReservationResponse>> GetAllAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        return reservations.Select(MapToResponse);
    }

    public async Task<ReservationResponse> GetByIdAsync(int id)
    {
        var reservation = await _reservationRepository.GetWithDetailsAsync(id)
            ?? throw new NotFoundException(nameof(Reservation), id);
        return MapToResponse(reservation);
    }

    public async Task<IEnumerable<ReservationResponse>> GetActiveAndUpcomingAsync()
    {
        var reservations = await _reservationRepository.GetActiveAndUpcomingAsync();
        return reservations.Select(MapToResponse);
    }

    public async Task<ReservationResponse> CreateAsync(CreateReservationRequest request)
    {
        // Validate dates
        if (request.CheckOutDate <= request.CheckInDate)
            throw new BusinessRuleException("Check-out date must be after check-in date.");

        // Validate entities exist
        var cabin = await _cabinRepository.GetByIdAsync(request.CabinId)
            ?? throw new NotFoundException(nameof(Cabin), request.CabinId);

        _ = await _guestRepository.GetByIdAsync(request.GuestId)
            ?? throw new NotFoundException(nameof(Guest), request.GuestId);

        var season = await _seasonRepository.GetByIdAsync(request.SeasonId)
            ?? throw new NotFoundException(nameof(Season), request.SeasonId);

        // Business rule: cabin must be available
        if (cabin.Status == CabinStatus.Maintenance)
            throw new BusinessRuleException($"Cabin '{cabin.Name}' is currently under maintenance.");

        // Business rule: no date conflicts
        var hasConflict = await _reservationRepository.HasConflictAsync(
            request.CabinId, request.CheckInDate, request.CheckOutDate);
        if (hasConflict)
            throw new BusinessRuleException("The cabin is already reserved for the selected dates.");

        // Calculate cost
        var nights = (request.CheckOutDate - request.CheckInDate).Days;
        var basePricePerNight = 100m; // base price — could come from Cabin entity later
        var costPerNight = basePricePerNight * season.PriceMultiplier;
        var totalCost = costPerNight * nights;

        var reservation = new Reservation
        {
            CabinId = request.CabinId,
            GuestId = request.GuestId,
            SeasonId = request.SeasonId,
            CheckInDate = request.CheckInDate,
            CheckOutDate = request.CheckOutDate,
            Status = ReservationStatus.Reserved,
            CostPerNight = costPerNight,
            TotalCost = totalCost
        };

        // Update cabin status
        cabin.Status = CabinStatus.Occupied;
        await _cabinRepository.UpdateAsync(cabin);

        var created = await _reservationRepository.AddAsync(reservation);

        // Re-fetch with details for the response
        var withDetails = await _reservationRepository.GetWithDetailsAsync(created.Id);
        return MapToResponse(withDetails!);
    }

    public async Task<ReservationResponse> UpdateAsync(int id, UpdateReservationRequest request)
    {
        var reservation = await _reservationRepository.GetWithDetailsAsync(id)
            ?? throw new NotFoundException(nameof(Reservation), id);

        if (reservation.Status == ReservationStatus.Cancelled)
            throw new BusinessRuleException("Cannot update a cancelled reservation.");

        if (request.CheckInDate is not null) reservation.CheckInDate = request.CheckInDate.Value;
        if (request.CheckOutDate is not null) reservation.CheckOutDate = request.CheckOutDate.Value;
        if (request.Status is not null) reservation.Status = request.Status.Value;
        reservation.UpdatedAt = DateTime.UtcNow;

        await _reservationRepository.UpdateAsync(reservation);
        return MapToResponse(reservation);
    }

    public async Task CancelAsync(int id)
    {
        var reservation = await _reservationRepository.GetWithDetailsAsync(id)
            ?? throw new NotFoundException(nameof(Reservation), id);

        if (reservation.Status == ReservationStatus.Cancelled)
            throw new BusinessRuleException("Reservation is already cancelled.");

        reservation.Status = ReservationStatus.Cancelled;
        reservation.UpdatedAt = DateTime.UtcNow;
        await _reservationRepository.UpdateAsync(reservation);

        // Free up the cabin
        var cabin = await _cabinRepository.GetByIdAsync(reservation.CabinId);
        if (cabin is not null)
        {
            cabin.Status = CabinStatus.Available;
            await _cabinRepository.UpdateAsync(cabin);
        }
    }

    private static ReservationResponse MapToResponse(Reservation r) =>
        new(
            r.Id,
            r.CabinId,
            r.Cabin?.Name ?? string.Empty,
            r.GuestId,
            r.Guest is not null ? $"{r.Guest.Name} {r.Guest.Surname}" : string.Empty,
            r.SeasonId,
            r.Season?.Name ?? string.Empty,
            r.CheckInDate,
            r.CheckOutDate,
            r.Status,
            r.CostPerNight,
            r.TotalCost
        );
}