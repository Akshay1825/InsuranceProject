﻿using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Models
{
    public class State:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; }
    }
}
