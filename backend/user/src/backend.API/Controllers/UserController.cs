using backend.API.Models;
using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Utilities;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IPublishEndpoint _publishEndpoint;
        public UserController(IUserService userService, IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _userService = userService;
            _configuration = configuration;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(ApiResponse<IEnumerable<UserIdResponseModel>>.Success(users));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUserAsync(NewUserModel model)
        {
            if (await _userService.GetUserByUserName(model.Username) != null)
            {
                return BadRequest(ApiResponse<string>.Fail(new[] { new ValidationError(null, "The username is already taken.") }));
            }
            await _userService.CreateUser(model);

            return Created("/api/User", ApiResponse<string>.Success(model.Username));
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(ApiResponse<string>.Fail(new[] { new ValidationError(null, "The username is not registered") }));
            }
            if (user.Role == "Admin")
            {
                return BadRequest(ApiResponse<string>.Fail(new[] { new ValidationError(null, "An admin can't be removed") }));
            }
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var userLogin = await _userService.GetUserByUsernameAndPassword(model.Username, model.Password);
            if (userLogin == null)
            {
                return NotFound(ApiResponse<string>.Fail(new[] { new ValidationError(null, "Username or password are wrong") }));
            }

            if (userLogin.Role == "Admin")
            {
                var userIdModel = await _userService.GetAdminByUsername(userLogin.Username);
                var code = await _userService.GenerateTwoFactorCode(userIdModel.Id);
                var tkn = TokenCreator.Create2FaToken(userIdModel,
                    _configuration.GetSection("AppSettings:Token").Value);
                var tknModel = new TokenResponseModel { Token = tkn, Role = "2FA" };

                var emailModel = MessagePublisher.PublishEmailAdmin(code, userIdModel.Email, userIdModel.Username);
                
                await _publishEndpoint.Publish(emailModel);

                return Ok(ApiResponse<TokenResponseModel>.Success(tknModel));

            }
            var token = TokenCreator.CreateToken(userLogin, _configuration.GetSection("AppSettings:Token").Value);
            var tokenModel = new TokenResponseModel { Token = token, Role = "User" };
            var tokenMessage = MessagePublisher.Publish(token);
            await _publishEndpoint.Publish(tokenMessage);

            return Ok(ApiResponse<TokenResponseModel>.Success(tokenModel));
        }

        [HttpPost("2FA"), Authorize(Roles = "2FA")]
        public async Task<IActionResult> TwoFactorLoginAdmin(TwoFactorModel model)
        {
            var userLoggedInClaims = HttpContext.User.FindAll(claim => claim.Type == ClaimTypes.Name || claim.Type == ClaimTypes.Role || claim.Type == "Id");
            var userId = Guid.Parse(userLoggedInClaims.Single(x => x.Type == "Id").Value);

            var adminLogin = await _userService.GetAdminByCodeAndId(userId, model.Code);
            if (adminLogin == null)
            {
                return NotFound(ApiResponse<string>.Fail(new[] { new ValidationError(null, "Incorrect 2FA code") }));
            }
            var token = TokenCreator.CreateToken(adminLogin, _configuration.GetSection("AppSettings:Token").Value);
            var tokenModel = new TokenResponseModel { Token = token, Role = "Admin" };
            var tokenMessage = MessagePublisher.Publish(token);
            await _publishEndpoint.Publish(tokenMessage);

            return Ok(ApiResponse<TokenResponseModel>.Success(tokenModel));
        }

        [HttpGet("me"), Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userLoggedInClaims = HttpContext.User.FindAll(claim => claim.Type == ClaimTypes.Name || claim.Type == ClaimTypes.Role);
            return Ok(ApiResponse<UserResponseModel>.Success(new UserResponseModel
            {
                Username = userLoggedInClaims.Single(x => x.Type == ClaimTypes.Name).Value,
                Role = userLoggedInClaims.Single(x => x.Type == ClaimTypes.Role).Value
            }));


        }
    }
}
