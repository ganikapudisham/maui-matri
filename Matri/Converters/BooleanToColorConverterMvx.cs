//using System.Globalization;

//namespace Matri.Converters
//{
//    /// <summary>
//    /// This class have methods to convert the Boolean values to color objects. 
//    /// This is needed to validate in the Entry controls. If the validation is failed, it will return the color code of error, otherwise it will be transparent.
//    /// </summary>
//    public class BooleanToColorConverterMvx : MvxFormsValueConverter<bool, string>
//    {
//        /// <summary>
//        /// This method is used to convert the bool to color.
//        /// </summary>
//        /// <param name="value">Gets the value.</param>
//        /// <param name="targetType">Gets the target type.</param>
//        /// <param name="parameter">Gets the parameter.</param>
//        /// <param name="culture">Gets the culture.</param>
//        /// <returns>Returns the color.</returns>
//        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (value == false)
//            {
//                return "#808080";
//            }
//            else
//            {
//                if (parameter.ToString() == "liked")
//                {
//                    return "#008000";
//                }
//                else if (parameter.ToString() == "blocked")
//                {
//                    return "#FF6347";
//                }
//            }
//            return "#808080";
//        }

//        /// <summary>
//        /// This method is used to convert the color to bool.
//        /// </summary>
//        /// <param name="value">Gets the value.</param>
//        /// <param name="targetType">Gets the target type.</param>
//        /// <param name="parameter">Gets the parameter.</param>
//        /// <param name="culture">Gets the culture.</param>
//        /// <returns>Returns the string.</returns>        
//        protected override bool ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}