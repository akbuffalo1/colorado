using System;
using System.Collections.Generic;
using System.Linq;
using ColoradoRoads.Models;
using ColoradoRoads.ViewModels.Base;
using MvvmCross.Core.ViewModels;

namespace ColoradoRoads.ViewModels
{
	public class LocationsListViewModel : ViewModelBase
	{
		public List<AddEditDeleteListItemDataHolder<IdDescriptionListItemModel>> LocationsList { get; set; }

		public LocationsListViewModel()
		{
		}

		protected async override void ObtainAndPrepareData()
		{
			var locations = await ServerCommandWrapper(_serverApiService.Value.GetAllLocations);
			if (locations?.Items?.Count > 0)
			{ 
				LocationsList = locations.Items.Select(item => new AddEditDeleteListItemDataHolder<IdDescriptionListItemModel>(item)
				{
					SelectItemCommand = new MvxCommand(() => HandleFavoritesSelectItemCommand(item))
				}).ToList();
			}
		}

		void HandleFavoritesSelectItemCommand(IdDescriptionListItemModel item)
		{
			ShowViewModel<LocationDetailsViewModel>(new { Id = item.Id });
		}
	}
}
