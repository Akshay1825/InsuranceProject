using Azure;
using InsuranceProject.DTOs;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InsuranceProject.Services
{
    public class LoginService:ILoginService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Admin> _adminRepo;
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IRepository<Agent> _agentRepo;
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<Role> _roleRepo;
        private readonly IConfiguration _configuration;

        public LoginService(IRepository<User> userRepo, IRepository<Admin> adminRepo, IRepository<Employee> employeeRepo,
            IRepository<Agent> agentRepo, IRepository<Customer> customerRepo,IRepository<Role> roleRepo, IConfiguration configuration)
        {
            _adminRepo = adminRepo;
            _userRepo = userRepo;
            _employeeRepo = employeeRepo;
            _agentRepo = agentRepo;
            _customerRepo = customerRepo;
            _roleRepo = roleRepo;
            _configuration = configuration;
        }

        public string GetUser(LoginDto loginDto)
        {
            var token = "";
            var existingUser = FindByUserName(loginDto.UserName);
            if (existingUser == null)
            {
                return null; 
            }
            var role = _roleRepo.Get(existingUser.RoleId);
            if (role == null)
            {
                return null; 
            }
            if (
                !BCrypt.Net.BCrypt.Verify(loginDto.Password, existingUser.PasswordHash))
            {
                return null;
            }
            var newToken = FindUser(role.RoleName.ToString(), existingUser.Id, ref token);
            return newToken;
        }
        public User FindByUserName(string userName)
        {
            return _userRepo.GetAll().Include(u => u.Role).Where(u => u.UserName == userName).FirstOrDefault();
        }
       
        public string FindUser(string role, Guid id, ref string token)
        {
            if (role == "ADMIN")
            {
                var admin = _adminRepo.GetAll().Where(u => u.UserId == id).FirstOrDefault();
                token = CreateTokenAdmin(role, admin);
                return token;
            }
            else if (role == "AGENT")
            {
                var agent = _agentRepo.GetAll().Where(u => u.UserId == id).FirstOrDefault();
                token = CreateTokenAgent(role, agent);
                return token;
            }
            else if (role == "CUSTOMER")
            {
                var customer = _customerRepo.GetAll().Where(u => u.UserId == id).FirstOrDefault();
                token = CreateTokenCustomer(role, customer);
                return token;
            }
            else if (role == "EMPLOYEE")
            {
                var agent = _employeeRepo.GetAll().Where(u => u.UserId == id).FirstOrDefault();
                token = CreateTokenEmployee(role, agent);
                return token;
            }
            return null;
        }

        private string CreateTokenAdmin(string role, Admin user)
        {

            List<Claim> claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private string CreateTokenEmployee(string role, Employee user)
        {

            List<Claim> claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private string CreateTokenAgent(string role, Agent user)
        {

            List<Claim> claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private string CreateTokenCustomer(string role, Customer user)
        {

            List<Claim> claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
