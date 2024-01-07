using EventBus.Base.Events;

namespace ReportService.API.IntegrationEvents.Events
{
    public class ReportStartedIntegrationEvent : IntegrationEvent
    {
        public ReportStartedIntegrationEvent() { }

        public ReportStartedIntegrationEvent(string reportId)
        {
            ReportId = reportId;
        }

        public string ReportId { get; set; }
    }
}
