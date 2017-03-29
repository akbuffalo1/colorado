using System;
using System.Collections.Generic;

namespace ColoradoRoads.Models
{
	public class TravelTimeSummaryModel
	{
		public string ForwardDirectionTitle { get; set; }
		public List<TravelTimeSummaryListItem> ForwardDirectionSegmentsList { get; set; }
		public string BackwardDirectionTitle { get; set; }
		public List<TravelTimeSummaryListItem> BackwardDirectionSegmentsList { get; set; }
	}

	public class TravelTimeSummaryListItem
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string TravelTime { get; set; }
		public string AverageSpeed { get; set; }
	}
}
