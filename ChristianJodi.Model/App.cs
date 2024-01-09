using System.Runtime.Serialization;

namespace ChristianJodi.Model
{
    [DataContract]
    public class App : BaseResource
    {
        public App()
        {
        }

        [DataMember]
        public string LatestVersion { get; set; }

        [DataMember]
        public string GooglePlayStoreLink { get; set; }

        [DataMember]
        public string WAAdminNumber { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string UserVersion { get; set; }

        [DataMember]
        public int LatestVersionNumber { get; set; }

        [DataMember]
        public bool ShowHinduFields { get; set; }

    }
}
