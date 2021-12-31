namespace ISO
{
    public class DateAndTime
    {
        public byte Year;
        public byte Month;
        public byte Day;
        public byte Hour;
        public byte Minute;
        public byte Second;
        public byte TimeZone;
        public DateTime? DateTime;
        public DateAndTime(byte[] value)
        {
            Year = value[0];
            Month = value[1];
            Day = value[2];
            Hour = value[3];
            Minute = value[4];
            Second = value[5];
            TimeZone = value[6];
            DateTime = TimeZone switch
            {
                32 => new(Year+1900, Month, Day, Hour, Minute, Second, DateTimeKind.Utc),
                0 => null,
                _ => throw new Exception("Not UTC Time")
            };
        }
        public override string ToString() => DateTime?.ToString();
    }
}
