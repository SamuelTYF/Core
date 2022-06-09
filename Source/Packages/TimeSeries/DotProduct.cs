using System;
namespace TimeSeries;
public class DotProduct : IOperator
{
	public double Multiply(double[] a, double[] b, int indexa, int indexb, int length)
	{
		double num = 0.0;
		for (int i = 0; i < length; i++)
		{
			num += a[indexa++] * b[indexb++];
		}
		return num;
	}
	public Series Execute(Series a, Series b)
	{
		Series series = new(a.Length);
		double num = Math.Sqrt(Multiply(b.Values, b.Values, 0, 0, b.Length));
		int i = a.Length - b.Length;
		for (int j = 0; j < i; j++)
		{
			double num2 = Multiply(a.Values, b.Values, j, 0, b.Length);
			if (num2 == 0.0)
				series.Values[j] = 0.0;
			else
				series.Values[j] = num2 / (num * Math.Sqrt(Multiply(a.Values, a.Values, j, j, b.Length)));
		}
		for (; i < a.Length; i++)
		{
			int length = a.Length - i;
			double num3 = Multiply(a.Values, b.Values, i, 0, length);
			if (num3 == 0.0)
				series.Values[i] = 0.0;
			else
				series.Values[i] = num3 / (num * Math.Sqrt(Multiply(a.Values, a.Values, i, i, length)));
		}
		return series;
	}
	public override string ToString()
	{
		return "Dot Product";
	}
}
