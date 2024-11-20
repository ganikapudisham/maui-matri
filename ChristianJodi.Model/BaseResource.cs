using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Matri.Model
{
    /// <summary>
    /// Base class for all the resources. 
    /// This contains messages details which should be conveyed back to the user.
    /// </summary>
    [DataContract]
    public abstract class BaseResource
    {
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public List<ErrorResponse> Errors { get; set; }

        //private static string[] _dateTimeFormats = Config.General.DateTimePattern.Split(',');
        //private static string _dateTimeFormatForSerialization = "yyyy-MM-dd hh:mm:ss";

        //[IgnoreDataMember]
        //[XmlIgnore]
        //public string[] DateTimeFormats
        //{
        //    get
        //    {
        //        if (_dateTimeFormats.Length > 0)
        //        {
        //            return _dateTimeFormats;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //    }
        //}

        //[IgnoreDataMember]
        //[XmlIgnore]
        //public string DateTimeFormatForSerialization
        //{
        //    get
        //    {
        //        return _dateTimeFormatForSerialization;
        //    }
        //    set
        //    {
        //    }
        //}
        // }

        [DataContract(Name = "Error")]
        public class ErrorResponse
        {
            [DataMember(Order = 2)]
            public string Message { get; set; }

            [DataMember(Order = 1, EmitDefaultValue = false)]
            public string Error { get; set; }
        }
    }
}