namespace ISO
{
    public class LB32
    {
        public int Value;
        public LB32(byte[] value)
        {
            int l = 0, r = 8;
            while (l < r)
                if (value[l++] != value[--r]) throw new Exception("LB16 Error");
            Value = BitConverter.ToInt32(value, 0);
        }
        public byte[] GetBytes()
        {
            byte[] r = new byte[8];
            byte[] t = BitConverter.GetBytes(Value);
            Array.Copy(t, 0, r, 0, 4);
            Array.Reverse(t);
            Array.Copy(t, 0, r, 4, 4);
            return r;
        }
    }
}
