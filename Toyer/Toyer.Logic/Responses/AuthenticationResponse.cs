using System.Text.Json;

namespace Toyer.Logic.Responses;

public class AuthenticationResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}