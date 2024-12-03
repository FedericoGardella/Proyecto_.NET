using GestionCitas.Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection; // Necesario para IServiceProvider
using BL.IBLs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ReminderService : BackgroundService
{
    private readonly ReminderPublisher _publisher;
    private readonly IServiceProvider _serviceProvider; // Para resolver servicios scoped

    public ReminderService(IServiceProvider serviceProvider)
    {
        _publisher = new ReminderPublisher();
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;

            Console.WriteLine($"Evaluando hora");

            // Si es 8 AM, publica recordatorios
            if (true) // Cambiar a hora real para producción
            {

                using (var scope = _serviceProvider.CreateScope()) // Crear un nuevo scope
                {
                    var blCitas = scope.ServiceProvider.GetRequiredService<IBL_Citas>();

                    // Llama al BL para obtener las citas del día de mañana
                    var citas = blCitas.GetTomorrowAppointments();

                    foreach (var cita in citas)
                    {
                        var message = $"Paciente ID: {cita.PacienteId}, Hora: {cita.Hora}";
                        Console.WriteLine($"[ReminderService] Notificacion para: {message}");
                        _publisher.Publish(message);
                    }
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Revisa cada 30 segundos
        }
    }
}