namespace InsuranceProject.Exceptions
{
    public class UserNameExistsException:Exception
    {
        public UserNameExistsException(string message):base(message) { }
       
    }
}
