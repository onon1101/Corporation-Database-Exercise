using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoggerService.Worker.Models
{
    public class LogEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("level")]
        public string Level { get; set; } = "INFO";

        /// <summary>
        /// where does source is, for example from user.sv or user.wk
        /// </summary>
        [BsonElement]
        public string Source { get; set; }

        [BsonElement("message")]
        public string Message { get; set; } = "";

        [BsonElement("exception")]
        public string? Exception { get; set; }

        [BsonElement("traceId")]
        public string? TraceId { get; set; }

        [BsonElement("additionalData")]
        public BsonDocument? AdditionalData { get; set; }
    }
}

/*
 {
  "timestamp": "2025-07-23T10:30:00Z",
  "level": "ERROR",
  "source": "UserService",
  "message": "User creation failed.",
  "exception": "System.NullReferenceException: Object reference not set...",
  "traceId": "eaf2bcd1-xyz-123",
  "additionalData": {
    "userId": "abc123",
    "ip": "192.168.0.1"
  }
}
*/