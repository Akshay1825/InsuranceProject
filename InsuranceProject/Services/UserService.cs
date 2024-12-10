using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        public UserService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Guid AddUser(UserDto userDto)
        {
            if (CheckUsername(userDto.UserName))
            {
                var user = _mapper.Map<User>(userDto);
                _repository.Add(user);
                return user.Id;
            }
            throw new UserNameExistsException("UserName Already Exists");
        }

        public bool CheckUsername(string username)
        {
            if (_repository.Any(u => u.UserName == username))
            {
                throw new UserNameExistsException("UserName already exists");
            }
            return true;
        }

        public bool DeleteUser(Guid id)
        {
            var user = _repository.Get(id);
            if (user != null)
            {
                _repository.Delete(user);
                return true;
            }
            return false;

        }

        public User GetById(Guid id)
        {
            return _repository.Get(id);
        }

        public List<UserDto> GetUsers()
        {
            var user = _repository.GetAll().ToList();
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(user);
            return userDtos;
        }

        public bool UpdateUser(UserDto userDto)
        {
            var existingUser = _repository.GetAll().AsNoTracking().Where(u => u.Id == userDto.Id);
            if (existingUser != null)
            {
                var user = _mapper.Map<User>(userDto);
                _repository.Update(user);
                return true;
            }
            return false;

        }

        public User FindUserByName(string userName)
        {
            return _repository.GetAll().Include(u => u.Role).Where(u => u.UserName == userName).FirstOrDefault();
        }
    }
}
