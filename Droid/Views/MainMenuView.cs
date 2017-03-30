
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ColoradoRoads.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ColoradoRoads.Droid.Views
{
	[Activity(Label = "MainMenuView", ParentActivity = typeof(HomeView))]
	public class MainMenuView : BaseActivity<MainMenuViewModel>
	{
		protected override int LayoutId() => Resource.Layout.layout_main_menu;

		protected override int MenuId() => Resource.Menu.menu_home;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.ActionMenu:
					ViewModel.CloseSelfCommand.Execute();
					return true;
			}
			return base.OnOptionsItemSelected(item);
		}
	}
}
