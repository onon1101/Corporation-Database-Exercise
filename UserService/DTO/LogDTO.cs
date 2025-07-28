using Confluent.Kafka;

namespace Api.DTO;

public class LogDTO
{
    public DateTime Timestamp { get; set; }
    
    public string Level { get; set; }
    
    public string Source { get; set; }
    
    public string Message { get; set; }   
    
    public string? exception { get; set; }
    
    public string? traceId { get; set; }
    
    public Dictionary<string, object>? AdditionalData { get; set; }
}