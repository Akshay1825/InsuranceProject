﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace InsuranceProject.Models
{
    public class Customer:BaseEntity
    {
        [Key]
        public Guid CustomerId { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "First name should not greater than 15")]
        public string LastName { get; set; }
        [Required]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email Should be at correct format")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public long MobileNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public string Address { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Nominee { get; set; }
        public string? NomineeRelation { get; set; }

        //public bool Status { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string? UserName { get; set; }

        public Agent? Agent { get; set; }
        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; }
        public List<Document>? Documents { get; set; }
        
        public List<Policy>? Policies { get; set; }

        public List<Complaint>? Complaints { get; set; }
    
    }

}
