using System;
using Prism.Mvvm;

namespace IcatuzinhoApp
{
    public class BasePageViewModel : BindableBase
    {
        public void SendToInsights(Exception ex) => LogExceptionHelper.SubmitToInsights(ex);

        public void RecordMetric(Transaction transaction, LogExceptionType type, Exception ex) => LogExceptionHelper.GenerateLogException(transaction, type, ex?.Message, ex.InnerException?.InnerException.Message);
    }
}