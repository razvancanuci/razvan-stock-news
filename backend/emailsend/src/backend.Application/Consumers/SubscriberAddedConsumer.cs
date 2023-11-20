using backend.Application.Models;
using backend.Application.Services;
using MassTransit;

namespace backend.Application.Consumers
{
    public class SubscriberAddedConsumer : IConsumer<SubscriberAddedMessage>
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberAddedConsumer(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }


        public async Task Consume(ConsumeContext<SubscriberAddedMessage> context)
        {
            await _subscriberService.AddAndSendEmailToSubscriber(context.Message);
        }
    }
}
