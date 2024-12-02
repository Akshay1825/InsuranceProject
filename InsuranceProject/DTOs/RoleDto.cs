using InsuranceProject.Models;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.DTOs
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public int TotalUser { get; set; }
    }
}
