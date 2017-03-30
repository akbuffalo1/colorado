using System;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using UIKit;

namespace ColoradoRoads.iOS
{
	public class Presenter : MvxBaseIosViewPresenter
	{
		public static IMvxIosViewCreator ViewCreator;

		private IControllerTypeLookup _fragmentTypeLookup;
		private MvxViewController _modalVC;

		public UINavigationController NavigationController { get; set; }

		protected UIWindow Window { get; set; }

		protected UIApplicationDelegate AppDelegate { get; set; }

		public Presenter(UIApplicationDelegate appDelegate, UIWindow window) : base()
        {
			Window = window;
			AppDelegate = appDelegate;
			_fragmentTypeLookup = new ControllerTypeLookup();
		}
	}
}
