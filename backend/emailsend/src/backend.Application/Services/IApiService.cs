using backend.Application.Models;

namespace backend.Application.Services;

public interface IApiService
{
    Task SendEmailToAllSubscribers(ApiResultModel apiModel);
}