﻿namespace FosorioServicesInterfaces.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T data, TimeSpan? expiry = null);
    }
}