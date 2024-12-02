using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IUserService
    {
        public List<UserDto> GetUsers();
        public User GetById(Guid id);
        public Guid AddUser(UserDto userDto);
        public bool DeleteUser(Guid id);
        public bool UpdateUser(UserDto userDto);
        public User FindUserByName(string userName);
    }
}
