namespace JPEG
{
    public class DHTNode
    {
        public int Length;
        public int Index;
        public int Code;
        public int Weight;
        public DHTNode(int length,int index,int code,int weight)
        {
            Length= length;
            Index = index;
            Code = code;
            Weight = weight;
        }
        public override string ToString() => System.Convert.ToString(Code, 2).PadLeft(Length, '0');
    }
}
