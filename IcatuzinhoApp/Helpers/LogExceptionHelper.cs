using System;
using Xamarin;

namespace IcatuzinhoApp
{
    public static class LogExceptionHelper
    {
        /// <summary>
        /// Gera um objeto de LogException
        /// </summary>
        /// <param name="trasaction">Trasaction.</param>
        /// <param name="type">Type.</param>
        /// <param name="exceptionMessage">Exception message.</param>
        /// <param name="innerExceptionMessage">Inner exception message.</param>
        public static LogException GenerateLogException(Transaction trasaction, LogExceptionType type, string exceptionMessage, string innerExceptionMessage)
        {
            return new LogException
            {
                Exception = exceptionMessage,
                InnerException = innerExceptionMessage,
                Trasaction = trasaction,
                Type = type
            };
        }

        /// <summary>
        /// Envia para o Insights o erro (Exception) ocorrido.
        /// </summary>
        /// <param name="ex">Ex.</param>
        public static void SubmitToInsights(Exception ex)
        {
            Insights.Report(ex);
        }
    }
}