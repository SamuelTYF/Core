namespace NumberTheory;

public class Mersenne
{
	public static BitInteger Mod(BitInteger a, BitInteger n, int mod)
	{
		if (a.CompareTo(n) < 0)
			return a;
		a.Split(mod, out var a2, out var b);
		return Mod(a2 + b, n, mod);
	}

	public static BitInteger QuickPower(BitInteger a, int p, BitInteger n, int mod)
	{
		BitInteger bitInteger = BitInteger.One;
		while (p-- != 0)
		{
			bitInteger = Mod(a * bitInteger, n, mod);
			a = Mod(a * a, n, mod);
		}
		return bitInteger;
	}

	public static BitInteger QuickPower2(BitInteger a, int p, BitInteger n, int mod)
	{
		while (p-- != 0)
		{
			a = Mod(a.Pow(), n, mod);
		}
		return a;
	}

	public bool Test(int p, BitInteger n, uint a)
	{
		BitInteger bitInteger = QuickPower(new BitInteger(a), p - 1, n, p);
		if (bitInteger.CompareTo(BitInteger.One) == 0)
			return true;
		if (n.CompareTo(bitInteger + BitInteger.Two) == 0)
			return true;
		return Mod(bitInteger * bitInteger, n, p).CompareTo(BitInteger.One) == 0;
	}

	public bool Test(int p)
	{
		if (p != 2)
			return QuickPower(new BitInteger(3u), p, BitInteger.TwoPower(p), p).CompareTo(BitInteger.Three) == 0;
		return true;
	}

	public bool TestQ(int p)
	{
		if (p >= 4)
			return QuickPower2(new BitInteger(3u), p, BitInteger.TwoPower(p), p).CompareTo(new BitInteger(9u)) == 0;
		return true;
	}
}
