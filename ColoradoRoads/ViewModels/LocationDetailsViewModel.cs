using System;
using ColoradoRoads.ViewModels.Base;

namespace ColoradoRoads.ViewModels
{
	public class LocationDetailsViewModel : ViewModelBase<int>
	{
		int locationId = -1;
		public LocationDetailsViewModel()
		{
		}

		protected override async void ObtainAndPrepareData()
		{
			
		}
	}
}
