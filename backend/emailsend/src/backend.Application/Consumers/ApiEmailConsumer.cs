using backend.Application.Models;
using backend.Application.Services;
using MassTransit;

namespace backend.Application.Consumers;

public class ApiEmailConsumer : IConsumer<ApiResultModel>
{
    private readonly IApiService _apiService;
    public ApiEmailConsumer(IApiService apiService)
    {
        _apiService = apiService;
    }
    
    public async Task Consume(ConsumeContext<ApiResultModel> context)
    {
        var model = context.Message;
        await _apiService.SendEmailToAllSubscribers(model);
    }
}