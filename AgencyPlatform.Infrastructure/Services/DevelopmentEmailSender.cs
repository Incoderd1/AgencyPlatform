using AgencyPlatform.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Services
{
    public class DevelopmentEmailSender : IEmailSender
    {
        private readonly ILogger<DevelopmentEmailSender> _logger;
        private readonly string _emailFolder;

        public DevelopmentEmailSender(ILogger<DevelopmentEmailSender> logger)
        {
            _logger = logger;

            // Crear carpeta para almacenar los emails simulados
            _emailFolder = Path.Combine(Directory.GetCurrentDirectory(), "dev-emails");
            if (!Directory.Exists(_emailFolder))
            {
                Directory.CreateDirectory(_emailFolder);
            }
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                // Registrar en logs
                _logger.LogInformation($"[EMAIL SIMULADO] Para: {to}");
                _logger.LogInformation($"[EMAIL SIMULADO] Asunto: {subject}");

                // Extraer token si existe en el cuerpo del mensaje
                string token = null;
                if (body.Contains("token="))
                {
                    int startIndex = body.IndexOf("token=") + 6;
                    int endIndex = body.IndexOf('"', startIndex);
                    if (endIndex == -1)
                    {
                        endIndex = body.IndexOf('\'', startIndex);
                    }
                    if (endIndex == -1)
                    {
                        endIndex = body.IndexOf('&', startIndex);
                    }
                    if (endIndex == -1)
                    {
                        endIndex = body.IndexOf(' ', startIndex);
                    }
                    if (endIndex == -1)
                    {
                        endIndex = body.Length;
                    }

                    token = body.Substring(startIndex, endIndex - startIndex);
                    _logger.LogInformation($"[EMAIL SIMULADO] Token extraído: {token}");
                }

                // Guardar el correo como archivo para revisión
                string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{to.Replace("@", "_at_")}.html";
                string filePath = Path.Combine(_emailFolder, fileName);

                // Añadir información de token en la parte superior para facilitar pruebas
                string contentToSave = $"<!-- \nEmail simulado\nPara: {to}\nAsunto: {subject}\n";

                if (!string.IsNullOrEmpty(token))
                {
                    contentToSave += $"Token: {token}\n";
                }

                contentToSave += "-->\n\n" + body;

                File.WriteAllText(filePath, contentToSave);

                _logger.LogInformation($"[EMAIL SIMULADO] Guardado en: {filePath}");

                // Crear un archivo con solo el token para facilitar pruebas
                if (!string.IsNullOrEmpty(token))
                {
                    string tokenFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_TOKEN_{to.Replace("@", "_at_")}.txt";
                    string tokenFilePath = Path.Combine(_emailFolder, tokenFileName);
                    File.WriteAllText(tokenFilePath, token);
                    _logger.LogInformation($"[EMAIL SIMULADO] Token guardado en: {tokenFilePath}");
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al simular envío de correo");
                return Task.CompletedTask; // No lanzamos la excepción para no interrumpir el flujo
            }
        }
    }
}
