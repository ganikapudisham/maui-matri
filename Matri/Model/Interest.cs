using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Matri.Model
{
    [DataContract]
    public class Interest
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public int Type { get; set; }
    }
}
