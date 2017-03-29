using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ColoradoRoads.Models;

namespace ColoradoRoads
{
	public interface IRestApiService
	{
		Task<List<RoadConditionNotificationModel>> GetNotifications();
		Task<FavouriteLocationsModel> GetFavouriteLocations();
		Task<TravelTimeSummaryModel> GetTravelSummary(string type);
	}
}
