using System;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;

namespace ColoradoRoads.Droid
{
	public class TrafficDiagramViewContainer : LinearLayout
	{
		Context _context;
		public TrafficDiagramViewContainer(Context context) : base (context)
        {
			Initialize(context);
		}

		public TrafficDiagramViewContainer(Context context, IAttributeSet attrs) : base (context,attrs)    
        {
			Initialize(context);
		}

		public TrafficDiagramViewContainer(Context context, IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
        {
			Initialize(context);
		}

		void Initialize(Context context)
		{
			_context = context;
		}
	}
}
