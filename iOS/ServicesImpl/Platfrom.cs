using System;
using ColoradoRoads.Platform;
using ToastIOS;

namespace ColoradoRoads.iOS
{
	public class Platfrom : IPlatform
	{
		public void ShowToast(string message)
		{
			Toast.MakeText(message).Show();
		}
	}
}
