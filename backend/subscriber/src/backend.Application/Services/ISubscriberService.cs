using backend.Application.Models;

namespace backend.Application.Services
{
    public interface ISubscriberService
    {
        Task<SubscriberResponseModel> AddSubscriber(NewSubscriberModel model);
        Task<SubscriberResponseModel> GetSubscriberByEmail(string email);
        Task<SubscriberEmailResponseModel> GetSubscriberById(Guid id);
        Task<SubscriberResponseModel> DeleteSubscriberById(Guid id);
        Task<IEnumerable<SubscriberResponseModel>> GetSubscribers();
        Task<SubscriberStatsModel> GetSubscriberStats();
        Task<SubscriberResponseModel> DeleteSusbcriberByEmail(string email);

    }
}
