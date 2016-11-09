using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface ILogExceptionService 
    {
        void SubmitToInsights(Exception ex);
    }
}

