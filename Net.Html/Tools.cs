using System.Collections.Generic;

namespace Net.Html
{
	public static class Tools
	{
		public static TValue Get<TValue>(this IEnumerable<TValue> values, int index)
		{
			IEnumerator<TValue> enumerator = values.GetEnumerator();
			for (int i = 0; i <= index; i++)
				if (!enumerator.MoveNext())
					return default(TValue);
			return enumerator.Current;
		}
	}
}
