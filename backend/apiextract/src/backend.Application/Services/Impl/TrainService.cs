using backend.Application.Models;
using backend.DataAccess.Entities;
using backend.DataAccess.Repositories;
using Microsoft.ML;
using MongoDB.Driver;

namespace backend.Application.Services.Impl;

public class TrainService : ITrainService
{
    public double PredictPrice(IEnumerable<ApiData> apiSet, ApiDataModel apiModel)
    {
        var mlContext = new MLContext();
        var inputSet = apiSet.Select(aset => new InputTrainModel { Value = (float)aset.RegularMarketPrice });
        
        var trainingData = inputSet.Zip(inputSet.Skip(1), (first, second) => (first, second)).ToList()
            .Select(train => new InputTrainModel {Value = train.first.Value, PredictionValue = train.second.Value});
        
        var dataView = mlContext.Data.LoadFromEnumerable(trainingData);
        
        var pipeline = mlContext.Transforms.Concatenate("Features", new[] {"Value"})
            .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "PredictionValue", maximumNumberOfIterations: 1000));
        
        var model = pipeline.Fit(dataView);
        
        var predictionEngine = mlContext.Model.CreatePredictionEngine<InputTrainModel, PredictionModel>(model);

        var nextValue = new InputTrainModel { Value = (float)apiModel.RegularMarketPrice };
        var prediction = predictionEngine.Predict(nextValue);

        double predictedValue = prediction.PredictionValue;

        return predictedValue;
    }

}