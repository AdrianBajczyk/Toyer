using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Toyer.API.Exceptions;

public record ErrorResponse
{
    public string Message { get; set; }
    public string Code { get; set; }
    public string ReqestId { get; set; }

}
