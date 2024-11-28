using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _repository;
        private readonly IMapper _mapper;

        public RoleService(IRepository<Role> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            _repository.Add(role);
            return role.RoleId;
        }

        public bool Delete(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            var existingRole = _repository.GetById(role.RoleId);
            if (existingRole != null)
            {
                _repository.Delete(existingRole);
                return true;
            }
            return false;
        }

        public RoleDto Get(Guid id)
        {
            var role = _repository.GetById(id);
            if (role == null)
            {
                throw new RoleNotFoundException("Role Not Found");
            }
            var roleDto = _mapper.Map<RoleDto>(role);
            return roleDto;
        }

        public List<RoleDto> GetAll()
        {
            var roles = _repository.GetAll().ToList();
            List<RoleDto> result = _mapper.Map<List<RoleDto>>(roles);
            return result;
        }

        public RoleDto Update(RoleDto roleDto)
        {
            var existingRole = _mapper.Map<Role>(roleDto);
            var updatedRole = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.RoleId == existingRole.RoleId);
            if (updatedRole != null)
            {
                _repository.Update(updatedRole);
            }
            var updatedRoleDto = _mapper.Map<RoleDto>(updatedRole);
            return updatedRoleDto;
        }
    }
}
