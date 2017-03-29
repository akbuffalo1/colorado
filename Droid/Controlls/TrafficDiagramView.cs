using System;
using Android.Content;
using Android.Util;
using Android.Views;

namespace ColoradoRoads.Droid.Controlls
{
	public class TrafficDiagramView : View
	{
		Context _context;
		public TrafficDiagramView(Context context) : base (context)
        {
			Initialize(context);
		}

		public TrafficDiagramView(Context context, IAttributeSet attrs) : base (context,attrs)    
        {
			Initialize(context);
		}

		public TrafficDiagramView(Context context, IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
        {
			Initialize(context);
		}

		void Initialize(Context context)
		{
			_context = context;
		}
	}
}
