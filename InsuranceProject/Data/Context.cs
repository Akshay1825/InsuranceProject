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

        public Context(DbContextOptions<Context> options) : base(options) { }
       
    }
}
