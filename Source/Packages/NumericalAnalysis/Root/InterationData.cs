using System;
using Collection;
namespace NumericalAnalysis.Root
{
	public class InterationData<T>
	{
		public string Name;
		public int Length;
		public List<T[]> Data;
		public InterationData(string name, int length)
		{
			Name = name;
			Length = length;
			Data = new List<T[]>();
		}
		public void Register(params T[] values)
		{
			if (values.Length != Length)
				throw new Exception();
			Data.Add(values);
		}
        public override string ToString() => string.Join("\n", Array.ConvertAll(Data.ToArray(), (T[] ts) => string.Join("\t", ts)));
        public string ToLatex() => string.Join("\\\\ \\hline \n", Array.ConvertAll(Data.ToArray(), (T[] ts) => string.Join("&", ts)));
    }
}
