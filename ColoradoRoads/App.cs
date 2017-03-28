using System;
using System.Reflection;
using GloriumTech.Plugins.Utils;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace ColoradoRoads
{
	public class App : MvxApplication
	{
		public const int MOCK_SERVICE_LEVEL = -1; // 0: Real service, 1: Mock service, -1: Hybrid - if overload method exist, we use it
		public const bool USE_LOCAL_HOST = false;
		public const bool USE_API_VERSIONING = true;

		public override void Initialize()
		{
			CreatableTypes()
				 .EndingWith("Config")
				 .AsInterfaces()
				 .RegisterAsMultiInstance();
			CreatableTypes()
				.EndingWith("Service")
				.DoesNotInherit(typeof(IRestApiService))
                //.DoesNotInherit(typeof(Service))
				.AsInterfaces()
				.RegisterAsLazySingleton();
			CreatableTypes()
				.EndingWith("Utility")
				.AsInterfaces()
				.RegisterAsMultiInstance();


#if (DEBUG)
			if (MOCK_SERVICE_LEVEL == 0)
			{
				Mvx.LazyConstructAndRegisterSingleton<IRestApiService, RestApiService>();
			}
			else
			{
				Mvx.LazyConstructAndRegisterSingleton<IRestApiService, DummyRestApiService>();
			}

#else
            Mvx.LazyConstructAndRegisterSingleton<IManagementApiService, RestApiService>();
#endif

			RegisterAppStart(new CustomAppStart());
		}

		protected override IMvxViewModelLocator CreateDefaultViewModelLocator()
		{
			var locator = base.CreateDefaultViewModelLocator();

			if (!Mvx.CanResolve<IMvxViewModelLocator>())
			{
				Mvx.RegisterSingleton<IMvxViewModelLocator>(locator);
			}
			return locator;
		}
	}
}
