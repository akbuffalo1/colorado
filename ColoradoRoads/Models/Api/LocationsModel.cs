using System;
using System.Collections.Generic;
using ColoradoRoads.Models;
using Newtonsoft.Json;

namespace ColoradoRoads.Api
{
	[JsonObject]
	public class LocationsModel
	{
		public List<IdDescriptionListItemModel> Items { get; set; }
	}
}
