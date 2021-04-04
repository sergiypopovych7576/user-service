using System;

namespace Services.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static string FormatException(this Exception exception)
        {
            var formattedMessage = $"[Exception]: {exception.Message}";
            formattedMessage += Environment.NewLine;
            formattedMessage += $"[Stack]: {exception.StackTrace}";

            if(exception.InnerException != null)
            {
                formattedMessage += Environment.NewLine;
                formattedMessage += $"[InnerStack]: {Environment.NewLine} {exception.InnerException.FormatException()}";
            }

            return formattedMessage;
        }
    }
}
