using System;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings;

namespace ColoradoRoads
{
	public static class MvxBindingContextOwnerExtensions
	{
		public static void AddLinqBinding<TViewModel, TProperty>(
			this IMvxBindingContextOwner owner,
			TViewModel viewModel,
			Expression<Func<TViewModel, TProperty>> propertyExpression,
			Action<TProperty> action) where TViewModel : INotifyPropertyChanged
		{
			var propertyInfo = GetPropertyInfoFromExpression<TViewModel, TProperty>(propertyExpression);
			var binding = new LinqBinding<TViewModel, TProperty>(viewModel, propertyInfo, action);
			owner.AddBinding(owner, binding);
		}

		public static void AddLinqBinding<TViewModel, TProperty>(
			this IMvxBindingContextOwner owner,
			TViewModel viewModel,
			Expression<Func<TViewModel, TProperty>> propertyExpression,
			Action action) where TViewModel : INotifyPropertyChanged
		{
			AddLinqBinding(owner, viewModel, propertyExpression, (TProperty v) => action());
		}

		private static PropertyInfo GetPropertyInfoFromExpression<TViewModel, TProperty>(
			Expression<Func<TViewModel, TProperty>> expression)
		{
			Type baseType = typeof(TViewModel);

			// get the member info
			MemberExpression member = expression.Body as MemberExpression;
			if (member == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.", expression));

			// get the property info
			PropertyInfo propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
				throw new ArgumentException(
					string.Format("Expression '{0}' refers to a field, not a property.",
						expression));

			// check the base is the right type
			var firstParameterType = expression.Parameters.First().Type;
			if (baseType != firstParameterType && !firstParameterType.GetTypeInfo().IsSubclassOf(baseType))
				throw new ArgumentException(
					string.Format("Expresion '{0}' refers to a property that is not from type {1}.",
						expression, baseType));

			// give back our work
			return propInfo;
		}

		// straight up LINQ binding to a property
		private class LinqBinding<TViewModel, TProperty> : IMvxUpdateableBinding
			where TViewModel : INotifyPropertyChanged
		{
			private TViewModel _ViewModel;
			private Action<TProperty> _OnUpdate;
			private PropertyInfo _PropertyInfo;
			private Func<TViewModel, object> _PropertyValueGetter;

			// set binding action
			public LinqBinding(TViewModel viewModel, PropertyInfo propertyInfo, Action<TProperty> action)
			{
				_ViewModel = viewModel;
				_OnUpdate = action;
				_PropertyInfo = propertyInfo;

				// set event and kick
				_ViewModel.PropertyChanged += OnPropertyChanged;

				_PropertyValueGetter = FastInvoke.BuildUntypedGetter<TViewModel>(propertyInfo);

				// kick once
				var propertyValue = (TProperty)_PropertyValueGetter(viewModel);
				action(propertyValue);
			}

			// no idea what this is for... let's just play along
			private object _DataContext;
			public object DataContext
			{
				get
				{
					return _DataContext;
				}
				set
				{
					_DataContext = value;
				}
			}

			// remove handler
			public void Dispose()
			{
				_ViewModel.PropertyChanged -= OnPropertyChanged;
			}

			// run action on update, for the correct property
			private void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
			{
				if (eventArgs.PropertyName == _PropertyInfo.Name)
				{
					var newPropertyValue = (TProperty)_PropertyValueGetter(_ViewModel);
					_OnUpdate(newPropertyValue);
				}
			}
		}
	}
}
