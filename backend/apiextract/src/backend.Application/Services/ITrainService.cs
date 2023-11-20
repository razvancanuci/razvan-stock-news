using backend.Application.Models;
using backend.DataAccess.Entities;

namespace backend.Application.Services;

public interface ITrainService
{
    double PredictPrice(IEnumerable<ApiData> apiSet, ApiDataModel apiModel);
}