using System;
using System.Collections.Generic;
using System.Text;

namespace Matri.Business
{
    internal static class Constants
    {
        internal const string API_URL_Authentication = "sessiontoken";
        internal const string API_URL_Register = "mini/signup";
        internal const string API_URL_GetMiniDetailsOfLoggedInUser = "mini";
        internal const string API_URL_ChangePassword = "password/change";
        internal const string API_URL_GetAppDetails = "app";
        internal const string API_URL_GetUserProfile = "profiles/mine";

        internal const string API_URL_UpdateUserLifeStyle = "profiles?step=lifestyle";
        internal const string API_URL_UpdateUserEducation = "profiles?step=education";
        internal const string API_URL_UpdateUserContact = "profiles?step=contact";
        internal const string API_URL_UpdateUserBasics = "profiles?step=Basic";
        internal const string API_URL_UpdateUserFamily = "profiles?step=family";
        internal const string API_URL_UpdateUserPhysical = "profiles?step=physical";
        internal const string API_URL_UpdateUserReligion = "profiles?step=religion";
        internal const string API_URL_UpdateUserExpectation = "partner?step=full";

        internal const string API_URL_ImagesRepo = "https://api.christianjodi.com/";
        internal const string API_URL_ContactUs = "email/contactus";
        internal const string API_URL_MasterData = "masterdata";
        internal const string API_URL_FCM = "devicetoken";
        internal const string API_URL_NotificationFrom = "featured";
        internal const string API_URL_AllRequests = "requests/all";

        internal const string PlayStoreAppUrl = "https://play.google.com/store/apps/details?id=";
    }
}
