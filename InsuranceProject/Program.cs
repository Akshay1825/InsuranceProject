
using InsuranceProject.Data;
using InsuranceProject.Exceptions;
using InsuranceProject.Mappers;
using InsuranceProject.Repositories;
using InsuranceProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace InsuranceProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("*");

                });
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Key").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IAgentService, AgentService>();
            builder.Services.AddTransient<IDocumentService, DocumentService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IPolicyService, PolicyService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<IClaimService, ClaimService>();
            builder.Services.AddTransient<IPaymentService, PaymentService>();
            builder.Services.AddTransient<IInsuranceScheme, InsuranceSchemeService>();
            builder.Services.AddTransient<IInsurancePlanService, InsurancePlanService>();
            builder.Services.AddTransient<IComplaintService, ComplaintService>();
            builder.Services.AddTransient<ISchemeDetailsService, SchemeDetailsService>();
            builder.Services.AddTransient<ICloudinaryService, CloudinaryService>();
            builder.Services.AddTransient<ITaxSettingsService, TaxSettingsService>();
            builder.Services.AddTransient<ICommissionService, CommissionService>();

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddSingleton<CloudinaryService>();
            builder.Services.AddExceptionHandler<ExceptionHandler>();
            //builder.Services.AddSession();
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseSession();
            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            app.UseCors("AllowAngularApp");
            app.UseAuthorization();
            app.UseExceptionHandler(_ => { });
            
            app.MapControllers();
            app.Run();
        }
    }
}
