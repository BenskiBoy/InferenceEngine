using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	// Abstract class to model the behaviour of a Not Horn Clause
	// E.g. "Not P1", which means symbol P1 is false.
	public class HornClauseNotClass : HornClauseClass
	{	
		// Atributes
		// In Horn form, the premise is called the body and the conclusion is called the head.
		public HornClauseClass Premise1;    // Phrases can consist of other phrases


		public HornClauseNotClass (HornClauseClass Premise1)
		{
			this.Premise1 = Premise1;
			this.Type = HornClauseClassType.Not;
		}

		// Method to return the symbol contained within the Fact Clause
		public override List<String> GetSymbols()
		{
			List<String> AllSymbols = new List<string>();
			List<String> Premise1Symbols = Premise1.GetSymbols();

			foreach (String Symbol in Premise1Symbols)
			{
				if (!AllSymbols.Contains(Symbol))
				{
					// only add symbols that aren't already contained
					AllSymbols.Add(Symbol);
				}
			}

			return AllSymbols;
		}


		// Method to return the premise symbols contained within the Not Clause
		public override List<String> GetPremiseSymbols()
		{
			List<String> AllSymbols = new List<string>();
			List<String> Premise1Symbols = Premise1.GetSymbols();

			foreach (String Symbol in Premise1Symbols)
			{
				if (!AllSymbols.Contains(Symbol))
				{
					// only add symbols that aren't already contained
					AllSymbols.Add(Symbol);
				}
			}

			return AllSymbols;
		}

		// method that takes a list of symbols and their values,
		// and returns true if the symbol contained within it
		// is in the list and is true. Else returns false.
        public override bool Evaluate(List<SymbolValue> SymbolValues)
        {	
			return(!Premise1.Evaluate (SymbolValues));

        }
    }
}

