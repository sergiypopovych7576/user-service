using Services.Shared.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Services.Shared.Services
{
    public static class Guard
    {
        public static void IsNull(Object source, string message = null)
        {
            if (source == null)
            {
                var errorMessage = message ?? $"Source should be null";

                throw new InvalidModelException(errorMessage);
            }
        }

        public static void AgainstNull(Object source, string message = null)
        {
            if (source == null)
            {
                var errorMessage = message ?? $"Source can not be null";

                throw new InvalidModelException(errorMessage);
            }
        }

        public static void MaxLength(string source, int maxValue, string message = null)
        {
            if (source.Length > maxValue)
            {
                var errorMessage = message ?? $"Source exceeded {maxValue} value";

                throw new InvalidModelException(errorMessage);
            }
        }

        public static void MinLength(string source, int minValue, string message = null)
        {
            if (source.Length < minValue)
            {
                var errorMessage = message ?? $"Source shoulbe more that {minValue}";

                throw new InvalidModelException(errorMessage);
            }
        }

        public static void IsValidEmail(string source, string message = null)
        {
            if (!new EmailAddressAttribute().IsValid(source))
            {
                var errorMessage = message ?? $"{source} is not a valid email address";

                throw new InvalidModelException(errorMessage);
            }
        }
    }
}
