using System;

namespace ColoradoRoadse.Exceptions
{
	public class UiApiException: Exception
	{
		public string Title { get; set; }

		public UiApiException(string message) : base(message)
		{
		}
	}
}