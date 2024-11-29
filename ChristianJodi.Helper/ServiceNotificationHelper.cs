//using FirebaseAdmin.Messaging;
//using Matri.Model;
//using Matri.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Matri.Helper
//{
//    public static class ServiceNotificationHelper
//    {
//        public static async Task SendNotification(List<FCMToken> recipientDeviceTokens, string notificationTitle, 
//            string notificationBody, string notificationFrom)
//        {
//            var messages = new List<Message>();

//            var NotificationFrom = new Dictionary<string, string>();
//            NotificationFrom.Add("NotificationFrom", notificationFrom);


//            foreach (var recipientDT in recipientDeviceTokens)
//            {
//                var message = new Message
//                {
//                    Token = recipientDT.Token,
//                    Notification = new Notification
//                    {
//                        Title = notificationTitle,
//                        Body = notificationBody
//                    },
//                    Data = NotificationFrom
//                };
//                messages.Add(message);
//            }
//            var response = await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);
//        }
//    }
//}
