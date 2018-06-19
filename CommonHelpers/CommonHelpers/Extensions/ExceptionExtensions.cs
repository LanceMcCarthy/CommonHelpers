using System;
using System.Text;

namespace CommonHelpers.Extensions
{
    public static class ExceptionExtensions
    {
        private static string GenerateErrorMessage(this Exception ex)
        {
            var messageBuilder = new StringBuilder();

            try
            {
                messageBuilder.AppendLine("-----------------------------------------------------------------\r");
                messageBuilder.AppendLine($"Source: {ex.Source.Trim()}");
                messageBuilder.AppendLine($"Timestamp: {DateTime.Now}");
                messageBuilder.AppendLine("-----------------------------------------------------------------\r");
                messageBuilder.AppendLine($"Method: {ex.Message.Trim()}");
                messageBuilder.AppendLine($"Exception :: {ex}");

                if(ex.InnerException != null)
                {
                    messageBuilder.AppendLine($"InnerException :: {ex.InnerException}");
                }

                messageBuilder.AppendLine("");

                return messageBuilder.ToString();
            }
            catch
            {
                messageBuilder.AppendLine("Exception:: Unknown Exception.");
                return messageBuilder.ToString();
            }
        }
    }
}