﻿using System.Text.Json;

namespace Toyer.Logic.Responses;

public class CustomResponse
{
    public string StatusCode { get; set; }
    public string Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
