using System;
using System.Linq;
using UIKit;

namespace ColoradoRoads.iOS.Utils.Attributes
{
	public static class AttributeEx
	{
		public static bool IsContainerElement(this Type fromFragmentType)
		{
			var attributes = fromFragmentType.GetCustomAttributes(typeof(ContainerElementAttribute), true);
			if (attributes.Any())
			{
				var attr = attributes.Cast<ContainerElementAttribute>().First();
				return attr.IsContainerElement;
			}
			return false;
		}

		public static bool IsNotCachable(this UIViewController viewController)
		{
			var attributes = viewController.GetType().GetCustomAttributes(typeof(NotCachableAttribute), true);
			if (attributes.Any())
			{
				var attr = attributes.Cast<NotCachableAttribute>().First();
				return attr.NotCachable;
			}
			return false;
		}
	}
}
