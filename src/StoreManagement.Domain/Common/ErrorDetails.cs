using System.Text.Json;

namespace StoreManagement.Application.Common;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Detailed { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}