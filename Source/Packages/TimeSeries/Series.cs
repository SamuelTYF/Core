using System;
using System.IO;
namespace TimeSeries;
public class Series
{
	public int Length;
	public double[] Values;
	public Series(int length)
	{
		Values = new double[Length = length];
	}
	public Series(params double[] values)
	{
		Length = (Values = values).Length;
	}
	public Series(double[] values, int offset, int length)
	{
		Values = new double[Length = length];
		Array.Copy(values, offset, Values, 0, length);
	}
	public Series Reverse()
	{
		Series series = new(Length);
		Array.Copy(Values, 0, series.Values, 0, Length);
		Array.Reverse(series.Values);
		return series;
	}
	public Series Clone()
	{
		double[] array = new double[Length];
		Array.Copy(Values, 0, array, 0, Length);
		return new(array);
	}
	public void CopyTo(Series other, int index)
	{
		Array.Copy(Values, 0, other.Values, index, Length);
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
			Complex complex = new(Math.Cos(num2), Math.Sin(num2));
			Complex complex2 = new(1.0);
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
	private Complex[] FFT_2_Time_Iteration(Complex[] complices, int index, int step, int length)
	{
		if (length == 1)
			return new Complex[1] { complices[index] };
		int num = length >> 1;
		int step2 = step << 1;
		int num2 = step * length >> 1;
		Complex[] array = FFT_2_Time_Iteration(complices, index, step2, num);
		Complex[] array2 = FFT_2_Time_Iteration(complices, index + step, step2, num);
		double num3 = Math.PI / num;
		Complex complex = new Complex(Math.Cos(num3), Math.Sin(num3));
		Complex complex2 = new Complex(1.0);
		Complex[] array3 = new Complex[length];
		for (int i = 0; i < num; i++)
		{
			array3[i + num] = array[i] - complex2 * array2[i];
			array3[i] = array[i] * 2.0 - array3[i + num];
			complex2 *= complex;
		}
		return array3;
	}
	public Frequency FFT_2_Time_Iteration()
	{
		int num = 1;
		int num2;
		for (num2 = Length - 1; num2 != 0; num2 >>= 1)
		{
			num <<= 1;
		}
		Complex[] array = new Complex[num];
		while (num2 < Length)
		{
			array[num2] = new Complex(Values[num2++]);
		}
		while (num2 < num)
		{
			array[num2++] = new Complex(0.0);
		}
		return new Frequency(FFT_2_Time_Iteration(array, 0, 1, num));
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
			array[num2] = new Complex(Values[num2++]);
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
	public Frequency FFT_2_Time_Position(int index, int L)
	{
		int num = 1;
		int num2;
		for (num2 = L - 1; num2 != 0; num2 >>= 1)
		{
			num <<= 1;
		}
		Complex[] array = new Complex[num];
		int num3 = ((index + L > Length) ? (Length - index) : L);
		while (num2 < num3)
		{
			array[num2] = new Complex(Values[index + num2++]);
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
	public Series Sub(int length, int offset = 0)
	{
		Series series = new(length);
		Array.Copy(Values, offset, series.Values, 0, length);
		return series;
	}
	public override string ToString()
	{
		return "{" + string.Join(",", Values) + "}";
	}
	public static Series operator *(Series a, Series b)
	{
		int l = a.Length + b.Length - 1;
		return (a.FFT_2_Time_Position(l) * b.FFT_2_Time_Position(l)).IFFT_2_Time_Position(l);
	}
	public static Series operator +(Series a, Series b)
	{
		if (a.Length < b.Length)
			return b + a;
		double[] array = new double[a.Length];
		int num = 0;
		while (num < b.Length)
		{
			array[num] = a.Values[num] + b.Values[num++];
		}
		while (num < a.Length)
		{
			array[num] = a.Values[num++];
		}
		return new(array);
	}
}
