using System;
using System.Text.RegularExpressions;
using MvvmCross.Platform;

namespace ColoradoRoads
{
	public static class RegularExpressions
	{
		public static string EmailRegexpr = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
			@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
			@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

		public static bool ValidateRegExpression(this string text, string regExpr)
		{
			try
			{
				return new Regex(regExpr, RegexOptions.IgnoreCase).IsMatch(text);
			}
			catch (Exception e)
			{
				Mvx.Trace(e.Message, e.StackTrace);
			}

			return false;
		}
	}

	public class Validator
	{
		string Name { get; set; }
		string ValidationText { get; set; }
		Func<bool> Expression { get; set; }

		public Validator(string name, string validationText, Func<bool> expression)
		{
			Name = name;
			ValidationText = validationText;
			Expression = expression;
		}

		public ValidatorDataItem Validate()
		{
			if (Expression != null && Expression.Invoke())
				return new ValidatorDataItem(Name, ValidationText);
			else
				return new ValidatorDataItem(Name, null);
		}
	}

	public class ValidatorDataItem
	{
		public string Name { get; set; }
		public string ValidationText { get; set; }

		public ValidatorDataItem(string name, string validationText)
		{
			Name = name;
			ValidationText = validationText;
		}
	}
}
