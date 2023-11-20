using AutoMapper;
using backend.Application.Mappers;
using backend.Application.Models;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.Application.Utilities;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;
using backend.DataAccess.Utilities;
using FluentAssertions;
using NSubstitute;

namespace backend.UnitTests.Tests
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Admin> _adminRepository;

        public UserServiceTests()
        {
            _userRepository = Substitute.For<IRepository<User>>();
            _adminRepository = Substitute.For<IRepository<Admin>>();

            var mapperConfig = new MapperConfiguration(x => x.AddMaps(typeof(UserMapper)));
            _mapper = new Mapper(mapperConfig);

            _userService = new UserService(_userRepository, _mapper, _adminRepository);
        }

        [Fact]
        public async void CreateUser_Should_Return_User()
        {
            // Arrange
            _userRepository.CreateAsync(Arg.Any<User>()).Returns(new User { Username = "vasivasi", Password = "vasivasi" });

            // Act
            var response = await _userService.CreateUser(new NewUserModel { Username = "vasivasi", Password = "vasivasi" });


            // Assert
            response.Should().NotBeNull();
            response.Username.Should().Be("vasivasi");
        }

        [Fact]
        public async void GetUserByUsername_Should_Return_A_Model()
        {
            // Arrange
            _userRepository.GetByAStringAsync("vasivasi").Returns(new User { Username = "vasivasi", Password = "vasivasi" });

            // Act
            var response = await _userService.GetUserByUserName("vasivasi");

            // Assert
            response.Should().NotBeNull();
            response.Username.Should().Be("vasivasi");
        }

        [Fact]
        public async void GetUsernameByUsernameAndPassword_Should_Return_A_Model()
        {
            // Arrange
            _userRepository.GetByStringAndPasswordAsync("vasivasi", "vasivasi").Returns(new User { Username = "vasivasi", Password = "vasivasi" });

            // Act
            var response = await _userService.GetUserByUsernameAndPassword("vasivasi", "vasivasi");

            // Assert
            response.Should().NotBeNull();
            response.Username.Should().Be("vasivasi");
        }

        [Fact]
        public async void DeleteUser_Should_Return_A_Model()
        {
            // Arrange
            _userRepository.DeleteByIdAsync(Guid.Parse("ebd132ab-85ca-4eda-aad2-2e2fadc51558")).Returns(new User { Username = "vasivasi", Password = "vasivasi" });

            // Act
            var response = await _userService.DeleteUser(Guid.Parse("ebd132ab-85ca-4eda-aad2-2e2fadc51558"));

            //Assert
            response.Should().NotBeNull();
            response.Username.Should().Be("vasivasi");
        }
        [Fact]
        public void Crypto_Works_As_Expected()
        {
            // Arrange
            var str = "parola";

            // Act
            var encrypted = Crypto.Encrypt(str);

            // Assert
            encrypted.Should().NotBeNull();
            encrypted.Should().NotBe(str);
            encrypted.Should().HaveLength(88);


        }

        [Fact]
        public void TokenCreator_Works_As_Expected()
        {
            // Arrange
            var securityKey = "100% secured I swear";
            var userModel = new UserResponseModel { Username = "Vasea", Role = "User" };

            // Act
            var token = TokenCreator.CreateToken(userModel, securityKey);

            // Assert
            token.Should().NotBeNull();
            token.Should().HaveLength(394);

        }

        [Fact]
        public async void GetAllUsers_Should_Return_Users()
        {
            // Arrange
            _userRepository.GetAllAsync().Returns(new[]
            {
                new User { Id = Guid.Parse("bb726692-f577-473a-97fc-70fddd4d9840"), Username = "vase", Password = "blase" },
                new User { Id = Guid.Parse("4dd1973f-9d0b-4311-8faa-d406daaf7664"), Username = "vaseee", Password = "blaseblase"}
            });
            
            // Act
            var users =  await _userService.GetAllUsers();
            
            // Assert
            users.Should().NotBeNull();
            users.Count().Should().Be(2);
        }



    }
}
