namespace FosorioServicesInterfaces.Interfaces
{
    public interface IFileStorageService
    {
        Task<Stream> GetFileAsync(string path);
        Task SaveFileAsync(string path, Stream data);
    }
}