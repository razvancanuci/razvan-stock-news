using AutoMapper;
using backend.Application.Models;
using backend.DataAccess.Entities;

namespace backend.Application.Mappers;

public class AnswerMapper: Profile
{
    public AnswerMapper()
    {
        CreateMap<NewAnswerModel, Answer>();
    }
}