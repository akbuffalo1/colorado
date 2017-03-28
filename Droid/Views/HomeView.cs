using System;
using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ColoradoRoads.Droid
{
	[Activity(
		Label = "Colorado Roads", 
		Icon = "@mipmap/ic_launcher", 
		Theme = "@style/Theme.AppCompat.Light.DarkActionBar",
		NoHistory = true)]
	public class HomeView : BaseActivity<HomeViewModel>
	{
		protected override int LayoutId() => Resource.Layout.ActivityHome;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}
	}
}
