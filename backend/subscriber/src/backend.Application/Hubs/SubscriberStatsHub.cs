using backend.Application.Models;
using backend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace backend.Application.Hubs;

public class SubscriberStatsHub : Hub
{
    private readonly ISubscriberService _subscriberService;
    private readonly ILogger<SubscriberStatsHub> _logger;
    public SubscriberStatsHub(ISubscriberService subscriberService, ILogger<SubscriberStatsHub> logger)
    {
        _subscriberService = subscriberService;
        _logger = logger;
    }
    
    [Authorize(Roles="User,Admin")]
    public async Task<SubscriberStatsModel> GetSubscriberStats()
    {
        _logger.LogInformation("Checking GetSubscriberStats method from signalRHub");
        return await _subscriberService.GetSubscriberStats();
    }
}