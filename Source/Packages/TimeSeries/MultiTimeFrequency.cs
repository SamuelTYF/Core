using System;
using System.Drawing;
using System.Linq;
namespace TimeSeries;
public class MultiTimeFrequency
{
	public int Length;
	public int Band;
	public double Definition;
	public Series[] Frequencies;
	public double Max;
	public MultiTimeFrequency(Series series, double duration, int band, double fs, double fstart, double fend)
	{
		int num = (int)(duration * fs);
		Definition = fs / band;
		Length = series.Length / num;
		Frequencies = new Series[Length];
		int index = (int)(fstart / Definition);
		Band = (int)((fend - fstart) / Definition);
		Max = 0.0;
		double num2 = 0.0;
		int num3 = 0;
		int num4 = 0;
		while (num3 < Length)
		{
			Frequencies[num3] = series.FFT_2_Time_Position(num4, band).Sub(Band, index).Abs();
			num2 = Frequencies[num3].Values.Max();
			if (num2 > Max)
				Max = num2;
			num3++;
			num4 += num;
		}
	}
	public Color GetColor(double t)
	{
		return Color.FromArgb((int)(t * 255.0), 0, 0);
	}
	public Series GetFrequencySeries()
	{
		Series series = new(Length);
		for (int i = 0; i < Length; i++)
		{
			double num = 0.0;
			int num2 = 0;
			for (int j = 0; j < Band; j++)
			{
				double num3;
				if (num < (num3 = Frequencies[i].Values[j]))
				{
					num = num3;
					num2 = j;
				}
			}
			series.Values[i] = num2 * Definition;
		}
		return series;
	}
}
