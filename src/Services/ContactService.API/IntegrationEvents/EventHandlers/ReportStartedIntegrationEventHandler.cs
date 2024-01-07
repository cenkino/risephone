using ContactService.API.Domain.Entities;
using ContactService.API.Infrastructure.Repository;
using ContactService.API.IntegrationEvents.Events;
using EventBus.Base.Abstraction;

namespace ContactService.API.IntegrationEvents.EventHandlers
{
    public class ReportStartedIntegrationEventHandler : IIntegrationEventHandler<ReportStartedIntegrationEvent>
    {
        private readonly IContactRepository repository;
        private readonly ILogger<ReportStartedIntegrationEvent> logger;
        private readonly IEventBus eventBus;

        public ReportStartedIntegrationEventHandler(IContactRepository repository, IEventBus eventBus, ILogger<ReportStartedIntegrationEvent> logger)
        {
            this.repository = repository;
            this.logger = logger;
            this.eventBus = eventBus;
        }

        public async Task Handle(ReportStartedIntegrationEvent @event)
        {
            logger.LogInformation("----- Handling integration event: {IntegrationEventId} at ContactService.API - ({@IntegrationEvent})", @event.ReportId, @event);

            var eventModel = new ReportCreatedIntegrationEvent(@event.ReportId);

            var contactInfos = await repository.GetAllContactInfosAsync();
            if (contactInfos == null || !contactInfos.Any())
            {
                eventBus.Publish(eventModel);
                return;
            }

            var locations = contactInfos.Where(x => x.InfoType == ContactInfo.ContactInfoType.Location);
            if (locations == null || !locations.Any())
            {
                eventBus.Publish(eventModel);
                return;
            }

            var distinctLocations = locations.Select(x => x.Value).Distinct();
            foreach (var location in distinctLocations)
            {
                var contacts = locations
                  .Where(x => x.Value == location)
                  .Select(x => x.ContactId)
                  .Distinct();

                var phoneNumbers = contactInfos
                  .Where(x => x.InfoType == ContactInfo.ContactInfoType.Phone && contacts.Contains(x.ContactId))
                  .Select(x => x.Value)
                  .Distinct()
                  .Count();

                eventModel.AddDetail(location, contacts.Count(), phoneNumbers);
            }

            try
            {
                eventBus.Publish(eventModel);
            }
            catch (Exception)
            {
                //logs
            }
        }

    }
}
