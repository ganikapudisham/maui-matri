using System.Runtime.Serialization;


namespace ChristianJodi.Model
{
    [DataContract]
    public class Password : BaseResource
    {
        [DataMember]
        public string Current { get; set; }

        [DataMember]
        public string New { get; set; }

        [DataMember]
        public string Confirm { get; set; }

    }
}
