namespace TimeSeries;
public class HaarWavelet
{
	public int Level;
	public Series[] Data;
	public void DWT(Series source, out Series a, out Series b)
	{
		int num = source.Length >> 1;
		if ((source.Length & 1) == 1)
		{
			a = new(num + 1);
			b = new(num + 1);
			a.Values[num] = (b.Values[num] = source.Values[source.Length - 1] / 2.0);
		}
		else
		{
			a = new(num);
			b = new(num);
		}
		for (int i = 0; i < num; i++)
		{
			int num2 = i << 1;
			int num3 = num2 | 1;
			a.Values[i] = (source.Values[num2] + source.Values[num3]) / 2.0;
			b.Values[i] = (source.Values[num2] - source.Values[num3]) / 2.0;
		}
	}
	public Series IDWF(Series a, Series b)
	{
		Series series = new(a.Length << 1);
		for (int i = 0; i < a.Length; i++)
		{
			int num = i << 1;
			int num2 = num | 1;
			series.Values[num] = a.Values[i] + b.Values[i];
			series.Values[num2] = a.Values[i] - b.Values[i];
		}
		return series;
	}
	public HaarWavelet(Series source, int level)
	{
		Level = level;
		Data = new Series[Level + 1];
		Series series = source;
		for (int i = 1; i <= level; i++)
		{
			DWT(series, out var a, out var b);
			Data[i] = b;
			series = a;
		}
		Data[0] = series;
	}
	public Series GetSeries()
	{
		Series series = Data[0];
		for (int num = Level; num > 0; num--)
		{
			series = IDWF(Data[num], series);
		}
		return series;
	}
}
