using System;

namespace AndroidServiceTest
{
    public static class Globals
    {
        public static AndroidServiceTest.Droid.CrossSomethingManager lMgr;
        public static string LogTag = "Intelli_AST";
    }

    public static class Dbg
    {
        public static void Log(string msg, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            Android.Util.Log.Debug(Globals.LogTag, $"{msg} \n\tCall Above From: {memberName}");
        }
    }
}
