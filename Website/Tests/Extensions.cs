using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
	public static class Extensions
	{
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
	}
}

