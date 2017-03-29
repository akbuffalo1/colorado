using System;
using Android.Content.PM;
using Android.Views;
using ColoradoRoads.ViewModels.Base;
using MvvmCross.Droid.FullFragging.Fragments;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Support.V7.AppCompat;
using Plugin.Permissions;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace ColoradoRoads.Droid
{
	public abstract class BaseActivity<TViewModel> : MvxAppCompatActivity<TViewModel>, ITitle
		where TViewModel : ViewModelBase
	{
		public SupportToolbar toolbar;

		protected abstract int LayoutId();

		protected virtual int MenuId()
		{
			return -1;
		}

		protected virtual int HomeIconId()
		{
			return -1;
		}

		public new string Title
		{
			set
			{
				if (SupportActionBar != null && !string.IsNullOrEmpty(value))
					SupportActionBar.Title = value;
			}
		}

		protected override void OnCreate(Android.OS.Bundle bundle)
		{
			var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
			setup.EnsureInitialized();

			base.OnCreate(bundle);

			SetContentView(LayoutId());

			toolbar = FindViewById<SupportToolbar>(Resource.Id.ToolBar);

			if (toolbar != null)
			{
				SetSupportActionBar(toolbar);
				if (HomeIconId() > -1)
					SupportActionBar.SetHomeAsUpIndicator(HomeIconId());
				SupportActionBar.SetDisplayShowHomeEnabled(true);
				SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			}


			this.AddLinqBinding(ViewModel, vm => vm.Title, (value) =>
			{
				Title = value;
			});
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			if (MenuId() != -1)
				MenuInflater.Inflate(MenuId(), menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					OnBackPressed();
					return true;
			}
			return base.OnOptionsItemSelected(item);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		public override void OnBackPressed()
		{
			base.OnBackPressed();
			//OverridePendingTransition(Resource.Animator.fadein, Resource.Animator.fadeout);
		}

		protected override void OnResume()
		{
			base.OnResume();
			ViewModel.OnResume();
		}

		protected override void OnPause()
		{
			base.OnPause();
			ViewModel.OnPause();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			ViewModel.OnDestroy();
		}

		public void ShowFragment(MvxFragment fragment, ViewModelBase viewModel, int frameContentId, string tag = null)
		{
			fragment.ViewModel = viewModel;
			RunOnUiThread(() =>
			{
				FragmentManager.BeginTransaction().Replace(frameContentId, fragment, tag).Commit();
			});
		}
	}
}
