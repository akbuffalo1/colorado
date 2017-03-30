using System;
using Android.Content;
using ColoradoRoads.Platform;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace ColoradoRoads.Droid.ServicesImpl
{
	public class FlagStore : IFlagStore
	{
		private const string FlagStorageName = "ColoradpFlagSore";

		public void Set(string key)
		{
			var act = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			act.GetSharedPreferences(FlagStorageName, FileCreationMode.Private).Edit().PutBoolean(key, true).Apply();
		}

		public void Unset(string key)
		{
			var act = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			act.GetSharedPreferences(FlagStorageName, FileCreationMode.Private).Edit().PutBoolean(key, false).Apply();
		}

		public bool IsSet(string key)
		{
			var act = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			return act.GetSharedPreferences(FlagStorageName, FileCreationMode.Private).GetBoolean(key, false);
		}

		public void UnsetAll()
		{
			var act = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			var preferences = act.GetSharedPreferences(FlagStorageName, FileCreationMode.Private);
			var editor = preferences.Edit();
			editor.Clear();
			editor.Commit();
		}
	}
}
