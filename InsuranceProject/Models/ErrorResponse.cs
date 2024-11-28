namespace InsuranceProject.Models
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }

        public string Title { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
