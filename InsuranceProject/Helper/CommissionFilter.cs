using InsuranceProject.Models;
using InsuranceProject.Types;
using System.Linq.Expressions;

namespace InsuranceProject.Helper
{
    public class CommissionFilter:PageParameter
    {
        public Guid? Id { get; set; }
    }
}
