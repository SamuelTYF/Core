namespace File.SV
{
	public class SV_Selector
	{
		public SV_Check Check;
        public SV_Selector(SV_Check check) => Check = check;
        public SVFile Select(SVFile source, out SVFile error)
		{
			SVFile cSV = new(source.Title);
			error = new(source.Title);
			foreach (SVLine value in source.Values)
				if (Check(value))cSV.Values.Add(value);
				else error.Values.Add(value);
			return cSV;
		}
	}
}
