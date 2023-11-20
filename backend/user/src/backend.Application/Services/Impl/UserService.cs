using AutoMapper;
using backend.Application.Models;
using backend.DataAccess.Entities;
using backend.DataAccess.Repository;
using backend.DataAccess.Utilities;

namespace backend.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<Admin> _adminRepository;
        private readonly IMapper _mapper;
        public UserService(IRepository<User> repository, IMapper mapper, IRepository<Admin> adminRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _adminRepository = adminRepository;
        }
        public async Task<UserResponseModel> CreateUser(NewUserModel model)
        {
            model.Password = Crypto.Encrypt(model.Password);
            var user = _mapper.Map<User>(model);
            user.Role = "User";
            var userResponse = await _repository.CreateAsync(user);
            return _mapper.Map<UserResponseModel>(userResponse);
        }

        public async Task<UserResponseModel> DeleteUser(Guid id)
        {
            return _mapper.Map<UserResponseModel>(await _repository.DeleteByIdAsync(id));
        }

        public async Task<UserResponseModel> GetUserById(Guid id)
        {
            return _mapper.Map<UserResponseModel>(await _repository.GetByIdAsync(id));
;        }

        public async Task<IEnumerable<UserIdResponseModel>> GetAllUsers()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserIdResponseModel>>(users);
        }

        public async Task<UserResponseModel> GetUserByUserName(string username)
        {
            return _mapper.Map<UserResponseModel>(await _repository.GetByAStringAsync(username));
        }

        public async Task<UserResponseModel?> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _repository.GetByStringAndPasswordAsync(username, password);
            return _mapper.Map<UserResponseModel>(user);
        }
        
        public async Task<string> GenerateTwoFactorCode(Guid userId)
        {
            var code = Enumerable.Range(0, 6).Select(x => new Random().Next(0,10))
                .Aggregate("",(x,y)=> x+y);
            await _adminRepository.UpdateCode(userId, code);
            return code;
        }

        public async Task<AdminResponseModel> GetAdminByUsername(string username)
        {
            var admin = await _adminRepository.GetByAStringAsync(username);
            return _mapper.Map<Admin, AdminResponseModel>(admin);
        }

        public async Task<UserResponseModel> GetAdminByCodeAndId(Guid id, string code)
        {
            var admin = await _adminRepository.GetByGuidAndStringAsync(id, code);
            return _mapper.Map<Admin, UserResponseModel>(admin);
        }
    }
}
