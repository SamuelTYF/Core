using Collection.Serialization;
namespace Hook
{
	public class Arg : ISerializable
	{
		public string Name;
		public object[] Params;
		public Arg(string name, params object[] ps)
		{
			Name = $"{name}`{ps.Length}";
			Params = ps;
		}
		public Arg(Formatter formatter)
		{
			Name = (string)formatter.Read();
			Params = (object[])formatter.Read();
		}
		public void Write(Formatter formatter)
		{
			formatter.Write(Name);
			formatter.Write(Params);
		}
	}
}
