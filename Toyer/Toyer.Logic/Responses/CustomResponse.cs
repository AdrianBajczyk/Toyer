using System.Text.Json;
using System.Web;

namespace Toyer.Logic.Responses;

public class CustomResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class AuthorizationResponse
{
    public string StatusCode { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}