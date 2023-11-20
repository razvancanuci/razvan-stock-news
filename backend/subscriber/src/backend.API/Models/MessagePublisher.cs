using backend.Application.Models;

namespace backend.API.Models
{
    public class MessagePublisher
    {
        public static SubscriberDeletedMessage PublishDelete(Guid id) => new SubscriberDeletedMessage { Id = id };
    }
}
