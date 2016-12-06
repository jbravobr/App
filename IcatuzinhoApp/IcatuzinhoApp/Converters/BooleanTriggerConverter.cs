using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin;
namespace IcatuzinhoApp
{
    public class BooleanTriggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ((int)value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Insights.Report(ex);
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

