﻿namespace FosorioServicesInterfaces.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsAsync(string to, string message);
    }
}
