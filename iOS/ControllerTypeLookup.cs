using System;
using System.Collections.Generic;
using System.Linq;
using ColoradoRoads.iOS.Utils.Attributes;
using MvvmCross.Platform.IoC;

namespace ColoradoRoads.iOS
{
	public interface IControllerTypeLookup
	{
		bool TryGetControllerType(Type viewModelType, out Type fragmentType);
	}

	public class ControllerTypeLookup : IControllerTypeLookup
	{
		public readonly IDictionary<string, Type> _controllerLookup = new Dictionary<string, Type>();

		public ControllerTypeLookup()
		{
			_controllerLookup =
				(from type in GetType().Assembly.ExceptionSafeGetTypes()
				 where !type.IsAbstract
					 && !type.IsInterface
					 && type.IsContainerElement()
					 && type.Name.EndsWith("View", StringComparison.CurrentCulture)
				 select type).ToDictionary(GetStrippedName);
		}

		public bool TryGetControllerType(Type viewModelType, out Type fragmentType)
		{
			var strippedName = GetStrippedName(viewModelType);

			if (!_controllerLookup.ContainsKey(strippedName))
			{
				fragmentType = null;

				return false;
			}

			fragmentType = _controllerLookup[strippedName];

			return true;
		}

		private string GetStrippedName(Type type)
		{
			return type.Name
					   .Replace("ViewModel", "")
					   .Replace("View", "");
		}
	}
}
