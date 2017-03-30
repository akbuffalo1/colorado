using System;
using Android.Views;
using Android.Widget;

namespace ColoradoRoads.Droid.Utils
{
	public static class ListViewEx
	{
		public static void SetListViewHeightBasedOnChildren(this ListView listView)
		{
			var listAdapter = listView.Adapter;
			if (listAdapter == null)
				return;

			int desiredWidth = View.MeasureSpec.MakeMeasureSpec(listView.Width, MeasureSpecMode.Unspecified);
			int totalHeight = 0;
			View view = null;
			for (int i = 0; i < listAdapter.Count; i++)
			{
				view = listAdapter.GetView(i, view, listView);
				if (i == 0)
					view.LayoutParameters = new ViewGroup.LayoutParams(desiredWidth, ViewGroup.LayoutParams.FillParent);

				view.Measure(desiredWidth, (int)MeasureSpecMode.Unspecified);
				totalHeight += view.MeasuredHeight;
			}
			var paramss = listView.LayoutParameters;
			paramss.Height = totalHeight + (listView.DividerHeight * (listAdapter.Count - 1));
			listView.LayoutParameters = paramss;
		}
	}
}
