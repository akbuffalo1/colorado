
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

namespace ColoradoRoads.Droid.Views
{
	[Activity(Label = "LocationsListView")]
	public class LocationsListView : BaseActivity<LocationsListViewModel>
	{
		protected override int LayoutId() => Resource.Layout.activity_locations_list;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
		}
	}
}
