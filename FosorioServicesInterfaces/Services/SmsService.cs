using FosorioServicesInterfaces.Interfaces;

namespace FosorioServicesInterfaces.Services
{
    public class SmsService : ISmsService
    {
        private readonly Func<string, string, Task> _smsSender;

        public SmsService(Func<string, string, Task> smsSender)
            => _smsSender = smsSender ?? throw new ArgumentNullException(nameof(smsSender),
                "SmsSender não pode ser nulo. Por favor, forneça uma função válida para enviar SMS.");

        public async Task SendSmsAsync(string to, string message)
        {
            if (string.IsNullOrEmpty(to))
                throw new ArgumentException("O destinatário da mensagem não pode ser nulo ou vazio.", nameof(to));

            if (to.Length != 10 || !long.TryParse(to, out _))
                throw new ArgumentException("O destinatário da mensagem deve ser um número de telefone válido de 10 dígitos.", nameof(to));

            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("A mensagem não pode ser nula ou vazia.", nameof(message));

            if (message.Length > 160)
                throw new ArgumentException("A mensagem não pode exceder 160 caracteres.", nameof(message));

            try
            {
                await _smsSender(to, message);
            }
            catch (Exception ex)
            {
                // Registre os detalhes da exceção
                throw new Exception($"Falha ao enviar SMS para {to}. Por favor, verifique a exceção interna para mais detalhes.", ex);
            }
        }
    }
}