namespace ReportService.API.Models
{
    public class ReportVal
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int Status { get; set; }
        public IList<ReportDetailsVal> Details { get; set; }
    }
}
