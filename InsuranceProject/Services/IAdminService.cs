using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IAdminService
    {
        public Guid Add(AdminRegisterDto adminRgisterDto);
        public bool Update(AdminDto adminDto);
        public List<AdminDto> GetAll();
        public AdminDto Get(Guid id);
        public bool Delete(Guid id);

        public Admin GetByUserName(string userName);

        public bool ChangePassword(ChangePasswordDto passwordDto);
    }
}
