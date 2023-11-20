using System.Net;
using System.Text;
using AutoMapper;
using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess.Entities;
using backend.DataAccess.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace backend.UnitTests.Tests;

public class ApiServiceTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly Mock<IMongoRepository> _mongoRepositoryMock;
    private readonly Mock<ITrainService> _trainServiceMock;
    private readonly IApiService _apiService;
    
    public ApiServiceTests()
    {
        var jsonData = "{\"quoteResponse\":{\"result\":[{\"symbol\": \"AAPL\", \"longName\": \"Apple Inc.\"}]}}";
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);

        _configurationMock = new Mock<IConfiguration>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _mongoRepositoryMock = new Mock<IMongoRepository>();
        _trainServiceMock = new Mock<ITrainService>();
        var mapperMock = new Mock<IMapper>();
        var loggerMock = new Mock<ILogger<ApiService>>();

        _apiService = new ApiService(
            httpClient,
            _configurationMock.Object,
            _publishEndpointMock.Object,
            _mongoRepositoryMock.Object,
            _trainServiceMock.Object,
            mapperMock.Object,
            loggerMock.Object);
    }

    [Theory]
    [InlineData("https://localhost:1234")]
    public async Task ExtractDataFromApi_PassedToPublisherRepositoryServiceAndConfiguration(string url)
    {
        // Arrange
        _configurationMock.Setup(x => x["Api"]).Returns(url);

        // Act
        await _apiService.ExtractDataFromApi();
        
        // Assert
        _publishEndpointMock.Verify(x => x.Publish(It.IsAny<ApiResultModel>(), 
            It.IsAny<CancellationToken>()));
        _configurationMock.Verify(x => x["Api"], Times.Once);
        _mongoRepositoryMock.Verify(x => x.GetBySymbolAsync(It.IsAny<string>()), Times.AtLeastOnce);
        _trainServiceMock
            .Verify(x => x.PredictPrice(
                It.IsAny<IEnumerable<ApiData>>(),
                It.IsAny<ApiDataModel>()), Times.AtLeastOnce);
        
    }
}
