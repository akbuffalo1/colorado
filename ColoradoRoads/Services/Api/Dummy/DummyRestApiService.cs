using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColoradoRoads.Api;
using ColoradoRoads.Dummy;
using ColoradoRoads.Models;

namespace ColoradoRoads
{
	public class DummyRestApiService : RestApiService
	{
		public override Task<List<RoadConditionNotificationModel>> GetNotifications()
		{
		//	if (App.MOCK_SERVICE_LEVEL == -1) return base.GetNotifications();
			return Task.Run(() =>
			{
				return DummyModels.RoadConditionNotificationModel;
			});
		}

		public override Task<LocationsModel> GetFavouriteLocations()
		{
			//	if (App.MOCK_SERVICE_LEVEL == -1) return base.GetFavouriteLocations();
			return Task.Run(() =>
			{
				return DummyModels.FavouritesLocationModel;
			});
		}

		public override Task<TravelTimeSummaryModel> GetTravelSummary(string type)
		{
			//	if (App.MOCK_SERVICE_LEVEL == -1) return base.GetFavouriteLocations();
			return Task.Run(() =>
			{
				return DummyModels.TravelTimeSummaryModel(type);
			});
		}

		public override Task<LocationsModel> GetAllLocations()
		{
			//	if (App.MOCK_SERVICE_LEVEL == -1) return base.GetFavouriteLocations();
			return Task.Run(() =>
			{
				return DummyModels.AllLocationModel;
			});
		}

		public override Task<List<WeatherNotification>> GetWeatherNotificationModel()
		{
			//	if (App.MOCK_SERVICE_LEVEL == -1) return base.GetFavouriteLocations();
			return Task.Run(() =>
			{
				return DummyModels.WeatherNotificationModel.Items;
			});
		}
	}
}