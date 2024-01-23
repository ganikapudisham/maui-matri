using Matri;

namespace Matri.Services
{
	public class FirebaseAnalytics : IFirebaseAnalytics
	{
		public void LogEvent(string eventId)
		{
			LogEvent(eventId, null);
		}

		public void LogEvent(string eventId, string paramName, string value)
		{
			LogEvent(eventId, new Dictionary<string, string>
		{
			{paramName, value}
		});
		}

		public void SetUserId(string userId)
		{
		}

		public void LogEvent(string eventId, IDictionary<string, string> parameters)
		{
		}
	}
}