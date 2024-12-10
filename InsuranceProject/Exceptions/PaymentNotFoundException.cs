namespace InsuranceProject.Exceptions
{
    public class PaymentNotFoundException:Exception
    {
        public PaymentNotFoundException(string message):base(message) { }
    }
}
