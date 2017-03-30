using System;
namespace ColoradoRoads.iOS
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ContainerElementAttribute : Attribute
	{
		public bool IsContainerElement { get; private set; }

		public ContainerElementAttribute()
		{
			IsContainerElement = true;
		}
	}
}
