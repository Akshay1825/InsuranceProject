﻿using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly IRepository<Role> _repositoryRole;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private Guid _roleId = new Guid("9d9c16e3-8eb7-4e8c-50bd-08dd115dd6c7");
        public EmployeeService(IRepository<Employee> employeeRepository, IMapper mapper, IRepository<Role> repositoryRole, IRepository<User> userRepository)
        {
            _repository = employeeRepository;
            _mapper = mapper;
            _repositoryRole = repositoryRole;
            _userRepository = userRepository;
        }
        public Guid AddEmployee(EmployeeRegisterDto employeeRegisterDto)
        {
            var user = new User()
            {
                UserName=employeeRegisterDto.UserName,
                PasswordHash=BCrypt.Net.BCrypt.HashPassword(employeeRegisterDto.Password),
                Status = true,
                RoleId = _roleId
            };
            _userRepository.Add(user);

            var role = _repositoryRole.Get(_roleId);
            role.Users.Add(user);

            employeeRegisterDto.UserId = user.Id;

            var employee = _mapper.Map<Employee>(employeeRegisterDto);
            _repository.Add(employee);
            return employee.Id;
        }

        public bool DeleteEmployee(Guid id)
        {
            var employee = _repository.Get(id);
            if (employee != null)
            {
                _repository.Delete(employee);
                return true;
            }
            return false;
        }

        public Employee GetById(Guid id)
        {
            return _repository.Get(id);
        }

        public PagedResult<EmployeeDto> GetAll(FilterParameter filterParameter)
        {
            var query = _repository.GetAll().AsNoTracking();
            int totalCount = query.Count();
            var pagedCustomers = query
            .Skip((filterParameter.PageNumber - 1) * filterParameter.PageSize)
            .Take(filterParameter.PageSize)
                .ToList();

            var customerDtos = _mapper.Map<List<EmployeeDto>>(pagedCustomers);
            var pagedResult = new PagedResult<EmployeeDto>
            {
                Items = customerDtos,
                TotalCount = totalCount,
                PageSize = filterParameter.PageSize,
                CurrentPage = filterParameter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasNext = filterParameter.PageNumber < (int)Math.Ceiling(totalCount / (double)filterParameter.PageSize),
                HasPrevious = filterParameter.PageNumber > 1
            };

            return pagedResult;
        }

        public bool UpdateEmployee(EmployeeDto employeeDto)
        {
            var existingEmployee = _repository.GetAll().AsNoTracking().Where(u => u.Id == employeeDto.Id);
            if (existingEmployee != null)
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                _repository.Update(employee);
                return true;
            }
            return false;
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
