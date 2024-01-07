using EventBus.Base.Abstraction;
using ReportService.API.Domain.Entities;
using ReportService.API.Infrastructure.Repository;
using ReportService.API.IntegrationEvents.Events;

namespace ReportService.API.IntegrationEvents.EventHandlers
{
    public class ReportCreatedIntegrationEventHandler : IIntegrationEventHandler<ReportCreatedIntegrationEvent>
    {
        private readonly IReportRepository repository;
        private readonly ILogger<ReportCreatedIntegrationEvent> logger;

        public ReportCreatedIntegrationEventHandler(IReportRepository repository, ILogger<ReportCreatedIntegrationEvent> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Handle(ReportCreatedIntegrationEvent @event)
        {
            logger.LogInformation("----- Handling integration event: {IntegrationEventId} at ReportService.API - ({@IntegrationEvent})", @event.ReportId, @event);

            var report = await repository.GetReportByIdAsync(@event.ReportId);
            if (report == null) return;

            try
            {
                await repository.ReportCompletedAsync(report.Id);
            }
            catch
            {
                //logs
            }

            if (@event.Details == null) return;

            var details = @event.Details
              .Select(x => new ReportDetail(@event.ReportId, x.Location, x.ContactCount, x.PhoneNumberCount))
              .ToList();

            await repository.CreateReportDetailsAsync(details);
        }
    }
}
