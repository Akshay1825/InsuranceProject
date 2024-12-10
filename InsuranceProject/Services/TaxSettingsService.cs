using InsurancePolicy.DTOs;
using InsuranceProject.Models;
using InsuranceProject.Repositories;

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
        
    }
}
