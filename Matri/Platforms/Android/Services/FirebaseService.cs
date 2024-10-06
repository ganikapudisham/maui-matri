using Android.App;
using Android.Content;
using AndroidX.Core.App;
using Firebase.Messaging;
using Google.Cloud.Firestore;

namespace Matri.Platforms.Services
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseService : FirebaseMessagingService
    {
        public FirebaseService()
        {

        }
        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            if (Preferences.ContainsKey("DeviceToken"))
            {
                Preferences.Remove("DeviceToken");
            }
            Preferences.Set("DeviceToken", token);
            //SendTokenToServer(token);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            var notification = message.GetNotification();

            SendNotification(notification.Body, notification.Title, message.Data);
        }

        private void SendNotification(string messageBody, string title, IDictionary<string, string> data)
        {

            var intent = new Intent(this, typeof(Matri.MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.AddFlags(ActivityFlags.SingleTop);


            foreach (var key in data.Keys)
            {
                string value = data[key];
                intent.PutExtra(key, value);
            }

            var pendingIntent = PendingIntent.GetActivity(this,
                MainActivity.NotificationID, intent, PendingIntentFlags.OneShot | PendingIntentFlags.Immutable);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.Channel_ID)
                .SetContentTitle(title)
                .SetSmallIcon(Resource.Mipmap.appicon)
                .SetContentText(messageBody)
                .SetChannelId(MainActivity.Channel_ID)
                .SetContentIntent(pendingIntent)
                .SetAutoCancel(true)
                .SetPriority((int)NotificationPriority.Max);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NotificationID, notificationBuilder.Build());
        }
        //private void SendTokenToServer(string token)
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory + @"admin_sdk.json";
        //    Environment.SetEnvironmentVariable("", path);
        //    var dbChristianJodi = FirestoreDb.Create("cloudfire-christianjodi");
        //    // If you're running your own server, call API to send token and today's date for the user

        //    var deviceTokensCollection = dbChristianJodi.Collection("DeviceTokens");
        //    var deviceToken = new Dictionary<string, string>()
        //    {
        //        { "deviceToken",token },
        //         { "timeStamp",DateTime.UtcNow.ToString() }
        //    };

        //    deviceTokensCollection.AddAsync(deviceToken);

        //}
    }
}
