using System;
using System.Collections.Generic;

namespace ColoradoRoads.Api
{
	public class WeatherNotificationModel
	{ 
		public List<WeatherNotification> Items { get; set; }
	}

	public class WeatherNotification
	{
		public int Id { get; set; }
		public string LocationName { get; set; }
		public string Exits { get; set; }
		public string WeatherIcon { get; set; }
		public string Temperature { get; set; }
		public Direction DirectionTo { get; set; }
		public Direction DirectionFrom { get; set; }
	}

	public class Direction
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}
