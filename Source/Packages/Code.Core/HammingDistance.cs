namespace Code.Core
{
	public class HammingDistance
	{
		public static int Get(byte a, byte b)
		{
			int r = 0;
			for (int c = a ^ b; c != 0; c -= c & -c)
				r++;
			return r;
		}
	}
}
