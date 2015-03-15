using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TestingMvc
{
	public static class Extensions
	{
		/// <summary>
		/// Yields the specified items in chunks of size number. 
		/// </summary>
		/// <param name="items">Items.</param>
		/// <param name="number">Amount to return in each chunk</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> items, int number)
		{
			if(number <= 1)
			{
				throw new ArgumentException("Number must be greater than 1");
			}
			if(items == null)
			{
				throw new NullReferenceException("items must not be null");
			}
			T[] buffer = new T[number];
			int currentIndex = 0;
			foreach(T i in items)
			{
				buffer[currentIndex] = i;
				currentIndex = (currentIndex + 1) % number;
				if(currentIndex == 0)
				{
					yield return buffer.AsEnumerable();
				}
			}
			if(currentIndex > 0)
			{
				yield return buffer.Take(currentIndex);
			}
		}

		public static string ToEnglishString(this TimeSpan duration)
		{
			string result = "{0:0.} {1}";
			if (duration < TimeSpan.FromMinutes(5))
			{
				result = "< 5 Minutes";
			} 
			else if (duration < TimeSpan.FromMinutes(60))
			{
				result = string.Format(result, duration.TotalMinutes, "Minutes");
			}
			else if (duration < TimeSpan.FromHours(24))
			{
				result = string.Format(result, duration.TotalHours, "Hours");
			}
			else
			{
				result = string.Format(result, duration.TotalDays, "Days");
			}
			return result;
		}

		public class GenericComparer<T> : IEqualityComparer<T> where T : class
		{
			private Func<T, object> _expr;

			public GenericComparer(Func<T, object> expr)
			{
				_expr = expr;
			}

			public bool Equals(T x, T y)
			{
				var first = _expr.Invoke(x);
				var second = _expr.Invoke(y);
				return first != null && first.Equals(second);
			}

			public int GetHashCode(T obj)
			{
				return _expr.Invoke(obj).GetHashCode();
			}
		}

		public static MvcHtmlString ClassIfInvalidField<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper, 
			Expression<Func<TModel, TProperty>> expression,
			string errorClassName)
		{
			var expressionText = ExpressionHelper.GetExpressionText(expression);
			var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);
			return htmlHelper.ViewData.ModelState.IsValidField(fullHtmlFieldName) ? MvcHtmlString.Empty : new MvcHtmlString(errorClassName);
		}
	}
}

