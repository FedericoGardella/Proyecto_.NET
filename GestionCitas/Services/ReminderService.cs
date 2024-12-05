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
    private readonly EmailSender _emailSender; // Instancia de EmailSender

    public ReminderService(IServiceProvider serviceProvider)
    {
        _publisher = new ReminderPublisher();
        _serviceProvider = serviceProvider;

        _emailSender = new EmailSender("notificacionesconsultorio8@gmail.com", "lvwi acgm fsil doph");
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
                    var blPacientes = scope.ServiceProvider.GetRequiredService<IBL_Pacientes>();

                    // Llama al BL de citas para obtener las citas del día de mañana
                    var citas = blCitas.GetTomorrowAppointments();

                    foreach (var cita in citas)
                    {
                        var message = $"Paciente ID: {cita.PacienteId}, Hora: {cita.Hora}";
                        Console.WriteLine($"[ReminderService] Notificación para: {message}");

                        // Publicar en RabbitMQ
                        _publisher.Publish(message);

                        // Obtener el correo electrónico del paciente desde el BL de pacientes
                        var pacienteEmail = GetEmailByPacienteId(cita.PacienteId, blPacientes);

                        if (!string.IsNullOrEmpty(pacienteEmail))
                        {
                            // Enviar correo electrónico
                            var emailBody = $"Estimado paciente,\n\nLe recordamos que tiene una cita programada para mañana a las {cita.Hora}.\n\nSaludos,\nConsultorio.";
                            await _emailSender.SendEmailAsync(pacienteEmail, "Recordatorio de Cita", emailBody);
                        }
                        else
                        {
                            Console.WriteLine($"[ReminderService] No se encontró un correo electrónico para el paciente ID {cita.PacienteId}");
                        }
                    }
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Revisa cada 30 segundos
        }
    }

    private string GetEmailByPacienteId(long? pacienteId, IBL_Pacientes blPacientes)
    {
        if (!pacienteId.HasValue)
        {
            Console.WriteLine("[ReminderService] Paciente ID es null");
            return null;
        }

        try
        {
            // Llamar al método del BL de pacientes para obtener el correo electrónico
            string email = blPacientes.GetPacienteEmail(pacienteId.Value);
            Console.WriteLine($"El email es {email}");
            return email;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ReminderService] Error al obtener el correo electrónico del paciente ID {pacienteId}: {ex.Message}");
            return null;
        }
    }
}
