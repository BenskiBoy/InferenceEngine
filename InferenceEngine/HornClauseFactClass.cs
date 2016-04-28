using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	// Abstract class to model the behaviour of a Fact Horn Clause
	// E.g. "P1", which means symbol P1 is true.
	public class HornClauseFactClass : HornClauseClass
	{	
		// Attributes
		public String Symbol;	// The symbol that is indicated as true by the Fact Horn Clause

		public HornClauseFactClass ()
		{
		}

		// Method to return the symbol contained within the Fact Clause
		public override List<String> GetSymbols()
		{
			List<String> Symbols = new List<string> ();
			Symbols.Add (Symbol);
			return Symbols;
		}
	}
}

