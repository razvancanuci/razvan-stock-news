using AutoMapper;
using backend.Application.Mappers;
using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace backend.UnitTests.tests
{
    public class SubscriberServiceTests
    {
        private readonly IRepository<Subscriber> _repository;
        private readonly ISubscriberService _subscriberService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public SubscriberServiceTests()
        {
            var mapperConfig = new MapperConfiguration(x => x.AddMaps(typeof(SubscriberMapper)));
            _mapper = new Mapper(mapperConfig);
            
            _emailService = Substitute.For<IEmailService>();

            _repository = Substitute.For<IRepository<Subscriber>>();
            _subscriberService = new SubscriberService(_emailService, _mapper, _repository);
        }

        [Fact]
        public async void AddSubscriber_Returns_Email()
        {
            // Arrange
            var message = new SubscriberAddedMessage { Id = Guid.Parse("6fb96f1b-610f-4720-a970-40456061c5bc"), Name = "Vasea", Email = "Vasea@yahoo.ro", PhoneNumber = "0712345678" };
            _emailService.SendEmailToSubscriber(Arg.Any<Subscriber>()).Returns(new EmailModel());

            // Act

            var result= await _subscriberService.AddAndSendEmailToSubscriber(message);


            // Assert
            result.Should().BeOfType<EmailModel>();
        }
        
        [Fact]
        public async void AddSubscriber_Throws_Exception()
        {
            // Arrange
            var message = new SubscriberAddedMessage { Id = Guid.Parse("6fb96f1b-610f-4720-a970-40456061c5bc"), Name = "Vasea", Email = "Vasea@yahoo.ro", PhoneNumber = "0712345678" };
            _repository.AddAsync(Arg.Any<Subscriber>()).Throws<ArgumentNullException>();

            // Act
            Func<Task> act = async () =>
            {
                await _subscriberService.AddAndSendEmailToSubscriber(message);
            };

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }


        [Fact]
        public async void DeleteSubscriber_Returns_The_Subscriber()
        {
            // Arrange
            var message = new SubscriberAddedMessage { Id = Guid.Parse("6fb96f1b-610f-4720-a970-40456061c5bc"), Name = "Vasea", Email = "Vasea@yahoo.ro", PhoneNumber = "0712345678" };
            var subscriber = _mapper.Map<Subscriber>(message);
            _repository.DeleteByGuid(Arg.Any<Guid>()).Returns(subscriber);


            // Act
            var response = await _subscriberService.DeleteSubscriberById(Guid.Parse("6fb96f1b-610f-4720-a970-40456061c5bc"));

            // Assert
            response.Should().NotBeNull();
        }
    }
}
