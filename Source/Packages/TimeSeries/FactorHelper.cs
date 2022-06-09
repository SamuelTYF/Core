using System;
using System.Collections.Generic;
namespace TimeSeries;
public class FactorHelper
{
	public static double LearningRate = 0.7;
	public static readonly Random _R = new Random(DateTime.Now.Millisecond);
	public static double[] Divide(double[] P, double u, double v, out double r1, out double r0)
	{
		int num = P.Length;
		if (num == 1)
		{
			r1 = 0.0;
			r0 = P[0];
			return new double[0];
		}
		r1 = P[--num];
		r0 = P[--num];
		double[] array = new double[num];
		while (num-- > 0)
		{
			array[num] = r1;
			r1 = r0 - u * r1;
			r0 = P[num] - v * array[num];
		}
		return array;
	}
	public static void NDivide(double[] P, double u, double v, out double r1, out double r0)
	{
		int num = P.Length;
		if (num == 1)
		{
			r1 = 0.0;
			r0 = 0.0 - P[0];
			return;
		}
		r1 = 0.0 - P[--num];
		r0 = 0.0 - P[--num];
		while (num-- > 0)
		{
			double num2 = r1;
			r1 = r0 - u * num2;
			r0 = 0.0 - P[num] - v * num2;
		}
	}
	public static void Update(double[] Q, ref double u, ref double v, double r1, double r0)
	{
		NDivide(Q, u, v, out var r2, out var r3);
		Divide(new double[3] { 0.0, r3, r2 }, u, v, out var r4, out var r5);
		double num = r4 * r3 - r5 * r2;
		u += LearningRate * (r0 * r2 - r1 * r3) / num;
		v += LearningRate * (r1 * r5 - r0 * r4) / num;
	}
	public static double[] Solve(double[] P, List<Complex> roots, double error = 0.01, double max = 100.0)
	{
		int num = 0;
		double u = _R.NextDouble();
		double v = _R.NextDouble();
		double r;
		double r2;
		double[] array = Divide(P, u, v, out r, out r2);
		while (!(r < error) || !(r > 0.0 - error) || !(r2 < error) || !(r2 > 0.0 - error))
		{
			if (r > max || r2 > max)
			{
				u = _R.NextDouble();
				v = _R.NextDouble();
				array = Divide(P, u, v, out r, out r2);
			}
			Update(array, ref u, ref v, r, r2);
			array = Divide(P, u, v, out r, out r2);
			num++;
			if (num > 100000000)
				throw new Exception();
		}
		double num2 = u * u - 4.0 * v;
		if (num2 < 0.0)
		{
			num2 = 0.0 - num2;
			roots.Add(new Complex((0.0 - u) / 2.0, (0.0 - Math.Sqrt(num2)) / 2.0));
			roots.Add(new Complex((0.0 - u) / 2.0, Math.Sqrt(num2) / 2.0));
		}
		else
		{
			roots.Add(new Complex((Math.Sqrt(num2) - u) / 2.0));
			roots.Add(new Complex((0.0 - (Math.Sqrt(num2) + u)) / 2.0));
		}
		return array;
	}
	public static Complex[] GetRoots(double[] P, double error = 0.01, double max = 100.0)
	{
		List<Complex> list = new List<Complex>(P.Length - 1);
		while (P.Length > 2)
		{
			P = Solve(P, list, error, max);
		}
		if (P.Length == 2)
			list.Add(new Complex((0.0 - P[0]) / P[1]));
		return list.ToArray();
	}
}
