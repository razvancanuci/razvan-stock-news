using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess.Entities;
using NSubstitute;

namespace backend.UnitTests.tests;

public class ApiServiceTests
{
    private readonly IApiService _apiService;
    private readonly IEmailService _emailService;
    private readonly ISubscriberService _subscriberService;

    public ApiServiceTests()
    {
        _emailService = Substitute.For<IEmailService>();
        _subscriberService = Substitute.For<ISubscriberService>();
        _apiService = new ApiService(_emailService, _subscriberService);
    }

    [Fact]
    public async void SendEmail_PassedToSubscriberServiceAndEmailService()
    {
        // Arrange
        var model = new ApiResultModel
        {
            Result = new List<ApiDataModel>
            {
                new ApiDataModel{Symbol = "NKE", MarketCap = 123456782, RegularMarketPrice = 123, LongName = "Nike Inc"}
            }
        };
        _subscriberService.GetAllSubscribers().Returns(
            new List<Subscriber>()
            {
                new Subscriber
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "vasi", Email = "vasi@vasi.vasi"
                },
                new Subscriber
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "vasi2", Email = "vasi2@vasi.vasi"
                },
                new Subscriber
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "vasi3", Email = "vasi3@vasi.vasi"
                }
            }
        );
        // Act
        await _apiService.SendEmailToAllSubscribers(model);

        // Assert
        await _subscriberService.Received(1).GetAllSubscribers();
        await _emailService.Received(3).SendEmailWithApiData(model, Arg.Any<Subscriber>());
    }
}