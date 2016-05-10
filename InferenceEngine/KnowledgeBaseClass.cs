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
		// public ? TruthTable = new TruthTable();	// TruthTable 

		// Methods
		public KnowledgeBaseClass ()
		{
		}

		// Function to allow for Querying of the KB
		public String MakeQuery (QueryClass Query)
		{
			String Result = "Initialised";
			Boolean QueryResult;
			int NumValidWorlds = 0;
			if (Query.InferenceType == InferenceType.TT) {
				// Implement Truth Table Query Here
				LoadSymbolsList(Query);
                TruthTableClass MyTT = new TruthTableClass(this);

				// Evaluate the clause columns in the TT
                MyTT.EvaluateClauses();
                
				// Evaluating the query column in the TT
				// and get the query result
				QueryResult = MyTT.EvaluateQuery (Query);

				// Format the Result string
				if (QueryResult == true) {
					// "When the method is TT and the answer is YES, 
					// it should be followed by a colon (:) and 
					// the number of models of KB"
					// TODO Work out what the hell that means
					// Ric: maybe this is the number of valid worlds?

					// count the number of valid worlds in the TT
					foreach (List<Boolean> Row in MyTT.Table) {
						if (Row [this.Symbols.Count + this.Clauses.Count] == true) {
							// if the world is valid, increment the counted
							NumValidWorlds = NumValidWorlds + 1;
						}
					}

					Result = "YES: " + NumValidWorlds;
				} else {
					Result = "NO";
				}

 				MyTT.PrintTT();	// print TT

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
		public void LoadSymbolsList (QueryClass Query)
		{

			// First build a list of symbols in the KB and query
			List <String> AllSymbols = new List<string>();	// List of symbols contained within the KB and Query
			List <String> ClauseSymbols = new List<string>();
			List <String> QuerySymbols = new List<string>();

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

			// Add the query symbols

			QuerySymbols = Query.QueryClause.GetSymbols();

			foreach (String Symbol in QuerySymbols){
				if (!AllSymbols.Contains (Symbol)) {
					// only add symbols that aren't already contained
					AllSymbols.Add (Symbol);
					Console.WriteLine ("New Symbol found in Query: " + Symbol);
				} else {
					Console.WriteLine ("Repeated Symbol found in Query: " + Symbol);
				}
			}

			// Save symbols to KB
			this.Symbols = AllSymbols;

		}




	}
}

