using AutoMapper;
using backend.Application.Models;
using backend.DataAccess.Entities;

namespace backend.Application.Mappers
{
    public class SubscriberMapper : Profile
    {
        public SubscriberMapper()
        {
            CreateMap<SubscriberAddedMessage, Subscriber>();
        }
    }
}
