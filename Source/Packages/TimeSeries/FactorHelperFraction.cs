using Collection;
using System;

namespace TimeSeries;
public class FactorHelperFraction
{
	public static Fraction LearningRate = new(1, 10);
	public static readonly Random _R = new(DateTime.Now.Millisecond);
	public static Fraction[] Divide(Fraction[] P, Fraction u, Fraction v, out Fraction r1, out Fraction r0)
	{
		int num = P.Length;
		if (num == 1)
		{
			r1 = 0;
			r0 = P[0];
			return new Fraction[0];
		}
		r1 = P[--num];
		r0 = P[--num];
		Fraction[] array = new Fraction[num];
		while (num-- > 0)
		{
			array[num] = r1;
			r1 = r0 - u * r1;
			r0 = P[num] - v * array[num];
		}
		return array;
	}
	public static Fraction[] NDivide(Fraction[] P, Fraction u, Fraction v, out Fraction r1, out Fraction r0)
	{
		int num = P.Length;
		if (num == 1)
		{
			r1 = 0;
			r0 = -P[0];
			return new Fraction[0];
		}
		r1 = -P[--num];
		r0 = -P[--num];
		Fraction[] array = new Fraction[num];
		while (num-- > 0)
		{
			array[num] = r1;
			r1 = r0 - u * r1;
			r0 = -P[num] - v * array[num];
		}
		return array;
	}
	public static void Update(Fraction[] Q, ref Fraction u, ref Fraction v, Fraction r1, Fraction r0)
	{
		Fraction r2;
		Fraction r3;
		Fraction[] a = NDivide(Q, u, v, out r2, out r3);
		Console.WriteLine($"u:{u}\tv:{v}\tr1:{r1}\tr0:{r0}");
		PolynomialFraction polynomialFraction = new(Q);
		Console.WriteLine(polynomialFraction);
		Console.WriteLine(polynomialFraction + PolynomialFraction.Times(new PolynomialFraction(a), u, v));
		Divide(new Fraction[3] { 0, r3, r2 }, u, v, out var r4, out var r5);
		Fraction fraction = r4 * r3 - r5 * r2;
		Console.WriteLine($"r1v:{r2}\tr0v:{r3}\tr1u:{r4}\tr0u:{r5}\tdet:{fraction}");
		u += (r0 * r2 - r1 * r3) / fraction;
		v += (r1 * r5 - r0 * r4) / fraction;
	}
	public static Fraction[] Solve(Fraction[] P, List<ComplexFraction> roots, Fraction error)
	{
		int num = 0;
		Fraction u = new((int)(_R.NextDouble() * 10000.0), 10000);
		Fraction v = new((int)(_R.NextDouble() * 10000.0), 10000);
		Fraction r;
		Fraction r2;
		Fraction[] array = Divide(P, u, v, out r, out r2);
		while (!(r < error) || !(r > -error) || !(r2 < error) || !(r2 > -error))
		{
			Update(array, ref u, ref v, r, r2);
			array = Divide(P, u, v, out r, out r2);
			num++;
			Console.WriteLine($"{num}\t{u}\t{v}\t{r}\t{r2}");
		}
		Fraction fraction = u * u - 4 * v;
		if (fraction < 0)
		{
			fraction = -fraction;
			roots.Add(new ComplexFraction(-u / 2, -Fraction.Sqrt(fraction) / 2));
			roots.Add(new ComplexFraction(-u / 2, Fraction.Sqrt(fraction) / 2));
		}
		else
		{
			roots.Add(new ComplexFraction((Fraction.Sqrt(fraction) - u) / 2, 0));
			roots.Add(new ComplexFraction(-(Fraction.Sqrt(fraction) - u) / 2, 0));
		}
		return array;
	}
	public static ComplexFraction[] GetRoots(Fraction[] P, Fraction error)
	{
		List<ComplexFraction> list = new(P.Length - 1);
		while (P.Length > 2)
		{
			P = Solve(P, list, error);
		}
		if (P.Length == 2)
			list.Add(new ComplexFraction(-(P[0] / P[1]), 0));
		return list.ToArray();
	}
}
