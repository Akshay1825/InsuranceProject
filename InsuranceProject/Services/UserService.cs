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
        public Guid Add(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _repository.Add(user);
            return user.UserId;
        }

        public bool Delete(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var existingUser = _repository.GetById(user.UserId);
            if (existingUser != null)
            {
                _repository.Delete(existingUser);
                return true;
            }
            return false;
        }

        public UserDto Get(Guid id)
        {
            var user = _repository.GetById(id);
            if (user == null)
            {
                throw new UserNotFoundException("User Not Found");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public List<UserDto> GetAll()
        {
            var users = _repository.GetAll().ToList();
            List<UserDto> result = _mapper.Map<List<UserDto>>(users);
            return result;
        }

        public UserDto Update(UserDto userDto)
        {
            var existingUser = _mapper.Map<User>(userDto);
            var updatedUser = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.UserId == existingUser.UserId);
            if (updatedUser != null)
            {
                _repository.Update(updatedUser);
            }
            var updatedUserDto = _mapper.Map<UserDto>(updatedUser);
            return updatedUserDto;
        }
    }
}
