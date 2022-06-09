using System;
using System.Collections.Generic;
namespace TimeSeries;
public class Frequency : ComplexSeries
{
	public Frequency(Complex[] values)
		: base(values)
	{
	}
	private void PreIFFT(Complex[] complices, Complex[] rs, int index, int step, int length, int offset)
	{
		if (length == 1)
		{
			rs[offset] = complices[index];
			return;
		}
		int step2 = step << 1;
		int num = length >> 1;
		PreIFFT(complices, rs, index, step2, num, offset);
		PreIFFT(complices, rs, index + step, step2, num, offset + num);
	}
	private void IFFT_2_Time_Position(Complex[] complices, int index, int length)
	{
		if (length != 1)
		{
			int num = length >> 1;
			IFFT_2_Time_Position(complices, index, num);
			IFFT_2_Time_Position(complices, index + num, num);
			double num2 = Math.PI / num;
			Complex complex = new Complex(Math.Cos(num2), 0.0 - Math.Sin(num2));
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
	private double[] PostIFFT(Complex[] complices)
	{
		int num = complices.Length;
		double[] array = new double[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = complices[i].R.CompareTo(0.0) * (complices[i].Norm() / num);
		}
		return array;
	}
	private Complex[] PostIFFT_Complex(Complex[] complices)
	{
		int num = complices.Length;
		Complex[] array = new Complex[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = complices[i] / num;
		}
		return array;
	}
	public Series IFFT_2_Time_Position(int L = -1)
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
		PreIFFT(array, array2, 0, 1, num, 0);
		IFFT_2_Time_Position(array2, 0, num);
		return new(PostIFFT(array2));
	}
	public ComplexSeries IFFT_2_Time_Position_Complex(int L = -1)
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
		PreIFFT(array, array2, 0, 1, num, 0);
		IFFT_2_Time_Position(array2, 0, num);
		return new ComplexSeries(PostIFFT_Complex(array2));
	}
	public new Frequency Sub(int length, int index = 0)
	{
		Complex[] array = new Complex[length];
		Array.Copy(Values, index, array, 0, length);
		return new Frequency(array);
	}
	public static Frequency operator *(Frequency a, Frequency b)
	{
		if (a.Length != b.Length)
			throw new Exception();
		Complex[] array = new Complex[a.Length];
		for (int i = 0; i < a.Length; i++)
		{
			array[i] = a.Values[i] * b.Values[i];
		}
		return new Frequency(array);
	}
	public static Frequency operator /(Frequency a, Frequency b)
	{
		if (a.Length != b.Length)
			throw new Exception();
		Complex[] array = new Complex[a.Length];
		for (int i = 0; i < a.Length; i++)
		{
			array[i] = a.Values[i] / b.Values[i];
		}
		return new Frequency(array);
	}
	public override string ToString()
	{
		return "{" + string.Join(",", (IEnumerable<Complex>)Values) + "}";
	}
}
