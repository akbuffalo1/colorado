using System;
using System.Collections.Generic;
using ColoradoRoads.Models;
using ColoradoRoads.ViewModels.Base;
using MvvmCross.Core.ViewModels;

namespace ColoradoRoads.ViewModels
{
	public class TravelTimeSummaryViewModel : ViewModelBase, IInitViewModel<string>
	{
		public string ForwardDirectionTitle { get; set; }
		public List<TravelTimeSummaryListItem> ForwardDirectionSegmentsList { get; set; }
		public string BackwardDirectionTitle { get; set; }
		public List<TravelTimeSummaryListItem> BackwardDirectionSegmentsList { get; set; }

		public IMvxCommand BackCommand { get; set; }

		string summaryType;
		public TravelTimeSummaryViewModel()
		{
			BackCommand = new MvxCommand(HandleBackCommand);
		}

		async void ObtainAndPrepareData()
		{
			var result = await ServerCommandWrapper(_serverApiService.Value.GetTravelSummary, summaryType);
			if (result != null)
			{
				ForwardDirectionTitle = result.ForwardDirectionTitle;
				ForwardDirectionSegmentsList = result.ForwardDirectionSegmentsList;
				BackwardDirectionTitle = result.BackwardDirectionTitle;
				BackwardDirectionSegmentsList = result.BackwardDirectionSegmentsList;
			}
		}

		void HandleBackCommand()
		{
			Close(this);
		}

		public override void OnResume()
		{
			base.OnResume();
			ObtainAndPrepareData();
		}

		public void Init(string type)
		{
			summaryType = type;
		}

		public void RealInit(string type)
		{
		}
	}
}
