using InsuranceProject.Data;
using System.ComponentModel.DataAnnotations;

namespace InsuranceProject.Helper
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (Context)validationContext.GetService(typeof(Context));

            if (dbContext.Users.Any(user => user.UserName == value.ToString()))
            {
                return new ValidationResult("Username is already taken. Please choose a different one.");
            }

            return ValidationResult.Success;
        }
    }
}
