using System;
using ColoradoRoads.ViewModels.Base;
using MvvmCross.Core.ViewModels;

namespace ColoradoRoads.ViewModels
{
	public class MainMenuViewModel : ViewModelBase
	{
		public IMvxCommand CloseSelfCommand { get; set; }

		public IMvxCommand OpenI70CurrentTravelInfoCommand { get; set; }
		public IMvxCommand OpenI70TravelTimeSummaryCommand{ get; set; }
		public IMvxCommand OpenI70ListOfLocationsCommand { get; set; }
		public IMvxCommand OpenI70CoalitionCommand { get; set; }

		public IMvxCommand OpenI25CurrentTravelInfoCommand { get; set; }
		public IMvxCommand OpenI25TravelTimeSummaryCommand { get; set; }

		public IMvxCommand OpenMapCommand { get; set; }
		public IMvxCommand OpenSevereAlertsCommand { get; set; }
		public IMvxCommand OpenRoadWorkAlertsCommand { get; set; }
		public IMvxCommand OpenTruckerInfoCommand { get; set; }
		public IMvxCommand OpenCdotNewsCommand { get; set; }
		public IMvxCommand OpenTwitterFeedCommand { get; set; }

		public IMvxCommand OpenAboutColoradoRoadsCommand { get; set; }
		public IMvxCommand OpenFrequentryAskedQuestionsCommand { get; set; }
		public IMvxCommand OpenWinterDrivingTipsCommand { get; set; }
		public IMvxCommand OpenTermsOfServiceCommand { get; set; }

		public MainMenuViewModel()
		{
			CloseSelfCommand = new MvxCommand(() => Close(this));

			OpenI70CurrentTravelInfoCommand = new MvxCommand(HandleOpenI70CurrentTravelInfoCommand);
			OpenI70TravelTimeSummaryCommand = new MvxCommand(HandleOpenI70TravelTimeSummaryCommand);
			OpenI70ListOfLocationsCommand = new MvxCommand(HandleOpenI70ListOfLocationsCommand);
			OpenI70CoalitionCommand = new MvxCommand(HandleOpenI70CoalitionCommand);

			OpenI25CurrentTravelInfoCommand = new MvxCommand(HandleOpenI25CurrentTravelInfoCommand);
			OpenI25TravelTimeSummaryCommand = new MvxCommand(HandleOpenI25TravelTimeSummaryCommand);

			OpenMapCommand = new MvxCommand(HandleOpenMapCommand);
			OpenSevereAlertsCommand = new MvxCommand(HandleOpenSevereAlertsCommand);
			OpenRoadWorkAlertsCommand = new MvxCommand(HandleOpenRoadWorkAlertsCommand);
			OpenTruckerInfoCommand = new MvxCommand(HandleOpenTruckerInfoCommand);
			OpenCdotNewsCommand = new MvxCommand(HandleOpenCdotNewsCommand);
			OpenTwitterFeedCommand = new MvxCommand(HandleOpenTwitterFeedCommand);

			OpenAboutColoradoRoadsCommand = new MvxCommand(HandleOpenAboutColoradoRoadsCommand);
			OpenFrequentryAskedQuestionsCommand = new MvxCommand(HandleOpenFrequentryAskedQuestionsCommand);
			OpenWinterDrivingTipsCommand = new MvxCommand(HandleOpenWinterDrivingTipsCommand);
			OpenTermsOfServiceCommand = new MvxCommand(HandleOpenTermsOfServiceCommand);
		}

		void HandleOpenI70CurrentTravelInfoCommand()
		{
			ShowViewModel<HomeViewModel>();
		}

		void HandleOpenI70TravelTimeSummaryCommand()
		{
			ShowViewModel<TravelTimeSummaryViewModel>(new { type = "I-25" });
		}

		void HandleOpenI70ListOfLocationsCommand()
		{
			ShowViewModel<LocationsListViewModel>();
		}

		void HandleOpenI70CoalitionCommand()
		{
			throw new NotImplementedException();
		}
	
		void HandleOpenI25CurrentTravelInfoCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenI25TravelTimeSummaryCommand()
		{
			ShowViewModel<TravelTimeSummaryViewModel>(new { type = "I-25" });
		}

		void HandleOpenMapCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenSevereAlertsCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenRoadWorkAlertsCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenTruckerInfoCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenCdotNewsCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenTwitterFeedCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenAboutColoradoRoadsCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenFrequentryAskedQuestionsCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenWinterDrivingTipsCommand()
		{
			throw new NotImplementedException();
		}

		void HandleOpenTermsOfServiceCommand()
		{
			throw new NotImplementedException();
		}
	}
}
