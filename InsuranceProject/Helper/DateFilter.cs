namespace InsuranceProject.Helper
{
    public class DateFilter:PageParameter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
