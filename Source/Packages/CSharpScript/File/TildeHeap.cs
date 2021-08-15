using System;
namespace CSharpScript.File
{
	public class TildeHeap
	{
		public int Reserved1;
		public byte MajorVersion;
		public byte MinorVersion;
		public HeapSizeFlags HeapSizes;
		public byte Reserved2;
		public long Valid;
		public TableFlags TableFlags;
		public long Sorted;
		public int TableCount;
		public int[] Rows;
		public ModuleTable ModuleTable;
		public TypeRefTable TypeRefTable;
		public TypeDefTable TypeDefTable;
		public FieldTable FieldTable;
		public MethodDefTable MethodDefTable;
		public ParamTable ParamTable;
		public InterfaceImplTable InterfaceImplTable;
		public MemberRefTable MemberRefTable;
		public ConstantTable ConstantTable;
		public CustomAttributeTable CustomAttributeTable;
		public FieldMarshalTable FieldMarshalTable;
		public DeclSecurityTable DeclSecurityTable;
		public ClassLayoutTable ClassLayoutTable;
		public FieldLayoutTable FieldLayoutTable;
		public StandAloneSigTable StandAloneSigTable;
		public EventMapTable EventMapTable;
		public EventTable EventTable;
		public PropertyMapTable PropertyMapTable;
		public PropertyTable PropertyTable;
		public MethodSemanticsTable MethodSemanticsTable;
		public MethodImplTable MethodImplTable;
		public ModuleRefTable ModuleRefTable;
		public TypeSpecTable TypeSpecTable;
		public ImplMapTable ImplMapTable;
		public FieldRVATable FieldRVATable;
		public AssemblyTable AssemblyTable;
		public AssemblyProcessorTable AssemblyProcessorTable;
		public AssemblyOSTable AssemblyOSTable;
		public AssemblyRefTable AssemblyRefTable;
		public AssemblyRefProcessorTable AssemblyRefProcessorTable;
		public AssemblyRefOSTable AssemblyRefOSTable;
		public FileTable FileTable;
		public ExportedTypeTable ExportedTypeTable;
		public ManifestResourceTable ManifestResourceTable;
		public NestedClassTable NestedClassTable;
		public GenericParamTable GenericParamTable;
		public MethodSpecTable MethodSpecTable;
		public GenericParamConstraintTable GenericParamConstraintTable;
		public static int GetOnes(long x)
		{
			int num = 0;
			while (x != 0)
			{
				x &= x - 1;
				num++;
			}
			return num;
		}
		public static int Read(byte[] bs, ref int index, bool islong)
		{
			int result;
			if (islong)
			{
				result = bs[index] | (bs[index + 1] << 8) | (bs[index + 2] << 16) | (bs[index + 3] << 24);
				index += 4;
			}
			else
			{
				result = bs[index] | (bs[index + 1] << 8);
				index += 2;
			}
			return result;
		}
		public TildeHeap(byte[] bs)
		{
			Reserved1 = bs[0] | (bs[1] << 8) | (bs[2] << 16) | (bs[3] << 24);
			MajorVersion = bs[4];
			MinorVersion = bs[5];
			HeapSizes = (HeapSizeFlags)bs[6];
			Reserved2 = bs[7];
			Valid = (long)(bs[8] | ((ulong)bs[9] << 8) | ((ulong)bs[10] << 16) | ((ulong)bs[11] << 24) | ((ulong)bs[12] << 32) | ((ulong)bs[13] << 40) | ((ulong)bs[14] << 48) | ((ulong)bs[15] << 56));
			Sorted = (long)(bs[16] | ((ulong)bs[17] << 8) | ((ulong)bs[18] << 16) | ((ulong)bs[19] << 24) | ((ulong)bs[20] << 32) | ((ulong)bs[21] << 40) | ((ulong)bs[22] << 48) | ((ulong)bs[23] << 56));
			TableFlags = (TableFlags)Valid;
			TableCount = GetOnes(Valid);
			Rows = new int[TableCount];
			for (int i = 0; i < TableCount; i++)
				Rows[i] = bs[24 + (i << 2)] | (bs[25 + (i << 2)] << 8) | (bs[26 + (i << 2)] << 16) | (bs[27 + (i << 2)] << 24);
			int[] array = new int[48];
			int num = 0;
			if (TableFlags.HasFlag(TableFlags.Module))
				array[0] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.TypeRef))
				array[1] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.TypeDef))
				array[2] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.Field))
				array[4] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.MethodDef))
				array[6] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.Param))
				array[8] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.InterfaceImpl))
				array[9] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.MemberRef))
				array[10] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.Constant))
				array[11] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.CustomAttribute))
				array[12] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.FieldMarshal))
				array[13] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.DeclSecurity))
				array[14] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.ClassLayout))
				array[15] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.FieldLayout))
				array[16] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.StandAloneSig))
				array[17] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.EventMap))
				array[18] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.Event))
				array[20] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.PropertyMap))
				array[21] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.Property))
				array[23] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.MethodSemantics))
				array[24] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.MethodImpl))
				array[25] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.ModuleRef))
				array[26] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.TypeSpec))
				array[27] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.ImplMap))
				array[28] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.FieldRVA))
				array[29] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.Assembly))
				array[32] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.AssemblyProcessor))
				array[33] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.AssemblyOS))
				array[34] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.AssemblyRef))
				array[35] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.AssemblyRefProcessor))
				array[36] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.AssemblyRefOS))
				array[37] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.File))
				array[38] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.ExportedType))
				array[39] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.ManifestResource))
				array[40] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.NestedClass))
				array[41] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.GenericParam))
				array[42] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.MethodSpec))
				array[43] = Rows[num++];
			if (TableFlags.HasFlag(TableFlags.GenericParamConstraint))
				array[44] = Rows[num++];
			num = 0;
			int index = 24 + (TableCount << 2);
			bool isStringLong = HeapSizes.HasFlag(HeapSizeFlags.String);
			bool isGuidLong = HeapSizes.HasFlag(HeapSizeFlags.GUID);
			bool isBlobLong = HeapSizes.HasFlag(HeapSizeFlags.Blob);
			ModuleTable = TableFlags.HasFlag(TableFlags.Module)
                ? new ModuleTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong)
                : default(ModuleTable);
            TypeRefTable = TableFlags.HasFlag(TableFlags.TypeRef)
                ? new TypeRefTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array)
                : default(TypeRefTable);
            TypeDefTable = TableFlags.HasFlag(TableFlags.TypeDef) ? new TypeDefTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(TypeDefTable);
			FieldTable = TableFlags.HasFlag(TableFlags.Field) ? new FieldTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(FieldTable);
			MethodDefTable = TableFlags.HasFlag(TableFlags.MethodDef) ? new MethodDefTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(MethodDefTable);
			ParamTable = TableFlags.HasFlag(TableFlags.Param) ? new ParamTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(ParamTable);
			InterfaceImplTable = TableFlags.HasFlag(TableFlags.InterfaceImpl) ? new InterfaceImplTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(InterfaceImplTable);
			MemberRefTable = TableFlags.HasFlag(TableFlags.MemberRef) ? new MemberRefTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(MemberRefTable);
			ConstantTable = TableFlags.HasFlag(TableFlags.Constant) ? new ConstantTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(ConstantTable);
			CustomAttributeTable = TableFlags.HasFlag(TableFlags.CustomAttribute) ? new CustomAttributeTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(CustomAttributeTable);
			FieldMarshalTable = TableFlags.HasFlag(TableFlags.FieldMarshal) ? new FieldMarshalTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(FieldMarshalTable);
			DeclSecurityTable = TableFlags.HasFlag(TableFlags.DeclSecurity) ? new DeclSecurityTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(DeclSecurityTable);
			ClassLayoutTable = TableFlags.HasFlag(TableFlags.ClassLayout) ? new ClassLayoutTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(ClassLayoutTable);
			FieldLayoutTable = TableFlags.HasFlag(TableFlags.FieldLayout) ? new FieldLayoutTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(FieldLayoutTable);
			StandAloneSigTable = TableFlags.HasFlag(TableFlags.StandAloneSig) ? new StandAloneSigTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(StandAloneSigTable);
			EventMapTable = TableFlags.HasFlag(TableFlags.EventMap) ? new EventMapTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(EventMapTable);
			EventTable = TableFlags.HasFlag(TableFlags.Event) ? new EventTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(EventTable);
			PropertyMapTable = TableFlags.HasFlag(TableFlags.PropertyMap) ? new PropertyMapTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(PropertyMapTable);
			PropertyTable = TableFlags.HasFlag(TableFlags.Property) ? new PropertyTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(PropertyTable);
			MethodSemanticsTable = TableFlags.HasFlag(TableFlags.MethodSemantics) ? new MethodSemanticsTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(MethodSemanticsTable);
			MethodImplTable = TableFlags.HasFlag(TableFlags.MethodImpl) ? new MethodImplTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(MethodImplTable);
			ModuleRefTable = TableFlags.HasFlag(TableFlags.ModuleRef) ? new ModuleRefTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(ModuleRefTable);
			TypeSpecTable = TableFlags.HasFlag(TableFlags.TypeSpec) ? new TypeSpecTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(TypeSpecTable);
			ImplMapTable = TableFlags.HasFlag(TableFlags.ImplMap) ? new ImplMapTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(ImplMapTable);
			FieldRVATable = TableFlags.HasFlag(TableFlags.FieldRVA) ? new FieldRVATable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(FieldRVATable);
			AssemblyTable = TableFlags.HasFlag(TableFlags.Assembly) ? new AssemblyTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(AssemblyTable);
			AssemblyProcessorTable = TableFlags.HasFlag(TableFlags.AssemblyProcessor) ? new AssemblyProcessorTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(AssemblyProcessorTable);
			AssemblyOSTable = TableFlags.HasFlag(TableFlags.AssemblyOS) ? new AssemblyOSTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(AssemblyOSTable);
			AssemblyRefTable = TableFlags.HasFlag(TableFlags.AssemblyRef) ? new AssemblyRefTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(AssemblyRefTable);
			AssemblyRefProcessorTable = TableFlags.HasFlag(TableFlags.AssemblyRefProcessor) ? new AssemblyRefProcessorTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(AssemblyRefProcessorTable);
			AssemblyRefOSTable = TableFlags.HasFlag(TableFlags.AssemblyRefOS) ? new AssemblyRefOSTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(AssemblyRefOSTable);
			FileTable = TableFlags.HasFlag(TableFlags.File) ? new FileTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong) : default(FileTable);
			ExportedTypeTable = TableFlags.HasFlag(TableFlags.ExportedType) ? new ExportedTypeTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(ExportedTypeTable);
			ManifestResourceTable = TableFlags.HasFlag(TableFlags.ManifestResource) ? new ManifestResourceTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(ManifestResourceTable);
			NestedClassTable = TableFlags.HasFlag(TableFlags.NestedClass) ? new NestedClassTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(NestedClassTable);
			GenericParamTable = TableFlags.HasFlag(TableFlags.GenericParam) ? new GenericParamTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(GenericParamTable);
			MethodSpecTable = TableFlags.HasFlag(TableFlags.MethodSpec) ? new MethodSpecTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(MethodSpecTable);
			GenericParamConstraintTable = TableFlags.HasFlag(TableFlags.GenericParamConstraint) ? new GenericParamConstraintTable(bs, ref index, Rows[num++], isStringLong, isGuidLong, isBlobLong, array) : default(GenericParamConstraintTable);
			if (num != TableCount)
				throw new Exception();
		}
        public override string ToString() => $"Reserved1:\t{Reserved1}\n" + $"MajorVersion:\t{MajorVersion}\n" + $"MinorVersion:\t{MinorVersion}\n" + $"HeapSizes:\t{HeapSizes}\n" + $"Reserved2:\t{Reserved2}\n" + "Valid:\t" + Convert.ToString(Valid, 2).PadLeft(64, '0') + "\nSorted:\t" + Convert.ToString(Sorted, 2).PadLeft(64, '0') + "\n" + $"TableCount:\t{TableCount:X}\n" + $"TableFlags:\t{TableFlags}\n" + "Rows:\t\t" + string.Join("\t", Array.ConvertAll(Rows, (int row) => $"{row:X}")) + "\n\n";
        public void ResolveSignature(MetadataRoot metadata)
		{
			AssemblyTable.ResolveSignature(metadata);
			ModuleTable.ResolveSignature(metadata);
			ModuleRefTable.ResolveSignature(metadata);
			AssemblyRefTable.ResolveSignature(metadata);
			TypeSpecTable.ResolveSignature(metadata);
			FieldTable.ResolveSignature(metadata);
			ParamTable.ResolveSignature(metadata);
			PropertyTable.ResolveSignature(metadata);
			FileTable.ResolveSignature(metadata);
			FieldLayoutTable.ResolveSignature(metadata);
			FieldMarshalTable.ResolveSignature(metadata);
			TypeRefTable.ResolveSignature(metadata);
			MethodDefTable.ResolveSignature(metadata);
			ConstantTable.ResolveSignature(metadata);
			FieldRVATable.ResolveSignature(metadata);
			AssemblyRefProcessorTable.ResolveSignature(metadata);
			AssemblyRefOSTable.ResolveSignature(metadata);
			ExportedTypeTable.ResolveSignature(metadata);
			TypeDefTable.ResolveSignature(metadata);
			ImplMapTable.ResolveSignature(metadata);
			ManifestResourceTable.ResolveSignature(metadata);
			ClassLayoutTable.ResolveSignature(metadata);
			InterfaceImplTable.ResolveSignature(metadata);
			MemberRefTable.ResolveSignature(metadata);
			EventTable.ResolveSignature(metadata);
			PropertyMapTable.ResolveSignature(metadata);
			NestedClassTable.ResolveSignature(metadata);
			GenericParamTable.ResolveSignature(metadata);
			EventMapTable.ResolveSignature(metadata);
			MethodSemanticsTable.ResolveSignature(metadata);
			MethodImplTable.ResolveSignature(metadata);
			MethodSpecTable.ResolveSignature(metadata);
			GenericParamConstraintTable.ResolveSignature(metadata);
			StandAloneSigTable.ResolveSignature(metadata);
			TryResolve(metadata);
		}
		public void TryResolve(MetadataRoot metadata)
		{
			CustomAttributeTable.ResolveSignature(metadata);
			DeclSecurityTable.ResolveSignature(metadata);
		}
	}
}
