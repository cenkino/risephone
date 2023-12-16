namespace ReportService.API.Domain.Entities
{
    public class ReportDetail
    {
        public ReportDetail(string reportId, string location, int contactCount, int phoneNumberCount)
        {
            ReportId = reportId;
            Location = location;
            ContactCount = contactCount;
            PhoneNumberCount = phoneNumberCount;
        }

        
        public string Id { get; set; }
        
        public string ReportId { get; set; }
        
        public string Location { get; set; }
        
        public int ContactCount { get; set; }
        
        public int PhoneNumberCount { get; set; }
    }
}
