using backend.Application.Models;
using backend.Application.Services;

namespace backend.API.GraphQL;

public class QueryGraphQl
{
    public async Task<IEnumerable<SubscriberResponseModel>> GetSubscribers([Service] ISubscriberService subscriberService)
    {
        return await subscriberService.GetSubscribers();
    }

    public async Task<SubscriberEmailResponseModel> GetSubscriberById( [Service] ISubscriberService subscriberService, Guid id)
    {
        return await subscriberService.GetSubscriberById(id);
    }
}