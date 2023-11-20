using backend.API.Models;
using backend.Application.Models;
using backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AnswerController: ControllerBase
{
    private readonly IAnswerService _answerService;
    private readonly ISubscriberService _subscriberService;

    public AnswerController(IAnswerService answerService, ISubscriberService subscriberService)
    {
        _answerService = answerService;
        _subscriberService = subscriberService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAnswer(NewAnswerModel model)
    {
        if (await _subscriberService.GetSubscriberById(model.SubscriberId) == null)
        {
            return BadRequest(ApiResponse<string>.Fail(new[] { new ValidationError(null, "Subscriber not found") }));
        }

        var answer = await _answerService.AddAnswer(model);
        if (answer == null)
        {
            return BadRequest(
                ApiResponse<string>.Fail(new[] { new ValidationError(null, "Answer already registered") }));
        }

        return Created("/api/Answer", ApiResponse<string>.Success(answer.OccQuestion));
    }
}