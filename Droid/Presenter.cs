using System;
using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.FullFragging.Fragments;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace ColoradoRoads.Droid
{
	public class Presenter : MvxAndroidViewPresenter
	{
		protected readonly IMvxViewModelLoader _viewModelLoader;
		protected readonly IFragmentTypeLookup _fragmentTypeLookup;
		protected MvxDialogFragment _dialogFragment;

		protected IHolder fragmentHolderActivity;

		public Presenter(IMvxViewModelLoader viewModelLoader, IFragmentTypeLookup fragmentTypeLookup)
		{
			_fragmentTypeLookup = fragmentTypeLookup;
			_viewModelLoader = viewModelLoader;
		}

		protected override void Show(Intent intent)
		{
			if (Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.GetType() != typeof(SplashScreen) &&
				intent.Component.ClassName.Contains("HomeView"))
			{
				intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
			}

			base.Show(intent);
		}

		public override void Show(MvxViewModelRequest request)
		{
			Type fragmentType;

			fragmentHolderActivity = Activity as IHolder;

			if (fragmentHolderActivity == null ||
				!_fragmentTypeLookup.TryGetFragmentType(request.ViewModelType, out fragmentType))
			{
				base.Show(request);
				return;
			}
			else
				ShowFragmnetFromCache(request, fragmentType);
		}

		public override void Close(IMvxViewModel viewModel)
		{
			TryCloseMvxDialog(viewModel);

			fragmentHolderActivity?.Close(viewModel);

			base.Close(viewModel);
		}

		void TryCloseMvxDialog(IMvxViewModel viewModel)
		{
			if (_dialogFragment != null)
			{
				if (_dialogFragment.ViewModel.GetType() == viewModel.GetType())
				{
					_dialogFragment.Dismiss();
					_dialogFragment = null;
					return;
				}
			}
		}

		protected virtual void ShowFragmnetFromCache(MvxViewModelRequest request, Type fragmentType)
		{
			MvxFragment fragment = GetCachedFragmentByType(fragmentType);

			if (fragment != null)
				ShowMvxFragment(fragment, request, fragmentType, fragment != null);
			else
			{
				var newFragment = CreateFragmentByType(fragmentType);

				if (newFragment is MvxDialogFragment)
				{
					_dialogFragment = newFragment as MvxDialogFragment;
					ShowMvxDialogFragment(_dialogFragment, request, fragmentType);
				}
				else
					ShowMvxFragment(newFragment as MvxFragment, request, fragmentType, fragment != null);
			}
		}

		protected virtual MvxFragment GetCachedFragmentByType(Type fragmentType)
		{
			return fragmentHolderActivity.GetCachedFragment(fragmentType);
		}

		protected virtual object CreateFragmentByType(Type fragmentType)
		{
			return Activator.CreateInstance(fragmentType);
		}

		protected IMvxViewModel GetViewModel(MvxFragment fragment, MvxViewModelRequest request)
		{
			return !(fragment is IHolderFragment) ? _viewModelLoader.LoadViewModel(request, null) : null;
		}

		protected virtual void ShowMvxDialogFragment(MvxDialogFragment fragment, MvxViewModelRequest request, Type fragmentType)
		{
			fragment.ViewModel = _viewModelLoader.LoadViewModel(request, null);
			fragment.Show(fragmentHolderActivity.GetFragmentManager(), fragmentType.Name);
		}

		protected virtual void ShowMvxFragment(MvxFragment fragment, MvxViewModelRequest request, Type fragmentType, bool isCachedInActivity)
		{
			if (fragment.ViewModel == null)
				fragment.ViewModel = GetViewModel(fragment, request);

			if (isCachedInActivity && fragment is INoCacheViewModel)
				fragment.ViewModel = _viewModelLoader.LoadViewModel(request, null);
			else
			{
				if (fragment is IHolderFragment)
				{
					fragmentHolderActivity.Show(fragment, fragmentType);

					if (fragment.ViewModel == null)
						fragment.ViewModel = _viewModelLoader.LoadViewModel(request, null);

					return;
				}

				if (fragment is IChildFragment)
				{
					var fragmentHolder = fragmentHolderActivity.GetHolderFragment();
					if (fragmentHolder != null)
					{
						fragmentHolder?.Show(fragment, fragmentType);
						return;
					}
				}
			}

			fragmentHolderActivity.Show(fragment, fragmentType);
		}
	}
}
