using System;
using MvvmCross.Core.ViewModels;

namespace ColoradoRoads.Droid
{
	public interface IApperance
	{
		void Show(object fragment, Type fragmentType);
		void Close(IMvxViewModel viewModel);
	}
}
