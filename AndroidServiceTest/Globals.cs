using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace AndroidServiceTest
{
    public static class Globals
    {
        public static AndroidServiceTest.Droid.CrossSomethingManager lMgr;
        public static string LogTag = "Intelli_AST";

        static bool _lmir = false;
        public static bool lManagerIsRunning 
        {
            get{
                return _lmir;
            }
            set{
                SetPropertyValue(ref _lmir, value);
            }
        }
		public static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { };

		public static void RaisePropertyChanged([CallerMemberName] string propertyName = "", Type t = null, object value = null)
		{
			GlobalPropertyChanged(typeof(Globals), new PropertyChangedEventArgs(propertyName));
		}

		public static bool SetPropertyValue<T>(ref T storageField, T newValue, Expression<Func<T>> propExpr)
		{
			if (Equals(storageField, newValue))
				return false;

			storageField = newValue;
			var prop = (PropertyInfo)((MemberExpression)propExpr.Body).Member;
			RaisePropertyChanged(prop.Name);

			return true;
		}

		public static bool SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
		{
			if (Equals(storageField, newValue))
				return false;

			storageField = newValue;
			RaisePropertyChanged(propertyName);

			return true;
		}
    }

    public static class Dbg
    {
        public static void Log(string msg, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            Android.Util.Log.Debug(Globals.LogTag, $"{msg} \n\tCall Above From: {memberName}");
        }
    }
}
