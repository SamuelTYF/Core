namespace File.SV
{
	public class SVLine
	{
		public string[] Values;
		public int Count;
		public string this[int index]
        {
			get => Values[index];
			set => Values[index] = value;
        }
        public SVLine(params string[] values) => Count = (Values = values).Length;
    }
}
