using System;
using System.Collections.Generic;
using ColoradoRoads.Interfaces;
using Newtonsoft.Json;

namespace ColoradoRoads.Models
{
	[JsonObject]
	public class IdDescriptionListItemModel : IIdentifiable
	{ 
		public int Id { get; set; } 
		public string Description { get; set; } 
	}
}
