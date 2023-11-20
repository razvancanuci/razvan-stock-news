using backend.Application.Models;
using backend.IntegrationTests.Common;
using backend.IntegrationTests.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TokenResponseModel = backend.IntegrationTests.Models.TokenResponseModel;

namespace backend.IntegrationTests.Tests
{
    public class UserTests
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        private UserContext _context;

        public UserTests()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async void User_Login_Should_Have_Status_OK()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Username = "tikitaka", Password = "Tiki1^2" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);

            var responseContent = await response.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Should().NotBeNull();
            responseContent.Result.Token.Length.Should().BeGreaterThanOrEqualTo(64);
        }

        [Fact]
        public async void User_Login_Should_Have_Status_BadRequest()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Username = "all", Password = "Aka2^2" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);

            var responseContent = await response.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseContent.Should().NotBeNull();
            responseContent.Errors.Count().Should().BeGreaterThanOrEqualTo(1);


        }

        [Fact]
        public async void Create_User_Should_Have_Status_Unauthorized()
        {
            // Arrange
            var newUserModel = new UserLoginModel { Username = "all", Password = "Aka2^2" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/User", newUserModel);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


        [Fact]
        public async void Create_User_Should_Have_Status_OK()
        {
            // Arrange
            var newUserModel = new NewUserModel { Username = "anduaaa", Password = "Aka2$221" };
            var userLoginModel = new UserLoginModel { Username = "vasivasi", Password = "Vasi1$2" };

            // Act
            var responseLogin = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);
            var token = (await responseLogin.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);
            
            using (var scope = _factory.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<UserContext>();
                
                var adminDb =
                    (await _context.Admins.Include(a => a.User).ToListAsync()).SingleOrDefault(a =>
                        a.User.Username == userLoginModel.Username);
                var twoFactorModel = new TwoFactorModel { Code = adminDb.TwoFactorCode };
                var response2FA = await _httpClient.PostAsJsonAsync("/api/User/2FA", twoFactorModel);
                var tokenLogin = (await response2FA.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenLogin.Token);
            
                var responseCreate = await _httpClient.PostAsJsonAsync("/api/User", newUserModel);

                var responseContent = (await responseCreate.Content.ReadFromJsonAsync<ApiResponse<string>>()).Result;
                
                // Assert
                responseCreate.StatusCode.Should().Be(HttpStatusCode.Created);
                responseContent.Should().NotBeNull();
                responseContent.Should().Be("anduaaa");
            }
            
        }

        [Fact]
        public async void Create_User_Should_Have_Status_Forbidden()
        {
            // Arrange
            var newUserModel = new NewUserModel { Username = "anduaaa", Password = "Aka2$221" };
            var userLoginModel = new UserLoginModel { Username = "tikitaka", Password = "Tiki1^2" };

            // Act
            var responseLogin = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);
            var token = (await responseLogin.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            var responseCreate = await _httpClient.PostAsJsonAsync("/api/User", newUserModel);

            // Assert
            responseCreate.StatusCode.Should().Be(HttpStatusCode.Forbidden);

        }

        [Fact]
        public async void Create_User_Should_Have_Status_BadRequest()
        {
            // Arrange
            var newUserModel = new NewUserModel { Username = "tikitaka", Password = "Aka2$221" };
            var userLoginModel = new UserLoginModel { Username = "vasivasi", Password = "Vasi1$2" };

            // Act
            var responseLogin = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);
            var token = (await responseLogin.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            using (var scope = _factory.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<UserContext>();

                var adminDb =
                    (await _context.Admins.Include(a => a.User).ToListAsync()).SingleOrDefault(a =>
                        a.User.Username == userLoginModel.Username);
                var twoFactorModel = new TwoFactorModel { Code = adminDb.TwoFactorCode };
                var response2FA = await _httpClient.PostAsJsonAsync("/api/User/2FA", twoFactorModel);
                var tokenLogin = (await response2FA.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>())
                    .Result;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", tokenLogin.Token);

                var responseCreate = await _httpClient.PostAsJsonAsync("/api/User", newUserModel);

                // Assert
                responseCreate.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            }
        }

        [Fact]
        public async void Delete_User_Should_Have_Status_OK()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Username = "vasivasi", Password = "Vasi1$2" };
            var id = Guid.Parse("318246ea-c33e-42f4-8f36-2b29d1e1981d");

            // Act
            var responseLogin = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);
            var token = (await responseLogin.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            using (var scope = _factory.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<UserContext>();

                var adminDb =
                    (await _context.Admins.Include(a => a.User).ToListAsync()).SingleOrDefault(a =>
                        a.User.Username == userLoginModel.Username);
                var twoFactorModel = new TwoFactorModel { Code = adminDb.TwoFactorCode };
                var response2FA = await _httpClient.PostAsJsonAsync("/api/User/2FA", twoFactorModel);
                var tokenLogin = (await response2FA.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>())
                    .Result;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", tokenLogin.Token);
                
                var responseDelete = await _httpClient.DeleteAsync($"/api/User/{id}");

                // Assert
                responseDelete.StatusCode.Should().Be(HttpStatusCode.NoContent);
            }
        }

        [Fact]
        public async void Delete_User_Should_Have_Status_NotFound()
        {
            // Arrange
            var id = Guid.Parse("548d5bde-867f-4cbf-823b-fcced5744b11");
            var userLoginModel = new UserLoginModel { Username = "vasivasi", Password = "Vasi1$2" };

            // Act
            var responseLogin = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);
            var token = (await responseLogin.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            using (var scope = _factory.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<UserContext>();

                var adminDb =
                    (await _context.Admins.Include(a => a.User).ToListAsync()).SingleOrDefault(a =>
                        a.User.Username == userLoginModel.Username);
                var twoFactorModel = new TwoFactorModel { Code = adminDb.TwoFactorCode };
                var response2FA = await _httpClient.PostAsJsonAsync("/api/User/2FA", twoFactorModel);
                var tokenLogin = (await response2FA.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>())
                    .Result;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", tokenLogin.Token);
                
                var responseDelete = await _httpClient.DeleteAsync($"/api/User/{id}");

                // Assert
                responseDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async void Delete_User_Should_Have_Status_Unauthorized()
        {
            // Arrange
            var id = Guid.Parse("548d5bde-867f-4cbf-823b-fcced5744b11");

            // Act
            var response = await _httpClient.DeleteAsync($"/api/User/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


        [Fact]
        public async void Delete_User_Should_Have_Status_Forbidden()
        {
            // Arrange
            var id = Guid.Parse("548d5bde-867f-4cbf-823b-fcced5744b11");
            var userLoginModel = new UserLoginModel { Username = "tikitaka", Password = "Tiki1^2" };

            // Act
            var responseLogin = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);
            var token = (await responseLogin.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            var responseDelete = await _httpClient.DeleteAsync($"/api/User/{id}");

            // Assert
            responseDelete.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async void GetMe_Should_Work_As_Expected()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Username = "tikitaka", Password = "Tiki1^2" };

            // Act
            var responseLogin = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);
            var token = (await responseLogin.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            var responseGet = await _httpClient.GetAsync($"/api/User/me");

            // Assert
            responseGet.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async void GetUsers_Should_Return_Status_Ok()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Username = "vasivasi", Password = "Vasi1$2" };

            // Act
            var responseLogin = await _httpClient.PostAsJsonAsync("/api/User/login", userLoginModel);
            var token = (await responseLogin.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>()).Result;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            using (var scope = _factory.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<UserContext>();

                var adminDb =
                    (await _context.Admins.Include(a => a.User).ToListAsync()).SingleOrDefault(a =>
                        a.User.Username == userLoginModel.Username);
                var twoFactorModel = new TwoFactorModel { Code = adminDb.TwoFactorCode };
                var response2FA = await _httpClient.PostAsJsonAsync("/api/User/2FA", twoFactorModel);
                var tokenLogin = (await response2FA.Content.ReadFromJsonAsync<ApiResponse<TokenResponseModel>>())
                    .Result;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", tokenLogin.Token);
                
                var responseGet = await _httpClient.GetAsync($"/api/User");
            
                // Assert
                responseGet.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}
