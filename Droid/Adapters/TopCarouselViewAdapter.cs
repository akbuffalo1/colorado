using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using ColoradoRoads.Models;
using Com.Bumptech.Glide;
using MvvmCross.Binding.Droid.Views;

namespace ColoradoRoads.Droid.Adapters
{
	public class TopCarouselViewAdapter : PagerAdapter
	{
		public event EventHandler MyCountChanged;

		Context _context;
		IList<object> _modelsList;
		int _templateId;
		Action<View, object> _fillData;

		public TopCarouselViewAdapter(Context context, int templateId, Action<View, object> fillData)
		{
			_context = context;
			_templateId = templateId;
			_fillData = fillData;
		}

		public IEnumerable<object> DataSource
		{
			get { return GetDataSource(); }
			set { SetDataSource(value); }
		}

		public override int Count => _modelsList?.Count ?? 0;

		public override bool IsViewFromObject(View view, Java.Lang.Object obj)
		{
			return view == ((View)obj);
		}

		public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
		{
			var dataModel = _modelsList[position];

			var template = LayoutInflater.From(_context).Inflate(_templateId, container, false);

			_fillData?.Invoke(template, dataModel);

			container.AddView(template);
			return template;
		}

		public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object view)
		{
			container.RemoveView(view as View);
		}

		public IEnumerable<object> GetDataSource()
		{
			return _modelsList;
		}

		public void SetDataSource(IEnumerable<object> modelsList)
		{
			_modelsList = modelsList.ToList();
			NotifyDataSetChanged();
		}
	}
}
