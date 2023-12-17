using EventBus.Base.Events;

namespace ContactService.API.IntegrationEvents.Events
{
    public class ReportStartedIntegrationEvent : IntegrationEvent
    {
        public string ReportId { get; set; }
    }
}
