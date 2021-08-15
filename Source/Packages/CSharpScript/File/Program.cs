using System;
using Collection;
namespace CSharpScript.File
{
	public class Program
	{
		private static void Run()
		{
			Console.WriteLine(new PEManager().Load("C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\mscorlib.dll").CLIHeader);
		}
		private static void Main(string[] args)
		{
			Run();
			Console.ReadLine();
		}
		private static void A(string name, int count)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			int num = -1;
			for (int i = 0; i < count; i++)
			{
				string text4 = Console.ReadLine();
				int num2 = int.Parse(text4.Substring(text4.IndexOf("/") + 1, text4.Length - text4.IndexOf("/") - 1));
				if (num < 0)
					num = num2;
				num2 -= num;
				string text5 = Console.ReadLine();
				int num3 = int.Parse(text5.Substring(text5.IndexOf("/") + 1, text5.Length - text5.IndexOf("/") - 1));
				string text6 = Console.ReadLine().Replace(" ", "");
				string text7 = Console.ReadLine();
				switch (num3)
				{
				case 1:
					text = text + "\t\t\t/// <summary>\n\t\t\t/// " + text7 + "\n\t\t\t/// </summary>\n\t\t\tpublic byte " + text6 + ";\n";
					text2 += $"\t\t\t{text6}=bs[{num2}];\n";
					text3 = text3 + "\t\t\t\t$\"" + text6 + ":\\t{" + text6 + "}\\n\"+\n";
					break;
				case 2:
					text = text + "\t\t\t/// <summary>\n\t\t\t/// " + text7 + "\n\t\t\t/// </summary>\n\t\t\tpublic short " + text6 + ";\n";
					text2 += $"\t\t\t{text6}=(short)(bs[{num2}]|(bs[{num2 + 1}]<<8));\n";
					text3 = text3 + "\t\t\t\t$\"" + text6 + ":\\t{" + text6 + "}\\n\"+\n";
					break;
				case 4:
					text = text + "\t\t\t/// <summary>\n\t\t\t/// " + text7 + "\n\t\t\t/// </summary>\n\t\t\tpublic int " + text6 + ";\n";
					text2 += $"\t\t\t{text6}=bs[{num2}]|(bs[{num2 + 1}]<<8)|(bs[{num2 + 2}]<<16)|(bs[{num2 + 3}]<<24);\n";
					text3 = text3 + "\t\t\t\t$\"" + text6 + ":\\t{" + text6 + "}\\n\"+\n";
					break;
				case 8:
				{
					text = text + "\t\t\t/// <summary>\n\t\t\t/// " + text7 + "\n\t\t\t/// </summary>\n\t\t\tpublic long " + text6 + ";\n";
					text2 += $"\t\t\t{text6}=bs[{num2}]";
					for (int j = 1; j < num3; j++)
					{
						text2 += $"|((long)bs[{num2 + j}]<<{j * 8})";
					}
					text2 += ";\n";
					text3 = text3 + "\t\t\t\t$\"" + text6 + ":\\t{" + text6 + "}\\n\"+\n";
					break;
				}
				default:
					throw new Exception();
				}
			}
			text3 = text3.Substring(0, text3.Length - 2) + ";";
			Console.Clear();
			Console.WriteLine("\t\tpublic struct " + name + "\n\t\t{\n" + text + "\n\t\t\tpublic " + name + "(byte[] bs)\n\t\t\t{\n" + text2 + "\t\t\t}\n\t\t\tpublic override string ToString() =>" + text3 + "\t\t}");
		}
		private static void B(string name, int count)
		{
			string text = "";
			string text2 = null;
			string text3 = null;
			for (int i = 0; i < count; i++)
			{
				string text4 = Console.ReadLine().Replace(" ", "");
				string text5 = Console.ReadLine();
				string text6 = Console.ReadLine();
				text = text + "\t\t\t\t/// <summary>\n\t\t\t\t/// " + text6 + "\n\t\t\t\t/// </summary>\n\t\t\t\t" + text4 + "=" + text5 + ",\n";
				text2 = ((text2 != null) ? (text2 + "," + text5) : text5);
				text3 = ((text3 != null) ? (text3 + "," + name + "." + text4) : (name + "." + text4));
			}
			Console.Clear();
			Console.WriteLine("\t\tpublic struct " + name + "s\n\t\t{\n\t\t\tpublic enum " + name + ":ushort\n\t\t\t{\n" + text + "\t\t\t}\n\t\t\t\tpublic ushort Value;\n\t\tpublic static readonly ushort[] _" + name + "Values={" + text2 + "};\n\t\tpublic static readonly " + name + "[] _" + name + "s={" + text3 + "};\n\t\tpublic " + name + "s(ushort value)=>Value=value;\n\t\tpublic override string ToString()\n\t\t\t{\n\t\t\t\tstring s = null;\n\t\t\t\tfor (int i = 0; i < _" + name + "Values.Length; i++)\n\t\t\t\t\tif ((Value & _" + name + "Values[i]) != 0)\n\t\t\t\t\t\tif (s == null)\n\t\t\t\t\t\t\ts = _" + name + "s[i].ToString();\n\t\t\t\t\t\telse s += $\"|{_" + name + "s[i]}\";\n\t\t\t\treturn s ?? $\"{Value:X}\";\n\t\t\t}\n\t\t}");
		}
		private static void C(string name, int count)
		{
			string text = "";
			for (int i = 0; i < count; i++)
			{
				string text2 = Console.ReadLine();
				int num = text2.IndexOf(" ");
				int num2 = text2.IndexOf(" ", num + 1);
				string text3 = text2.Substring(0, num);
				string text4 = text2.Substring(num + 1, num2 - num - 1);
				string text5 = text2.Substring(num2 + 1, text2.Length - num2 - 1);
				text = text + "\t\t\t\t/// <summary>\n\t\t\t\t/// " + text5 + "\n\t\t\t\t/// </summary>\n\t\t\t\t" + text3 + "=" + text4 + ",\n";
			}
			Console.Clear();
			Console.WriteLine("\t\t[Flags]\n\t\tpublic enum " + name + "\n\t\t\t{\n" + text + "\t\t\t}");
		}
		private static void D()
		{
			string text = Console.ReadLine();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			List<string> list4 = new List<string>();
			while (true)
			{
				string text2 = Console.ReadLine().Replace(" ", "");
				if (text2 == "")
					break;
				string text3 = Console.ReadLine();
				string text4 = Console.ReadLine();
				list.Add("\t\t/// <summary>\n\t\t/// " + text4 + "\n\t\t/// </summary>\n\t\tpublic int " + text2 + ";");
				list2.Add("{" + text2 + ":X}");
				list3.Add(text2);
				switch (text3)
				{
				case "2":
					list4.Add("\t\t\t\t\t" + text2 + " = TildeHeap.Read(bs, ref index, false)");
					break;
				case "4":
					list4.Add("\t\t\t\t\t" + text2 + " = TildeHeap.Read(bs, ref index, true)");
					break;
				case "Blob":
					list4.Add("\t\t\t\t\t" + text2 + " = TildeHeap.Read(bs, ref index, isBlobLong)");
					break;
				case "String":
					list4.Add("\t\t\t\t\t" + text2 + " = TildeHeap.Read(bs, ref index, isStringLong)");
					break;
				case "Guid":
					list4.Add("\t\t\t\t\t" + text2 + " = TildeHeap.Read(bs, ref index, isGuidLong)");
					break;
				default:
					throw new Exception();
				}
			}
			Console.Clear();
			Console.WriteLine("\tpublic struct " + text + "Row");
			Console.WriteLine("\t{");
			Console.WriteLine(string.Join("\n", list));
			Console.WriteLine("\t\tpublic override string ToString()=>$\"" + string.Join("\\t", list2) + "\";");
			Console.WriteLine("\t}");
			Console.WriteLine("\tpublic struct " + text + "Table");
			Console.WriteLine("\t{");
			Console.WriteLine("\t\tpublic " + text + "Row[] " + text + "Rows;");
			Console.WriteLine("\t\tpublic " + text + "Table(byte[] bs,ref int index,int count,bool isStringLong,bool isGuidLong,bool isBlobLong)");
			Console.WriteLine("\t\t{");
			Console.WriteLine("\t\t\t" + text + "Rows=new " + text + "Row[count];");
			Console.WriteLine("\t\t\tfor (int i = 0; i < count; i++)");
			Console.WriteLine("\t\t\t" + text + "Rows[i]=new " + text + "Row()");
			Console.WriteLine("\t\t\t{");
			Console.WriteLine(string.Join(",\n", list4));
			Console.WriteLine("\t\t\t};");
			Console.WriteLine("\t\t}");
			Console.WriteLine("\t\tpublic override string ToString()=>$\"" + string.Join("\\t", list3) + "\\n{string.Join(\"\\n\"," + text + "Rows)}\";");
			Console.WriteLine("\t}");
		}
		private static void E()
		{
			string[] names = Enum.GetNames(typeof(TableFlags));
			foreach (string text in names)
			{
				Console.WriteLine($"\t\t\tif (TableFlags.HasFlag(TableFlags.{text}))TableCounts[0x{(int)Math.Log((long)Enum.Parse(typeof(TableFlags), text), 2.0):X}]=Rows[p++];");
			}
		}
		private static void F()
		{
			string text = Console.ReadLine();
			string text2 = "";
			string text3 = Console.ReadLine();
			while (text3 != "")
			{
				text2 = text2 + "\t\t" + text3.Replace(" ", "=") + ",\n";
				text3 = Console.ReadLine();
			}
			Console.Clear();
			Console.WriteLine("\t[Flags]\n\tpublic enum " + text + "\n\t{\n" + text2 + "\t}");
		}
		private static void G()
		{
			string text = Console.ReadLine();
			List<string> list = new List<string>();
			while (text != "")
			{
				if (Enum.TryParse<TableFlags>(text, out var result))
					list.Add($"(Rows[0x{(int)Math.Log((double)result, 2.0):X}]>>13!=0)");
				text = Console.ReadLine();
			}
			Console.Clear();
			Console.WriteLine(string.Join("||", list));
		}
		private static void H()
		{
			Operator[] operators = Operators.Operators1;
			foreach (Operator @operator in operators)
			{
				if (@operator != null)
					Console.WriteLine("\t\t\t\tcase 0x" + $"{@operator.Value:X}".PadLeft(4, '0') + ":break;//" + @operator.Name);
			}
			Operator[] operators2 = Operators.Operators2;
			foreach (Operator operator2 in operators2)
			{
				if (operator2 != null)
					Console.WriteLine("\t\t\t\tcase 0x" + $"{operator2.Value:X}".PadLeft(4, '0') + ":break;//" + operator2.Name);
			}
		}
	}
}
