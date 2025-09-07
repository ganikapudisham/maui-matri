//using Matri.Abstract;

//#if IOS
//    using Foundation;
//#endif

//#if ANDROID
//using Android.Content;
//using Android.OS;
//using Firebase.Analytics;
//#endif

//namespace Matri.Services
//{

//    public class FirebaseAnalyticsService : IFirebaseAnalyticsService
//    {
//        public void Log(string value, string eventName = "screen_view", string paramName = "screen_name")
//        {
//            Log(eventName, new Dictionary<string, string>
//            {
//                { paramName, value.Replace("ViewModel", "", StringComparison.InvariantCulture) }
//            });
//        }

//        public void Log(string eventName, IDictionary<string, string> parameters)
//        {

//#if ANDROID
//            var firebaseAnalytics = FirebaseAnalytics.GetInstance(Platform.CurrentActivity);

//            if (parameters == null)
//            {
//                firebaseAnalytics.LogEvent(eventName, null);
//                return;
//            }

//            var bundle = new Bundle();
//            foreach (var param in parameters)
//            {
//                bundle.PutString(param.Key, param.Value);
//            }

//            firebaseAnalytics.LogEvent(eventName, bundle);
//#endif
//        }
//    }
//}