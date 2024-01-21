using System.Runtime.Serialization;

namespace ChristianJodi.Model
{
    [DataContract]
    public class AppDets : BaseResource
    {
        public AppDets()
        {
        }

        
        public string LatestVersion { get; set; }

        
        public string GooglePlayStoreLink { get; set; }

        
        public string WAAdminNumber { get; set; }

        
        public string Name { get; set; }

        
        public string UserVersion { get; set; }

        
        public int LatestVersionNumber { get; set; }

        
        public bool ShowHinduFields { get; set; }

    }
}
