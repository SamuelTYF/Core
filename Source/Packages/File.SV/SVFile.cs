using Collection;

namespace File.SV
{
	public class SVFile
	{
		public SVLine Title;
		public List<SVLine> Values;
		public int Length => Values.Length;
		public SVLine this[int index]
        {
			get => Values[index];
			set => Values[index] = value;
        }
		public SVFile(SVLine title)
		{
			Title = title;
			Values = new List<SVLine>();
		}
		public SVFile(params string[] title)
		{
			Title = new SVLine(title);
			Values = new List<SVLine>();
		}
		public SVFile(SVLine title, System.Collections.Generic.IEnumerable<SVLine> values)
		{
			Title = title;
			Values = new List<SVLine>(values);
		}
		public void Insert(SVLine line) => Values.Add(line);
		public void Insert(params string[] line) => Values.Add(new SVLine(line));
	}
}
