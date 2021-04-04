using Services.Shared.Exceptions;
using System;

namespace User.Domain.Exceptions
{
    public class UnAuthorizedException : UserException 
    {
        public UnAuthorizedException()
        {
        }

        public UnAuthorizedException(string message)
            : base(message)
        {
        }

        public UnAuthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
