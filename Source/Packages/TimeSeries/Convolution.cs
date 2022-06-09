namespace TimeSeries;
public class Convolution : IOperator
{
	public DotProduct DotProduct;
	public Convolution()
	{
		DotProduct = new DotProduct();
	}
	public Series Execute(Series a, Series b)
	{
		Series series = b.Reverse();
		Series series2 = new(a.Length + b.Length - 1);
		a.CopyTo(series2, b.Length - 1);
		Series series3 = new(a.Length + b.Length - 1);
		for (int i = 0; i < a.Length - 1; i++)
		{
			series3.Values[i] = DotProduct.Multiply(series2.Values, series.Values, i, 0, series.Length);
		}
		for (int j = 0; j < b.Length; j++)
		{
			series3.Values[a.Length - 1 + j] = DotProduct.Multiply(series2.Values, series.Values, a.Length - 1 + j, 0, b.Length - j);
		}
		return series3;
	}
	public override string ToString()
	{
		return "Convolution";
	}
}
