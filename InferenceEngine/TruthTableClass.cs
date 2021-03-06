﻿using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	// Class to model the behaviour of a Truth Table, used in the TT Query Method
	class TruthTableClass : InferenceType
	{
		// Attributes
		public List <List <Boolean>> Table = new List<List<Boolean>>(); // The actual TT

		// Methods
		public TruthTableClass (List<HornClauseClass> KB, QueryClass Query)
		{
			KnowledgeBase = KB;   // Assign the KB to the object being constructed
            LoadSymbolsList(Query);
            // create 2^n Rows, with n + 1 collumns (+1 column for the query result)
            int NumSymbols = Symbols.Count;  // The order of this list is the order of columns in the table
            int NumClauses = KnowledgeBase.Count;

            List<Boolean> TempRow = new List<bool>();   // used to build rows before they are put into the table

            for (int RowNum = 0; RowNum < Math.Pow(2, NumSymbols); RowNum++)
            {
                TempRow = new List<bool>(); // Reset TempRow at the start of every row
                for (int ColNum = 0; ColNum < NumSymbols + NumClauses + 2; ColNum++)
                {
                    // set alternating boolean value for first NumSymbols" rows
                    if (ColNum < NumSymbols)
                    {
                        if ((RowNum % Math.Pow(2, NumSymbols - ColNum)) < Math.Pow(2, NumSymbols - ColNum - 1))
                        {
                            TempRow.Add(false);
                        }
                        else {
                            TempRow.Add(true);
                        }
                    }
                    else if (ColNum >= NumSymbols && ColNum < (NumSymbols + NumClauses + 1))
                    {
                        // These are the Clause evaluation cells
                        // Set to false by default
                        TempRow.Add(false);
                    }
                    else {
                        // These are the Query evaluation cells
                        // Set to false by default
                        TempRow.Add(false);
                    }
                }
                Table.Add(TempRow); // Add row to the table
            }
            EvaluateClauses();
            //Console.WriteLine("TT World Computation Done");
        }

        // Method to evaluate the clause collumns of the TT
        
        // Is this even used? 
        private void EvaluateClauses()
        {
            List<String> ClauseSymbols = new List<String>();
            List<SymbolValue> SymbolValues = new List<SymbolValue>();
            SymbolValue TempSymbolValue = new SymbolValue();

            for (int ClauseNum = 0; ClauseNum < KnowledgeBase.Count; ClauseNum++)
            {
                ClauseSymbols = KnowledgeBase[ClauseNum].GetSymbols();
                foreach (String clausey in ClauseSymbols)
                {
                    TempSymbolValue.SymbolName = clausey;
                    TempSymbolValue.Value = false; //Set to false by default

                    SymbolValues.Add(TempSymbolValue);
                }

                //SymbolValues now contains all symbols but still need to set values
                foreach (List<Boolean> Row in Table)
                {
                    for (int ColNum = 0; ColNum < Symbols.Count; ColNum++)
                    {
                        for (int ClauseSymbolNum = 0; ClauseSymbolNum < SymbolValues.Count; ClauseSymbolNum++)
                        {
                            if (Symbols[ColNum] == SymbolValues[ClauseSymbolNum].SymbolName)
                            {
                                TempSymbolValue = SymbolValues[ClauseSymbolNum];
                                TempSymbolValue.Value = Row[ColNum];
                                SymbolValues[ClauseSymbolNum] = TempSymbolValue;
                            }
                        }
                    }
                    Row[ClauseNum + Symbols.Count] = KnowledgeBase[ClauseNum].Evaluate(SymbolValues);
                }
            }

            bool tempBool = true; 

            //Now evaluate sum of KB
            foreach (List<Boolean> Row in Table)
            {
                tempBool = true; // re-init to true
				for (int ClauseNum = 0; ClauseNum < KnowledgeBase.Count; ClauseNum++)
                {
					if(!Row[Symbols.Count + ClauseNum])
                    {
                        tempBool = false;
                    }
                }
                Row[Symbols.Count + KnowledgeBase.Count] = tempBool;
            }
        }

        // Method to evaluate the querey collumn of the TT 
        // return value shows number of times query enatils KB...
		override public string EvaluateQuery(QueryClass Query, Boolean Debug)
        {
            // first fill in the collumn, then check if query is a subset of
            // the valid worlds of the TT (the rows where SUM clauses = true)
            LoadSymbolsList(Query);
            List<String> ClauseSymbols = new List<String>();
            List<SymbolValue> SymbolValues = new List<SymbolValue>();
            SymbolValue TempSymbolValue = new SymbolValue();


            ClauseSymbols = Query.QueryClause.GetSymbols();
            foreach (String clausey in ClauseSymbols)
            {
                TempSymbolValue.SymbolName = clausey;
                TempSymbolValue.Value = false; //Set to false by default

                SymbolValues.Add(TempSymbolValue);
            }
            //SymbolValues now contains all symbols but still need to set values
            foreach (List<Boolean> Row in Table)
            {
                for (int ColNum = 0; ColNum < Symbols.Count; ColNum++)
                {
                    for (int ClauseSymbolNum = 0; ClauseSymbolNum < SymbolValues.Count; ClauseSymbolNum++)
                    {
                        if (Symbols[ColNum] == SymbolValues[ClauseSymbolNum].SymbolName)
                        {
                            TempSymbolValue = SymbolValues[ClauseSymbolNum];
                            TempSymbolValue.Value = Row[ColNum];
                            SymbolValues[ClauseSymbolNum] = TempSymbolValue;
                        }
                    }
                }

                Row[Symbols.Count + KnowledgeBase.Count + 1] = Query.QueryClause.Evaluate(SymbolValues);
            }

            // Check if query is a subset of the valid worlds of the TT 
            // (the rows where SUM clauses = true)

            Boolean QueryResult = true;
			int NumValidWorlds = 0;
            foreach (List<Boolean> Row in Table)
            {
                if (Row[Symbols.Count + KnowledgeBase.Count] == true)
                {
                    // if the SUM clauses entry is true, check if the Query entry is true
                    if (Row[Symbols.Count + KnowledgeBase.Count + 1] == true)
                    {
                        // if it's untrue, then change the QueryResult to false.
                        // as the valid worlds / KB is NOT a subset of the query
						NumValidWorlds++;
                    }
                    else
                    {
                        // if it's false, then don't change the temp bool
						QueryResult = false;
                    }
                }
            }

            // Format the Result string
			if(Debug) {this.PrintTT ();} // for debugging
			if (QueryResult)
            {
                // "When the method is TT and the answer is YES, 
                // it should be followed by a colon (:) and 
                // the number of models of KB"
				return "YES: " + NumValidWorlds;
            }
            else
            {
                return "NO";
            }

        }
        
        // Method to print the TT 
        public void PrintTT ()
		{
			String RowString = "| ";

			// first print collumn titles
			// put in the symbol headers
			foreach (String Symbol in Symbols) {
				RowString = RowString + Symbol + "    " + " | ";
			}

			// put in the clause headers
			for (int ClauseNum = 0; ClauseNum < KnowledgeBase.Count; ClauseNum ++) {
				RowString = RowString + "Cl" + ClauseNum.ToString() + "  " + " | ";
			}

			// put in the sum of clause header
			RowString = RowString + "SumCl" + " | ";

			// put in the query header
			RowString = RowString + "Query" + " | ";

			int RowCharCount = RowString.Length;
			Console.WriteLine(RowString);

			// then print a horizontal line
			RowString = "";

			for (int CharNum = 0; CharNum < RowCharCount; CharNum++) {
				RowString = RowString + "-";
			}
			Console.WriteLine(RowString);

			// then print the table cells
			foreach (List<Boolean> Row in Table) {
				RowString = "| ";	// reset at beginning of every row
				foreach (Boolean Cell in Row) {
					if(Cell){
						RowString = RowString + "true ";
					}
					else{
						RowString = RowString + "false";
					}
					RowString = RowString + " | ";
				}
				Console.WriteLine(RowString);
			}
		}
	}
}



