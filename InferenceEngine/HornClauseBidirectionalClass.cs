﻿using System;
using System.Collections.Generic;


namespace InferenceEngine
{	
	// Class to model the behaviour of an implication clause
	// E.g. A <=> B
	// E.g. Premise1 <=> Premise2
	public class HornClauseBidirectionalClass : HornClauseClass
	{	
		// Atributes
		// In Horn form, the premise is called the body and the conclusion is called the head.
		public HornClauseClass Premise1;	// Phrases can consist of other phrases
		public HornClauseClass Premise2;

		public HornClauseBidirectionalClass (HornClauseClass Premise1, HornClauseClass Premise2)
		{
			this.Premise1 = Premise1;
			this.Premise2 = Premise2;
			this.Type = HornClauseClassType.Biconditional;
		}

		// Method to return the symbols contained within the Biconditional Clause
		public override List<String> GetSymbols()
		{
			List<String> AllSymbols = new List<string> ();
			List<String> Premise1Symbols = new List<string> ();
			List<String> Premise2Symbols = new List<string> ();

			Premise1Symbols = Premise1.GetSymbols ();
			Premise2Symbols = Premise2.GetSymbols ();

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

			return AllSymbols;
		}



		// Method to return the symbols contained within the Implication Clause premise
		public override List<String> GetPremiseSymbols()
		{
			List<String> AllSymbols = new List<string> ();
			List<String> Premise1Symbols = new List<string> ();
			List<String> Premise2Symbols = new List<string> ();

			Premise1Symbols = Premise1.GetSymbols ();
			Premise2Symbols = Premise2.GetSymbols ();


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

			return AllSymbols;
		}



		// Method to evaluate the clause for the truth table row represented by the
		// SymbolValues list
		public override bool Evaluate(List<SymbolValue> SymbolValues)
		{
			// evaluate using A<=>B  : (A&B) OR (-A & -B)
			return ( (!this.Premise1.Evaluate (SymbolValues) && !this.Premise2.Evaluate (SymbolValues)) 
				  || ( this.Premise1.Evaluate (SymbolValues) &&  this.Premise2.Evaluate (SymbolValues)) );
		}
	}
}

