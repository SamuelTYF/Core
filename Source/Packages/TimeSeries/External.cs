using Hook;
namespace TimeSeries;
public static class External
{
	public static readonly Manager DisplayForm = new("D:\\Core\\Time Series\\DisplayForm.exe");
	public static readonly Manager MorletForm = new("D:\\Core\\Time Series\\MorletForm.exe");
	public static void Display(this Series series)
	{
		DisplayForm.Start();
		DisplayForm.Execute("DisplaySeries", new object[1] { series });
	}
	public static void Display(this Frequency frequency, bool f = true)
	{
		DisplayForm.Start();
		DisplayForm.Execute("DisplayFrequency", new object[2] { frequency, f });
	}
	public static void Display(this PDF pdf)
	{
		DisplayForm.Start();
		DisplayForm.Execute("DisplayPDF", new object[1] { pdf });
	}
	public static void DisplayCDF(this PDF pdf)
	{
		DisplayForm.Start();
		DisplayForm.Execute("DisplayCDF", new object[1] { pdf });
	}
	public static void Display(this ComplexMorlet morlet)
	{
		MorletForm.Start();
		MorletForm.Execute("RegisterMorlet", new object[1] { morlet });
	}
	public static void Close()
	{
		DisplayForm.Dispose();
		MorletForm.Dispose();
	}
}
