using System;
using System.Collections.Generic;
using System.Text;

namespace Matri.Abstract
{
    public interface IFirebaseAnalyticsService
    {
        void Log(string value, string eventName = "screen_view", string paramName = "screen_name");
        void Log(string eventName, IDictionary<string, string> parameters);
    }
}
