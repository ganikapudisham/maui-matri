//using System.Globalization;

//namespace Matri.Converters
//{
//    public class NegateBooleanConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//        {
//            return !(bool)value;
//        }
//        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//        {
//            return !(bool)value;
//        }
//    }

//    public class BackGroundColor : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {

//            if (value == null || parameter == null) return Color.White;
//            var index = (parameter as ListView).ItemsSource.Cast<object>().ToList().IndexOf(value);
//            return (index % 2 == 0) ? Color.White : Color.Gray;

//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    //public class RegisterDate : IValueConverter
//    //{
//    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    //    {

//    //        var ddlDate = (parameter as Picker);

//    //        for (int i = 1; i < 32; i++)
//    //        {
//    //            ddlDate.Items.Add(i.ToString());
//    //        }

//    //        return ddlDate;

//    //    }

//    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    //    {
//    //        throw new NotImplementedException();
//    //    }
//    //}

//    //public class RegisterMonth : IValueConverter
//    //{
//    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    //    {

//    //        var ddlMonth = (parameter as Picker);
//    //        string[] names = DateTimeFormatInfo.CurrentInfo.MonthNames;
//    //        foreach (var name in names)
//    //        {
//    //            ddlMonth.Items.Add(name);
//    //        }

//    //        return ddlMonth;

//    //    }

//    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    //    {
//    //        throw new NotImplementedException();
//    //    }
//    //}

//    //public class RegisterYear : IValueConverter
//    //{
//    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    //    {

//    //        int maxYear = DateTime.Now.Year - 18;
//    //        int minYear = maxYear - 75;

//    //        var ddlYear = (parameter as Picker);

//    //        for (int i = maxYear; i >= minYear; i--)
//    //        {
//    //            ddlYear.Items.Add(i.ToString());
//    //        }

//    //        return ddlYear;

//    //    }

//        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        //{
//        //    throw new NotImplementedException();
//        //}
//    //}
//}
