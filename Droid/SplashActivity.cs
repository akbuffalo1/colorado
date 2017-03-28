using System;
using Android.App;
using MvvmCross.Droid.Views;

namespace ColoradoRoads.Droid
{
	[Activity( Label = "Colorado Roads", MainLauncher = true, Icon = "@mipmap/ic_launcher", NoHistory = true)]
	public class SplashScreen : MvxSplashScreenActivity
	{
		public SplashScreen() : base(Resource.Layout.Splash)
		{
		}
	}
}
