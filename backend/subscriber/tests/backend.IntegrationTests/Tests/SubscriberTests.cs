using backend.Application.Models;
using backend.IntegrationTests.Common;
using backend.IntegrationTests.Models;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace backend.IntegrationTests.Tests
{
    public class SubscriberTests
    {
        private readonly HttpClient _httpClient;

        public SubscriberTests()
        {
            _httpClient = new CustomWebApplicationFactory<Program>().CreateClient();
        }

        private string CreateToken()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                "my top secret key"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [Fact]
        public async void AddSubscriber_Return_Status_OK()
        {
            // Arrange
            var subscriberModel = new NewSubscriberModel { Name = "Vasile", Email = "vasi12@yahoo.com" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/Subscriber", subscriberModel);

            var responseContent = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.Created);
            responseContent.Result.Should().Be(subscriberModel.Email);
        }

        [Fact]
        public async void AddSubscriber_Return_Status_BadRequest()
        {
            // Arrange
            var subscriberModel = new NewSubscriberModel { Name = "Vasile1", Email = "vasi12@yahoo.com" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/Subscriber", subscriberModel);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void DeleteSubscriber_Return_Status_OK()
        {
            // Arrange
            var id = Guid.Parse("6ab34493-6146-4520-81f4-9899871d994d");

            // Act
            var response = await _httpClient.DeleteAsync($"/api/Subscriber/{id}");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        }

        [Fact]
        public async void DeleteSubscriber_Return_Status_NotFound()
        {
            // Arrange
            var id = Guid.Parse("f4925f37-71bd-49f4-8714-9f03d0c264b8");

            // Act
            var response = await _httpClient.DeleteAsync($"/api/Subscriber/{id}");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void GetSubscribers_Return_Status_OK()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", CreateToken());

            // Act
            var response = await _httpClient.GetAsync("/api/Subscriber");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);

        }

        [Fact]
        public async void GetStats_Return_Status_OK()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", CreateToken());

            // Act
            var response = await _httpClient.GetAsync("/api/Subscriber/statistics");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetEmailById_Return_Status_Ok()
        {
            // Arrange
            var id = Guid.Parse("318246ea-c33e-42f4-8f36-2b29d1e1981d");

            // Act
            var response = await _httpClient.GetAsync($"/api/Subscriber/{id}");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetEmailById_Return_Status_NotFound()
        {
            // Arrange
            var id = Guid.Parse("f4925f37-71bd-49f4-8714-9f03d0c264b2");

            // Act
            var response = await _httpClient.GetAsync($"/api/Subscriber/{id}");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void DeleteByEmail_Return_Status_NoContent()
        {
            // Arrange
            var email = "vasivasi@g.ro";

            // Act
            var response = await _httpClient.DeleteAsync($"/api/Subscriber?subscriberEmail={email}");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        }

        [Fact]
        public async void DeleteByEmail_Return_Status_NotFound()
        {
            // Arrange
            var email = "aaa@aa.a";

            // Act
            var response = await _httpClient.DeleteAsync($"/api/Subscriber?subscriberEmail={email}");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }

    }
}
