using System;
using System.Collections.Generic;
using System.Linq;
using ColoradoRoads.Api;
using ColoradoRoads.Models;

namespace ColoradoRoads.Dummy
{
	public static class DummyModels
	{
		static Random random = new Random();

		public static List<string> WeatherIcons = new List<string>
		{
			"https://www.mikeafford.com/store/store-images/weather-icon-font-ms01preview.png",
			"https://cdn3.iconfinder.com/data/icons/weather-and-forecast/51/Weather_icons_grey-03-512.png",
			"https://cdn1.iconfinder.com/data/icons/weather-18/512/rain_and_sun-512.png",
			"https://maxcdn.icons8.com/Share/icon/Weather//partly_cloudy_night1600.png",
			"http://simpleicon.com/wp-content/uploads/cloudy.png"
		};

		public static List<RoadConditionNotificationModel> RoadConditionNotificationModel = Enumerable.Range(0, 5).Select(index => new RoadConditionNotificationModel
		{
			Icon = @"http://icons.iconarchive.com/icons/custom-icon-design/mono-general-1/512/alert-icon.png",
			Title = $"TRACTION LAW - {index}",
			Description = $"I-70{index} ajsdajskd asdasd asd asd a sd   adasdasdasd asdasdasdasd asdasdasd"
		}).ToList();

		public static LocationsModel FavouritesLocationModel = new LocationsModel
		{
			Items = Enumerable.Range(0, 5).Select(index => new IdDescriptionListItemModel
			{
				Id = index,
				Description = $"Location - {index}"
			}).ToList()
		};

		public static LocationsModel AllLocationModel = new LocationsModel
		{
			Items = Enumerable.Range(0, 15).Select(index => new IdDescriptionListItemModel
			{
				Id = index,
				Description = $"Location - {index}"
			}).ToList()
		};

		public static TravelTimeSummaryModel TravelTimeSummaryModel(string type) => new TravelTimeSummaryModel
		{
			ForwardDirectionTitle = type.Equals("I-70") ? "EASTBOUND SUMMARY" : "NORTHBOUND SUMMARY",
			ForwardDirectionSegmentsList = Enumerable.Range(0, 5).Select(index => new TravelTimeSummaryListItem {
				Description = $"Position{index} to Position{index+1}",
				TravelTime = $"{index}2 min",
				AverageSpeed = $"5{index}mph"
			}).ToList(),
			BackwardDirectionTitle = type.Equals("I-70") ? "WESTBOUND SUMMARY" : "SOUTHBOUND SUMMARY",
			BackwardDirectionSegmentsList = Enumerable.Range(0, 5).Reverse().Select(index => new TravelTimeSummaryListItem
			{
				Description = $"Position{index+1} to Position{index}",
				TravelTime = $"{index}2 min",
				AverageSpeed = $"5{index}mph"
			}).ToList(),
		};

		public static WeatherNotificationModel WeatherNotificationModel = new WeatherNotificationModel
		{
			Items = Enumerable.Range(0, 15).Select(index => new WeatherNotification
			{
				Id = index,
				LocationName = $"Location{index}",
				Exits = $"Exit{index} & Exit{index + 1}",
				WeatherIcon = WeatherIcons[random.Next(0, WeatherIcons.Count)],
				Temperature = $"High: 5{random.Next(0,9)}F / Low: 3{random.Next(0, 9)}F ",
				DirectionTo = new Direction 
				{ 
					Title = "Glenwood Springs to C-470",
					Description = $"{random.Next(1,5)}H {random.Next(0, 5)}1 MIN"
				},
				DirectionFrom = new Direction
				{
					Title = "C-470 to Glenwood Springs",
					Description = $"{random.Next(1, 5)}H {random.Next(0, 5)}1 MIN"
				}
			}).ToList()
		};
	}
}
