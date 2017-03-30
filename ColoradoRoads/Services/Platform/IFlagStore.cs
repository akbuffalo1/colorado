using System;
namespace ColoradoRoads.Platform
{
	public interface IFlagStore
	{
		void Set(string key);
		void Unset(string key);
		bool IsSet(string key);
		void UnsetAll();
	}

	public static class IFlagStoreEx
	{
		public static void ExecuteIfSet(this IFlagStore self, string key, Action act)
		{
			if (self.IsSet(key))
			{
				act();
			}
		}

		public static void ExecuteIfNotSet(this IFlagStore self, string key, Action act)
		{
			if (!self.IsSet(key))
			{
				act();
			}
		}

		public static void ExecuteIfSetOrNot(this IFlagStore self, string key, Action actIfYes, Action actIfNot)
		{
			if (self.IsSet(key))
				actIfYes();
			else
				actIfNot();
		}

		public static void ExecuteIfSetThenUnset(this IFlagStore self, string key, Action act)
		{
			if (self.IsSet(key))
			{
				act();
				self.Unset(key);
			}
		}

		public static void ExecuteIfNotSetThenSet(this IFlagStore self, string key, Action act)
		{
			if (!self.IsSet(key))
			{
				act();
				self.Set(key);
			}
		}
	}
}
