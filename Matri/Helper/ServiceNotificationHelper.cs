using FirebaseAdmin.Messaging;
using Matri.Model;
using Matri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Helper
{
    public static class ServiceNotificationHelper
    {
        public static async Task SendNotification(List<FCMToken> recipientDeviceTokens, string notificationTitle, string notificationBody)
        {
            var messages = new List<Message>();

            foreach(var recipientDT in recipientDeviceTokens)
            {
                var message = new Message
                {
                    Token = recipientDT.Token,
                    Notification = new Notification
                    {
                        Title = notificationTitle,
                        Body = notificationBody
                    },
                };
                messages.Add(message);
            }
            var response = await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);
        }
    }
}
