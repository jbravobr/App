using System;

using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public abstract class BaseRepository
    {
        readonly ILogExceptionService _log;
        const string salt = "ngNrOqLYqEe1R53acbHa";

        public BaseRepository()
        {
            if (_log == null)
                _log = App._container.Resolve<ILogExceptionService>();
        }

        public void Log(Exception ex) => _log.SubmitToInsights(ex);
    }
}

