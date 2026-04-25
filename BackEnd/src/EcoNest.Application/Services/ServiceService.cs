using EcoNest.Application.DTOs.Service;
using EcoNest.Application.Interfaces;
using EcoNest.Domain.Exceptions;
using ServiceEntity = EcoNest.Domain.Entities.Service;

namespace EcoNest.Application.Services;

public class ServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<IEnumerable<ServiceResponse>> GetAllAsync()
    {
        var services = await _serviceRepository.GetAllAsync();
        return services.Select(MapToResponse);
    }

    public async Task<ServiceResponse> GetByIdAsync(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(ServiceEntity), id);
        return MapToResponse(service);
    }

    public async Task<ServiceResponse> CreateAsync(CreateServiceRequest request)
    {
        if (request.Price < 0)
            throw new BusinessRuleException("Price cannot be negative.");

        var service = new ServiceEntity
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        var created = await _serviceRepository.AddAsync(service);
        return MapToResponse(created);
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdateServiceRequest request)
    {
        var service = await _serviceRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(ServiceEntity), id);

        if (request.Name is not null) service.Name = request.Name;
        if (request.Description is not null) service.Description = request.Description;
        if (request.Price is not null) service.Price = request.Price.Value;
        service.UpdatedAt = DateTime.UtcNow;

        await _serviceRepository.UpdateAsync(service);
        return MapToResponse(service);
    }

    public async Task DeleteAsync(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(ServiceEntity), id);
        await _serviceRepository.DeleteAsync(service);
    }

    private static ServiceResponse MapToResponse(ServiceEntity s) =>
        new(s.Id, s.Name, s.Description, s.Price);
}