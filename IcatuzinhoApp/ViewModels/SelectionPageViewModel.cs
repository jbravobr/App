using System;
using Acr.UserDialogs;
namespace IcatuzinhoApp
{
    public class SelectionPageViewModel : BasePageViewModel
    {
        IUserDialogs _userDialogs;

        public SelectionPageViewModel(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;

            try
            {
                _userDialogs.HideLoading();
            }
            catch (Exception ex)
            {
                SendToInsights(ex);
            }
        }
    }
}

