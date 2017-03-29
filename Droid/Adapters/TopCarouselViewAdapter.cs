using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using ColoradoRoads.Models;
using Com.Bumptech.Glide;

namespace ColoradoRoads.Droid.Adapters
{
	public class TopCarouselViewAdapter : PagerAdapter
	{
		Context _context;
		IList<RoadConditionNotificationModel> _notifications;

		public TopCarouselViewAdapter(Context context, IList<RoadConditionNotificationModel> notifications)
		{
			_context = context;
			_notifications = notifications;
		}

		public override int Count => _notifications?.Count ?? 0;

		public override bool IsViewFromObject(View view, Java.Lang.Object obj)
		{
			return view == ((View)obj);
		}

		public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
		{
			var dataModel = _notifications[position];

			var template = LayoutInflater.From(_context).Inflate(Resource.Layout.LayoutNotificationItem, null);

			var image = template.FindViewById<ImageView>(Resource.Id.ivIcon);
			var title = template.FindViewById<TextView>(Resource.Id.tvTitle);
			var description = template.FindViewById<TextView>(Resource.Id.tvDescription);

			Glide.With(_context).Load(dataModel.Icon).FitCenter().CenterCrop().Into(image);
			title.Text = dataModel.Title;
			description.Text = dataModel.Description;
			return template;
		}

		public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object view)
		{
			var viewPager = container.JavaCast<ViewPager>();
			viewPager.RemoveView(view as View);
		}
	}
}
