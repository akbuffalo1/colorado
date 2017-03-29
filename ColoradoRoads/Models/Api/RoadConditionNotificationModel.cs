using System;
using ColoradoRoads.Enums;

namespace ColoradoRoads.Models
{
	public class RoadConditionNotificationModel
	{
		public string Icon { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public NotificationType Type { get; set; }
	}
}
