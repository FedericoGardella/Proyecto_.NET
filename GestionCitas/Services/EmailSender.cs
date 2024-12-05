using MailKit.Net.Smtp; // Asegúrate de incluir el espacio de nombres de MailKit
using MimeKit;
using System;
using System.Threading.Tasks;

public class EmailSender
{
    private readonly string _smtpServer = "smtp.gmail.com"; // Servidor SMTP de Gmail
    private readonly int _port = 587; // Puerto para TLS
    private readonly string _email; // Tu correo electrónico de Gmail
    private readonly string _password; // Contraseña de aplicación o contraseña normal (si no usas 2FA)

    public EmailSender(string email, string password)
    {
        _email = email;
        _password = password;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Consultorio", _email)); 
        message.To.Add(new MailboxAddress("Destinatario", to));

        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            TextBody = body
        };
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_smtpServer, _port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_email, _password);
                await client.SendAsync(message);
                Console.WriteLine($"Correo enviado a {to}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
