using System;
namespace TimeSeries;
public abstract class IWalshFunction
{
	public abstract double PositiveFunction(double a, double b);
	public abstract double NegetiveFunction(double a, double b);
	public abstract double InversePositiveFunction(double a, double b);
	public abstract double InverseNegetiveFunction(double a, double b);
	public void FDWT(double[] values, int l, int r)
	{
		if (l + 1 != r)
		{
			int num = l + r >> 1;
			FDWT(values, l, num);
			FDWT(values, num, r);
			for (int i = l; i < num; i++)
			{
				double num2 = PositiveFunction(values[i], values[i + num - l]);
				double num3 = NegetiveFunction(values[i], values[i + num - l]);
				values[i] = num2;
				values[i + num - l] = num3;
			}
		}
	}
	public void IFDWT(double[] values, int l, int r)
	{
		if (l + 1 != r)
		{
			int num = l + r >> 1;
			for (int i = l; i < num; i++)
			{
				double num2 = InversePositiveFunction(values[i], values[i + num - l]);
				double num3 = InverseNegetiveFunction(values[i], values[i + num - l]);
				values[i] = num2;
				values[i + num - l] = num3;
			}
			IFDWT(values, l, num);
			IFDWT(values, num, r);
		}
	}
	public Series FWT(Series s, int index, int L)
	{
		int num = 1;
		for (int num2 = L - 1; num2 != 0; num2 >>= 1)
		{
			num <<= 1;
		}
		double[] array = new double[num];
		if (L + index > s.Length)
			Array.Copy(s.Values, index, array, 0, s.Length - index);
		else
			Array.Copy(s.Values, index, array, 0, L);
		FDWT(array, 0, num);
		return new(array);
	}
	public Series IFWT(Series s, int L)
	{
		if (L > s.Length)
			throw new Exception();
		int num = 1;
		for (int num2 = s.Length - 1; num2 != 0; num2 >>= 1)
		{
			num <<= 1;
		}
		double[] array = new double[num];
		Array.Copy(s.Values, 0, array, 0, s.Length);
		IFDWT(array, 0, s.Length);
		return new(array, 0, L);
	}
}
