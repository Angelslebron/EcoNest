using EcoNest.Application.DTOs.Season;
using EcoNest.Application.Interfaces;
using EcoNest.Domain.Entities;
using EcoNest.Domain.Exceptions;

namespace EcoNest.Application.Services;

public class SeasonService
{
    private readonly ISeasonRepository _seasonRepository;

    public SeasonService(ISeasonRepository seasonRepository)
    {
        _seasonRepository = seasonRepository;
    }

    public async Task<IEnumerable<SeasonResponse>> GetAllAsync()
    {
        var seasons = await _seasonRepository.GetAllAsync();
        return seasons.Select(MapToResponse);
    }

    public async Task<SeasonResponse> GetByIdAsync(int id)
    {
        var season = await _seasonRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Season), id);
        return MapToResponse(season);
    }

    public async Task<SeasonResponse?> GetActiveAsync(DateTime date)
    {
        var season = await _seasonRepository.GetActiveSeasonAsync(date);
        return season is null ? null : MapToResponse(season);
    }

    public async Task<SeasonResponse> CreateAsync(CreateSeasonRequest request)
    {
        if (request.EndDate <= request.StartDate)
            throw new BusinessRuleException("End date must be after start date.");

        if (request.PriceMultiplier <= 0)
            throw new BusinessRuleException("Price multiplier must be greater than zero.");

        var hasOverlap = await _seasonRepository.HasOverlapAsync(request.StartDate, request.EndDate);
        if (hasOverlap)
            throw new BusinessRuleException("The season dates overlap with an existing season.");

        var season = new Season
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PriceMultiplier = request.PriceMultiplier
        };

        var created = await _seasonRepository.AddAsync(season);
        return MapToResponse(created);
    }

    public async Task<SeasonResponse> UpdateAsync(int id, UpdateSeasonRequest request)
    {
        var season = await _seasonRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Season), id);

        if (request.Name is not null) season.Name = request.Name;
        if (request.StartDate is not null) season.StartDate = request.StartDate.Value;
        if (request.EndDate is not null) season.EndDate = request.EndDate.Value;
        if (request.PriceMultiplier is not null) season.PriceMultiplier = request.PriceMultiplier.Value;
        season.UpdatedAt = DateTime.UtcNow;

        await _seasonRepository.UpdateAsync(season);
        return MapToResponse(season);
    }

    public async Task DeleteAsync(int id)
    {
        var season = await _seasonRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Season), id);
        await _seasonRepository.DeleteAsync(season);
    }

    private static SeasonResponse MapToResponse(Season s) =>
        new(s.Id, s.Name, s.StartDate, s.EndDate, s.PriceMultiplier);
}