using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess.Entities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace backend.UnitTests.tests
{
    public class EmailServiceTests
    {
        private readonly IEmailService _emailService;
        private readonly ITemplateService _templateService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        public EmailServiceTests()
        {
            var configurationDict = new Dictionary<string, string?> {
                {"Smtp:FromAddress", "email"},
                {"Smtp:Port", "2"},
                {"Smtp.Server", "server"},
                {"Smtp:Password","pass"}
            };
            
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(configurationDict).Build();

            _logger = Substitute.For<ILogger<EmailService>>();
            _templateService = Substitute.For<ITemplateService>();
            _emailService = new EmailService(_templateService, _configuration, _logger);
        }
        
        [Fact]
        public async Task SendEmailToSubscriber_Returns_Model()
        {
            // Arrange
            var subscriber = new Subscriber {Id=Guid.Parse("ea23e16e-7ba3-4260-8cba-a14c1932f84d"), Name = "Vasilie", Email="vasi@yahoo.com", PhoneNumber = "0707070707"};
            
            // Act
            var result = await _emailService.SendEmailToSubscriber(subscriber);

            // Assert
             result.Should().BeOfType<EmailModel>();
        }
        
        [Fact]
        public async Task SendEmailToAdmin_Returns_Model()
        {
            // Arrange
            var admin = new AdminEmailModel {Username = "VASI", Code = "000000", Email = "aaa@a.a"};
            
            // Act
            var result = await _emailService.SendEmailToAdmin(admin);

            // Assert
            result.Should().BeOfType<EmailModel>();
        }
        
        [Fact]
        public async Task SendEmailApi_Throws_DirectoryNotFoundException()
        {
            // Arrange
            
            var model = new ApiResultModel
            {
                Result = new List<ApiDataModel>
                {
                    new ApiDataModel{Symbol = "NKE", MarketCap = 123456782, RegularMarketPrice = 123, LongName = "Nike Inc"}
                }
            };
            
            var subscriber = new Subscriber
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "vasi", Email = "vasi@vasi.vasi"
            };
            
            // Act
            var act = async () => await _emailService.SendEmailWithApiData(model, subscriber);

            // Assert
            await act.Should().ThrowAsync<DirectoryNotFoundException>();
        }
    }
}
