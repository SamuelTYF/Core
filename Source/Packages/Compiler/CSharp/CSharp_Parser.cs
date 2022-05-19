using Compiler.CSharp.Metadata;
using Compiler.CSharp.Searching;

namespace Compiler.CSharp
{
	public class CSharp_Parser : IParser<Token, object, ParsingFile>
	{
		public static readonly int[,] VariableTable ={
		{-1,1,-1,2,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,4,5,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,8,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,10,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,12,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,14,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,17,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,19,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,21,-1,-1,-1,-1,-1,22,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,24,25,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,26,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,34,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,36,-1,-1,-1,-1,-1,-1,-1,-1,37,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,39,40,41,-1,-1,-1,42,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,43,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,44,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,50,51,52,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,55,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,56,57,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,69,70,71,72,73,74,75,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,87,70,71,72,73,74,75,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,97,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,98,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,103,70,71,72,73,74,75,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,-1,-1,104,-1,-1,74,75,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,-1,-1,105,-1,-1,74,75,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,106,70,71,72,73,74,75,76,77,78,79,80,81,82,107,108,109},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,110,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,111,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,112,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,115,70,71,72,73,74,75,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,116,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,117,118,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,123,51,52,-1,-1,-1,-1,-1,124,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,-1,-1,127,-1,-1,74,75,76,77,78,79,80,81,82,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,68,51,52,-1,-1,-1,-1,-1,-1,-1,106,70,71,72,73,74,75,76,77,78,79,80,81,82,-1,-1,128},
		{-1,-1,129,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,131,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,132,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,123,51,52,-1,-1,-1,-1,-1,134,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,135,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1}
	};
		public override ParsingFile Parse(ITokenizer<Token> tokenizer)
		{
			Init();
			File = new();
			Root = new();
			Root.LoadAssembly(typeof(string).Assembly);
			Token token = tokenizer.Get();
			int symbol = 0;
			bool mode = true;
			Token[] tokens;
			object[] values;
			object value;
			while (true)
			{
				if (mode)
				{
					switch (StateStack.Peek())
					{
						case 0:
							if (token.Type == "EOF")
							{

								ValueStack.Push(null);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "using")
							{

								ValueStack.Push(null);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "namespace")
							{

								ValueStack.Push(null);
								symbol = 3;
								mode = false;
							}
							else return Error(token);
							break;
						case 1:
							if (token.Type == "EOF")
							{
								values = PopValue(1);
								File.Build(new(Root));
								return File;
								ValueStack.Push(null);
								symbol = 0;
								mode = false;
							}
							else return Error(token);
							break;
						case 2:
							if (token.Type == "EOF")
							{
								Namespace = new();
								ValueStack.Push(null);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == "using")
							{
								TokenStack.Push(token);
								StateStack.Push(3);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "namespace")
							{
								Namespace = new();
								ValueStack.Push(null);
								symbol = 4;
								mode = false;
							}
							else return Error(token);
							break;
						case 3:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(6);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 4:
							if (token.Type == "EOF")
							{
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "namespace")
							{
								TokenStack.Push(token);
								StateStack.Push(7);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 5:
							if (token.Type == "EOF")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "using")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "namespace")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 6:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(11);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 7:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(13);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 8:
							if (token.Type == "EOF")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "namespace")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 9:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "using")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Symbol")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "namespace")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "{")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "}")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "class")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "void")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "int")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "UInt32")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Int32")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Double")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "String")
							{
								tokens = PopToken(1);

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 10:
							if (token.Type == "EOF")
							{
								values = PopValue(3);

								ValueStack.Push(null);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "using")
							{
								values = PopValue(3);

								ValueStack.Push(null);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "namespace")
							{
								values = PopValue(3);

								ValueStack.Push(null);
								symbol = 3;
								mode = false;
							}
							else return Error(token);
							break;
						case 11:
							if (token.Type == " ")
							{
								tokens = PopToken(1);
								NamespaceName = new();
								NamespaceName.Add(tokens[0].Value_String);
								ValueStack.Push(null);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == ";")
							{
								tokens = PopToken(1);
								NamespaceName = new();
								NamespaceName.Add(tokens[0].Value_String);
								ValueStack.Push(null);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(1);
								NamespaceName = new();
								NamespaceName.Add(tokens[0].Value_String);
								ValueStack.Push(null);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "{")
							{
								tokens = PopToken(1);
								NamespaceName = new();
								NamespaceName.Add(tokens[0].Value_String);
								ValueStack.Push(null);
								symbol = 6;
								mode = false;
							}
							else return Error(token);
							break;
						case 12:
							if (token.Type == ";")
							{
								TokenStack.Push(token);
								StateStack.Push(15);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ".")
							{
								TokenStack.Push(token);
								StateStack.Push(16);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 13:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(11);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 14:
							if (token.Type == "EOF")
							{
								values = PopValue(3);
								File.InsertNamespace(Namespace);
								Namespace = new();
								ValueStack.Push(null);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == "namespace")
							{
								values = PopValue(3);
								File.InsertNamespace(Namespace);
								Namespace = new();
								ValueStack.Push(null);
								symbol = 4;
								mode = false;
							}
							else return Error(token);
							break;
						case 15:
							if (token.Type == "EOF")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								UsingNamespace();
								ValueStack.Push(null);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == " ")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								UsingNamespace();
								ValueStack.Push(null);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "using")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								UsingNamespace();
								ValueStack.Push(null);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "namespace")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								UsingNamespace();
								ValueStack.Push(null);
								symbol = 5;
								mode = false;
							}
							else return Error(token);
							break;
						case 16:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(18);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 17:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ".")
							{
								TokenStack.Push(token);
								StateStack.Push(16);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "{")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 18:
							if (token.Type == " ")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								NamespaceName.Add(tokens[1].Value_String);
								ValueStack.Push(null);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == ";")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								NamespaceName.Add(tokens[1].Value_String);
								ValueStack.Push(null);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								NamespaceName.Add(tokens[1].Value_String);
								ValueStack.Push(null);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "{")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								NamespaceName.Add(tokens[1].Value_String);
								ValueStack.Push(null);
								symbol = 6;
								mode = false;
							}
							else return Error(token);
							break;
						case 19:
							if (token.Type == "{")
							{
								TokenStack.Push(token);
								StateStack.Push(20);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 20:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "}")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "class")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "public")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "static")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 21:
							if (token.Type == "}")
							{
								values = PopValue(1);
								Type = new();
								ValueStack.Push(null);
								symbol = 8;
								mode = false;
							}
							else if (token.Type == "class")
							{
								values = PopValue(1);
								Type = new();
								ValueStack.Push(null);
								symbol = 8;
								mode = false;
							}
							else if (token.Type == "public")
							{
								values = PopValue(1);
								Type = new();
								ValueStack.Push(null);
								symbol = 8;
								mode = false;
							}
							else if (token.Type == "static")
							{
								values = PopValue(1);
								Type = new();
								ValueStack.Push(null);
								symbol = 8;
								mode = false;
							}
							else return Error(token);
							break;
						case 22:
							if (token.Type == "}")
							{
								TokenStack.Push(token);
								StateStack.Push(23);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "class")
							{
								value = new List<string>();
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else if (token.Type == "public")
							{
								value = new List<string>();
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else if (token.Type == "static")
							{
								value = new List<string>();
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else return Error(token);
							break;
						case 23:
							if (token.Type == "EOF")
							{
								tokens = PopToken(4);
								values = PopValue(3);
								DefineNamespace();
								ValueStack.Push(null);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == " ")
							{
								tokens = PopToken(4);
								values = PopValue(3);
								DefineNamespace();
								ValueStack.Push(null);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "namespace")
							{
								tokens = PopToken(4);
								values = PopValue(3);
								DefineNamespace();
								ValueStack.Push(null);
								symbol = 7;
								mode = false;
							}
							else return Error(token);
							break;
						case 24:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "}")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "class")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "public")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "static")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 25:
							if (token.Type == "class")
							{
								TokenStack.Push(token);
								StateStack.Push(27);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "public")
							{
								TokenStack.Push(token);
								StateStack.Push(28);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "static")
							{
								TokenStack.Push(token);
								StateStack.Push(29);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 26:
							if (token.Type == "}")
							{
								values = PopValue(3);
								Namespace.InsertType(Type);
								Type = new();
								ValueStack.Push(null);
								symbol = 8;
								mode = false;
							}
							else if (token.Type == "class")
							{
								values = PopValue(3);
								Namespace.InsertType(Type);
								Type = new();
								ValueStack.Push(null);
								symbol = 8;
								mode = false;
							}
							else if (token.Type == "public")
							{
								values = PopValue(3);
								Namespace.InsertType(Type);
								Type = new();
								ValueStack.Push(null);
								symbol = 8;
								mode = false;
							}
							else if (token.Type == "static")
							{
								values = PopValue(3);
								Namespace.InsertType(Type);
								Type = new();
								ValueStack.Push(null);
								symbol = 8;
								mode = false;
							}
							else return Error(token);
							break;
						case 27:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(30);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 28:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(31);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 29:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(32);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 30:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(33);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 31:
							if (token.Type == "class")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else return Error(token);
							break;
						case 32:
							if (token.Type == "class")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 10;
								mode = false;
							}
							else return Error(token);
							break;
						case 33:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "{")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 34:
							if (token.Type == "{")
							{
								TokenStack.Push(token);
								StateStack.Push(35);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 35:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Symbol")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "}")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "public")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "static")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "void")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "int")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 36:
							if (token.Type == "Symbol")
							{
								values = PopValue(1);
								Field = new();
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "}")
							{
								values = PopValue(1);
								Field = new();
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "public")
							{
								values = PopValue(1);
								Field = new();
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "static")
							{
								values = PopValue(1);
								Field = new();
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "void")
							{
								values = PopValue(1);
								Field = new();
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "int")
							{
								values = PopValue(1);
								Field = new();
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else return Error(token);
							break;
						case 37:
							if (token.Type == "Symbol")
							{
								value = new List<string>();
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "}")
							{
								TokenStack.Push(token);
								StateStack.Push(38);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "public")
							{
								value = new List<string>();
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "static")
							{
								value = new List<string>();
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "void")
							{
								value = new List<string>();
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "int")
							{
								value = new List<string>();
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else return Error(token);
							break;
						case 38:
							if (token.Type == " ")
							{
								tokens = PopToken(5);
								values = PopValue(3);
								Type.Name = tokens[2].Value_String;
								Type.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 9;
								mode = false;
							}
							else if (token.Type == "}")
							{
								tokens = PopToken(5);
								values = PopValue(3);
								Type.Name = tokens[2].Value_String;
								Type.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 9;
								mode = false;
							}
							else if (token.Type == "class")
							{
								tokens = PopToken(5);
								values = PopValue(3);
								Type.Name = tokens[2].Value_String;
								Type.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 9;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(5);
								values = PopValue(3);
								Type.Name = tokens[2].Value_String;
								Type.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 9;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(5);
								values = PopValue(3);
								Type.Name = tokens[2].Value_String;
								Type.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 9;
								mode = false;
							}
							else return Error(token);
							break;
						case 39:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Symbol")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "}")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "public")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "static")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "void")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "int")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 40:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Symbol")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "}")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "public")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "static")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "void")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "int")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 41:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "public")
							{
								TokenStack.Push(token);
								StateStack.Push(46);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "static")
							{
								TokenStack.Push(token);
								StateStack.Push(47);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 42:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ";")
							{
								TokenStack.Push(token);
								StateStack.Push(53);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "{")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "=>")
							{
								TokenStack.Push(token);
								StateStack.Push(54);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 43:
							if (token.Type == "Symbol")
							{
								values = PopValue(3);
								Type.InsertField(Field);
								Field = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "}")
							{
								values = PopValue(3);
								Type.InsertField(Field);
								Field = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "public")
							{
								values = PopValue(3);
								Type.InsertField(Field);
								Field = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "static")
							{
								values = PopValue(3);
								Type.InsertField(Field);
								Field = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "void")
							{
								values = PopValue(3);
								Type.InsertField(Field);
								Field = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "int")
							{
								values = PopValue(3);
								Type.InsertField(Field);
								Field = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else return Error(token);
							break;
						case 44:
							if (token.Type == "Symbol")
							{
								values = PopValue(3);
								Type.InsertMethod(Method);
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "}")
							{
								values = PopValue(3);
								Type.InsertMethod(Method);
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "public")
							{
								values = PopValue(3);
								Type.InsertMethod(Method);
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "static")
							{
								values = PopValue(3);
								Type.InsertMethod(Method);
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "void")
							{
								values = PopValue(3);
								Type.InsertMethod(Method);
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else if (token.Type == "int")
							{
								values = PopValue(3);
								Type.InsertMethod(Method);
								Method = new();
								ValueStack.Push(null);
								symbol = 11;
								mode = false;
							}
							else return Error(token);
							break;
						case 45:
							if (token.Type == " ")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ";")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(1);
								value = new List<string>();
								(value as List<string>).Add(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else return Error(token);
							break;
						case 46:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(58);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 47:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(59);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 48:
							if (token.Type == " ")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ";")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Void" };
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else return Error(token);
							break;
						case 49:
							if (token.Type == " ")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == ";")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(1);
								value = new string[] { "System", "Int32" };
								ValueStack.Push(value);
								symbol = 16;
								mode = false;
							}
							else return Error(token);
							break;
						case 50:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(60);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 51:
							if (token.Type == " ")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "/")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "%")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else return Error(token);
							break;
						case 52:
							if (token.Type == " ")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ";")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ".")
							{
								TokenStack.Push(token);
								StateStack.Push(61);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "/")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else if (token.Type == "%")
							{
								values = PopValue(1);
								value = (values[0] as List<string>).ToArray();
								ValueStack.Push(value);
								symbol = 15;
								mode = false;
							}
							else return Error(token);
							break;
						case 53:
							if (token.Type == " ")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								Method.IsAbstract = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "Symbol")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								Method.IsAbstract = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "}")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								Method.IsAbstract = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								Method.IsAbstract = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								Method.IsAbstract = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "void")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								Method.IsAbstract = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "int")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								Method.IsAbstract = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else return Error(token);
							break;
						case 54:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 55:
							if (token.Type == "{")
							{
								TokenStack.Push(token);
								StateStack.Push(83);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 56:
							if (token.Type == ";")
							{
								TokenStack.Push(token);
								StateStack.Push(84);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 57:
							if (token.Type == " ")
							{
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "Symbol")
							{
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "}")
							{
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "public")
							{
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "static")
							{
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "void")
							{
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "int")
							{
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else return Error(token);
							break;
						case 58:
							if (token.Type == "Symbol")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "void")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "int")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else return Error(token);
							break;
						case 59:
							if (token.Type == "Symbol")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "void")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else if (token.Type == "int")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[0].Type);
								ValueStack.Push(value);
								symbol = 14;
								mode = false;
							}
							else return Error(token);
							break;
						case 60:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(85);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 61:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(86);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 62:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 63:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(1);
								value = new Command_Constant_UInt32(tokens[0].Value_UInt32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else return Error(token);
							break;
						case 64:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(1);
								value = new Command_Constant<int>(tokens[0].Value_Int32.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else return Error(token);
							break;
						case 65:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(1);
								value = new Command_Constant<double>(tokens[0].Value_Double.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else return Error(token);
							break;
						case 66:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(1);
								value = new Command_Constant_Char(tokens[0].Value_Char.Value);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else return Error(token);
							break;
						case 67:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(1);
								value = new Command_Constant_String(tokens[0].Value_String);
								ValueStack.Push(value);
								symbol = 38;
								mode = false;
							}
							else return Error(token);
							break;
						case 68:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(88);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ";")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "/")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "%")
							{
								values = PopValue(1);
								value = new Command_Load(values[0] as string[]);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else return Error(token);
							break;
						case 69:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								Method.Commands.Add(values[0] as ICommand);
								ValueStack.Push(null);
								symbol = 19;
								mode = false;
							}
							else return Error(token);
							break;
						case 70:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 25;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 25;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 25;
								mode = false;
							}
							else return Error(token);
							break;
						case 71:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 28;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 28;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 28;
								mode = false;
							}
							else if (token.Type == "=")
							{
								TokenStack.Push(token);
								StateStack.Push(89);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 28;
								mode = false;
							}
							else return Error(token);
							break;
						case 72:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 25;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 25;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 25;
								mode = false;
							}
							else if (token.Type == "?")
							{
								TokenStack.Push(token);
								StateStack.Push(90);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 73:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 28;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 28;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 28;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 28;
								mode = false;
							}
							else return Error(token);
							break;
						case 74:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else return Error(token);
							break;
						case 75:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 27;
								mode = false;
							}
							else if (token.Type == "+")
							{
								TokenStack.Push(token);
								StateStack.Push(91);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 76:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else return Error(token);
							break;
						case 77:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(92);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 31;
								mode = false;
							}
							else if (token.Type == "*")
							{
								TokenStack.Push(token);
								StateStack.Push(93);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "/")
							{
								TokenStack.Push(token);
								StateStack.Push(94);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "%")
							{
								TokenStack.Push(token);
								StateStack.Push(95);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 78:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ".")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "/")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "%")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else return Error(token);
							break;
						case 79:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == ".")
							{
								TokenStack.Push(token);
								StateStack.Push(96);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "/")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "%")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else return Error(token);
							break;
						case 80:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "/")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else if (token.Type == "%")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 33;
								mode = false;
							}
							else return Error(token);
							break;
						case 81:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ".")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "/")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "%")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else return Error(token);
							break;
						case 82:
							if (token.Type == ";")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ".")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "=")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "?")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == ":")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "/")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else if (token.Type == "%")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 35;
								mode = false;
							}
							else return Error(token);
							break;
						case 83:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Symbol")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "}")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "void")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "int")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "(")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "UInt32")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Int32")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Double")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Char")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "String")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 84:
							if (token.Type == " ")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								Method.Lambda = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "Symbol")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								Method.Lambda = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "}")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								Method.Lambda = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								Method.Lambda = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								Method.Lambda = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "void")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								Method.Lambda = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else if (token.Type == "int")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								Method.Lambda = true;
								ValueStack.Push(null);
								symbol = 13;
								mode = false;
							}
							else return Error(token);
							break;
						case 85:
							if (token.Type == ";")
							{
								TokenStack.Push(token);
								StateStack.Push(99);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(100);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 86:
							if (token.Type == " ")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ";")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as List<string>).Add(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 17;
								mode = false;
							}
							else return Error(token);
							break;
						case 87:
							if (token.Type == ")")
							{
								TokenStack.Push(token);
								StateStack.Push(101);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 88:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(102);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 89:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 90:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 91:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 92:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ")")
							{
								value = new List<ICommand>();
								ValueStack.Push(value);
								symbol = 39;
								mode = false;
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 93:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 94:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 95:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 96:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(113);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 97:
							if (token.Type == "Symbol")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "}")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "void")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "int")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "UInt32")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "Int32")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "Double")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "String")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else return Error(token);
							break;
						case 98:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "}")
							{
								TokenStack.Push(token);
								StateStack.Push(114);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 99:
							if (token.Type == " ")
							{
								tokens = PopToken(3);
								values = PopValue(2);
								Field.Name = tokens[1].Value_String;
								Field.TypeFullName = values[1] as string[];
								Field.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 12;
								mode = false;
							}
							else if (token.Type == "Symbol")
							{
								tokens = PopToken(3);
								values = PopValue(2);
								Field.Name = tokens[1].Value_String;
								Field.TypeFullName = values[1] as string[];
								Field.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 12;
								mode = false;
							}
							else if (token.Type == "}")
							{
								tokens = PopToken(3);
								values = PopValue(2);
								Field.Name = tokens[1].Value_String;
								Field.TypeFullName = values[1] as string[];
								Field.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 12;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(3);
								values = PopValue(2);
								Field.Name = tokens[1].Value_String;
								Field.TypeFullName = values[1] as string[];
								Field.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 12;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(3);
								values = PopValue(2);
								Field.Name = tokens[1].Value_String;
								Field.TypeFullName = values[1] as string[];
								Field.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 12;
								mode = false;
							}
							else if (token.Type == "void")
							{
								tokens = PopToken(3);
								values = PopValue(2);
								Field.Name = tokens[1].Value_String;
								Field.TypeFullName = values[1] as string[];
								Field.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 12;
								mode = false;
							}
							else if (token.Type == "int")
							{
								tokens = PopToken(3);
								values = PopValue(2);
								Field.Name = tokens[1].Value_String;
								Field.TypeFullName = values[1] as string[];
								Field.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 12;
								mode = false;
							}
							else return Error(token);
							break;
						case 100:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Symbol")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "void")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "int")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == ")")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 101:
							if (token.Type == ";")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else return Error(token);
							break;
						case 102:
							if (token.Type == ";")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								Method.RegisterVariable(values[0] as string[], tokens[1].Value_String);
								value = new Command_Load(tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 36;
								mode = false;
							}
							else return Error(token);
							break;
						case 103:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Set(values[0] as ICommand, values[1] as ICommand);
								ValueStack.Push(value);
								symbol = 26;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Set(values[0] as ICommand, values[1] as ICommand);
								ValueStack.Push(value);
								symbol = 26;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Set(values[0] as ICommand, values[1] as ICommand);
								ValueStack.Push(value);
								symbol = 26;
								mode = false;
							}
							else return Error(token);
							break;
						case 104:
							if (token.Type == ":")
							{
								TokenStack.Push(token);
								StateStack.Push(119);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 105:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "+");
								ValueStack.Push(value);
								symbol = 30;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "+");
								ValueStack.Push(value);
								symbol = 30;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "+");
								ValueStack.Push(value);
								symbol = 30;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "+");
								ValueStack.Push(value);
								symbol = 30;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "+");
								ValueStack.Push(value);
								symbol = 30;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "+");
								ValueStack.Push(value);
								symbol = 30;
								mode = false;
							}
							else return Error(token);
							break;
						case 106:
							if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 41;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 41;
								mode = false;
							}
							else return Error(token);
							break;
						case 107:
							if (token.Type == ")")
							{
								TokenStack.Push(token);
								StateStack.Push(120);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 108:
							if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 39;
								mode = false;
							}
							else if (token.Type == ",")
							{
								TokenStack.Push(token);
								StateStack.Push(121);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 109:
							if (token.Type == ")")
							{
								values = PopValue(1);
								value = new List<ICommand>();
								(value as List<ICommand>).Add(values[0] as ICommand);
								ValueStack.Push(value);
								symbol = 40;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(1);
								value = new List<ICommand>();
								(value as List<ICommand>).Add(values[0] as ICommand);
								ValueStack.Push(value);
								symbol = 40;
								mode = false;
							}
							else return Error(token);
							break;
						case 110:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "*");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "*");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "*");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "*");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "*");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "*");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "*");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else return Error(token);
							break;
						case 111:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "/");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "/");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "/");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "/");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "/");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "/");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "/");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else return Error(token);
							break;
						case 112:
							if (token.Type == ";")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "%");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "%");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "%");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "%");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "%");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "%");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = new Command_Operator2(values[0] as ICommand, values[1] as ICommand, "%");
								ValueStack.Push(value);
								symbol = 32;
								mode = false;
							}
							else return Error(token);
							break;
						case 113:
							if (token.Type == ";")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Command_Get(values[0] as ICommand, tokens[1].Value_String);
								ValueStack.Push(value);
								symbol = 37;
								mode = false;
							}
							else return Error(token);
							break;
						case 114:
							if (token.Type == " ")
							{
								tokens = PopToken(2);
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 20;
								mode = false;
							}
							else if (token.Type == "Symbol")
							{
								tokens = PopToken(2);
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 20;
								mode = false;
							}
							else if (token.Type == "}")
							{
								tokens = PopToken(2);
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 20;
								mode = false;
							}
							else if (token.Type == "public")
							{
								tokens = PopToken(2);
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 20;
								mode = false;
							}
							else if (token.Type == "static")
							{
								tokens = PopToken(2);
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 20;
								mode = false;
							}
							else if (token.Type == "void")
							{
								tokens = PopToken(2);
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 20;
								mode = false;
							}
							else if (token.Type == "int")
							{
								tokens = PopToken(2);
								values = PopValue(2);

								ValueStack.Push(null);
								symbol = 20;
								mode = false;
							}
							else return Error(token);
							break;
						case 115:
							if (token.Type == ";")
							{
								TokenStack.Push(token);
								StateStack.Push(122);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 116:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 21;
								mode = false;
							}
							else return Error(token);
							break;
						case 117:
							if (token.Type == ")")
							{
								TokenStack.Push(token);
								StateStack.Push(125);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 118:
							if (token.Type == ")")
							{
								values = PopValue(1);

								ValueStack.Push(null);
								symbol = 21;
								mode = false;
							}
							else if (token.Type == ",")
							{
								TokenStack.Push(token);
								StateStack.Push(126);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 119:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 120:
							if (token.Type == ";")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == ".")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "=")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == ":")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "/")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else if (token.Type == "%")
							{
								tokens = PopToken(2);
								values = PopValue(2);
								value = new Command_Call(values[0] as ICommand, (values[1] as List<ICommand>).ToArray());
								ValueStack.Push(value);
								symbol = 34;
								mode = false;
							}
							else return Error(token);
							break;
						case 121:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(62);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "UInt32")
							{
								TokenStack.Push(token);
								StateStack.Push(63);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Int32")
							{
								TokenStack.Push(token);
								StateStack.Push(64);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Double")
							{
								TokenStack.Push(token);
								StateStack.Push(65);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(66);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "String")
							{
								TokenStack.Push(token);
								StateStack.Push(67);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 122:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Symbol")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "}")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "void")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "int")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "(")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "UInt32")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Int32")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Double")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Char")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "String")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 123:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(130);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 124:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ")")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == ",")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 125:
							if (token.Type == " ")
							{
								tokens = PopToken(4);
								values = PopValue(3);
								Method.ReturnTypeFullName = values[1] as string[];
								Method.Name = tokens[1].Value_String;
								Method.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 18;
								mode = false;
							}
							else if (token.Type == ";")
							{
								tokens = PopToken(4);
								values = PopValue(3);
								Method.ReturnTypeFullName = values[1] as string[];
								Method.Name = tokens[1].Value_String;
								Method.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 18;
								mode = false;
							}
							else if (token.Type == "{")
							{
								tokens = PopToken(4);
								values = PopValue(3);
								Method.ReturnTypeFullName = values[1] as string[];
								Method.Name = tokens[1].Value_String;
								Method.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 18;
								mode = false;
							}
							else if (token.Type == "=>")
							{
								tokens = PopToken(4);
								values = PopValue(3);
								Method.ReturnTypeFullName = values[1] as string[];
								Method.Name = tokens[1].Value_String;
								Method.UpdateAttributes(values[0] as List<string>);
								ValueStack.Push(null);
								symbol = 18;
								mode = false;
							}
							else return Error(token);
							break;
						case 126:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Symbol")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "void")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "int")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 127:
							if (token.Type == ";")
							{
								tokens = PopToken(2);
								values = PopValue(3);
								value = new Command_If(values[0] as ICommand, values[1] as ICommand, values[2] as ICommand);
								ValueStack.Push(value);
								symbol = 29;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(2);
								values = PopValue(3);
								value = new Command_If(values[0] as ICommand, values[1] as ICommand, values[2] as ICommand);
								ValueStack.Push(value);
								symbol = 29;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(2);
								values = PopValue(3);
								value = new Command_If(values[0] as ICommand, values[1] as ICommand, values[2] as ICommand);
								ValueStack.Push(value);
								symbol = 29;
								mode = false;
							}
							else if (token.Type == "?")
							{
								tokens = PopToken(2);
								values = PopValue(3);
								value = new Command_If(values[0] as ICommand, values[1] as ICommand, values[2] as ICommand);
								ValueStack.Push(value);
								symbol = 29;
								mode = false;
							}
							else return Error(token);
							break;
						case 128:
							if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[0];
								(value as List<ICommand>).Add(values[1] as ICommand);
								ValueStack.Push(value);
								symbol = 40;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[0];
								(value as List<ICommand>).Add(values[1] as ICommand);
								ValueStack.Push(value);
								symbol = 40;
								mode = false;
							}
							else return Error(token);
							break;
						case 129:
							if (token.Type == "Symbol")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "}")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "void")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "int")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "UInt32")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "Int32")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "Double")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else if (token.Type == "String")
							{
								tokens = PopToken(1);
								values = PopValue(3);
								Method.Commands.Add(values[1] as ICommand);
								ValueStack.Push(null);
								symbol = 24;
								mode = false;
							}
							else return Error(token);
							break;
						case 130:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(133);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 131:
							if (token.Type == ")")
							{
								values = PopValue(3);

								ValueStack.Push(null);
								symbol = 22;
								mode = false;
							}
							else if (token.Type == ",")
							{
								values = PopValue(3);

								ValueStack.Push(null);
								symbol = 22;
								mode = false;
							}
							else return Error(token);
							break;
						case 132:
							if (token.Type == "Symbol")
							{
								TokenStack.Push(token);
								StateStack.Push(45);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "void")
							{
								TokenStack.Push(token);
								StateStack.Push(48);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "int")
							{
								TokenStack.Push(token);
								StateStack.Push(49);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 133:
							if (token.Type == " ")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Parameter(values[0] as string[], tokens[1].Value_String);
								Method.InsertParameter(value as Parameter);
								ValueStack.Push(value);
								symbol = 23;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Parameter(values[0] as string[], tokens[1].Value_String);
								Method.InsertParameter(value as Parameter);
								ValueStack.Push(value);
								symbol = 23;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = new Parameter(values[0] as string[], tokens[1].Value_String);
								Method.InsertParameter(value as Parameter);
								ValueStack.Push(value);
								symbol = 23;
								mode = false;
							}
							else return Error(token);
							break;
						case 134:
							if (token.Type == " ")
							{
								TokenStack.Push(token);
								StateStack.Push(9);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ")")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == ",")
							{

								ValueStack.Push(null);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 135:
							if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(4);

								ValueStack.Push(null);
								symbol = 22;
								mode = false;
							}
							else if (token.Type == ",")
							{
								tokens = PopToken(1);
								values = PopValue(4);

								ValueStack.Push(null);
								symbol = 22;
								mode = false;
							}
							else return Error(token);
							break;
						default:
							return Error(token);
					}
				}
				else
				{
					int vt = VariableTable[StateStack.Peek(), symbol];
					if (vt < 0) return Error(token);
					StateStack.Push(vt);
					mode = true;
				}
			}
		}
		private ParsingFile File;
		private List<string> NamespaceName;
		private void UsingNamespace()
		{
			File.Usings.Add(string.Join(".", NamespaceName));

		}
		private void DefineNamespace()
		{
			string[] names = NamespaceName.ToArray();
			Namespace.Name = string.Join(".", NamespaceName);
			for (int i = NamespaceName.Count - 2; i >= 0; i--)
			{
				UserNamespace @namespace = new();
				@namespace.Namespaces[names[i]] = Namespace;
				@namespace.Name = string.Join(".", names[0..i]);
				Namespace = @namespace;
			}
		}
		private UserNamespace Namespace;
		private UserType Type;
		private UserField Field;
		private UserMethod Method;
		private SearchingResult Top;
		private SearchingNode_Root Root;
	}
}
