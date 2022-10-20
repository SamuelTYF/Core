namespace NumberTheory;

public class MillerRabin_BigInteger
{
	public UnsigendBigInterger[] TestPrimes;

	public MillerRabin_BigInteger()
	{
		TestPrimes = new UnsigendBigInterger[5] { 2, 5, 7, 37, 61 };
	}

	public MillerRabin_BigInteger(params UnsigendBigInterger[] primes)
	{
		TestPrimes = primes;
	}

	public static UnsigendBigInterger QuickPower(UnsigendBigInterger a, UnsigendBigInterger p, UnsigendBigInterger mod)
	{
		UnsigendBigInterger unsigendBigInterger = 1;
		while (p != 0)
		{
			UnsigendBigInterger.Divide(p, 2, out var m, out var n);
			if (n == 1)
				unsigendBigInterger = a * unsigendBigInterger % mod;
			a = a * a % mod;
			p = m;
		}
		return unsigendBigInterger;
	}

	public static UnsigendBigInterger QuickPower(UnsigendBigInterger a, UnsigendBigInterger p)
	{
		UnsigendBigInterger unsigendBigInterger = 1;
		while (p != 0)
		{
			UnsigendBigInterger.Divide(p, 2, out var m, out var n);
			if (n == 1)
				unsigendBigInterger = a * unsigendBigInterger;
			a *= a;
			p = m;
		}
		return unsigendBigInterger;
	}

	public bool Test(UnsigendBigInterger n)
	{
		UnsigendBigInterger[] testPrimes = TestPrimes;
		for (int i = 0; i < testPrimes.Length; i++)
		{
			if (n == testPrimes[i])
				return true;
		}
		if (n % 2 == 0 || n == 1)
			return false;
		UnsigendBigInterger unsigendBigInterger = n - 1;
		UnsigendBigInterger.Divide(unsigendBigInterger, 2, out var m, out var n2);
		int num = 0;
		while (n2 == 0)
		{
			unsigendBigInterger = m;
			UnsigendBigInterger.Divide(unsigendBigInterger, 2, out m, out n2);
			num++;
		}
		testPrimes = TestPrimes;
		for (int i = 0; i < testPrimes.Length; i++)
		{
			UnsigendBigInterger unsigendBigInterger2 = QuickPower(testPrimes[i], unsigendBigInterger, n);
			UnsigendBigInterger unsigendBigInterger3 = unsigendBigInterger2;
			for (int j = 0; j < num; j++)
			{
				unsigendBigInterger2 = unsigendBigInterger2 * unsigendBigInterger2 % n;
				if (unsigendBigInterger2 == 1 && unsigendBigInterger3 != 1 && unsigendBigInterger3 != n - 1)
					return false;
				unsigendBigInterger3 = unsigendBigInterger2;
			}
			if (unsigendBigInterger2 != 1)
				return false;
		}
		return true;
	}
}
