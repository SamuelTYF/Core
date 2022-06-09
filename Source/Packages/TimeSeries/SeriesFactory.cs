using System;
namespace TimeSeries;
public class SeriesFactory
{
	public static Series CreateSinSeries(double start, double end, double f, double a, double w)
	{
		int num = (int)((end - start) * f);
		Series series = new(num + 1);
		for (int i = 0; i <= num; i++)
		{
			series.Values[i] = a * Math.Sin(w * (start + i / f));
		}
		return series;
	}
	public static Series CreateCosSeries(double start, double end, double f, double a, double w)
	{
		int num = (int)((end - start) * f);
		Series series = new(num + 1);
		for (int i = 0; i <= num; i++)
		{
			series.Values[i] = a * Math.Cos(w * (start + i / f));
		}
		return series;
	}
}
