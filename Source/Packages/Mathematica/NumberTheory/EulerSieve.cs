using System.Collections.Generic;

namespace NumberTheory;

public class EulerSieve
{
	public List<int> Primes;

	public int PrimeCount;

	public int[] Factors;

	public int Length;

	public EulerSieve(int length)
	{
		Primes = new List<int>();
		PrimeCount = 0;
		Length = length;
		Factors = new int[length];
	}

	public IEnumerable<int> Run()
	{
		for (int i = 2; i < Length; i++)
		{
			if (Factors[i] == 0)
			{
				yield return i;
				Primes.Add(i);
				PrimeCount++;
				for (int j = 0; j < PrimeCount && Primes[j] * i < Length; j++)
				{
					Factors[Primes[j] * i] = j + 1;
				}
			}
			else
			{
				for (int k = 0; k < Factors[i] && Primes[k] * i < Length; k++)
				{
					Factors[Primes[k] * i] = k + 1;
				}
			}
		}
	}
}
