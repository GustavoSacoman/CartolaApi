namespace CartolaApi.Responses.JsonResponse;

public class JsonResponse
{
    /// <summary>
    /// Retorna uma resposta JSON padronizada de sucesso.
    /// </summary>
    /// <param name="status">Uma string indicando a mensagem de status.</param>
    /// <param name="data">Os dados da resposta, que podem ser um dicionário, lista, string ou nulo.</param>
    /// <param name="statusCode">O código de status HTTP a ser retornado com a resposta.</param>
    /// <returns>Uma tupla contendo o dicionário de resposta e o código de status.</returns>
    public static (Dictionary<string, object> Response, int StatusCode) 
        JsonSuccessResponse(
        string status,
        object data,
        int statusCode
        )
    {
        var response = new Dictionary<string, object>
        {
            { "status", status },
            { "data", data }
        };
        return (response, statusCode);
    }

    /// <summary>
    /// Retorna uma resposta JSON padronizada de erro.
    /// </summary>
    /// <param name="status">Uma string indicando a mensagem de status.</param>
    /// <param name="data">Os detalhes do erro, que podem ser um dicionário, lista, string ou nulo.</param>
    /// <param name="statusCode">O código de status HTTP a ser retornado com a resposta.</param>
    /// <returns>Uma tupla contendo o dicionário de resposta e o código de status.</returns>
    public static (Dictionary<string, object> Response, int StatusCode)
        JsonErrorResponse(
        string status,
        object data,
        int statusCode
        )
    {
        var response = new Dictionary<string, object>
        {
            { "status", status },
            { "error", data }
        };
        return (response, statusCode);
    }
}