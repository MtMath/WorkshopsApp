using System.Text.Json.Serialization;

namespace Workshops.Application.DTOs;

/// <summary>
/// Use generic entity model to standard all responses
/// </summary>
/// <typeparam name="TDto">Use generic entity model to standard responses</typeparam>
public record struct GenericResponseDto<TDto>(
    [property: JsonPropertyName("data")] TDto? Data,
    [property: JsonPropertyName("message")] string Message) where TDto : class
{
    [JsonPropertyName("timestamp")] public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;
}