using System;

namespace ColoradoRoads.Exceptions
{
	public class ApiException : Exception
	{
		public ApiException (string message) : base(message)
		{
		}
	}
}

