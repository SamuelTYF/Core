namespace CSharpScript.DomTree
{
	public abstract class ILBlock : ILCode
	{
		public ILCode[] Codes;
		public ILBlock(int offset, int length, ILCodeFlag flag)
			: base(offset, length, flag)
		{
		}
		public void Load(ILCode[] codes)
		{
			Codes = codes;
			for (int i = 0; i < codes.Length; i++)
			{
				codes[i].Parent = this;
				codes[i].Info.Index = i;
			}
		}
	}
}
