using Syncfusion.Maui.ListView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matri.Model;

namespace Matri.Converters;

public class IndexToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var miniProfile = value as MiniProfile;

        if (miniProfile != null)
        {
            return miniProfile.Index % 2 == 0 ? Color.FromArgb("#fef9ea") : Color.FromArgb("#dfebfb");
        }
        return Color.FromArgb("#fef9ea");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
