using backend.Application.Models;

namespace backend.Application.Services;

public interface IAdminService
{
    Task<EmailModel> SendEmailToAdmin(AdminEmailModel admin);
}