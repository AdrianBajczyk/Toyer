using System.Text.Json;

namespace Toyer.Logic.Responses;

public class CustomResponse
{
    public int status { get; set; }
    public string message { get; set; }
    public string error { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
