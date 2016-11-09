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

        public void SendToInsights(Exception ex)
        {

#if DEBUG
            throw ex;
#else
            _logExceptionService.SubmitToInsights(ex);
#endif

        }
    }
}

