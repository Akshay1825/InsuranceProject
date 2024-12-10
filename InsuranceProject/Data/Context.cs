using InsuranceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Data
{
    public class Context:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Claimm> claims { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<InsurancePlan> Plans { get; set; }

        public DbSet<InsuranceScheme> InsuranceSchemes { get; set; }

        public DbSet<SchemeDetails> SchemeDetails { get; set; }

        public DbSet<Complaint> Complaints { get; set; }

        public DbSet<Installment> Installments { get; set; }    

        public DbSet<Nominee> Nominees { get; set; }

        public DbSet<Commission> Commissions { get; set; }

        public DbSet<InsuranceSettings> InsuranceSettings { get; set; }

        public DbSet<TaxSettings> Taxs { get; set; }

        public DbSet<PolicyType> PolicyTypes { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }
       
    }
}
