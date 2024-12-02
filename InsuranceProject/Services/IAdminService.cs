using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IAdminService
    {
        public Guid Add(AdminRegisterDto adminRgisterDto);
        public bool Update(AdminDto adminDto);
        public List<AdminDto> GetAll();
        public AdminDto Get(Guid id);
        public bool Delete(Guid id);
    }
}
