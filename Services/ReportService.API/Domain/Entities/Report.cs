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

        
        public string Id { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? CompletedDate { get; set; }
        public ReportStatus Status { get; set; }

        public enum ReportStatus
        {
            Preparing = 0,
            Completed = 1
        }
    }
}
