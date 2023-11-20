using backend.Application.Models;

namespace backend.Application.Services.Impl;

public class AdminService : IAdminService
{
    private readonly IEmailService _emailService;
    
    public AdminService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<EmailModel> SendEmailToAdmin(AdminEmailModel admin)
    {
        var email = await _emailService.SendEmailToAdmin(admin);
        return email;
    }
}