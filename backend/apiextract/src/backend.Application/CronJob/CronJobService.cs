using backend.Application.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace backend.Application.CronJob;

public class CronJobService : BackgroundService
{
    public static DateTime DateTimeReference = new (2023,06,30,19,07,00,DateTimeKind.Utc);
    private readonly IApiService _apiService;
    private readonly ILogger<CronJobService> _logger;
    public CronJobService(IApiService apiService, ILogger<CronJobService> logger)
    {
        _apiService = apiService;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var date = DateTime.UtcNow;

            if (date.DayOfWeek == DateTimeReference.DayOfWeek &&
                date.Hour == DateTimeReference.Hour &&
                date.Minute == DateTimeReference.Minute &&
                date.Second == DateTimeReference.Second)
            {
                await _apiService.ExtractDataFromApi();
                _logger.LogInformation($"Sending the data from API: {date}");
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}