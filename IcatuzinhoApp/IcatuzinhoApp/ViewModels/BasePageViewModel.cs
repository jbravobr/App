using System;
using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public class BasePageViewModel : Prism.Mvvm.BindableBase
    {
        ILogExceptionService _logExceptionService;

        public BasePageViewModel()
        {
            try
            {
                if (_logExceptionService == null)
                    _logExceptionService = App._container.Resolve<ILogExceptionService>();
            }
            catch (Exception ex)
            {
                SendToInsights(ex);
            }
        }

        public void SendToInsights(Exception ex) => _logExceptionService.SubmitToInsights(ex);

        public void RecordMetric(Transaction transaction, LogExceptionType type, Exception ex) => _logExceptionService.Save(transaction, type, ex?.Message, ex.InnerException?.InnerException.Message);
    }
}

