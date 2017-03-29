using System;
using MvvmCross.Core.ViewModels;

namespace ColoradoRoads.Models
{
	public class AddEditDeleteListItemDataHolder<TDataModel>
	{
		public TDataModel DataModel { get; set; }

		public string DisplayDescription { get; set; }

		public AddEditDeleteListItemDataHolder(TDataModel model)
		{
			DataModel = model;
		}

		public bool IsDeletable { get; set; }

		public IMvxCommand AddCommand { get; set; }
		public IMvxCommand EditCommand { get; set; }
		public IMvxCommand RemoveCommand { get; set; }
		public IMvxCommand SelectItemCommand { get; set; }

		public virtual Func<object, string, bool> IsHitPredicate { get; set; }

		public bool IsHit(string searchStr)
		{
			return IsHitPredicate?.Invoke(DataModel, searchStr) ?? false;
		}
	}
}
