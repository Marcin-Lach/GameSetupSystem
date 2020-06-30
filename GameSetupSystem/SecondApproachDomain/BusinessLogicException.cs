using System;

namespace SecondApproachDomain
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message)
            : base(message)
        {
        }
    }
}