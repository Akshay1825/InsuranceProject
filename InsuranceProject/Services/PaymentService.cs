using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Exceptions;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InsuranceProject.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _repository;
        private readonly IRepository<Policy> _policyRepository;
        private readonly IRepository<InsuranceScheme> _schemeRepository;
        private readonly IRepository<Commission> _commissionRepository;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<Payment> repository,IRepository<Commission> commissionRepository,IMapper mapper,IRepository<InsuranceScheme> schemeRepository, IRepository<Policy> policyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _policyRepository = policyRepository;
            _schemeRepository = schemeRepository;
            _commissionRepository = commissionRepository;
        }
        public Guid Add(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            _repository.Add(payment);
            var insuranceScheme = _schemeRepository.GetAll().AsNoTracking().FirstOrDefault(x=>x.SchemeId==paymentDto.InsuranceSchemeId);
            var commissionPercent = insuranceScheme.InstallmentCommRatio;

            if (paymentDto.AgentId != null)
            {
                var commission = new Commission()
                {
                    CommissionId = new Guid(),
                    AgentId = paymentDto.AgentId,
                    Date = paymentDto.PaymentDate,
                    CommissionType = "Installment",
                    policyNumber = paymentDto.PolicyNumber,
                    PolicyId = paymentDto.PolicyId,
                    Amount = (paymentDto.Amount * (commissionPercent / 100)),
                    Status = 1
                };
                _commissionRepository.Add(commission);
            }
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

        public PageList<Payment> GetAll(DateFilter dateFilter)
        {
            var query = _repository.GetAll().AsNoTracking().ToList();

            if (dateFilter.FromDate.HasValue && dateFilter.ToDate.HasValue)
            {
                query = query.Where(p => p.PaymentDate >= dateFilter.FromDate.Value &&
                                         p.PaymentDate <= dateFilter.ToDate.Value).ToList();
            }

            if (query.Any())
            {
                return PageList<Payment>.ToPagedList(query, dateFilter.PageNumber, dateFilter.PageSize);
            }

            throw new DocumentNotFoundException("No data found");
        }
    }
}
