using System;
using System.Threading.Tasks;
using MvvmCross.Core.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;

using Chance.MvvmCross.Plugins.UserInteraction;
using MvvmCross.Platform.Platform;
using System.Diagnostics;
using ColoradoRoadse.Exceptions;
using PropertyChanged;
using Newtonsoft.Json;

namespace ColoradoRoads.ViewModels.Base
{
	[ImplementPropertyChanged]
	public class ViewModelBase : MvxViewModel
	{ 
		Lazy<IMvxViewModelLocator> _viewModelLocator = new Lazy<IMvxViewModelLocator>(Mvx.Resolve<IMvxViewModelLocator>);
		protected Lazy<IRestApiService> _serverApiService = new Lazy<IRestApiService>(Mvx.Resolve<IRestApiService>);

		public Dictionary<string, string> Errors { get; set; }
		public List<Validator> Validators { get; set;}
		public virtual bool IsBusy { get; set; }
		public string OnErrorMessage { get; set; }

		public ViewModelBase()
		{

		}

		public virtual string Title
		{
			get { return ""; }
		}


		protected virtual void SetValidators()
		{
		}

		protected bool Validate()
		{
			foreach (var item in Validators)
				UpdateError(item.Validate());

			return Errors.Count == 0;
		}

		protected virtual async Task ServerCommandWrapperParrallel(Func<Task> action)
		{
			try
			{
				IsBusy = true;
				await action();
			}
			catch (Exception ex)
			{
				Mvx.Trace(ex.Message + "PARALEL", ex.StackTrace);
			}
			finally
			{
				IsBusy = false;
			}
		}

		protected virtual async Task ServerCommandWrapper(Func<Task> action)
		{
			if (IsBusy)
			{
				return;
			}

			try
			{
				IsBusy = true;
				await action();
			}
			catch (Exception ex)
			{
				if (ex is UiApiException)
				{
					await Mvx.Resolve<IUserInteraction>().AlertAsync(ex.Message, "Error has occured");
				}
				else
				{
					Mvx.Trace(ex.Message, ex.StackTrace);
				}
			}
			finally
			{
				IsBusy = false;
			}
		}

		protected virtual async Task<TResult> ServerCommandWrapper<TResult>(Func<Task<TResult>> function)
		{
			if (IsBusy)
			{
				return await Task.Run(() => default(TResult));
			}

			try
			{
				IsBusy = true;
				return await function();
			}
			catch (Exception ex)
			{
				if (ex is UiApiException)
				{
					await Mvx.Resolve<IUserInteraction>().AlertAsync(ex.Message, "Error has occured");
				}
				else
				{
					Mvx.Trace(ex.Message, ex.StackTrace);
				}
				return await Task.Run(() => default(TResult));
			}
			finally
			{
				IsBusy = false;
			}
		}

		protected virtual async Task ServerCommandWrapper<T>(Func<T, Task> action, T arg)
		{
			if (IsBusy)
			{
				return;
			}
			try
			{
				IsBusy = true;
				await action(arg);
			}
			catch (Exception ex)
			{
				Mvx.Trace(ex.Message, ex.StackTrace);
			}
			finally
			{
				IsBusy = false;
			}
		}

		protected virtual async Task<TResult> ServerCommandWrapper<TResult, T>(Func<T, Task<TResult>> function, T arg)
		{
			if (IsBusy)
			{
				return await Task.Run(() => default(TResult));
			}

			try
			{
				IsBusy = true;
				return await function(arg);
			}
			catch (Exception ex)
			{
				if (ex is UiApiException)
				{
					await Mvx.Resolve<IUserInteraction>().AlertAsync(ex.Message, "Error has occured");
				}
				else
				{
					Mvx.Trace(ex.Message, ex.StackTrace);
				}
				return await Task.Run(() => default(TResult));
			}
			finally
			{
				IsBusy = false;
			}
		}

		protected void UpdateError(ValidatorDataItem item)
		{
			if (!string.IsNullOrEmpty(item.ValidationText))
			{
				if (!Errors.ContainsKey(item.Name))
					Errors[item.Name] = item.ValidationText;
			}
			else
			{
				if (Errors.ContainsKey(item.Name))
					Errors.Remove(item.Name);
			}

			RaisePropertyChanged(() => Errors);
		}

		protected TViewModel GetViewModel<TViewModel>() where TViewModel : IMvxViewModel
		{
			IMvxViewModel viewModel = _viewModelLocator.Value.Load(typeof(TViewModel), new MvxBundle(), new MvxBundle());
			return (TViewModel)viewModel;
		}

		protected TViewModel GetViewModel<TViewModel>(object parameters) where TViewModel : IMvxViewModel
		{
			IMvxViewModel viewModel = _viewModelLocator.Value.Load(typeof(TViewModel), new MvxBundle(parameters.ToSimplePropertyDictionary()), new MvxBundle());
			return (TViewModel)viewModel;
		}

		protected bool ShowViewModelWithParameters<TViewModel, TInit>(TInit parameter) where TViewModel : class, IMvxViewModel, IInitViewModel<TInit>
		{
			var text = Mvx.Resolve<IMvxJsonConverter>().SerializeObject(parameter);
			return base.ShowViewModel<TViewModel>(new { parameter = text });
		}

		protected new bool ShowViewModel<TViewModel>(object parameterValuesObject, IMvxBundle presentationBundle = null, MvxRequestedBy requestedBy = null)
			where TViewModel : IMvxViewModel
		{
			return Navigate<TViewModel>(() => base.ShowViewModel<TViewModel>(parameterValuesObject, presentationBundle, requestedBy));
		}

		protected new bool ShowViewModel<TViewModel>(IDictionary<string, string> parameterValues, IMvxBundle presentationBundle = null, MvxRequestedBy requestedBy = null)
			where TViewModel : IMvxViewModel
		{
			return Navigate<TViewModel>(() => base.ShowViewModel<TViewModel>(parameterValues, presentationBundle, requestedBy));
		}

		protected new bool ShowViewModel<TViewModel>(IMvxBundle parameterBundle = null, IMvxBundle presentationBundle = null, MvxRequestedBy requestedBy = null)
			where TViewModel : IMvxViewModel
		{
			return Navigate<TViewModel>(() => base.ShowViewModel<TViewModel>(parameterBundle, presentationBundle, requestedBy));
		}

		protected new bool ShowViewModel(Type viewModelType, object parameterValuesObject, IMvxBundle presentationBundle = null, MvxRequestedBy requestedBy = null)
		{
			return Navigate(viewModelType, () => base.ShowViewModel(viewModelType, parameterValuesObject, presentationBundle, requestedBy));
		}

		protected new bool ShowViewModel(Type viewModelType, IDictionary<string, string> parameterValues, IMvxBundle presentationBundle = null, MvxRequestedBy requestedBy = null)
		{
			return Navigate(viewModelType, () => base.ShowViewModel(viewModelType, parameterValues, presentationBundle, requestedBy));
		}

		protected new bool ShowViewModel(Type viewModelType, IMvxBundle parameterBundle = null, IMvxBundle presentationBundle = null, MvxRequestedBy requestedBy = null)
		{
			return Navigate(viewModelType, () => base.ShowViewModel(viewModelType, parameterBundle, presentationBundle, requestedBy));
		}

		private bool Navigate<TViewModel>(Func<bool> showViewModel)
			where TViewModel : IMvxViewModel
		{
			return Navigate(typeof(TViewModel), showViewModel);
		}

		private bool Navigate(Type viewModelType, Func<bool> showViewModel)
		{
			showViewModel();
			return true;
		}

		void DebugMethod([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
		{
			Debug.WriteLine(string.Format("{1} of {0}", memberName, this.GetType().FullName));
		}

		public virtual void OnResume()
		{
			DebugMethod();
			ObtainAndPrepareData();
		}

		public virtual void OnPause()
		{
			DebugMethod();
		}

		public virtual void OnDestroy()
		{
			DebugMethod();
		}

		protected virtual void ObtainAndPrepareData() { }
	}

	public abstract class ViewModelBase<TInit> : ViewModelBase, IInitViewModel<TInit>
	{
		protected TInit InitData;
		public virtual void Init(TInit initData) 
		{
			InitData = initData;
		}

		public virtual void Init(string initData) 
		{
			if (!string.IsNullOrEmpty(initData))
			{
				try
				{
					InitData = Mvx.Resolve<IMvxJsonConverter>().DeserializeObject<TInit>(initData);
				}
				catch (JsonException ex)
				{
					Mvx.Trace(ex.Message);
				}
			}
		}

		public virtual void RealInit(TInit parameter) { }
	}
}

