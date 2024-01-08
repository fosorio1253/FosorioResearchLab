using FosorioServicesInterfaces.Interfaces;

namespace FosorioServicesInterfaces.Services
{
    public class FileStorageService : IFileStorageService
    {
        public async Task<Stream> GetFileAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("O caminho do arquivo não pode ser nulo ou vazio.", nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException($"O arquivo {path} não foi encontrado.");

            return new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        public async Task SaveFileAsync(string path, Stream data)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("O caminho do arquivo não pode ser nulo ou vazio.", nameof(path));

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Os dados não podem ser nulos.");

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await data.CopyToAsync(fileStream);
            }
        }
    }
}