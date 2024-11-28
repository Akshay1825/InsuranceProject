using AutoMapper;
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

        public AdminService(IRepository<Admin> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Guid Add(AdminDto adminDto)
        {
            var admin = _mapper.Map<Admin>(adminDto);
            _repository.Add(admin);
            return admin.Id;
        }

        public bool Delete(AdminDto adminDto)
        {
            var admin = _mapper.Map<Admin>(adminDto);
            var existingAdmin = _repository.GetById(admin.Id);
            if (existingAdmin != null)
            {
                _repository.Delete(existingAdmin);
                return true;
            }
            return false;
        }

        public AdminDto Get(Guid id)
        {
            var admin = _repository.GetById(id);
            if (admin == null)
            {
                throw new AdminNotFoundException("Admin Not Found");
            }
            var adminDto = _mapper.Map<AdminDto>(admin);
            return adminDto;
        }

        public List<AdminDto> GetAll()
        {
            var admins = _repository.GetAll().ToList();
            List<AdminDto> result = _mapper.Map<List<AdminDto>>(admins);
            return result;
        }

        public AdminDto Update(AdminDto adminDto)
        {
            var existingAdmin = _mapper.Map<Admin>(adminDto);
            var updatedAdmin = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.Id == existingAdmin.Id);
            if (updatedAdmin != null)
            {
                _repository.Update(updatedAdmin);
            }
            var updatedAdminDto = _mapper.Map<AdminDto>(updatedAdmin);
            return updatedAdminDto;
        }
    }
}
