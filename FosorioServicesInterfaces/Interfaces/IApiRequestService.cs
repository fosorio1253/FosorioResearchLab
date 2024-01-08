namespace FosorioServicesInterfaces.Interfaces
{
    // A interface IRequestService define um contrato para um serviço que pode enviar solicitações HTTP de forma assíncrona.
    // Ela tem um método RequestAsync que aceita um método HTTP, um endpoint,
    // um objeto de dados opcional para ser enviado como corpo da solicitação, e um dicionário opcional de cabeçalhos.
    // O método retorna uma tarefa que, quando concluída, produz uma resposta desserializada do tipo especificado.

    // Aqui estão os detalhes do método RequestAsync:

    // HttpMethod method: O método HTTP a ser usado na solicitação (por exemplo, GET, POST, PUT, DELETE, etc.).
    // string endpoint: O endpoint específico da API para o qual a solicitação deve ser enviada.
    // object data = null: Um objeto opcional que será serializado como JSON e incluído no corpo da solicitação.
    // Isso é útil para métodos HTTP como POST e PUT que enviam dados para o servidor.
    // Dictionary<string, string> headers = null: Um dicionário opcional de cabeçalhos que serão incluídos na solicitação.

    //O método retorna Task<T>, onde T é o tipo de objeto que você espera receber da API.
    //A tarefa pode ser aguardada usando a palavra-chave await, e quando a tarefa é concluída,
    //ela produz uma resposta desserializada do tipo especificado.

    public interface IApiRequestService
    {
        Task<T> RequestAsync<T>(HttpMethod method, string endpoint, object data = null, Dictionary<string, string> headers = null);
    }
}
