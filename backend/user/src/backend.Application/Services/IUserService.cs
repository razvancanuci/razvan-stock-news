using backend.Application.Models;

namespace backend.Application.Services
{
    public interface IUserService
    {
        public Task<UserResponseModel> CreateUser(NewUserModel model);
        public Task<UserResponseModel> GetUserByUserName(string username);
        public Task<UserResponseModel?> GetUserByUsernameAndPassword(string username, string password);
        public Task<UserResponseModel> DeleteUser(Guid id);
        public Task<UserResponseModel> GetUserById(Guid id);

        public Task<IEnumerable<UserIdResponseModel>> GetAllUsers();
        public Task<string> GenerateTwoFactorCode(Guid userId);
        public Task<AdminResponseModel> GetAdminByUsername(string username);
        public Task<UserResponseModel> GetAdminByCodeAndId(Guid id, string code);
    }
}
