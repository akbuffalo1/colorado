using System;
using System.Collections.Generic;
using System.Linq;
using ColoradoRoads.Api;
using ColoradoRoads.Models;
using ColoradoRoads.ViewModels;
using ColoradoRoads.ViewModels.Base;
using MvvmCross.Core.ViewModels;

namespace ColoradoRoads
{
	public class HomeViewModel : ViewModelBase
	{
		public List<RoadConditionNotificationModel> NotificationModelsList { get; set; }
		public List<WeatherNotification> WeatherNotificationModelsList { get; set; }
		public List<AddEditDeleteListItemDataHolder<IdDescriptionListItemModel>> FavouriteLocations { get; set; }

		public IMvxCommand ShowMainMenuCommand { get; set; }
		public IMvxCommand ShowAllLocationsCommand { get; set; }

		public HomeViewModel()
		{
			ShowMainMenuCommand = new MvxCommand(HandleShowMainMenuCommand);
			ShowAllLocationsCommand = new MvxCommand(HandleShowAllLocationsCommand);
		}

		protected override async void ObtainAndPrepareData()
		{
			var notifications = await ServerCommandWrapper(_serverApiService.Value.GetNotifications);
			if (notifications?.Count > 0)
			{
				NotificationModelsList = notifications;
			}

			var favLocations = await ServerCommandWrapper(_serverApiService.Value.GetFavouriteLocations);
			if (favLocations?.Items?.Count > 0)
			{
				FavouriteLocations = favLocations.Items.Select(item => new AddEditDeleteListItemDataHolder<IdDescriptionListItemModel>(item)
				{ 
					SelectItemCommand = new MvxCommand(() => HandleFavoritesSelectItemCommand(item))
				}).ToList();
			}

			var weatherNotificationModelsList = await ServerCommandWrapper(_serverApiService.Value.GetWeatherNotificationModel);
			if (weatherNotificationModelsList?.Count > 0)
			{
				WeatherNotificationModelsList = weatherNotificationModelsList;
			}

		}

		void HandleFavoritesSelectItemCommand(IdDescriptionListItemModel itemModel)
		{
			ShowViewModel<LocationDetailsViewModel>(new { Id = itemModel.Id });
		}

		void HandleShowAllLocationsCommand()
		{
			ShowViewModel<LocationsListViewModel>();
		}

		void HandleShowMainMenuCommand()
		{
			ShowViewModel<MainMenuViewModel>();
		}
	}
}
