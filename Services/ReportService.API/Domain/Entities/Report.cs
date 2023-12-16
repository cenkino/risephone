using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReportService.API.Domain.Entities
{
    public class Report
    {
        public Report() { }

        public Report(DateTime createdDate, ReportStatus status)
        {
            CreatedDate = createdDate;
            Status = status;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? CompletedDate { get; set; }
        public ReportStatus Status { get; set; }

        public enum ReportStatus
        {
            Preparing = 0,
            Completed = 1
        }
    }
}
