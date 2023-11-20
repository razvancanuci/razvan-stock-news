using AutoMapper;
using backend.Application.Models;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;

namespace backend.Application.Services.Impl
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IRepository<Subscriber> _repository;
        public SubscriberService(IEmailService emailService, IMapper mapper, IRepository<Subscriber> repository)
        {
            _emailService = emailService;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<EmailModel> AddAndSendEmailToSubscriber(SubscriberAddedMessage subscriberAdded)
        {
            var subscriber = _mapper.Map<Subscriber>(subscriberAdded);
            await _repository.AddAsync(subscriber);
            var email = await _emailService.SendEmailToSubscriber(subscriber);
            return email;
        }

        public async Task<Subscriber> DeleteSubscriberById(Guid id)
        {
            var subscriber = await _repository.DeleteByGuid(id);
            return subscriber;
        }

        public async Task<IEnumerable<Subscriber>> GetAllSubscribers()
        {
            return await _repository.GetAll();
        }
    }
}
