using System;
using ColoradoRoads.Platform;
using Foundation;

namespace ColoradoRoads.iOS
{
	public class FlagStore : IFlagStore
	{
		public bool IsSet(string key)
		{
			return NSUserDefaults.StandardUserDefaults.BoolForKey(key);
		}

		public void UnsetAll()
		{
			throw new System.NotImplementedException();
		}

		public void Set(string key)
		{
			NSUserDefaults.StandardUserDefaults.SetBool(true, key);
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}

		public void Unset(string key)
		{
			NSUserDefaults.StandardUserDefaults.SetBool(false, key);
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}
	}
}
