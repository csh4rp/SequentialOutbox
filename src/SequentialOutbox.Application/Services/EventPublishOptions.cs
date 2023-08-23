namespace SequentialOutbox.Application.Services;

public class EventPublishOptions
{
    public string? Stream { get; init; } 
    
    public string? Topic { get; init; } 
}