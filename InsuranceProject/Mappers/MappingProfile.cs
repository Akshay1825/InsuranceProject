using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, EmployeeDto>();
            CreateMap<EmployeeDto, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Agent, AgentDto>();
            CreateMap<AgentDto, Agent>();

            CreateMap<Document, DocumentDto>();
            CreateMap<DocumentDto, Document>();

            CreateMap<Admin, AdminDto>();
            CreateMap<AdminDto, Admin>();

            CreateMap<Policy, PolicyDto>();
            CreateMap<PolicyDto, Policy>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
        }
    }
}
