using AutoMapper;
using backend.Application.Models;
using backend.DataAccess.Entities;

namespace backend.Application.Mappers;

public class ApiDataMapper : Profile
{
    public ApiDataMapper()
    {
        CreateMap<ApiDataModel, ApiData>();
    }
}