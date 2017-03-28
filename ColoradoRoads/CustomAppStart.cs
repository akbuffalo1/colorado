using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace ColoradoRoads
{
	public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
	{
		public void Start(object hint = null)
		{
			ShowViewModel<HomeViewModel>();
		}
	}
}
