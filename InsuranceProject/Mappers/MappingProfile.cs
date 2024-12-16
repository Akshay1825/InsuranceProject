using AutoMapper;
using InsurancePolicy.DTOs;
using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleDto>().ForMember(dest => dest.TotalUser, val => val.MapFrom(src => src.Users.Count));
            CreateMap<RoleDto, Role>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Employee, EmployeeDto>()
                     .ForMember(dest => dest.TotalCustomers, val => val.MapFrom(src => src.Customers.Count));

            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeRegisterDto>().ForMember(dest => dest.UserId, val => val.MapFrom(src => src.UserId));
            CreateMap<EmployeeRegisterDto, Employee>();

            CreateMap<Customer, CustomerDto>();
                

            CreateMap<Customer, CustomerRegistrationDto>();
            CreateMap<CustomerRegistrationDto, Customer>();

            CreateMap<CustomerDto, Customer>();

            CreateMap<Admin, AdminDto>()
                .ForMember(dest => dest.TotalEmployees, val => val.MapFrom(src => src.Employees.Count))
                .ForMember(dest => dest.TotalCustomers, val => val.MapFrom(src => src.Customers.Count))
                .ForMember(dest => dest.TotalAgents, val => val.MapFrom(src => src.Agents.Count))
                .ForMember(dest => dest.TotalPolicies, val => val.MapFrom(src => src.Policies.Count));
            CreateMap<AdminDto, Admin>();

            CreateMap<Agent, AgentDto>().ForMember(dest => dest.TotalCustomers, val => val.MapFrom(src => src.Customers.Count));
            CreateMap<AgentDto, Agent>();
            CreateMap<Agent, AgentRegisterDto>().ForMember(dest => dest.UserId, val => val.MapFrom(src => src.UserId));
            CreateMap<AgentRegisterDto, Agent>();

            CreateMap<AdminRegisterDto, Admin>();
            CreateMap<Admin, AdminRegisterDto>();

            CreateMap<Policy, PolicyDto>();
            CreateMap<PolicyDto, Policy>();

            CreateMap<Claimm, ClaimDto>().ForMember(dest => dest.PoliciesCount, val => val.MapFrom(src => src.Policies.Count));
            CreateMap<ClaimDto, Claimm>();

            CreateMap<Complaint, ComplaintDto>();
            CreateMap<ComplaintDto, Complaint>();

            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();

            CreateMap<InsurancePlan, InsurancePlanDto>();
            CreateMap<InsurancePlanDto, InsurancePlan>();

            CreateMap<InsuranceScheme, InsuranceSchemeDto>()
                .ForMember(dest => dest.Requireddocuments, opt =>
                opt.MapFrom(src => src.RequiredDocuments.Select(d => d.ToString()).ToList()));
            CreateMap<InsuranceSchemeDto, InsuranceScheme>();
                

            CreateMap<SchemeDetails, SchemeDetailsDto>();
            CreateMap<SchemeDetailsDto, SchemeDetails>();

            CreateMap<Nominee, NomineeRequestDto>();
            CreateMap<NomineeRequestDto, Nominee>();

            CreateMap<Commission, CommissionRequestDto>();
            CreateMap<CommissionRequestDto, Commission>();


        }
    }
}
