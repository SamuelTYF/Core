using System;
namespace CSharpScript.File
{
	public sealed class CSharpCodeNewArray : CSharpCode
	{
		public TypeDefOrRef Type;
		public CSharpCode Length;
		public int DupCount;
		public CSharpCode[] Dups;
		public int _Length;
		public CSharpCodeNewArray(TypeDefOrRef type, CSharpCode length)
		{
			Type = type;
			Length = length;
			DupCount = 0;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + $"new {Type}[{Length}]" + PrintDup();
		}
		public void GetLength()
		{
			_Length = (Length as CSharpCodeConstant).ToInt32();
			Dups = new CSharpCode[_Length];
		}
		public void Register(CSharpCode code)
		{
			if (code is CSharpCodeLdFieldToken)
			{
				RegisterToken(code as CSharpCodeLdFieldToken);
				return;
			}
			if (code is CSharpCodeStElem)
			{
				RegisterStElem(code as CSharpCodeStElem);
				return;
			}
			throw new Exception();
		}
		public void RegisterStElem(CSharpCodeStElem code)
		{
			if (DupCount == 0)
				GetLength();
			DupCount++;
			if (code.Value is CSharpCodeBlock)
				;
			Dups[(int)(code.Index as CSharpCodeConstant).Value] = code.Value;
		}
		public string PrintDup()
		{
			if (DupCount == 0)
				return "";
			string[] array = new string[_Length];
			for (int i = 0; i < Dups.Length; i++)
			{
				array[i] = Dups[i]?.Print();
			}
			return "{" + string.Join(",", array) + "}";
		}
		public void RegisterToken(CSharpCodeLdFieldToken token)
		{
			GetLength();
			byte[] data = token.RVA.Data;
			int num = data.Length / _Length;
			DupCount = _Length;
			string text = Type.ToString();
			if (!(text == "System.Int32"))
			{
				if (!(text == "System.Int16"))
					throw new Exception();
				if (num != 2)
					throw new Exception();
				for (int i = 0; i < _Length; i++)
				{
					Dups[i] = new CSharpCodeConstant(ElementType.ELEMENT_TYPE_I2, BitConverter.ToInt16(data, i << 1));
				}
			}
			else
			{
				if (num != 4)
					throw new Exception();
				for (int j = 0; j < _Length; j++)
				{
					Dups[j] = new CSharpCodeConstant(ElementType.ELEMENT_TYPE_I4, BitConverter.ToInt32(data, j << 2));
				}
			}
		}
	}
}
