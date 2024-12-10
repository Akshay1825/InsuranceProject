namespace InsuranceProject.Models
{
    public class BaseEntity
    {
        public Guid BaseId { get; set; } 
        public bool IsDeleted { get; set; } = false; 
    }
}
