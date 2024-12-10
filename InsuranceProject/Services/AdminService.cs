using AutoMapper;
using InsuranceProject.Data;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private Guid _roleId = new Guid("8c1bc05b-21a8-4b86-50bc-08dd115dd6c7");

        public AdminService(IRepository<Admin> repository, IMapper mapper, IRepository<User> userRepository, IRepository<Role> roleRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public Guid Add(AdminRegisterDto adminRgisterDto)
        {
            var user = new User()
            {
                UserName = adminRgisterDto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminRgisterDto.Password),
                RoleId = _roleId,
                Status = true
            };
            _userRepository.Add(user);

            var role = _roleRepository.Get(_roleId);

            if (role.Users == null)
            {
                role.Users = new List<User>();
            }
            role.Users.Add(user);
            adminRgisterDto.UserId = user.Id;

            var admin = _mapper.Map<Admin>(adminRgisterDto);
            _repository.Add(admin);
            return admin.Id;
        }

        public bool Delete(Guid id)
        {
            var admin = _repository.Get(id);
            if (admin != null)
            {
                _repository.Delete(admin);
                return true;
            }
            return false;
        }

        public AdminDto Get(Guid id)
        {
            var admin = _repository.Get(id);
            if (admin != null)
            {
                var adminDto = _mapper.Map<AdminDto>(admin);
                return adminDto;
            }
            throw new Exception("No such admin exist");
        }

        public List<AdminDto> GetAll()
        {
            var admins = _repository.GetAll();
            var adminDtos = _mapper.Map<List<AdminDto>>(admins);
            return adminDtos;
        }

        public bool Update(AdminDto adminDto)
        {
            var existingAdmin = _repository.GetAll().AsNoTracking().Where(u => u.Id == adminDto.Id);
            if (existingAdmin != null)
            {
                var admin = _mapper.Map<Admin>(adminDto);
                _repository.Update(admin);
                return true;
            }
            return false;
        }

        public Admin GetByUserName(string userName)
        {
            var admin = _repository.GetAll().AsNoTracking().FirstOrDefault(u => u.UserName == userName);
            return admin;
        }

        public bool ChangePassword(ChangePasswordDto passwordDto)
        {
            var customer = _repository.GetAll().AsNoTracking().Include(a => a.User).Where(a => a.User.UserName == passwordDto.UserName).FirstOrDefault();
            if (customer != null)
            {
                if (BCrypt.Net.BCrypt.Verify(passwordDto.Password, customer.User.PasswordHash))
                {
                    customer.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordDto.NewPassword);
                    _repository.Update(customer);
                    return true;
                }
            }
            return false;
        }
    }
}
