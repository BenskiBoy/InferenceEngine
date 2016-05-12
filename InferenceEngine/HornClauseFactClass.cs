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

		public HornClauseFactClass (String Symbol)
		{
			this.Symbol = Symbol;
			this.Type = HornClauseClassType.Fact;
		}

		// Method to return the symbol contained within the Fact Clause
		public override List<String> GetSymbols()
		{
			List<String> Symbols = new List<string> ();
			Symbols.Add (Symbol);
			return Symbols;
		}


		// Method to return the premise symbols contained within the Fact Clause
		public override List<String> GetPremiseSymbols()
		{
			List<String> Symbols = new List<string> ();
			Symbols.Add (Symbol);
			return Symbols;
		}

		// method that takes a list of symbols and their values,
		// and returns true if the symbol contained within it
		// is in the list and is true. Else returns false.
        public override bool Evaluate(List<SymbolValue> SymbolValues)
        {	
            foreach(SymbolValue Symbol in SymbolValues)
            {
                if(Symbol.SymbolName == this.Symbol)
                {
					if (Symbol.Value == true) {
						return true;
					} else {
						//If symbol but value is false
						return false;
					}
                }
            }
            //If symbol not found in the list
            return false;
        }
    }
}

