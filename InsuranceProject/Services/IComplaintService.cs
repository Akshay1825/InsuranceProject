﻿using InsuranceProject.DTOs;
using InsuranceProject.Helper;

namespace InsuranceProject.Services
{
    public interface IComplaintService
    {
        public Guid Add(ComplaintDto complaintDto);
        public ComplaintDto Get(Guid id);
        public PagedResult<ComplaintDto> GetAll(FilterParameter filterParameter);
        public bool Update(ComplaintDto complaintDto);
        public bool Delete(Guid id);
    }
}