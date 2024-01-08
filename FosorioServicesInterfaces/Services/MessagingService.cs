using FosorioServicesInterfaces.Interfaces;

public class MessagingService : IMessagingService
{
    private readonly IEmailService _emailService;
    private readonly ISmsService _smsService;

    public MessagingService(IEmailService emailService, ISmsService smsService)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        if (string.IsNullOrEmpty(to))
            throw new ArgumentException("Message recipient cannot be null or empty", nameof(to));

        if (string.IsNullOrEmpty(subject))
            throw new ArgumentException("Message subject cannot be null or empty", nameof(subject));

        if (string.IsNullOrEmpty(body))
            throw new ArgumentException("Message body cannot be null or empty", nameof(body));

        try
        {
            await _emailService.SendEmailAsync(to, subject, body);
        }
        catch (Exception ex)
        {
            // Log the exception details
            throw;
        }
    }

    public async Task SendSmsAsync(string to, string message)
    {
        if (string.IsNullOrEmpty(to))
            throw new ArgumentException("Message recipient cannot be null or empty", nameof(to));

        if (string.IsNullOrEmpty(message))
            throw new ArgumentException("Message cannot be null or empty", nameof(message));

        try
        {
            await _smsService.SendSmsAsync(to, message);
        }
        catch (Exception ex)
        {
            // Log the exception details
            throw;
        }
    }
}