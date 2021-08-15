using System.Collections.Generic;
namespace File.SV
{
	public class SV_Translator
	{
		public SV_Translate Translate;
        public SV_Translator(SV_Translate translate) => Translate = translate;
        public IEnumerable<SVLine> Do(IEnumerable<SVLine> lines)
		{
			foreach (SVLine line in lines)
				yield return Translate(line);
		}
		public SVFile Select(SVFile source)=>new(source.Title, Do(source.Values));
	}
}
