using backend.Application.Models;
using backend.DataAccess.Entities;

namespace backend.Application.Services
{
    public interface IEmailService
    {
        Task<EmailModel> SendEmailToSubscriber(Subscriber subscriber);
        Task<EmailModel> SendEmailToAdmin(AdminEmailModel admin);

        Task<EmailModel> SendEmailWithApiData(ApiResultModel apiModel, Subscriber subscriber);
    }
}
