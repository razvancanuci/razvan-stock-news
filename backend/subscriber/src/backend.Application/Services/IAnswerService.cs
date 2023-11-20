using backend.Application.Models;
using backend.DataAccess.Entities;

namespace backend.Application.Services;

public interface IAnswerService
{
    Task<Answer> AddAnswer(NewAnswerModel model);
}