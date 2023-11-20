using AutoMapper;
using backend.Application.Hubs;
using backend.Application.Models;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace backend.Application.Services.Impl
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IRepository<Subscriber> _repository;
        private readonly IMapper _mapper;
        private readonly IHubContext<SubscriberStatsHub> _hubContext;
        private readonly ILogger<SubscriberService> _logger;
        public SubscriberService(
            IRepository<Subscriber> repository,
            IMapper mapper,
            IHubContext<SubscriberStatsHub> hubContext,
            ILogger<SubscriberService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task<SubscriberResponseModel> AddSubscriber(NewSubscriberModel model)
        {
            var subscriber = _mapper.Map<Subscriber>(model);
            subscriber.SubscriptionDate = DateTime.UtcNow;
            var subscriberResponse = await _repository.CreateAsync(subscriber);
            await NotifyHubAsync();
            return _mapper.Map<SubscriberResponseModel>(subscriberResponse);
        }

        public async Task<SubscriberResponseModel> DeleteSubscriberById(Guid id)
        {
            var subscriber = await _repository.DeleteByGuidAsync(id);
            await NotifyHubAsync();
            return _mapper.Map<SubscriberResponseModel>(subscriber);
        }

        public async Task<SubscriberResponseModel> DeleteSusbcriberByEmail(string email)
        {
            var subscriber = await _repository.DeleteByAStringAsync(email);
            await NotifyHubAsync();
            return _mapper.Map<SubscriberResponseModel>(subscriber);
        }

        public async Task<SubscriberResponseModel> GetSubscriberByEmail(string email)
        {
            var subscriber = await _repository.GetByStringAsync(email);
            return _mapper.Map<SubscriberResponseModel>(subscriber);
        }

        public async Task<SubscriberEmailResponseModel> GetSubscriberById(Guid id)
        {
            var subscriber = await _repository.GetByGuidAsync(id);
            return _mapper.Map<SubscriberEmailResponseModel>(subscriber);
        }

        public async Task<IEnumerable<SubscriberResponseModel>> GetSubscribers()
        {
            var subscribers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubscriberResponseModel>>(subscribers);
        }

        public async Task<SubscriberStatsModel> GetSubscriberStats()
        {
            var currentDate = DateTime.UtcNow;
            var last7Days = new List<DateTime>()
            {
                currentDate.AddDays(-6),
                currentDate.AddDays(-5),
                currentDate.AddDays(-4),
                currentDate.AddDays(-3),
                currentDate.AddDays(-2),
                currentDate.AddDays(-1),
                currentDate
            };
            
            var subscribers = await _repository.GetAllAsync();
            var dates = subscribers.Aggregate(
                new int[] { 0, 0, 0, 0, 0, 0, 0 },
                (list, subscriber) =>
                {
                    var correspondingDate = last7Days
                        .FirstOrDefault(day => day.Year == subscriber.SubscriptionDate.Year
                                               && day.Month == subscriber.SubscriptionDate.Month
                                               && day.Day == subscriber.SubscriptionDate.Day);
                    var index = last7Days.FindIndex(day => day == correspondingDate);
                    
                    if(index >= 0) 
                    {
                       list[index]++;
                    }

                    return list;
                }, list => list
            );
            
            return new SubscriberStatsModel
            {
                SubscribedLast7D = dates,
                PercentageSubscribersWithPhoneNumber = subscribers.Count(x => !x.PhoneNumber.IsNullOrEmpty()) / (double)subscribers.Count() * 100.0
            };
        }

        private async Task NotifyHubAsync()
        {
            var subscriberStats = await GetSubscriberStats();
            await _hubContext.Clients.All.SendAsync("updateSubscriberStats", subscriberStats);
            _logger.LogInformation("Notify SignalR succeeded");
        }
    }
}
