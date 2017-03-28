using System;
namespace ColoradoRoads.Droid
{
	public interface IHolderFragment : IApperance
	{
		bool PopBackStackImmediateForHolder();
	}
}
