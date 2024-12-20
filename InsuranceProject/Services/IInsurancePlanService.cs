﻿using InsuranceProject.DTOs;
using InsuranceProject.Helper;
using InsuranceProject.Models;

namespace InsuranceProject.Services
{
    public interface IInsurancePlanService
    {
        public Guid Add(InsurancePlanDto insurancePlanDto);
        public InsurancePlanDto Get(Guid id);
        public PagedResult<InsurancePlanDto> GetAll(FilterParameter filterParameter);
        public bool Update(InsurancePlanDto insurancePlanDto);
        public bool Delete(Guid id);

        public InsurancePlan GetByUserName(InsurancePlanDto insurancePlanDto);

        public PagedResult<InsurancePlanDto> GetAlll(FilterParameter filterParameter);
    }
}
