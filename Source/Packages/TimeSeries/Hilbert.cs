namespace TimeSeries;
public static class Hilbert
{
	public static ComplexSeries Translate(Series a)
	{
		Frequency frequency = a.FFT_2_Time_Position();
		int num = frequency.Length >> 1;
		for (int i = 0; i < num; i++)
		{
			frequency.Values[i] /= Complex.i;
		}
		for (int j = num; j < frequency.Length; j++)
		{
			frequency.Values[j] *= Complex.i;
		}
		return frequency.IFFT_2_Time_Position_Complex(a.Length);
	}
}
