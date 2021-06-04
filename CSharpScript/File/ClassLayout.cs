using System;
namespace CSharpScript.File
{
	public class ClassLayout
	{
		public LayoutKind LayoutKind;
		public int PackingSize;
		public int ClassSize;
		public TypeDef Parent;
		public ClassLayoutRow Row;
		public int Token;
		public ClassLayout(ClassLayoutRow row)
		{
			ClassLayoutRow classLayoutRow = (Row = row);
			Token = classLayoutRow.Index | 0xF000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			PackingSize = Row.PackingSize;
			ClassSize = Row.ClassSize;
			Parent = metadata.TildeHeap.TypeDefTable.TypeDefs[Row.Parent - 1];
            LayoutKind = (Parent.Flags & TypeAttributes.LayoutMask) switch
            {
                TypeAttributes.SequentialLayout => LayoutKind.Sequential,
                TypeAttributes.ExplicitLayout => LayoutKind.Explicit,
                _ => throw new Exception(),
            };
            Parent.ClassLayout = this;
		}
        public override string ToString() => string.Format("[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.{0}{1}{2})]", LayoutKind, (PackingSize == 0) ? "" : $", Pack = 0x{PackingSize:X}", (ClassSize == 0) ? "" : $", Size = 0x{ClassSize:X}");
    }
}
