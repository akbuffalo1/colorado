using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ColoradoRoads.Api;
using ColoradoRoads.Models;

namespace ColoradoRoads
{
	public interface IRestApiService
	{
		Task<List<RoadConditionNotificationModel>> GetNotifications();
		Task<LocationsModel> GetFavouriteLocations();
		Task<LocationsModel> GetAllLocations();
		Task<TravelTimeSummaryModel> GetTravelSummary(string type);
	}
}
