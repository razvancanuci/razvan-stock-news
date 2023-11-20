using backend.Application.Models;
using backend.Application.Services;
using MassTransit;

namespace backend.Application.Consumers
{
    public class SubscriberDeletedConsumer : IConsumer<SubscriberDeletedMessage>
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberDeletedConsumer(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public async Task Consume(ConsumeContext<SubscriberDeletedMessage> context)
        {
            var id = context.Message.Id;
            await _subscriberService.DeleteSubscriberById(id);
        }
    }
}
