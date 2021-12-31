using Collection;
namespace CSharpScript.File
{
	public class CustomMod
	{
		public ElementType ElementType;
		public TypeDefOrRefOrSpecEncoded Encoded;
		public CustomMod(ElementType element, TypeDefOrRefOrSpecEncoded encoded)
		{
			ElementType = element;
			Encoded = encoded;
		}
		public static CustomMod[] TryAnaylzeCustomModes(byte[] bs, ref int index, MetadataRoot metadata)
		{
			List<CustomMod> list = new();
			while (index < bs.Length)
			{
				ElementType elementType = (ElementType)bs[index++];
				if (elementType is ElementType.ELEMENT_TYPE_CMOD_OPT or ElementType.ELEMENT_TYPE_CMOD_REQD)
				{
					list.Add(new CustomMod(elementType, new TypeDefOrRefOrSpecEncoded(bs, ref index, metadata)));
					continue;
				}
				index--;
				break;
			}
			return list.ToArray();
		}
	}
}
