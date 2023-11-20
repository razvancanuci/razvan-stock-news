using System.Net.Http.Json;
using AutoMapper;
using backend.Application.Models;
using backend.DataAccess.Entities;
using backend.DataAccess.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace backend.Application.Services.Impl;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMongoRepository _mongoRepository;
    private readonly ITrainService _trainService;
    private readonly IMapper _mapper;
    private readonly ILogger<ApiService> _logger;
    
    public ApiService(HttpClient httpClient,
        IConfiguration configuration,
        IPublishEndpoint publishEndpoint,
        IMongoRepository mongoRepository,
        ITrainService trainService,
        IMapper mapper,
        ILogger<ApiService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _publishEndpoint = publishEndpoint;
        _mongoRepository = mongoRepository;
        _trainService = trainService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task ExtractDataFromApi()
    {
        var httpResponse = await _httpClient.GetAsync(_configuration["Api"]);
        var httpContent = await httpResponse.Content.ReadFromJsonAsync<ApiQuoteResponseModel>();
        var dataRequested = httpContent.QuoteResponse;

        foreach (var data in dataRequested.Result)
        {
            var dbRequestData = await _mongoRepository.GetBySymbolAsync(data.Symbol);
            var dbData = _mapper.Map<ApiData>(data);
            await _mongoRepository.AddDataAsync(dbData);
            var pricePredicted = _trainService.PredictPrice(dbRequestData, data);
            data.PredictedPrice = pricePredicted;
            _logger.LogInformation("{symbol}  {pricePredicted}", data.Symbol, pricePredicted);
        }
        
        await _publishEndpoint.Publish(dataRequested);
        
    }
}