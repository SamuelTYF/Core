using System;
namespace Remote.Module
{
	public class ModuleTools_Normal : ModuleTools
	{
		public override void Write(object value, int color = -1)
		{
			if (color == -1)
			{
				Console.Write(value);
				return;
			}
			Console.ForegroundColor = (ConsoleColor)color;
			Console.Write(value);
			Console.ResetColor();
		}
        public override void Throw() => throw new Exception();
    }
}
