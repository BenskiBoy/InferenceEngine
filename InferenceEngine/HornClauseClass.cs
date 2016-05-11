using System;
using System.Collections.Generic;

namespace InferenceEngine
{
    // Abstract class to model the behaviour of a generic Horn Clause, 
    // which makes up a particular KB

    abstract public class HornClauseClass
	{
		public HornClauseClassType Type;

		public HornClauseClass ()
		{
		}

		virtual public List<String> GetSymbols(){
			// Inherited
			List<String> Symbols = new List<string> ();
			Symbols.Add ("This shouldn't be run");
			return Symbols;
		}

		virtual public List<String> GetPremiseSymbols(){
			// Inherited
			List<String> Symbols = new List<string> ();
			Symbols.Add ("This shouldn't be run");
			return Symbols;
		}

		virtual public String GetConclusionSymbol(){
			// Inherited
			return "This shouldn't be run";
		}

        virtual public bool Evaluate(List<SymbolValue> SymbolValues)
        {
            //This should never run
            return false;
        }
	}
}

