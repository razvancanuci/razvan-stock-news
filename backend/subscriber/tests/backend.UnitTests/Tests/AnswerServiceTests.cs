using AutoMapper;
using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;
using FluentAssertions;
using NSubstitute;

namespace backend.UnitTests.Tests;

public class AnswerServiceTests
{
    private readonly IRepository<Answer> _repository;
    private readonly IAnswerService _answerService;
    
    public AnswerServiceTests()
    {
        _repository = Substitute.For<IRepository<Answer>>();
        var mapper = Substitute.For<IMapper>();
        _answerService = new AnswerService(_repository, mapper);
    }

    [Fact]
    public async Task AddAnswer_Returns_Null()
    {
       // Arrange
       var answer = new Answer();
       _repository.GetByGuidAsync(Arg.Any<Guid>()).Returns(answer);
       
       // Act
       var result = await _answerService.AddAnswer(new NewAnswerModel());
       
       // Assert
       result.Should().BeNull();
    }
    
    [Fact]
    public async Task AddAnswer_Returns_AnAnswer()
    {
        // Arrange
        var answer = new Answer();
        _repository.CreateAsync(Arg.Any<Answer>()).Returns(answer);
       
        // Act
        var result = await _answerService.AddAnswer(new NewAnswerModel());
       
        // Assert
        result.Should().BeOfType<Answer>();
    }
}