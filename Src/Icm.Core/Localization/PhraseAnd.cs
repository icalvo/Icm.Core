using System.Collections.Generic;

namespace Icm.Localization
{
	public class PhraseAnd : Phrase
	{


		private List<object> _arguments;
		public ICollection<object> Arguments {
			get { return _arguments; }
		}

		public PhraseAnd(params object[] args)
		{
			if (args == null) {
				_arguments = new List<object>();
			} else {
				_arguments = new List<object>(args);
			}
		}

		public PhraseAnd(IEnumerable<object> args)
		{
			if (args == null) {
				_arguments = new List<object>();
			} else {
				_arguments = new List<object>(args);
			}
		}

		public override string Translate(int lcid, ILocalizationRepository locRepo)
		{
			dynamic args = Arguments.Select(trans => TranslateObject(locRepo, trans).ToString);

			if (args.Count == 0) {
				return "";
			}

			dynamic primaryLang = lcid & 0x3ff;

			if (primaryLang == 10) {
				return args.Take(args.Count - 1).JoinStr(", ") + args.Count <= 1 ? "" : " y " + args.Last;
			} else if (primaryLang == 22) {
				return args.Take(args.Count - 1).JoinStr(", ") + args.Count <= 1 ? "" : " y " + args.Last;
			} else {
				return args.Take(args.Count - 1).JoinStr(", ") + args.Count <= 1 ? "" : args.Count == 2 ? " and " : ", and " + args.Last;
			}
			return args.JoinStr(", ");
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
