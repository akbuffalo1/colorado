using System;
using Android.Widget;
using ColoradoRoads.Platform;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace ColoradoRoads.Droid.ServicesImpl
{
	public class Platfrom : IPlatform
	{
		public void ShowToast(string message)
		{
			var context = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.ApplicationContext;
			Toast.MakeText(context, message, ToastLength.Long).Show();
		}
	}
}