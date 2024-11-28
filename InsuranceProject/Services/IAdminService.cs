using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface IAdminService
    {
        public List<AdminDto> GetAll();
        public AdminDto Get(Guid id);
        public Guid Add(AdminDto adminDto);
        public AdminDto Update(AdminDto adminDto);
        public bool Delete(AdminDto adminDto);
    }
}
