using System;
using System.Collections.Generic;

namespace InferenceEngine
{
    // Abstract class to model the behaviour of a generic Horn Clause, 
    // which makes up a particular KB

    abstract public class HornClauseClass
	{
		public HornClauseClass ()
		{
		}

		virtual public List<String> GetSymbols(){
			// Inherited
			List<String> Symbols = new List<string> ();
			Symbols.Add ("This shouldn't be run");
			return Symbols;
		}
	}
}

