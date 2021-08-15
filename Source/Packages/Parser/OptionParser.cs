using System.Collections.Generic;
namespace Parser
{
	public class OptionParser : IOptionalParser
	{
		public IParser[] Options;
		public IParser Task;
		public int Count;
		public bool CanEmpty;
		public OptionParser(bool empty, int count)
		{
			Count = count;
			Options = new IParser[Count];
			CanEmpty = empty;
		}
		public OptionParser(IParser task, bool empty, params IParser[] options)
		{
			Task = task;
			Options = options;
			Count = options.Length;
			CanEmpty = empty;
		}
		public override IParseResult Parse(IStringArg s)
		{
			OptionParseResult optionParseResult = new(this,s, Count, CanEmpty);
			for (int i = 0; i < Count; i++)
			{
				if ((optionParseResult.OptionResults[i] = Options[i].Parse(s)).Success && (optionParseResult.TaskResults[i] = Task.Parse(s)).Success)
				{
					optionParseResult.Success = true;
					optionParseResult.Index = i;
					optionParseResult.EndIndex = s.Index;
					return optionParseResult;
				}
				s.Restore(optionParseResult.State);
			}
			if (CanEmpty && (optionParseResult.TaskResults[Count] = Task.Parse(s)).Success)
			{
				optionParseResult.Success = true;
				optionParseResult.Index = Count;
			}
			return optionParseResult;
		}
		public override IEnumerable<IParseResult> Parses(IStringArg s)
		{
			OptionParseResult r = new(this,s, Count, CanEmpty);
			for (int i = 0; i < Count; i++)
			{
				foreach (IParseResult or in Options[i].GetEnumerator(s))
				{
					r.OptionResults[i] = or;
					if (or.Success)
					{
						foreach (IParseResult tr in Task.GetEnumerator(s))
						{
							r.TaskResults[i] = tr;
							if (tr.Success)
							{
								r.Success = true;
								r.Index = i;
								r.EndIndex = s.Index;
								yield return r;
							}
							s.Restore(tr.State);
						}
					}
					s.Restore(or.State);
				}
				s.Restore(r.State);
			}
			if (CanEmpty)
				foreach (IParseResult tr2 in Task.GetEnumerator(s))
				{
					r.TaskResults[Count] = tr2;
					if (tr2.Success)
					{
						r.Success = true;
						r.Index = Count;
						r.EndIndex = s.Index;
						yield return r;
					}
					s.Restore(tr2.State);
				}
			r.Success = false;
			yield return r;
		}
		public override string ToString()
		{
			string s = TreeNode.Main ? $"{TreeNode.Name}:" : "";
			List<string> t = new();
			foreach (IParser task in Options)
				t.Add(task.Print());
			return $"{s}({string.Join('|', t)})";
		}
	}
}
