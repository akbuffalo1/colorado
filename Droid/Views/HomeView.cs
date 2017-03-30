using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ColoradoRoads;
using ColoradoRoads.Api;
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
		ViewPager bottomPager;
		TopCarouselViewAdapter topPagerAdapter;
		TopCarouselViewAdapter bottomPagerAdapter;
		MvxListView favLocations;

		protected override int LayoutId() => Resource.Layout.activity_home;

		protected override int MenuId() => Resource.Menu.menu_home;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SupportActionBar.SetHomeAsUpIndicator(Resources.GetDrawable(Resource.Drawable.ic_launcher));

			topPagerAdapter = new TopCarouselViewAdapter(this, Resource.Layout.layout_notification_item, FillTopPager);
			topPager = FindViewById<ViewPager>(Resource.Id.TopCarouselView);
			topPager.Adapter = topPagerAdapter;
			topPager.SetClipToPadding(false);
			topPager.PageMargin = 12;

			FindViewById<TabLayout>(Resource.Id.tlTopPagerDotIndicator).SetupWithViewPager(topPager, true);

			bottomPagerAdapter = new TopCarouselViewAdapter(this, Resource.Layout.layout_weather_notification_item, FillBottomPager);
			bottomPager = FindViewById<ViewPager>(Resource.Id.BottomCarouselView);
			bottomPager.Adapter = bottomPagerAdapter;
			bottomPager.SetClipToPadding(false);
			bottomPager.PageMargin = 12;

			FindViewById<TabLayout>(Resource.Id.tlBottomPagerDotIndicator).SetupWithViewPager(bottomPager, true);

			favLocations = FindViewById<MvxListView>(Resource.Id.lvFavouriteLocations);
			//favLocations.SetListViewHeightBasedOnChildren();
			//favLocations.SetOnTouchListener(this);

			this.AddLinqBinding(ViewModel, vm => vm.FavouriteLocations, list =>
			{
				if (list?.Count > 0)
				{ 
        			ViewGroup.LayoutParams layoutParameters = favLocations.LayoutParameters;
					favLocations.LayoutParameters = new FrameLayout.LayoutParams(layoutParameters.Width, 42 * list.Count);				
				}
			});

			this.CreateBinding(topPagerAdapter).For(adapter => adapter.DataSource).To<HomeViewModel>(vm => vm.NotificationModelsList).Apply();
			this.CreateBinding(bottomPagerAdapter).For(adapter => adapter.DataSource).To<HomeViewModel>(vm => vm.WeatherNotificationModelsList).Apply();
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

		private void FillBottomPager(View template, object model)
		{
			var image = template.FindViewById<ImageView>(Resource.Id.ivIcon);
			var title = template.FindViewById<TextView>(Resource.Id.tvTitle);
			var description = template.FindViewById<TextView>(Resource.Id.tvDescription);

			var dataModel = model as WeatherNotification;
			if (dataModel != null)
			{
				Glide.With(this).Load(dataModel.WeatherIcon).FitCenter().CenterCrop().Into(image);
				title.Text = dataModel.LocationName;
				description.Text = dataModel.Temperature;
			}
		}

		public bool OnTouch(View v, MotionEvent e)
		{
			v.Parent.RequestDisallowInterceptTouchEvent(true);
			return false;
		}
	}
}