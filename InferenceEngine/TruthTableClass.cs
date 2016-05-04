using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	// Class to model the behaviour of a Truth Table, used in the TT Query Method
	public class TruthTableClass
	{
		// Attributes
		public KnowledgeBaseClass KB = new KnowledgeBaseClass(); // The associated KB (linked to the actual KB due to C#)
		public List <List <Boolean>> Table = new List<List<Boolean>>(); // The actual TT

		// Methods
		public TruthTableClass (KnowledgeBaseClass KB)
		{
			this.KB = KB;	// Assign the KB to the object being constructed
		}

		// Method to compute the world collumns of the TT 
		public void ComputeTruthTableWorlds ()
		{
			//TODO - create 2^n Rows, with n + 1 collumns (+1 column for the query result)
			int NumSymbols = KB.Symbols.Count;	// The order of this list is the order of columns in the table
			int NumClauses = KB.Clauses.Count;

			List <Boolean> TempRow = new List<bool> ();	// used to build rows before they are put into the table

			for (int RowNum = 0; RowNum < Math.Pow (2, NumSymbols); RowNum++) {
				TempRow = new List<bool> ();	// Reset TempRow at the start of every row
				for (int ColNum = 0; ColNum < NumSymbols + NumClauses + 1; ColNum++) {
					// TODO - set alternating boolean value for first NumSymbols" rows
					if (ColNum < NumSymbols) {
						if ((RowNum % Math.Pow(2,NumSymbols-ColNum)) < Math.Pow (2, NumSymbols-ColNum-1)) {
							TempRow.Add (false);
						} else {
							TempRow.Add (true);
						}
					} else if (ColNum >= NumSymbols && ColNum < (NumSymbols + NumClauses)) {
						// These are the Clause evaluation cells
						// Set to false by default
						TempRow.Add (false);
					} else {
						// These are the Query evaluation cells
						// Set to false by default
						TempRow.Add (false);
					}
				}
				Table.Add (TempRow);	// Add row to the table
			}
			Console.WriteLine ("TT World Computation Done");
		}

		// Method to print the TT 
		public void PrintTT ()
		{
			String RowString = "";

			foreach (List<Boolean> Row in Table) {
				RowString = "";	// reset at beginning of every row
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



