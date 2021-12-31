namespace ISO
{
    public class LB16
    {
        public short Value;
        public LB16(byte[] value)
        {
            int l = 0, r = 4;
            while (l < r)
                if (value[l++] != value[--r]) throw new Exception("LB16 Error");
            Value = BitConverter.ToInt16(value, 0);
        }
        public byte[] GetBytes()
        {
            byte[] r = new byte[4];
            byte[] t = BitConverter.GetBytes(Value);
            Array.Copy(t, 0, r, 0, 2);
            Array.Reverse(t);
            Array.Copy(t, 0, r, 2, 2);
            return r;
        }
    }
}
