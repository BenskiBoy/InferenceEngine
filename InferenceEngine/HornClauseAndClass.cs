using System;
using System.Collections.Generic;

namespace InferenceEngine
{	
	// Class to model the behaviour of an AND clause
	// E.g. A & B => C
	// E.g. Premise1 & Premise2 => Conclusion
	public class HornClauseAndClass : HornClauseClass
	{	
		// Atributes
		// In Horn form, the premise is called the body and the conclusion is called the head.
		public HornClauseClass Premise1;	// Phrases can consist of other phrases
		public HornClauseClass Premise2;	
		public HornClauseClass Conclusion;

		public HornClauseAndClass ()
		{
		}

		// Method to return the symbols contained within the "And" Clause
		public override List<String> GetSymbols()
		{
			List<String> AllSymbols = new List<string> ();

			List<String> Premise1Symbols = Premise1.GetSymbols ();
			List<String> Premise2Symbols = Premise2.GetSymbols ();
			List<String> ConclusionSymbols = Conclusion.GetSymbols ();

			foreach (String Symbol in Premise1Symbols) {
				if (!AllSymbols.Contains (Symbol)) {
					// only add symbols that aren't already contained
					AllSymbols.Add (Symbol);
				}
			}

			foreach (String Symbol in Premise2Symbols) {
				if (!AllSymbols.Contains (Symbol)) {
					// only add symbols that aren't already contained
					AllSymbols.Add (Symbol);
				}
			}

			foreach (String Symbol in ConclusionSymbols) {
				if (!AllSymbols.Contains (Symbol)) {
					// only add symbols that aren't already contained
					AllSymbols.Add (Symbol);
				}
			}

			return AllSymbols;
		}
	}
}

