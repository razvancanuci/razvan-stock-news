using AutoMapper;
using backend.Application.Models;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;

namespace backend.Application.Services.Impl;

public class AnswerService :  IAnswerService
{
    private readonly IRepository<Answer> _repository;
    private readonly IMapper _mapper;

    public AnswerService(IRepository<Answer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Answer> AddAnswer(NewAnswerModel model)
    {
        var answer = await _repository.GetByGuidAsync(model.SubscriberId);
        
        if (answer != null)
        {
            return null;
        }

        var answerModel = await _repository.CreateAsync(_mapper.Map<Answer>(model));
        return answerModel;
    }
}