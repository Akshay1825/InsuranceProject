using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface ILoginService
    {
        public string GetUser(LoginDto loginDto);
        public User FindByUserName(string userName);
        public string FindUser(string role, Guid id, ref string token);
    }
}
