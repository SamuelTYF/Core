using System.Collections.Generic;
using Collection;
namespace Parser
{
	public class JoinParser : IOptionalParser
	{
		public IParser Task;
		public IParser Sperater;
		public bool IsOptional;
		public int Min;
		public int Max;
		public JoinParser(int min, int max)
		{
			Min = min;
			Max = max;
		}
		public JoinParser(IParser task, IParser sperater, int min, int max)
		{
			Task = task;
			Sperater = sperater;
			Min = min;
			Max = max;
			IsOptional = Task is IOptionalParser || sperater is IOptionalParser;
		}
		public override IParseResult Parse(IStringArg s)
		{
			JoinParseResult joinParseResult = new(this,s);
			if (s.NotOver)
			{
				IParseResult parseResult;
				joinParseResult.TaskResults.Add(parseResult = Task.Parse(s));
				if (parseResult.Success)
					joinParseResult.Count = 1;
			}
			while (s.NotOver)
			{
				IParseResult parseResult2;
				joinParseResult.SperaterResults.Add(parseResult2 = Sperater.Parse(s));
				bool flag = false;
				if (parseResult2.Success)
				{
					IParseResult parseResult;
					joinParseResult.TaskResults.Add(parseResult = Task.Parse(s));
					if (parseResult.Success)
						joinParseResult.Count++;
					else
						flag = true;
				}
				else
					flag = true;
				if (flag)
				{
					s.Restore(parseResult2.State);
					break;
				}
			}
			joinParseResult.Success = joinParseResult.Count >= Min && joinParseResult.Count <= Max;
			return joinParseResult;
		}
		public override IEnumerable<IParseResult> Parses(IStringArg s)
		{
			JoinParseResult r = new(this,s);
			if (IsOptional)
			{
				foreach (IParseResult tr2 in Task.GetEnumerator(s))
				{
					r.TaskResults.Add(tr2);
					if (tr2.Success)
					{
						r.Count = 1;
						foreach (IParseResult item in Parses(s, r))
						{
							_ = item;
							if (r.Success)
								yield return r;
						}
					}
					r.TaskResults.Pop();
				}
				yield break;
			}
			if (s.NotOver)
			{
				Collection.List<IParseResult> taskResults = r.TaskResults;
				IParseResult value;
				IParseResult tr3 = (value = Task.Parse(s));
				taskResults.Add(value);
				if (tr3.Success)
					r.Count = 1;
			}
			while (s.NotOver)
			{
				Collection.List<IParseResult> speraterResults = r.SperaterResults;
				IParseResult value;
				IParseResult sr = (value = Sperater.Parse(s));
				speraterResults.Add(value);
				bool fail = false;
				if (sr.Success)
				{
					Collection.List<IParseResult> taskResults2 = r.TaskResults;
					IParseResult tr3 = (value = Task.Parse(s));
					taskResults2.Add(value);
					if (tr3.Success)
						r.Count++;
					else
						fail = true;
				}
				else
					fail = true;
				if (!fail)
					continue;
				s.Restore(sr.State);
				while (r.Count >= 0)
				{
					if (r.Count >= Min && r.Count <= Max)
					{
						r.Success = true;
						yield return r;
						r.Count--;
						continue;
					}
					r.Success = false;
					yield return r;
					break;
				}
				break;
			}
			r.Success = r.Count >= Min && r.Count <= Max;
			yield return r;
		}
		public IEnumerable<IParseResult> Parses(IStringArg s, JoinParseResult r)
		{
			r.Count++;
			foreach (IParseResult sr in Sperater.GetEnumerator(s))
			{
				r.SperaterResults.Add(sr);
				if (sr.Success)
				{
					foreach (IParseResult tr in Task.GetEnumerator(s))
					{
						r.TaskResults.Add(tr);
						if (tr.Success)
						{
							foreach (IParseResult item in Parses(s, r))
							{
								_ = item;
								if (r.Success)
									yield return r;
							}
							r.Success = r.Count >= Min && r.Count <= Max;
							r.EndIndex = s.Index;
							yield return r;
						}
						s.Restore(tr.State);
						r.TaskResults.Pop();
					}
				}
				s.Restore(sr.State);
				r.SperaterResults.Pop();
			}
			r.Count--;
			r.Success = true;
			yield return r;
		}
	}
}
