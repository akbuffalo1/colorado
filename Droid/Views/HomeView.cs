using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using ColoradoRoads.Droid.Adapters;
using ColoradoRoads.Dummy;
using Com.Bumptech.Glide;
using Com.Synnapps.Carouselview;
using MvvmCross.Droid.Support.V7.AppCompat;
using CarouselView = Com.Synnapps.Carouselview.BoundCarouselView;

namespace ColoradoRoads.Droid
{
	[Activity(Label = "Colorado Roads", Icon = "@mipmap/ic_launcher")]
	public class HomeView : BaseActivity<HomeViewModel>
	{
		ViewPager carouselView;

		protected override int LayoutId() => Resource.Layout.ActivityHome;

		protected override int MenuId() => Resource.Menu.HomeMenu;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//carouselView = FindViewById<CarouselView>(Resource.Id.TopCarouselView);
			//carouselView = new Com.Synnapps.Carouselview.BoundCarouselView(this);
			//var container = FindViewById<LinearLayout>(Resource.Id.TopCarouselViewContainer);
			//container.AddView(carouselView);
			//carouselView.SetViewListener(new CustomViewListener(this, carouselView));

			carouselView = FindViewById<ViewPager>(Resource.Id.TopCarouselView);



			carouselView.Adapter = new TopCarouselViewAdapter(this, DummyModels.RoadConditionNotificationModel);

			this.AddLinqBinding(ViewModel, vm => vm.NotificationModelsList, notificatins =>
			{
				if (notificatins?.Count > 0)
				{
					carouselView.Adapter = new TopCarouselViewAdapter(this, notificatins);
				}
			});

			SupportActionBar.SetHomeAsUpIndicator(Resources.GetDrawable(Resource.Drawable.ic_launcher));
		}

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

		/*
		public class CustomViewListener : IViewListener
		{
			Context _context;
			CarouselView _carouselView;

			String[] sampleTitles = { "Orange", "Grapes", "Strawberry", "Cherry", "Apricot" };
			String[] sampleNetworkImageURLs = {
					"https://placeholdit.imgix.net/~text?txtsize=15&txt=image1&txt=350%C3%97150&w=350&h=150",
					"https://placeholdit.imgix.net/~text?txtsize=15&txt=image2&txt=350%C3%97150&w=350&h=150",
					"https://placeholdit.imgix.net/~text?txtsize=15&txt=image3&txt=350%C3%97150&w=350&h=150",
					"https://placeholdit.imgix.net/~text?txtsize=15&txt=image4&txt=350%C3%97150&w=350&h=150",
					"https://placeholdit.imgix.net/~text?txtsize=15&txt=image5&txt=350%C3%97150&w=350&h=150"
			};

			public IntPtr Handle
			{
				get;
				set;
			}

			public CustomViewListener(Context context, CarouselView carouselView)
			{
				_context = context;
				_carouselView = carouselView;
				_carouselView.PageCount = sampleNetworkImageURLs.Length;
			}


			public View SetViewForPosition(int position)
			{
				View customView = LayoutInflater.From(_context).Inflate(Resource.Layout.LayoutRoadCondition, null);

				//TextView labelTextView = customView.FindViewById<TextView>(Resource.Id.l);
				//ImageView fruitImageView = customView.FindViewById<ImageView>(Resource.Id.fr);


				//Glide.With(_context).Load(sampleNetworkImageURLs[position]).FitCenter().CenterCrop().Into(fruitImageView);
				//labelTextView.Text = sampleTitles[position];

				_carouselView.IndicatorGravity = (int) (GravityFlags.CenterHorizontal | GravityFlags.Top);

				return customView;
			}

			public void Dispose()
			{
				_context = null;
				_carouselView = null;
			}
		}
		*/
	}
}
