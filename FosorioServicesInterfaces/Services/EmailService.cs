using FosorioServicesInterfaces.Interfaces;

namespace FosorioServicesInterfaces.Services
{
    public class EmailService : IEmailService
    {
        private readonly Func<string, string, string, Task> _emailSender;

        public EmailService(Func<string, string, string, Task> emailSender)
            => _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender), 
                "EmailSender não pode ser nulo. Por favor, forneça uma função válida para enviar e-mails.");

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            if (string.IsNullOrEmpty(to))
                throw new ArgumentException("O destinatário do e-mail não pode ser nulo ou vazio.", nameof(to));

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentException("O assunto do e-mail não pode ser nulo ou vazio.", nameof(subject));

            if (string.IsNullOrEmpty(body))
                throw new ArgumentException("O corpo do e-mail não pode ser nulo ou vazio.", nameof(body));

            try
            {
                await _emailSender(to, subject, body);
            }
            catch (Exception ex)
            {
                // Registre os detalhes da exceção
                throw new Exception($"Falha ao enviar e-mail para {to}. Por favor, verifique a exceção interna para mais detalhes.", ex);
            }
        }
    }
}