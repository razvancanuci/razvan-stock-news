using AutoMapper;
using backend.Application.Hubs;
using backend.Application.Mappers;
using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace backend.UnitTests.Tests
{
    public class SubscriberServiceTests
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;
        private readonly IRepository<Subscriber> _subscriberRepository;

        private readonly Subscriber _subscriber = new Subscriber { Id = Guid.Parse("58ee7292-c608-40b5-ae2b-1a20ecd42a17"), Name = "Vasile", Email = "vasi123@a.ro", SubscriptionDate = new DateTime(2022, 11, 1, 10, 0, 0, DateTimeKind.Utc) };

        public SubscriberServiceTests()
        {
            _subscriberRepository = Substitute.For<IRepository<Subscriber>>();

            var mapperConfig = new MapperConfiguration(x => x.AddMaps(typeof(SubscriberMapper)));
            _mapper = new Mapper(mapperConfig);
            var hubContextMock = Substitute.For<IHubContext<SubscriberStatsHub>>();
            var loggerMock = Substitute.For<ILogger<SubscriberService>>();
            _subscriberService = new SubscriberService(_subscriberRepository, _mapper, hubContextMock, loggerMock);

            _subscriberRepository.GetAllAsync().Returns(new List<Subscriber>()
            {
                _subscriber,
                new Subscriber{ Id = Guid.Parse("bf52be36-650b-40d3-af87-883f5096f2fc"), Name = "Dorutz", Email = "doru_doru@yahoo.com", PhoneNumber = "0707070707", SubscriptionDate = new DateTime(2022, 11, 1, 10, 0, 0, DateTimeKind.Utc) },
                new Subscriber{ Id = Guid.Parse("89f53f00-7761-4d34-a3f5-38a71e2b176a"), Name = "Alessio", Email = "camatar@jmk.ro", SubscriptionDate = new DateTime(2022, 11, 1, 10, 0, 0, DateTimeKind.Utc) }

            });
        }

        [Fact]
        public async void GetSubscribers_Should_Work_Fine()
        {
            // Act
            var subscriberResponse = await _subscriberService.GetSubscribers();

            // Assert
            subscriberResponse.Should().NotBeEmpty();
            subscriberResponse.Should().HaveCount(3);
        }

        [Fact]
        public async void GetSubscribersStats_Should_Work_Fine()
        {
            // Act
            var stats = await _subscriberService.GetSubscriberStats();

            // Assert
            stats.Should().NotBeNull();
            stats.PercentageSubscribersWithPhoneNumber.Should().BeGreaterThan(33);
        }

        [Fact]
        public async void GetSubscriberByEmail_Should_Work_Fine()
        {
            // Arrange
            _subscriberRepository.GetByStringAsync(_subscriber.Email).Returns(_subscriber);

            // Act
            var result = await _subscriberService.GetSubscriberByEmail(_subscriber.Email);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(_subscriber.Name);
        }

        [Fact]
        public async void GetSubscriberById_Should_Work_Fine()
        {
            // Arrange
            _subscriberRepository.GetByGuidAsync(_subscriber.Id).Returns(_subscriber);

            // Act
            var result = await _subscriberService.GetSubscriberById(_subscriber.Id);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(_subscriber.Email);
        }

        [Fact]
        public async void AddSubscriber_Should_Work_Fine()
        {
            // Arrange
            var newModel = new NewSubscriberModel { Name = "Andu", Email = "vtm@t.com" };
            var subscriber = _mapper.Map<Subscriber>(newModel);
            subscriber.SubscriptionDate = DateTime.UtcNow;

            _subscriberRepository.CreateAsync(subscriber).Returns(subscriber);

            // Act
            var result = await _subscriberService.AddSubscriber(newModel);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void DeleteSubscriber_Should_Work_Fine()
        {
            // Arrange
            _subscriberRepository.DeleteByGuidAsync(_subscriber.Id).Returns(_subscriber);

            // Act
            var result = await _subscriberService.DeleteSubscriberById(_subscriber.Id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(_subscriber.Name);
        }

        [Fact]
        public async void DeleteByEmail_Works_Fine()
        {
            // Arrange
            var email = "doru_doru@yahoo.com";
            _subscriberRepository.DeleteByAStringAsync(email).Returns(new Subscriber { Id = Guid.Parse("bf52be36-650b-40d3-af87-883f5096f2fc"), Name = "Dorutz", Email = "doru_doru@yahoo.com", PhoneNumber = "0707070707", SubscriptionDate = new DateTime(2022, 11, 1, 10, 0, 0, DateTimeKind.Utc) });

            // Act
            var result = await _subscriberService.DeleteSusbcriberByEmail(email);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Dorutz");

        }
    }
}
