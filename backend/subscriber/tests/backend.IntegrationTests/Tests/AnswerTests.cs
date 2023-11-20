using System.Net;
using System.Net.Http.Json;
using backend.DataAccess.Entities;
using backend.IntegrationTests.Common;
using FluentAssertions;

namespace backend.IntegrationTests.Tests;

public class AnswerTests
{
    private readonly HttpClient _httpClient;
    
    public AnswerTests()
    {
        _httpClient = new CustomWebApplicationFactory<Program>().CreateClient();
    }

    [Fact]
    public async Task AddAnswer_Should_Return_Status_Created()
    {
        // Arrange
        var answer = new Answer { SubscriberId = Guid.Parse("0350d100-c8a4-41e9-aa25-6e7b22fab2c3"), OccQuestion = "Investor", AgeQuestion = 23 };
        
        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Answer", answer);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task AddAnswer_Should_Return_Status_BadRequest()
    {
        // Arrange
        var answer = new Answer { SubscriberId = Guid.Parse("318246ea-c33e-42f4-8f36-2b29d1e1981d"), OccQuestion = "Unemployed", AgeQuestion = 55 };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Answer", answer);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}