using EventBus.Base.Events;

namespace ContactService.API.IntegrationEvents.Events
{
    public class ReportCreatedIntegrationEvent : IntegrationEvent
    {
        public ReportCreatedIntegrationEvent(string reportId)
        {
            ReportId = reportId;
            Details = new List<ReportDetailDto>();
        }

        public void AddDetail(string location, int contactCount, int phoneNumberCount)
        {
            Details.Add(new ReportDetailDto(location, contactCount, phoneNumberCount));
        }

        public string ReportId { get; private set; }
        public IList<ReportDetailDto> Details { get; private set; }

        public class ReportDetailDto
        {
            public ReportDetailDto(string location, int contactCount, int phoneNumberCount)
            {
                Location = location;
                ContactCount = contactCount;
                PhoneNumberCount = phoneNumberCount;
            }

            public string Location { get; private set; }
            public int ContactCount { get; private set; }
            public int PhoneNumberCount { get; private set; }
        }
    }
}
