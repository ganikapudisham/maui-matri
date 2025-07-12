using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri.Abstract;

public interface IDateNotificationScheduler
{
    void ScheduleNotification(DateTime dateTime, string title, string message, string tag);

    void CancelNotification(string tag);
}

