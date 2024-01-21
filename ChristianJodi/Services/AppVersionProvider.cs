using ChristianJodi.Business;

namespace ChristianJodi.Services
{
    public class AppVersionProvider : IAppVersionProvider
    {
        //PackageInfo _appInfo;

        public AppVersionProvider()
        {
            //var context = global::Android.App.Application.Context;
            //_appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }
        public string AppVersion
        {
            get
            {
                return $"test";
            }
        }

        public string AppWebsite
        {
            get
            {
                return "test";
            }
        }

        public string GooglePlayStoreAppId
        {
            get
            {
                return "";
            }
        }

        public int AppVersionNumber
        {
            get
            {
                return 2;
            }
        }

    }
}