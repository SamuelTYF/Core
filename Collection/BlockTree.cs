using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Collection
{
	public class BlockTree<T>
	{
		public int LI;
		public int RI;
		public BlockTree<T> L;
		public BlockTree<T> R;
		public T Value;
		public Combine<T> Combine;
		public BlockTree(Combine<T> combine, params T[] values)
			: this(combine, 0, values.Length - 1, values)
		{
		}
		public BlockTree(Combine<T> combine, int li, int ri, T[] values)
		{
			Combine = combine;
			LI = li;
			RI = ri;
			if (LI == RI)
			{
				Value = values[LI];
				return;
			}
			int num = LI + RI >> 1;
			L = new BlockTree<T>(combine, li, num, values);
			R = new BlockTree<T>(combine, num + 1, ri, values);
			Value = combine(L.Value, R.Value);
		}
        public T GetExcept(int index) => LI <= index && RI >= index
                ? L.LI == index && L.LI == L.RI
                    ? R.Value
                    : L.RI >= index
                    ? Combine(L.GetExcept(index), R.Value)
                    : R.LI == index && R.LI == R.RI ? L.Value : Combine(L.Value, R.GetExcept(index))
                : Value;
    }
}
