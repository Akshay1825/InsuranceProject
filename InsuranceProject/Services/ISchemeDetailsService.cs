using InsuranceProject.DTOs;

namespace InsuranceProject.Services
{
    public interface ISchemeDetailsService
    {
        public Guid Add(SchemeDetailsDto schemeDetailsDto);
        public SchemeDetailsDto Get(Guid id);
        public List<SchemeDetailsDto> GetAll();
        public bool Update(SchemeDetailsDto schemeDetailsDto);
        public bool Delete(Guid id);
    }
}
