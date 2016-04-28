using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	// Class to model behaviour of a Knowledge Base (KB)
	public class KnowledgeBaseClass
	{	

		// Attributes
		public List <HornClauseClass> Clauses = new List<HornClauseClass>();	// list of clauses contained within the KB
		public List <String> Symbols = new List<String>(); // List of symbols contained within the KB

		// Methods
		public KnowledgeBaseClass ()
		{
		}

		// Function to allow for Querying of the KB
		public String MakeQuery (QueryClass Query)
		{
			String Result = "Initialised";
			if (Query.InferenceType == InferenceType.TT) {
				// Implement Truth Table Query Here
				Result = TT_Entails(Query);
			} else if (Query.InferenceType == InferenceType.FC) {
				// TODO Implement Forward Chaining Query Here
			} else if (Query.InferenceType == InferenceType.BC) {
				// TODO Implement Backward Chaining Query Here
			} else {
				// Error
				Result = "ERROR - INVALID INFERENCE TYPE!";
			}

			return Result;
		}


		// Function to check whether the KB entails the Query
		// Returns a string response: YES or NO.
		// If the answer is YES, it is follwed by a colon (:) 
		// and the number of models of KB
		public String TT_Entails (QueryClass Query)
		{
			String Result = "Initialised";

			// First build a list of symbols in the KB and query
			List <String> AllSymbols = new List<string>();	// List of symbols contained within the KB and Query
			List <String> ClauseSymbols = new List<string>();

			// Check the KB clauses for symbols
			foreach (HornClauseClass Clause in this.Clauses){
				ClauseSymbols = Clause.GetSymbols ();

				foreach (String Symbol in ClauseSymbols){
					if (!AllSymbols.Contains (Symbol)) {
						// only add symbols that aren't already contained
						AllSymbols.Add (Symbol);
						Console.WriteLine ("New Symbol found in KB: " + Symbol);
					} else {
						Console.WriteLine ("Repeated Symbol found in KB: " + Symbol);
					}
				}
			}

			// Add the query symbol
			if (!AllSymbols.Contains (Query.PropositionSymbol)) {
				// only add symbols that aren't already contained
				AllSymbols.Add (Query.PropositionSymbol);
				Console.WriteLine ("New Symbol found in query: " + Query.PropositionSymbol);
			} else {
				Console.WriteLine ("Repeated Symbol found in query: " + Query.PropositionSymbol);
			}


			// Second: from notes: return TT_CheckAll(KB,α,symbols,{})
			// Result = TT_CheckAll(Query, AllSymbols, EmptyModel)

			return Result;
		}

		// Function to decide propositional entailment.
		// Returns true if a sentence holds within a model.
		// See pg 248 of AI Textbook
		public String TT_CheckAll (QueryClass Query, List<String> Symbols, KnowledgeBaseClass Model)
		{
			String Result = "Initialised";

			// TODO Implement Logic Here //

			return Result;
		}
	}
}

