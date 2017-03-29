using System;
using Android.App;
using ColoradoRoads.ViewModels;

namespace ColoradoRoads.Droid.Views
{
	[Activity(Label = nameof(LocationDetailsView))]
	public class LocationDetailsView : BaseActivity<LocationDetailsViewModel>
	{
		protected override int LayoutId() => Resource.Layout.activity_location_details;
	}
}
