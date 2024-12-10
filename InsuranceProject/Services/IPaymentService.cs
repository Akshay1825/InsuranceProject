using InsuranceProject.DTOs;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IPaymentService
    {
        public Guid Add(PaymentDto paymentDto);
        public PaymentDto Get(Guid id);
        public List<PaymentDto> GetAll();
        public bool Update(PaymentDto paymentDto);
        public bool Delete(Guid id);

        public Payment GetID(int index, Guid policyId);
    }
}
