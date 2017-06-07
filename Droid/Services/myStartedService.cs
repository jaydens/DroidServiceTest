using System;
using System.Threading;
using Android.App;
using Android.OS;

namespace AndroidServiceTest.Droid
{
    // Poss put Enabled flag in?
	[Service(Label = "locationStartedService")] //, Process = "com.intellidrive.locationservice", Enabled = true, Exported = true)]
	[IntentFilter(new String[] { "com.intellidrive.wakeful.locationStartedService" })]
    public class MyStartedService : Service
    {
        IBinder binder;
		public bool keepAlive;
		public static bool isRunning;
        public static MyStartedService CurrentInstance { get; private set; }

		static Thread t1;
		static Thread t2;
		static readonly object _locker = new Object();

        public MyStartedService()
        {
        }

        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            Dbg.Log($"MyStartedService Start Command Called PID: {Android.OS.Process.MyPid()}");
			return base.OnStartCommand(intent, flags, startId); if (keepAlive && !isRunning)
			{// We need to start the service
				t1 = new Thread(startLocManager);
				t1.IsBackground = true;
				t1.Name = "LocationManagerThread";
				t1.Priority = System.Threading.ThreadPriority.Normal;
				t1.Start();
				isRunning = true;
			}
			CurrentInstance = this;

			return StartCommandResult.RedeliverIntent;
		}

		static void startLocManager()
		{
            var lMgr = new CrossSomethingManager();
            lMgr.StartDoingSomething();

			t2 = new Thread(thread2);

			t2.IsBackground = true;
			t2.Priority = System.Threading.ThreadPriority.BelowNormal;
			t2.Name = "KeepAliveThread";
			t2.Start();
			while (t2.IsAlive)
			{
                Dbg.Log("KeepAliveThread....Running...");
				Thread.Sleep(30000);
			}
			try
			{
                Globals.lMgr = lMgr;
			}
			catch (Exception ex)
			{
				// If we fall into here then Globals doesn't exist, highly unlikely
				// but it could be app has been disposed and service is still running
				// So send with intent
				// var bIntent = new Intent("com.intellidrive.app.UpdateData");
				Dbg.Log($"Unable to set the Globals Instance of the locMgs: {ex.Message}");
			}
		}

        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            binder = new MyStartedServiceBinder(this);
            return binder;
        }
    }

	public class MyStartedServiceBinder : Binder
	{
        readonly MyStartedService service;

		public MyStartedServiceBinder(MyStartedService service)
		{
			this.service = service;
		}

		public MyStartedService GetlocationStartedService()
		{
			return service;
		}
	}
}
