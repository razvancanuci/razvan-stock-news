using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess.Entities;
using FluentAssertions;

namespace backend.UnitTests.Tests;

public class TrainServiceTests
{
    private readonly ITrainService _trainService;

    public TrainServiceTests()
    {
        _trainService = new TrainService();
    }

    [Fact]
    public void PredictPrice_Should_NotReturnNullValue()
    {
        // Arrange
        var inputData = new List<ApiData>
        {
            new ApiData{ RegularMarketPrice = 1 },
            new ApiData { RegularMarketPrice = 2 },
            new ApiData {RegularMarketPrice = 3 }
        };
        var testData = new ApiDataModel { RegularMarketPrice = 1 };

        // Act
        var result = _trainService.PredictPrice(inputData, testData);

        // Assert
        result.Should().NotBeInRange(-1, 0);
    }
}