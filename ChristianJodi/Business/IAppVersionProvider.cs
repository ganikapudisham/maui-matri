using System;
using System.Collections.Generic;
using System.Text;

namespace ChristianJodi.Business
{
    public interface IAppVersionProvider
    {
        string AppVersion { get; }
        string AppWebsite { get; }

        string GooglePlayStoreAppId { get; }

        int AppVersionNumber { get; }
    }
}
