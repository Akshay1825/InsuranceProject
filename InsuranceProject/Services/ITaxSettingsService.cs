using InsurancePolicy.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface ITaxSettingsService
    {
        public List<TaxSettings> Get();

        public Guid Add(TaxSettings taxSettings);


        public bool Update(TaxSettings tax);
    }
}
