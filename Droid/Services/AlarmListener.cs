using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using WakefulIntentService;

namespace AndroidServiceTest.Droid
{
	[Register("com.intellidrive.ast.AlarmListener")]
	public class AlarmListener : Java.Lang.Object, WakefulIntentService.WakefulIntentService.IAlarmListener
	{
		/// <summary>
		/// Gets the interval period of alarm.
		/// </summary>
		public int intervalMin = 1;
		public static bool isSet { get; private set; } = false;
		public AlarmListener()
		{
			if (isSet)
				return;
		}

		public long GetMaxAge()
		{
			return (1000 * 60 * 5);
		}

		public void ScheduleAlarms(AlarmManager manager, PendingIntent pendingIntent, Context context)
		{
			var dt = DateTime.UtcNow;
			dt = dt.AddMinutes(intervalMin);

			long epochTicks = new DateTime(1970, 1, 1).Ticks;
			var diff = new TimeSpan(dt.Ticks - epochTicks);
			long msSince1970 = (long)Math.Round(diff.TotalMilliseconds);
			long triggerInterval = 1000 * 60 * intervalMin;
			//manager.SetExact(AlarmType.RtcWakeup, msSince1970, pendingIntent);
			manager.SetRepeating(AlarmType.RtcWakeup, msSince1970, triggerInterval, pendingIntent);

			var time = dt.ToString("HH:m:ss");
			Log.Info(this.ToString(), $"Set Alarm For {msSince1970} ms\n{time} UTC");
			isSet = true;
		}

		public void SendWakefulWork(Context context)
		{
			WakefulIntentService.WakefulIntentService.SendWakefulWork(context, typeof(WakefulAppService));
		}
    }
}
