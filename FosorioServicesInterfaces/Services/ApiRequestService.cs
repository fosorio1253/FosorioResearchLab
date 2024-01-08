using FosorioServicesInterfaces.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace FosorioServicesInterfaces.Services
{
    public class ApiRequestService : IApiRequestService, IDisposable
    {
        private HttpClient _httpClient;
        private bool _disposed;

        public ApiRequestService(HttpClient httpClient)
            => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<T> RequestAsync<T>(HttpMethod method, string endpoint, object data = null, Dictionary<string, string> headers = null)
        {
            CheckDisposed();

            var request = new HttpRequestMessage(method, endpoint);

            if (headers != null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

            if (data != null)
            {
                try
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                }
                catch (JsonSerializationException ex)
                {
                    // Tratamento de erro para falha na serialização do objeto 'data'
                    throw new ArgumentException("Falha ao serializar o objeto de dados para JSON", ex);
                }
            }

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                // Tratamento de erro para falha na solicitação HTTP
                throw new InvalidOperationException($"Falha ao enviar a solicitação para {endpoint}", ex);
            }

            // Tratamento de erro para resposta HTTP não bem-sucedida
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"A solicitação para {endpoint} falhou com o código de status {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (JsonSerializationException ex)
            {
                // Tratamento de erro para falha na desserialização da resposta
                throw new InvalidOperationException("Falha ao desserializar a resposta para o tipo esperado", ex);
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _httpClient?.Dispose();
            _httpClient = null;
            _disposed = true;
        }

        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException("RequestService");
        }
    }
}