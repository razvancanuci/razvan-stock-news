using backend.Application.Models;
using backend.Application.Services;
using MassTransit;

namespace backend.Application.Consumers;

public class AdminEmailConsumer: IConsumer<AdminEmailModel>
{
    private readonly IAdminService _adminService;
    
    public AdminEmailConsumer(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    public async Task Consume(ConsumeContext<AdminEmailModel> context)
    {
        await _adminService.SendEmailToAdmin(context.Message);
    }
}