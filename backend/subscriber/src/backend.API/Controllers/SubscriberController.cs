using AutoMapper;
using backend.API.Models;
using backend.Application.Models;
using backend.Application.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IPublishEndpoint _publisher;
        private readonly IMapper _mapper;
        public SubscriberController(ISubscriberService subscriberService, IPublishEndpoint publisher, IMapper mapper)
        {
            _subscriberService = subscriberService;
            _publisher = publisher;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSubscriber(NewSubscriberModel model)
        {
            if (await _subscriberService.GetSubscriberByEmail(model.Email) != null)
            {
                return BadRequest(ApiResponse<string>.Fail(new[] { new ValidationError(null, "Email is already subscribed") }));
            }
            var subscriberResponse = await _subscriberService.AddSubscriber(model);

            var subscriberMessage = _mapper.Map<SubscriberAddedMessage>(subscriberResponse);
            await _publisher.Publish(subscriberMessage);

            return Created("/api/Subscriber", ApiResponse<string>.Success(subscriberResponse.Email));
        }

        [HttpDelete("{subscriberId}")]
        public async Task<IActionResult> DeleteSubscriberById(Guid subscriberId)
        {
            if (await _subscriberService.GetSubscriberById(subscriberId) == null)
            {
                return NotFound(ApiResponse<string>.Fail(new[] { new ValidationError(null, "Id not found") }));
            }
            await _subscriberService.DeleteSubscriberById(subscriberId);
            var subscriberDeletedMessage = MessagePublisher.PublishDelete(subscriberId);

            await _publisher.Publish(subscriberDeletedMessage);

            return NoContent();
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSubscribers()
        {
            var subscribers = await _subscriberService.GetSubscribers();
            return Ok(ApiResponse<IEnumerable<SubscriberResponseModel>>.Success(subscribers));
        }

        [HttpGet("statistics"), Authorize(Roles="User,Admin")]
        public async Task<IActionResult> GetSubscribersStats()
        {
            return Ok(ApiResponse<SubscriberStatsModel>.Success(await _subscriberService.GetSubscriberStats()));
        }

        [HttpGet("{subscriberId}")]
        public async Task<IActionResult> GetEmailById(Guid subscriberId)
        {
            var subscriber = await _subscriberService.GetSubscriberById(subscriberId);

            if (subscriber == null)
            {
                return NotFound(ApiResponse<string>.Fail(new[] { new ValidationError(null, "Id not found") }));
            }
            return Ok(ApiResponse<SubscriberEmailResponseModel>.Success(subscriber));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubscriberByEmail(string subscriberEmail)
        {
            var subscriber = await _subscriberService.GetSubscriberByEmail(subscriberEmail);
            if (subscriber == null)
            {
                return NotFound(ApiResponse<string>.Fail(new[] { new ValidationError(null, "Email not found") }));
            }
            await _subscriberService.DeleteSusbcriberByEmail(subscriberEmail);

            var subscriberDeletedMessage = MessagePublisher.PublishDelete(subscriber.Id);
            await _publisher.Publish(subscriberDeletedMessage);

            return NoContent();

        }
    }
}
