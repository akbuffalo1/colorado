using System;
using ColoradoRoads.ViewModels.Base;

namespace ColoradoRoads.ViewModels
{
	public class LocationDetailsViewModel : ViewModelBase, IInitViewModel<int>
	{
		int locationId = -1;
		public LocationDetailsViewModel()
		{
		}

		async void ObtainAndPrepareData()
		{
			
		}

		public void Init(int Id)
		{
			locationId = Id;
		}

		public void RealInit(int locationId)
		{
			
		}

		public void Init(string parameter)
		{
			
		}

		public override void OnResume()
		{
			base.OnResume();
			ObtainAndPrepareData();
		}
	}
}
