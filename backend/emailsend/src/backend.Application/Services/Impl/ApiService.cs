using backend.Application.Models;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;

namespace backend.Application.Services.Impl;

public class ApiService : IApiService
{
    private readonly IEmailService _emailService;
    private readonly ISubscriberService _subscriberService;
    public ApiService(IEmailService emailService, ISubscriberService subscriberService)
    {
        _emailService = emailService;
        _subscriberService = subscriberService;
    }


    public async Task SendEmailToAllSubscribers(ApiResultModel apiModel)
    {
        var subscribers = await _subscriberService.GetAllSubscribers();
        foreach (var subscriber in subscribers)
        {
            await _emailService.SendEmailWithApiData(apiModel, subscriber);
        }
    }
}