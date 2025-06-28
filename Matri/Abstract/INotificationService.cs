using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Abstract;

public interface INotificationService
{
    void ScheduleBirthdayNotification(int day, int month, int notificationId, string title, string message);

    void CancelBirthdayNotification(int notificationId);
}

