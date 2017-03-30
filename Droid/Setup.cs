using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using Chance.MvvmCross.Plugins.UserInteraction;
using Chance.MvvmCross.Plugins.UserInteraction.Droid;
using MvvmCross.Platform.IoC;
using System;
using System.Collections.Generic;
using System.Reflection;
using ColoradoRoads.Droid.CustomBindings;
using ColoradoRoads.Droid.Adapters;
using ColoradoRoads.Platform;
using ColoradoRoads.Droid.ServicesImpl;

namespace ColoradoRoads.Droid
{
	public class Setup : MvxAndroidSetup
	{
		public Setup(Context applicationContext) : base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new MvxDebugTrace();
		}

		protected override void InitializeIoC()
		{
			base.InitializeIoC();

			Mvx.ConstructAndRegisterSingleton<IFragmentTypeLookup, FragmentTypeLookup>();
		}

		protected override IMvxAndroidViewPresenter CreateViewPresenter()
		{
			var presenter = Mvx.IocConstruct<Presenter>();

			Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(presenter);

			return presenter;
		}

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			MvxAppCompatSetupHelper.FillTargetFactories(registry);

			registry.RegisterCustomBindingFactory<TopCarouselViewAdapter>("DataSource", binary => new ViewPagerTargetBinding(binary));
			base.FillTargetFactories(registry);
		}

		protected override void InitializeLastChance()
		{
			System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

			base.InitializeLastChance();
		}

		public override void LoadPlugins(IMvxPluginManager pluginManager)
		{
			base.LoadPlugins(pluginManager);
			Mvx.LazyConstructAndRegisterSingleton<IUserInteraction, UserInteraction>();
		}

		protected override void InitializePlatformServices()
		{
			base.InitializePlatformServices();

			Mvx.LazyConstructAndRegisterSingleton<IPlatform, Platfrom>();
			Mvx.LazyConstructAndRegisterSingleton<IFlagStore, FlagStore>();
		}

		protected override void FillValueConverters(IMvxValueConverterRegistry registry)
		{
			foreach (var item in CreatableTypes().EndingWith("Converter"))
				registry.AddOrOverwrite(item.Name, (IMvxValueConverter)Activator.CreateInstance(item));
		}

		protected override IEnumerable<Assembly> AndroidViewAssemblies
		{
			get
			{
				var toReturn = new List<Assembly>(base.AndroidViewAssemblies);
				toReturn.Add(this.GetType().Assembly);
				return toReturn;
			}
		}
	}
}