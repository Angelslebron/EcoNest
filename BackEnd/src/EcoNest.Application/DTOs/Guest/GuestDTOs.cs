namespace EcoNest.Application.DTOs.Guest;

public record GuestResponse(
    int Id,
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Cedula,
    string Address,
    string Nationality
);

public record CreateGuestRequest(
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Cedula,
    string Address,
    string Nationality
);

public record UpdateGuestRequest(
    string? Name,
    string? Surname,
    string? Email,
    string? PhoneNumber,
    string? Cedula,
    string? Address,
    string? Nationality
);