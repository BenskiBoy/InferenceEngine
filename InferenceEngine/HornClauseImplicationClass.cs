﻿using System;
using System.Collections.Generic;


namespace InferenceEngine
{	
	// Class to model the behaviour of an implication clause
	// E.g. A => B
	// E.g. Premise => Conclusion
	public class HornClauseImplicationClass : HornClauseClass
	{	
		// Atributes
		// In Horn form, the premise is called the body and the conclusion is called the head.
		public HornClauseClass Premise;	// Phrases can consist of other phrases
		public HornClauseClass Conclusion;

		public HornClauseImplicationClass ()
		{
		}

		// Method to return the symbols contained within the Implication Clause
		public override List<String> GetSymbols()
		{
			List<String> AllSymbols = new List<string> ();

			List<String> PremiseSymbols = Premise.GetSymbols ();
			List<String> ConclusionSymbols = Conclusion.GetSymbols ();

			foreach (String Symbol in PremiseSymbols) {
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
