using backend.Application.CronJob;
using backend.Application.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace backend.UnitTests.Tests;

public class CronJobServiceTests
{
    private readonly CronJobService _cronjobService;
    private readonly Mock<IApiService> _apiService;
    private readonly Mock<ILogger<CronJobService>> _logger;
    public CronJobServiceTests()
    {
        _apiService = new Mock<IApiService>();
        _logger = new Mock<ILogger<CronJobService>>();
        _cronjobService = new CronJobService(_apiService.Object, _logger.Object);
    }

    [Fact]
    public async Task StartAsync_PassedToApiService()
    {
        // Arrange
        var cancellationTokenSource = new CancellationTokenSource();
        CronJobService.DateTimeReference = DateTime.UtcNow;

        // Act
        await _cronjobService.StartAsync(cancellationTokenSource.Token);
        await Task.Delay(1000); 
        cancellationTokenSource.Cancel(); 

        // Assert
        _apiService.Verify(apis => apis.ExtractDataFromApi());
    }
    
}