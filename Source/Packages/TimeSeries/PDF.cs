using System.Collections.Generic;
namespace TimeSeries;
public class PDF
{
	public Dictionary<double, int> Counts;
	public PDF(Series series)
	{
		Counts = new Dictionary<double, int>();
		double[] values = series.Values;
		foreach (double num in values)
		{
			if (Counts.ContainsKey(num))
			{
				Dictionary<double, int> counts = Counts;
				double num2 = num;
				counts[num2]=counts[num2] + 1;
			}
			else
                Counts[num]= 1;
		}
	}
	public PDF(double[] keys, int[] values)
	{
		Counts = new Dictionary<double, int>();
		for (int i = 0; i < keys.Length; i++)
			Counts[keys[i]]= values[i];
	}
}
