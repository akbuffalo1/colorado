using System;
namespace ColoradoRoads
{
	public interface IInitViewModel<in TInit>
	{
		void Init(string parameter);
		void RealInit(TInit parameter);
	}
}
