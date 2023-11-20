using backend.Application.Models;
using backend.DataAccess.Entities;

namespace backend.Application.Services
{
    public interface ISubscriberService
    {
        Task<EmailModel> AddAndSendEmailToSubscriber(SubscriberAddedMessage subscriberAdded);
        Task<Subscriber> DeleteSubscriberById(Guid id);
        Task<IEnumerable<Subscriber>> GetAllSubscribers();
    }
}
