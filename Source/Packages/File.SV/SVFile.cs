using System.Collections.Generic;
namespace File.SV
{
	public class SVFile
	{
		public SVLine Title;
		public Collection.List<SVLine> Values;
		public int Length => Values.Length;
		public SVLine this[int index]
        {
			get => Values[index];
			set => Values[index] = value;
        }
		public SVFile(SVLine title)
		{
			Title = title;
			Values = new Collection.List<SVLine>();
		}
		public SVFile(params string[] title)
		{
			Title = new SVLine(title);
			Values = new Collection.List<SVLine>();
		}
		public SVFile(SVLine title, IEnumerable<SVLine> values)
		{
			Title = title;
			Values = new Collection.List<SVLine>(values);
		}
		public void Insert(SVLine line) => Values.Add(line);
		public void Insert(params string[] line) => Values.Add(new SVLine(line));
	}
}
