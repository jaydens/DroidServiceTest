using System;
using Android.Content;
using Android.Runtime;
using Android.App;

namespace AndroidServiceTest.Droid
{
    public class BootReciever
    {
		[BroadcastReceiver(Enabled = true)]
		[Register("com.intellidrive.ast.OnBootReciever")]
		[IntentFilter(new string[] { Intent.ActionBootCompleted },
				  Categories = new[] { "Intent.CategoryDefault" })]//"android.intent.category.DEFAULT"})]
		public class OnBootReciever : BroadcastReceiver
		{
			public override void OnReceive(Context context, Intent intent)
			{
                Dbg.Log($"WAKEFUL OnBoot Recieved - Trying To Schedule Alarm On PID {Android.OS.Process.MyPid()}");
				WakefulIntentService.WakefulIntentService.ScheduleAlarms(new AlarmListener(), context);
                Dbg.Log("WAKEFUL Restarting locationsStartedService");
                var d = new Intent(context, typeof(MyStartedService));
				context.StartService(d);
			}
		}
    }
}
