using System;
using Android.App;
using MvvmCross.Droid.FullFragging.Fragments;

namespace ColoradoRoads.Droid
{
	public interface IHolder : IApperance
	{
		MvxFragment GetCachedFragment(Type fragmentType);
		IHolderFragment GetHolderFragment();
		FragmentManager GetFragmentManager();
	}
}
