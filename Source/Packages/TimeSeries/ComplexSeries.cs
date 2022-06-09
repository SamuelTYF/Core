using System;
namespace TimeSeries;
public class ComplexSeries
{
	public int Length;
	public Complex[] Values;
	public ComplexSeries(Series real, Series imagine)
	{
		Length = ((real.Length < imagine.Length) ? real.Length : imagine.Length);
		Values = new Complex[Length];
		for (int i = 0; i < Length; i++)
		{
			Values[i] = new Complex(real.Values[i], imagine.Values[i]);
		}
	}
	public ComplexSeries(Complex[] complices)
	{
		Length = complices.Length;
		Values = complices;
	}
	public Series Abs()
	{
		Series series = new(Length);
		for (int i = 0; i < Length; i++)
		{
			series.Values[i] = Values[i].Norm();
		}
		return series;
	}
	public Series Phi()
	{
		Series series = new(Length);
		for (int i = 0; i < Length; i++)
		{
			series.Values[i] = Values[i].Phi();
		}
		return series;
	}
	public ComplexSeries Sub(int length, int offset = 0)
	{
		Complex[] array = new Complex[length];
		Array.Copy(Values, offset, array, 0, length);
		return new ComplexSeries(array);
	}
	private void PreFFT(Complex[] complices, Complex[] rs, int index, int step, int length, int offset)
	{
		if (length == 1)
		{
			rs[offset] = complices[index];
			return;
		}
		int step2 = step << 1;
		int num = length >> 1;
		PreFFT(complices, rs, index, step2, num, offset);
		PreFFT(complices, rs, index + step, step2, num, offset + num);
	}
	private void FFT_2_Time_Position(Complex[] complices, int index, int length)
	{
		if (length != 1)
		{
			int num = length >> 1;
			FFT_2_Time_Position(complices, index, num);
			FFT_2_Time_Position(complices, index + num, num);
			double num2 = Math.PI / num;
			Complex complex = new Complex(Math.Cos(num2), Math.Sin(num2));
			Complex complex2 = new Complex(1.0);
			for (int i = 0; i < num; i++)
			{
				int num3 = index + i;
				int num4 = num3 + num;
				complices[num4] = complices[num3] - complex2 * complices[num4];
				complices[num3] = complices[num3] * 2.0 - complices[num4];
				complex2 *= complex;
			}
		}
	}
	public Frequency FFT_2_Time_Position(int L = -1)
	{
		if (L < 0)
			L = Length;
		int num = 1;
		int num2;
		for (num2 = L - 1; num2 != 0; num2 >>= 1)
		{
			num <<= 1;
		}
		Complex[] array = new Complex[num];
		while (num2 < Length)
		{
			array[num2] = Values[num2++];
		}
		while (num2 < num)
		{
			array[num2++] = new Complex(0.0);
		}
		Complex[] array2 = new Complex[num];
		PreFFT(array, array2, 0, 1, num, 0);
		FFT_2_Time_Position(array2, 0, num);
		return new Frequency(array2);
	}
}
