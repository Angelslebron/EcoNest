namespace EcoNest.Infrastructure.Files;

public interface IFileService
{
    Task<byte[]> GenerateReservationPdfAsync(int reservationId);
}

public class FileService : IFileService
{
    public async Task<byte[]> GenerateReservationPdfAsync(int reservationId)
    {
        // Generar PDF con detalles de la reserva
        Console.WriteLine($"[PDF] Generando PDF para reserva #{reservationId}");
        return await Task.FromResult(Array.Empty<byte>());
    }
}
