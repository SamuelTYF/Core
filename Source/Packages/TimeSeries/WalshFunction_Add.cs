namespace TimeSeries;
public class WalshFunction_Add : IWalshFunction
{
	public override double InverseNegetiveFunction(double a, double b)
	{
		return (a - b) / 2.0;
	}
	public override double InversePositiveFunction(double a, double b)
	{
		return (a + b) / 2.0;
	}
	public override double NegetiveFunction(double a, double b)
	{
		return a - b;
	}
	public override double PositiveFunction(double a, double b)
	{
		return a + b;
	}
}
