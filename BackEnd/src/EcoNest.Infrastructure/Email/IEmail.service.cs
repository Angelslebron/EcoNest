using EcoNest.Application.Interfaces;

namespace EcoNest.Infrastructure.Email;

// Interfaz en Application 
// Implementación aquí en Infrastructure

public interface IEmailService
{
    Task SendReservationConfirmationAsync(string toEmail, string guestName, int reservationId);
}

public class EmailService : IEmailService
{    
    public async Task SendReservationConfirmationAsync(string toEmail, string guestName, int reservationId)
    {       
        Console.WriteLine($"[EMAIL] Confirmación enviada a {toEmail} para reserva #{reservationId}");
        await Task.CompletedTask;
    }
}

