using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IRoleService
    {
        public List<RoleDto> GetAll();
        public RoleDto Get(Guid id);
        public Guid Add(RoleDto roleDto);
        public RoleDto Update(RoleDto roleDto);
        public bool Delete(RoleDto roleDto);
    }
}
