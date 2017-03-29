using System;
using System.Collections.Generic;
using System.Linq;
using ColoradoRoads.Models;
using ColoradoRoads.ViewModels;
using ColoradoRoads.ViewModels.Base;
using MvvmCross.Core.ViewModels;

namespace ColoradoRoads
{
	public class HomeViewModel : ViewModelBase
	{
		public List<RoadConditionNotificationModel> NotificationModelsList { get; set; }
		public List<AddEditDeleteListItemDataHolder<IdDescriptionListItemModel>> FavouriteLocations { get; set; }

		public IMvxCommand ShowMainMenuCommand { get; set; }

		public HomeViewModel()
		{
			ShowMainMenuCommand = new MvxCommand(HandleShowMainMenuCommand);
		}

		async void ObtainAndPrepareData()
		{
			var result = await ServerCommandWrapper(_serverApiService.Value.GetNotifications);
			if (result?.Count > 0)
			{
				NotificationModelsList = result;
			}

			var favLocations = await ServerCommandWrapper(_serverApiService.Value.GetFavouriteLocations);
			if (favLocations?.Items?.Count > 0)
			{
				FavouriteLocations = favLocations.Items.Select(item => new AddEditDeleteListItemDataHolder<IdDescriptionListItemModel>(item)
				{ 
					SelectItemCommand = new MvxCommand(() => HandleFavoritesSelectItemCommand(item))
				}).ToList();
			}
		}

		void HandleFavoritesSelectItemCommand(IdDescriptionListItemModel itemModel)
		{
			ShowViewModel<LocationDetailsViewModel>(new { Id = itemModel.Id });
		}

		void HandleShowMainMenuCommand()
		{
			ShowViewModel<MainMenuViewModel>();
		}

		public override void OnResume()
		{
			base.OnResume();
			ObtainAndPrepareData();
		}
	}
}
