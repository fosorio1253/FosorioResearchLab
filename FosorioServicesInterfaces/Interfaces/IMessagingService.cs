namespace FosorioServicesInterfaces.Interfaces
{
    public interface IMessagingService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendSmsAsync(string to, string message);
    }
}