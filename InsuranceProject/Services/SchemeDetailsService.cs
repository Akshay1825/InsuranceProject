using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Models;
using InsuranceProject.Repositories;

namespace InsuranceProject.Services
{
    public class SchemeDetailsService : ISchemeDetailsService
    {
        private readonly IRepository<SchemeDetails> _repository;
        private readonly IMapper _mapper;

        public SchemeDetailsService(IRepository<SchemeDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Add(SchemeDetailsDto schemeDetailsDto)
        {
            var schemeDetails = _mapper.Map<SchemeDetails>(schemeDetailsDto);
            _repository.Add(schemeDetails);
            return schemeDetails.DetailId;
        }

        public bool Delete(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                _repository.Delete(policy);
                return true;
            }
            return false;
        }

        public SchemeDetailsDto Get(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                var policydto = _mapper.Map<SchemeDetailsDto>(policy);
                return policydto;
            }
            throw new Exception("No such Scheme details exist");
        }

        public List<SchemeDetailsDto> GetAll()
        {
            var policies = _repository.GetAll().ToList();
            var policydtos = _mapper.Map<List<SchemeDetailsDto>>(policies);
            return policydtos;
        }

        public bool Update(SchemeDetailsDto schemeDetailsDto)
        {
            var existingPolicy = _repository.Get(schemeDetailsDto.DetailId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<SchemeDetails>(schemeDetailsDto);
                _repository.Update(policy);
                return true;
            }
            return false;
        }
    }
}
