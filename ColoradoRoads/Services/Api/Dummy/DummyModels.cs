using System;
using System.Collections.Generic;
using System.Linq;
using ColoradoRoads.Models;

namespace ColoradoRoads.Dummy
{
	public static class DummyModels
	{
		public static List<RoadConditionNotificationModel> RoadConditionNotificationModel = Enumerable.Range(0, 5).Select(index => new RoadConditionNotificationModel
		{
			Icon = @"http://icons.iconarchive.com/icons/custom-icon-design/mono-general-1/512/alert-icon.png",
			Title = $"TRACTION LAW - {index}",
			Description = $"I-70{index} ajsdajskd asdasd asd asd a sd   adasdasdasd asdasdasdasd asdasdasd"
		}).ToList();

		public static FavouriteLocationsModel FavouritesLocationModel = new FavouriteLocationsModel
		{
			Items = Enumerable.Range(0, 5).Select(index => new IdDescriptionListItemModel
			{
				Id = index,
				Description = $"Location{index}"
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
	}
}
