using System;

namespace ThirdApproachDomain
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message)
            : base(message)
        {
        }
    }
}