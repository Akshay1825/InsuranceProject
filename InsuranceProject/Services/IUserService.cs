using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IUserService
    {
        public List<UserDto> GetAll();
        public UserDto Get(Guid id);
        public Guid Add(UserDto userDto);
        public UserDto Update(UserDto userDto);
        public bool Delete(UserDto userDto);
    }
}
