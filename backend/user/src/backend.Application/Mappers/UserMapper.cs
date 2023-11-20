using AutoMapper;
using backend.Application.Models;
using backend.DataAccess.Entities;
using System.Diagnostics.CodeAnalysis;

namespace backend.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            this.CreateMap<NewUserModel, User>();
            this.CreateMap<User, NewUserModel>();
            this.CreateMap<User, UserResponseModel>();
            this.CreateMap<User, UserIdResponseModel>();
            this.CreateMap<Admin, AdminResponseModel>()
                .ForMember(dest => dest.Id,
                    dest => dest.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Username,
                    dest => dest.MapFrom(src => src.User.Username));
            this.CreateMap<Admin, UserResponseModel>()
                .ForMember(dest => dest.Username, 
                    dest => dest.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Role, 
                    dest => dest.MapFrom(src => src.User.Role));
        }
    }
}
