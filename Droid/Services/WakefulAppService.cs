using System;
using Android.App;
using Android.Content;

namespace AndroidServiceTest.Droid
{
	[Service(Name = "com.intellidrive.wakeful.WakefulAppService")]
	public class WakefulAppService : WakefulIntentService.WakefulIntentService
	{
		string TAG = "WakefulAppService";
		public WakefulAppService() : base("WakefulAppService")
		{
            Dbg.Log($"{TAG} | In Constructor!");
		}

		protected override void DoWakefulWork(Intent intent)
		{
            if (!MyStartedService.isRunning)
			{
				Dbg.Log($"{TAG} | Restarting Location Started Service");
				var driveIntent = new Intent(BaseContext, typeof(MyStartedService));
				StartService(driveIntent);
			}
			else
				Dbg.Log($"{TAG} | Location Service Is Already Running");
		}
	}
}
