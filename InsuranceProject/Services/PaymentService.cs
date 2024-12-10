using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Models;
using InsuranceProject.Repositories;

namespace InsuranceProject.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _repository;
        private readonly IRepository<Policy> _policyRepository;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<Payment> repository,IMapper mapper, IRepository<Policy> policyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _policyRepository = policyRepository;
        }
        public Guid Add(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            _repository.Add(payment);
            return payment.PaymentId;
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

        public PaymentDto Get(Guid id)
        {
            var policy = _repository.Get(id);
            if (policy != null)
            {
                var policydto = _mapper.Map<PaymentDto>(policy);
                return policydto;
            }
            throw new Exception("No such Payment exist");
        }

        public List<PaymentDto> GetAll()
        {
            var policies = _repository.GetAll().ToList();
            var policydtos = _mapper.Map<List<PaymentDto>>(policies);
            return policydtos;
        }

        public bool Update(PaymentDto paymentDto)
        {
            var existingPolicy = _repository.Get(paymentDto.PaymentId);
            if (existingPolicy != null)
            {
                var policy = _mapper.Map<Payment>(paymentDto);
                _repository.Update(policy);
                return true;
            }
            return false;
        }

        public Payment GetID(int index,Guid policyId)
        {
            var payments = _repository.GetAll().Where(x=>x.indexId==index)
                .ToList();
            if (payments==null || payments.Count==0 )
            {
                throw new PaymentNotFoundException("Payment Not Found");
            }
            var payment = payments.FirstOrDefault(x=>x.PolicyId==policyId);
            return payment;
        }
    }
}
