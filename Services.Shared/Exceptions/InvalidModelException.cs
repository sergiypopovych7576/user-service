using System;

namespace Services.Shared.Exceptions
{
    public class InvalidModelException : UserException
    {
        public InvalidModelException()
        {
        }

        public InvalidModelException(string message)
            : base(message)
        {
        }

        public InvalidModelException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
