using System;
using Chance.MvvmCross.Plugins.UserInteraction;
using Chance.MvvmCross.Plugins.UserInteraction.Touch;
using ColoradoRoads.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.Plugins;
using UIKit;

namespace ColoradoRoads.iOS
{
	public class Setup : MvxIosSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
		{
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new MvxDebugTrace();
		}

		protected override void InitializePlatformServices()
		{
			base.InitializePlatformServices();

			Mvx.LazyConstructAndRegisterSingleton<IPlatform, Platfrom>();
			Mvx.LazyConstructAndRegisterSingleton<IFlagStore, FlagStore>();
		}

		public override void InitializeSecondary()
		{
			base.InitializeSecondary();
		}

		protected override IMvxPluginManager CreatePluginManager()
		{
			return new MvxPluginManager();
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}

		public override void LoadPlugins(IMvxPluginManager pluginManager)
		{
			base.LoadPlugins(pluginManager);
			Mvx.LazyConstructAndRegisterSingleton<IUserInteraction, UserInteraction>();
		}

		protected override void InitializeLastChance()
		{
			base.InitializeLastChance();

		}

		protected override IMvxIosViewPresenter CreatePresenter()
		{
			var presenter = new Presenter((MvxApplicationDelegate)ApplicationDelegate, Window);
			return presenter;
		}
	}
}
