namespace GA.MP
{
    //TODO: 增加多维
    public class Codes:ICode
    {
        public ICode[] Values;
        public Codes(params ICode[] values)=> Values = values;
    }
}
