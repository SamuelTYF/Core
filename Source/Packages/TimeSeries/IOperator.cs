namespace TimeSeries;
public interface IOperator
{
	Series Execute(Series a, Series b);
}
