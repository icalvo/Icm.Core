using System.Collections.Generic;
using System.Linq;
using Icm.Collections;

namespace Icm.Localization
{
	public class PhraseAnd : Phrase
	{


		private List<object> _arguments;
		public ICollection<object> Arguments => _arguments;

	    public PhraseAnd(params object[] args)
	    {
	        _arguments = args == null ? new List<object>() : new List<object>(args);
	    }

	    public PhraseAnd(IEnumerable<object> args)
		{
		    _arguments = args == null ? new List<object>() : new List<object>(args);
		}

	    public override string Translate(int lcid, ILocalizationRepository locRepo)
		{
			var args = Arguments.Select(trans => TranslateObject(locRepo, trans).ToString()).ToArray();

			if (!args.Any()) {
				return "";
			}

			var primaryLang = lcid & 0x3ff;

			switch (primaryLang)
			{
			    case 10:
			        return args.Take(args.Count() - 1).JoinStr(", ") + (args.Count() <= 1 ? "" : " y " + args.Last());
			    case 22:
			        return args.Take(args.Count() - 1).JoinStr(", ") + (args.Count() <= 1 ? "" : " y " + args.Last());
			    default:
			        return args.Take(args.Count() - 1).JoinStr(", ") + (args.Count() <= 1 ? "" : args.Count() == 2 ? " and " : ", and " + args.Last());
			}
		}

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
