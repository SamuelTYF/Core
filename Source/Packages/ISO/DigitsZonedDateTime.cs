using System.Text;

namespace ISO
{
    public class DigitsZonedDateTime
    {
        public byte[] Year;
        public byte[] Month;
        public byte[] Day;
        public byte[] Hour;
        public byte[] Minute;
        public byte[] Second;
        public byte[] HSecond;
        public byte TimeZone;
        public DateTime? DateTime;
        public DigitsZonedDateTime(byte[] value)
        {
            Year = value[0..4];
            Month = value[4..6];
            Day = value[6..8];
            Hour = value[8..10];
            Minute = value[10..12];
            Second = value[12..14];
            HSecond = value[14..16];
            TimeZone = value[16];
            DateTime = TimeZone switch
            {
                32 => new(
                int.Parse(Encoding.UTF8.GetString(Year)),
                int.Parse(Encoding.UTF8.GetString(Month)),
                int.Parse(Encoding.UTF8.GetString(Day)),
                int.Parse(Encoding.UTF8.GetString(Hour)),
                int.Parse(Encoding.UTF8.GetString(Minute)),
                int.Parse(Encoding.UTF8.GetString(Second)),
                DateTimeKind.Utc),
                0 => null,
                _ => throw new Exception("Not UTC Time")
            };
        }
        public override string ToString() => DateTime?.ToString();
    }
}
