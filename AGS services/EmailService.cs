using AGS_Models.DTO;
using AGS_services.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace AGS_services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EnviarCorreoContacto(ContactoDTO contacto)
        {
            try
            {
                var emailOrigen = _configuration["EmailSettings:EmailOrigen"];
                var password = _configuration["EmailSettings:Password"];
                var host = _configuration["EmailSettings:Host"];             
                var port = int.TryParse(_configuration["EmailSettings:Port"], out int p) ? p : 587;

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("Web AGS", emailOrigen));

                message.To.Add(new MailboxAddress("Admin", emailOrigen));

                message.Subject = $"Nuevo Contacto Web: {contacto.NombreCompleto} - {contacto.TipoProyecto}";

                var bodyBuilder = new BodyBuilder();

                bodyBuilder.HtmlBody = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 0; padding: 0; background-color: #f4f6f9; }}
    </style>
</head>
<body style='font-family: Arial, sans-serif; background-color: #f4f6f9; margin: 0; padding: 20px;'>
    
    <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 10px rgba(0,0,0,0.05);'>
        
        <div style='background-color: #406AFF; padding: 30px; text-align: center;'>
            <h1 style='color: #ffffff; margin: 0; font-size: 24px; font-weight: bold;'>Nuevo Contacto Web</h1>
            <p style='color: #e0e0e0; margin: 5px 0 0 0; font-size: 14px;'>Has recibido una nueva consulta</p>
        </div>

        <div style='padding: 40px 30px;'>
            
            <div style='margin-bottom: 25px;'>
                <p style='margin: 0; font-size: 12px; color: #888; text-transform: uppercase; font-weight: bold; letter-spacing: 1px;'>Nombre del Cliente</p>
                <p style='margin: 5px 0 0 0; font-size: 18px; color: #333;'>{contacto.NombreCompleto}</p>
            </div>

            <div style='margin-bottom: 25px;'>
                <p style='margin: 0; font-size: 12px; color: #888; text-transform: uppercase; font-weight: bold; letter-spacing: 1px;'>Correo Electrónico</p>
                <a href='mailto:{contacto.Email}' style='margin: 5px 0 0 0; font-size: 18px; color: #406AFF; text-decoration: none; display: block;'>{contacto.Email}</a>
            </div>

            <div style='margin-bottom: 25px;'>
                <p style='margin: 0; font-size: 12px; color: #888; text-transform: uppercase; font-weight: bold; letter-spacing: 1px;'>Teléfono</p>
                <p style='margin: 5px 0 0 0; font-size: 18px; color: #333;'>{contacto.Telefono}</p>
            </div>

            <div style='margin-bottom: 25px;'>
                <p style='margin: 0; font-size: 12px; color: #888; text-transform: uppercase; font-weight: bold; letter-spacing: 1px;'>Tipo de Proyecto</p>
                <span style='background-color: #f0f2f5; color: #406AFF; padding: 5px 10px; border-radius: 15px; font-size: 14px; font-weight: bold; display: inline-block; margin-top: 5px;'>{contacto.TipoProyecto}</span>
            </div>

            <hr style='border: none; border-top: 1px solid #eee; margin: 30px 0;' />

            <div style='margin-bottom: 20px;'>
                <p style='margin: 0; font-size: 12px; color: #888; text-transform: uppercase; font-weight: bold; letter-spacing: 1px;'>Mensaje</p>
                <div style='background-color: #f9f9f9; padding: 15px; border-radius: 5px; margin-top: 10px; border-left: 4px solid #406AFF;'>
                    <p style='margin: 0; font-size: 16px; color: #555; line-height: 1.5;'>{contacto.Mensaje}</p>
                </div>
            </div>

            <div style='text-align: center; margin-top: 40px;'>
                <a href='mailto:{contacto.Email}' style='background-color: #406AFF; color: #ffffff; padding: 12px 25px; border-radius: 50px; text-decoration: none; font-weight: bold; display: inline-block;'>Responder al Cliente</a>
            </div>
        </div>

        <div style='background-color: #f8f9fa; padding: 20px; text-align: center; border-top: 1px solid #eee;'>
            <p style='margin: 0; color: #aaa; font-size: 12px;'>Este es un mensaje automático enviado desde tu sitio web.</p>
        </div>
    </div>
</body>
</html>";

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    try
                    {
                        Console.WriteLine($"[EMAIL] Iniciando conexión a {host}:{port}...");

                        client.CheckCertificateRevocation = false;
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                        Console.WriteLine("[EMAIL] Conexión establecida. Autenticando...");

                        await client.AuthenticateAsync(emailOrigen, password);
                        Console.WriteLine("[EMAIL] Autenticado. Enviando mensaje...");

                        await client.SendAsync(message);
                        Console.WriteLine("[EMAIL] ¡Mensaje enviado con éxito!");

                        await client.DisconnectAsync(true);
                        return true;
                    }
                    catch (Exception exConexion)
                    {
                        Console.WriteLine($"[EMAIL ERROR] Falló en paso específico: {exConexion.Message}");
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error crítico enviando correo: " + e.Message);
                return false;
            }
        }
    }
}