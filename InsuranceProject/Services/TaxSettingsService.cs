using InsurancePolicy.DTOs;
using InsuranceProject.DTOs;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProject.Services
{
    public class TaxSettingsService:ITaxSettingsService
    {
        private readonly IRepository<TaxSettings> _repository;

        public TaxSettingsService(IRepository<TaxSettings> repository)

        {
            _repository = repository;
        }

        public List<TaxSettings> Get()
        {
            var tax = _repository.GetAll().ToList();
            return tax;
        }

        public Guid Add(TaxSettings tax)
        {
            _repository.Add(tax);
            return tax.TaxId;
        }

        public bool Update(TaxSettings tax)
        {
            var existingEmployee = _repository.GetAll().AsNoTracking().FirstOrDefault(u => u.TaxId == tax.TaxId);
            if (existingEmployee != null)
            {
                existingEmployee.TaxPercentage = tax.TaxPercentage;
                existingEmployee.UpdatedAt = DateTime.Now;
                existingEmployee.IsDeleted = false;
                _repository.Update(existingEmployee);
                return true;
            }
            return false;
        }

    }
}
