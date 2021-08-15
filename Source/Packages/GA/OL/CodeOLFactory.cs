using System;

namespace GA.OL
{
    public class CodeOLFactory : ICodeFactory<CodeOL, int[]>
    {
        public int Length;
        public CodeOLFactory(int length) => Length = length;
        public int[] Decode(CodeOL code) => code.Values;
        public CodeOL Encode(int[] value) => throw new NotImplementedException();
        public bool IsLegal(CodeOL code) => throw new NotImplementedException();
        public bool IsLegal(int[] value) => throw new NotImplementedException();
        public void Random(out CodeOL code, out int[] value)
        {
            double[] values = new double[Length];
            value = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                values[i]=RandomHelper.NextDouble();
                value[i] = i;
            }
            Array.Sort(value, (a, b) => values[a].CompareTo(values[b]));
            code = new(value);
        }
    }
}
