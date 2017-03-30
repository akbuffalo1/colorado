using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ColoradoRoads;
using ColoradoRoads.Droid.Adapters;
using ColoradoRoads.Droid.Utils;
using ColoradoRoads.Dummy;
using ColoradoRoads.Models;
using Com.Bumptech.Glide;
using Com.Synnapps.Carouselview;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using CarouselView = Com.Synnapps.Carouselview.BoundCarouselView;

namespace ColoradoRoads.Droid
{
	[Activity(Label = "Colorado Roads", Icon = "@mipmap/ic_launcher")]
	public class HomeView : BaseActivity<HomeViewModel>, View.IOnTouchListener
	{
		ViewPager topPager;
		TopCarouselViewAdapter adapter;
		MvxListView favLocations;

		protected override int LayoutId() => Resource.Layout.activity_home;

		protected override int MenuId() => Resource.Menu.menu_home;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SupportActionBar.SetHomeAsUpIndicator(Resources.GetDrawable(Resource.Drawable.ic_launcher));

			adapter = new TopCarouselViewAdapter(this, Resource.Layout.layout_notification_item, FillTopPager);
			topPager = FindViewById<ViewPager>(Resource.Id.TopCarouselView);
			topPager.Adapter = adapter;
			topPager.SetClipToPadding(false);
			topPager.PageMargin = 12;

			favLocations = FindViewById<MvxListView>(Resource.Id.lvFavouriteLocations);
			//favLocations.SetListViewHeightBasedOnChildren();

			//favLocations.SetOnTouchListener(this);

			this.AddLinqBinding(ViewModel, vm => vm.NotificationModelsList, list =>
			{
				if (list?.Count > 0)
				{ 
        			ViewGroup.LayoutParams layoutParameters = favLocations.LayoutParameters;
					favLocations.LayoutParameters = new FrameLayout.LayoutParams(layoutParameters.Width, 30 * list.Count);				
				}
			});

			this.CreateBinding(adapter).For(adapter => adapter.DataSource).To<HomeViewModel>(vm => vm.NotificationModelsList).Apply();
		}

		IList<RoadConditionNotificationModel> items;
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.ActionMenu:
					ViewModel.ShowMainMenuCommand.Execute();
					return true;
			}
			return base.OnOptionsItemSelected(item);
		}

		private void FillTopPager(View template, object model)
		{
			var image = template.FindViewById<ImageView>(Resource.Id.ivIcon);
			var title = template.FindViewById<TextView>(Resource.Id.tvTitle);
			var description = template.FindViewById<TextView>(Resource.Id.tvDescription);

			var dataModel = model as RoadConditionNotificationModel;
			if (dataModel != null)
			{ 
				Glide.With(this).Load(dataModel.Icon).FitCenter().CenterCrop().Into(image);
				title.Text = dataModel.Title;
				description.Text = dataModel.Description;
			}
		}

		public bool OnTouch(View v, MotionEvent e)
		{
			v.Parent.RequestDisallowInterceptTouchEvent(true);
			return false;
		}
	}
}