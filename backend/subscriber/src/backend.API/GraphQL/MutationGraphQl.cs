using backend.Application.Models;
using backend.Application.Services;

namespace backend.API.GraphQL;

public class MutationGraphQl
{
    public async Task<SubscriberResponseModel> AddSubscriber([Service] ISubscriberService service, string name, string email, string phoneNumber)
    {
        var subscriber = new NewSubscriberModel
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber
        };
        return await service.AddSubscriber(subscriber);
    }
}