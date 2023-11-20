using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using NSubstitute;

namespace backend.UnitTests.tests;

public class AdminServiceTests
{
    private readonly IAdminService _adminService;
    private readonly IEmailService _emailService;
    public AdminServiceTests()
    {
        _emailService = Substitute.For<IEmailService>();
        _adminService = new AdminService(_emailService);
    }

    [Fact]
    public async void SendEmailToAdmin_PassedToEmailService()
    {
        // Arrange
        var model = new AdminEmailModel { };
        _emailService.SendEmailToAdmin(model).Returns(new EmailModel());
        
        // Act
        
        await _adminService.SendEmailToAdmin(model);

        // Assert
        await _emailService.Received().SendEmailToAdmin(model);
    }
}