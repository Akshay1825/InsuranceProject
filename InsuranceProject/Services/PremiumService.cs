using AutoMapper;
using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;
using InsuranceProject.Repositories;
using InsuranceProject.Types;

namespace InsuranceProject.Services
{
    public class PremiumService:IPremiumService
    {
        //private readonly IRepository<Payment> _paymentRepository;
        //private readonly IRepository<Premium> _premiumRepository;
        //private readonly IRepository<Commission> _commissionRepository;
        //private readonly IRepository<Policy> _policyRepository;
        //private readonly IRepository<Agent> _agentRepository;
        //private readonly IRepository<InsuranceScheme> _insuranceSchemeRepository;
        //private readonly IMapper _mapper;

        //public PremiumService(IRepository<Payment> paymentRepository, IRepository<Premium> premiumRepository, IRepository<Commission> commissionRepository, IRepository<Policy> policyRepository,IRepository<InsuranceScheme> insuranceSchemeRepository, IRepository<Agent> agentRepository, IMapper mapper)
        //{
        //    _paymentRepository = paymentRepository;
        //    _premiumRepository = premiumRepository;
        //    _commissionRepository = commissionRepository;
        //    _policyRepository = policyRepository;
        //    _agentRepository = agentRepository;
        //    _insuranceSchemeRepository = insuranceSchemeRepository;
        //    _mapper = mapper;
        //}

        //public PaymentDto PayPremium(Guid premiumId)
        //{
        //    var premium = _premiumRepository.Get(premiumId);

        //    if (premium == null || premium.Status == "Paid")
        //        return new PaymentDto { Status = "UnPaid", Amount = premium.Amount };

        //    // Save payment details
        //    var payment = new Payment
        //    {
        //        PaymentId = Guid.NewGuid(),
        //        PremiumId = premiumId,
        //        Amount = premium.Amount,
        //        PaymentDate = DateTime.UtcNow,
        //        Status = "Paid"
        //    };
        //    _paymentRepository.Add(payment);

        //    premium.Status = "Paid";
        //    premium.PaymentDate = DateTime.UtcNow;
        //    _premiumRepository.Update(premium);

        //    var policy = _policyRepository.Get(premium.PolicyId);
        //    if (premium.AgentId != null)
        //    {
                //var agent = _agentRepository.Get((Guid)premium.AgentId);
                //var premiumCommission =  * premium.Amount / 100.0;
                //var agentCommission = new Commission
                //{
                //    AgentId = (Guid)premium.AgentId,
                //    PolicyId = premium.PolicyId,
                //    CommissionAmount = premiumCommission,
                //    EarnedDate = DateTime.UtcNow,
                //    CommissionType = CommissionType.PREMIUM
                //};
                //agent.CurrentCommisionBalance += agentCommission.CommissionAmount;
                //agent.TotalCommissionEarned += agentCommission.CommissionAmount;

                //_commissionRepository.Add(agentCommission);
        //    }
        //    return new PaymentDto { Amount = premium.Amount, Status = "Paid" };
        //}

        //public List<PremiumDto> GetPremiumStatuses(Guid policyId)
        //{
        //    return _premiumRepository.GetAll()
        //        .Where(p => p.PolicyId == policyId)
        //        .Select(p => new PremiumDto
        //        {
        //            PremiumId = p.PremiumId,
        //            DueDate = p.DueDate,
        //            Amount = p.Amount,
        //            Status = p.Status,
        //            PaymentDate = p.PaymentDate
        //        }).ToList();
        //}

        //public PageList<PremiumDto> GetPremiumByPolicyAccount(Guid id, PageParameter pageParameter)
        //{
        //    var account = _policyRepository.Get(id);
        //    var premiums = _premiumRepository.GetAll().Where(a => a.CustomerId == account.CustomerId).Where(a => a.PolicyId == account.PolicyID).ToList();

        //    var premiumDto = _mapper.Map<List<PremiumDto>>(premiums);
        //    return PageList<PremiumDto>.ToPagedList(premiumDto, pageParameter.PageNumber, pageParameter.PageSize); ;
        //}

    }
}
