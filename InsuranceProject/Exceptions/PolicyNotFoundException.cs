﻿namespace InsuranceProject.Exceptions
{
    public class PolicyNotFoundException:Exception
    {
        public PolicyNotFoundException(string message):base(message) { }
       
    }
}