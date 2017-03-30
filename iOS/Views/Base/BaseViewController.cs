using System;
using UIKit;
using Foundation;
using MvvmCross.Binding.BindingContext;
using Cirrious.FluentLayouts.Touch;
using System.Collections.Generic;
using GloriumTech.Plugins.Utils;
using System.Threading;
using System.Linq;
using MvvmCross.Platform;
using ColoradoRoads.ViewModels.Base;
using iDermCharts.Core.Controllers.Base;
using ColoradoRoads.Platform;
using ColoradoRoads.iOS.Utils.Attributes;
using ColoradoRoads.iOS.Utils.Extensions;

namespace ColoradoRoads.iOS.Views.Base
{
	public class BaseViewController<TViewModel> :  MvxTextFieldResponderController<TViewModel> where TViewModel : ViewModelBase
    {
        UIActivityIndicatorView _activityView;

        public BaseViewController() : base()
        {
        }

		protected BaseViewController(IntPtr handle) : base(handle)
        {
        }

        public BaseViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.All;
		}

		public override bool ShouldAutorotate() => true;

		protected virtual bool ShowActivityIndicator { get; set; } = true;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			AddIndicatorView();

            if (NavigationController != null)
			{
                NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
                NavigationController.NavigationBar.BarTintColor = new UIColor(red: 0.22f, green: 0.26f, blue: 0.33f, alpha: 0.7f);
                NavigationController.NavigationBar.TintColor = UIColor.FromRGB(121,142,187);
                NavigationController.NavigationBar.Hidden = false;

                var logoutIcon = UIImage.FromBundle("ic_logout");
                UIImageView image = new UIImageView(new CoreGraphics.CGRect(0, 0, 28, 20));
                image.Image = logoutIcon;
                image.UserInteractionEnabled = true;

                //TODO: Uncomment it ASAP!!! this.CreateBinding(image).For("Tap").To<ViewModelBase>(vm => vm.LogOutCommand).Apply();

                NavigationItem.RightBarButtonItem = new UIBarButtonItem(image);
				SetNavigationItem(NavigationItem);
			}

			this.AddLinqBinding(ViewModel, vm => vm.IsBusy, IsBusy =>
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = IsBusy;
				if (ShowActivityIndicator)
				{
					if (IsBusy)
						InvokeOnMainThread(() => _activityView.StartAnimating());
					else
						_activityView.StopAnimating();
				}
			});

            this.AddLinqBinding(ViewModel, vm => vm.OnErrorMessage, message =>
            {
                if (!string.IsNullOrEmpty(message))
                    Mvx.Resolve<IPlatform>().ShowToast(message);
            });

            InitializeObjects();
			
            //this.AddLinqBinding(ViewModel, vm => vm.IsBusy, isBusy => UIApplication.SharedApplication.InvokeOnMainThread(() => ToggleLoaderOverlay(isBusy)));

            InitializeMobileLogic();

			InitializeBindings();
        }

		protected virtual void SetNavigationItem(UINavigationItem item)
		{
		}

        protected virtual void InitializeMobileLogic()
        {
        }

        protected virtual void InitializeTabletLogic()
        {
        }

		protected virtual void InitializeObjects()
		{
		}

		protected virtual void InitializeBindings()
		{
		}

        public override void DidMoveToParentViewController(UIKit.UIViewController parent)
        {
            if (parent == null && !GetType().IsContainerElement())
                Dispose(true);

            base.DidMoveToParentViewController(parent);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel.OnResume();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            if (IsViewLoaded)
                ViewModel.OnPause();
        }

		void AddIndicatorView()
		{
			_activityView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			_activityView.Alpha = 0.5f;
			_activityView.HidesWhenStopped = true;
			_activityView.BackgroundColor = UIColor.Black;
			View.AddIfNotNull(_activityView);
			View.AddConstraints(
				_activityView.AtTopOf(View),
				_activityView.AtLeftOf(View),
				_activityView.AtRightOf(View),
				_activityView.AtBottomOf(View)
			);
		}

  //      private void ToggleLoaderOverlay(bool hasToShow)
		//{
		//	if (_loader != null)
		//	{
  //              OnLoaderOverlay(_loader, false);
		//	}
		//	if (hasToShow)
		//	{
		//		_loader = new LoadingOverlay(this.View.Bounds);
  //              OnLoaderOverlay(_loader, true);
		//	}
		//}

        //protected virtual void OnLoaderOverlay(LoadingOverlay overlay, bool hasToShow)
        //{
        //    if (hasToShow)
        //    {
        //        this.View.AddSubview(_loader);
        //    } 
        //    else 
        //    {
        //        _loader.Hide();
        //        _loader.RemoveFromSuperview();
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
			if (disposing)
			{
				ViewModel?.OnDestroy();
			}

            base.Dispose(disposing);
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}
