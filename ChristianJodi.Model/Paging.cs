using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Matri.Model
{
    [DataContract]
    public class Paging<T> : BaseResource
    {
        public Paging()
        {
            Data = new List<T>();
            MetaData = new PagingMetadata();
        }

        [DataMember]
        public PagingMetadata MetaData { get; set; }

        [DataMember]
        public IList<T> Data { get; set; }

    }
}