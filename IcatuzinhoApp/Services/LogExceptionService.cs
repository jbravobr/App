using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xamarin;
using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public class LogExceptionService : ILogExceptionService
    {
        /// <summary>
        /// Envia para o Insights o erro (Exception) ocorrido.
        /// </summary>
        /// <param name="ex">Ex.</param>
        public void SubmitToInsights(Exception ex) => Insights.Report(ex);
    }
}

