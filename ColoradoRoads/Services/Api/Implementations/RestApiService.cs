using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ColoradoRoads.Api;
using ColoradoRoads.Models;

namespace ColoradoRoads
{
	public class RestApiService : RestApiServiceProvider, IRestApiService
	{
		public virtual Task<LocationsModel> GetAllLocations()
		{
			throw new NotImplementedException();
		}

		public virtual Task<LocationsModel> GetFavouriteLocations()
		{
			throw new NotImplementedException();
		}

		public virtual Task<List<RoadConditionNotificationModel>> GetNotifications()
		{
			throw new NotImplementedException();
		}

		public virtual Task<TravelTimeSummaryModel> GetTravelSummary(string type)
		{
			throw new NotImplementedException();
		}

		public virtual Task<List<WeatherNotification>> GetWeatherNotificationModel()
		{
			throw new NotImplementedException();
		}
	}
}
