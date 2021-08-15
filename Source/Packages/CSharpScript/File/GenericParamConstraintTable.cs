namespace CSharpScript.File
{
	public struct GenericParamConstraintTable
	{
		public int Count;
		public GenericParamConstraint[] GenericParamConstraints;
		public GenericParamConstraintTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			GenericParamConstraints = new GenericParamConstraint[count];
			bool islong = Rows[42] >> 16 != 0;
			bool islong2 = Rows[1] >> 14 != 0 || Rows[2] >> 14 != 0 || Rows[27] >> 14 != 0;
			for (int i = 0; i < count; i++)
			{
				GenericParamConstraints[i] = new GenericParamConstraint(new GenericParamConstraintRow
				{
					Index = i + 1,
					Owner = TildeHeap.Read(bs, ref index, islong),
					Constraint = TildeHeap.Read(bs, ref index, islong2)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (GenericParamConstraints != null)
			{
				GenericParamConstraint[] genericParamConstraints = GenericParamConstraints;
				for (int i = 0; i < genericParamConstraints.Length; i++)
				{
					genericParamConstraints[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
