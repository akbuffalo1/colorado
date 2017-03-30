using System;
using System.Collections;
using System.Collections.Generic;
using ColoradoRoads.Droid.Adapters;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;

namespace ColoradoRoads.Droid.CustomBindings
{
	public class ViewPagerTargetBinding : MvxAndroidTargetBinding
	{
		protected TopCarouselViewAdapter Carousel
		{
			get { return (TopCarouselViewAdapter)Target; }
		}

		public ViewPagerTargetBinding(TopCarouselViewAdapter target) : base(target)
		{
		}

		public override void SubscribeToEvents()
		{
			Carousel.MyCountChanged += TargetOnMyCountChanged;
		}

		private void TargetOnMyCountChanged(object sender, EventArgs eventArgs)
		{
			var target = Target as TopCarouselViewAdapter;

			if (target == null)
				return;

			var value = target.GetDataSource();
			FireValueChanged(value);
		}

		protected override void SetValueImpl(object target, object value)
		{
			var carousel = (TopCarouselViewAdapter)target;
			if (value != null)
			{ 
				carousel.SetDataSource((IEnumerable<object>)value);
			}
		}

		public override Type TargetType
		{
			get { return typeof(IEnumerable<object>); }
		}

		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.TwoWay; }
		}

		protected override void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				var target = Target as TopCarouselViewAdapter;
				if (target != null)
				{
					target.MyCountChanged -= TargetOnMyCountChanged;
				}
			}
			base.Dispose(isDisposing);
		}
	}
}
