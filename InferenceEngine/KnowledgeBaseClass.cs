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
			int QueryResult;
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
				if (QueryResult > 0) {
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

				// first load in all of the symbols (clauses and query)
				LoadSymbolsList(Query);

				// create a list to keep track of the number of unknown premises
				// remaining for each clause
				// Indexed by clause number
				// Initialised to the number of premises in the clause
				List <int> NumPremisesRemaining = new List<int> ();
				int TempInt = 0;
				for (int ClauseNum = 0; ClauseNum < this.Clauses.Count; ClauseNum++) {
					// TODO make the symbols hornclause classes so we can 
					// see how many premise symbols they have
					TempInt = this.Clauses[ClauseNum].GetPremiseSymbols().Count;
					NumPremisesRemaining.Add (TempInt);
				}

				// create a list to keep track of which symbols have been inferred
				List <Boolean> SymbolIsInferred = new List<Boolean> ();
				// initialise this list
				for (int SymbolNum = 0; SymbolNum < this.Symbols.Count; SymbolNum++) {
					SymbolIsInferred.Add (false);
				}

				// create the agenda of symbols which are to be tested 
				List <String> Agenda = new List<String>();
				String TempString;
				// initially, we populate the agenda with any known symbols

				foreach (HornClauseClass Clause in this.Clauses){
					if (Clause.Type == HornClauseClassType.Fact) {
						TempString = Clause.GetSymbols () [0];
						Agenda.Add (TempString);
					}
				}

				String P;
				List<String> CPremiseSymbols = new List <String> ();
				Boolean TempBool = false;

                String ResultSymbols = "";
                             
				while (Agenda.Count > 0) {
					// while agenda is not empty
					// assign the first item of the agenda to P
					P = Agenda[0];
					// then remove it from the Agenda
					Agenda.RemoveAt (0);

					// return true if P = Q 
					if (P == Query.QueryClause.GetPremiseSymbols()[0]) {
						Result = "YES: " + ResultSymbols + P;
                        return Result;
					}

					int SymbolIndex = 0;
					// find the corresponding index for the symbol P
					for (int SymbolNum = 0; SymbolNum < this.Symbols.Count; SymbolNum ++){
						if (this.Symbols[SymbolNum] == P) {
							SymbolIndex = SymbolNum;
						}
					}

					// if inferred[p] = false
					// ie. if the symbol P is known to be inferred
					if( SymbolIsInferred [SymbolIndex] == false) {
						// set the inferred[p] to true;
						SymbolIsInferred [SymbolIndex] = true;
                        ResultSymbols += Symbols[SymbolIndex] + ", ";

                        for (int ClauseNum = 0; ClauseNum < this.Clauses.Count; ClauseNum++){
							// check if P is in C.premise
							CPremiseSymbols = new List <String> ();
							CPremiseSymbols = this.Clauses[ClauseNum].GetPremiseSymbols ();
							TempBool = false;
							foreach (String Symbol in CPremiseSymbols) {
								if (Symbol == P) {
									TempBool = true;
								}
							}
							if (TempBool == true) {
								// we've found P in this clause
								// decrement count[ClauseNum]
								NumPremisesRemaining[ClauseNum] = NumPremisesRemaining[ClauseNum] - 1;
								// if count[c] = 0 then add c.CONCLUSION to agenda
								if(NumPremisesRemaining[ClauseNum] == 0){
									Agenda.Add(this.Clauses [ClauseNum].GetConclusionSymbol());
								}
							}

						}
					}
				}
				Result = "NO";
				// end of FC procedure

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

