using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			KnowledgeBaseClass MyKB = new KnowledgeBaseClass ();

			HornClauseFactClass Fact1 = new HornClauseFactClass ();
			Fact1.Symbol = "A";
			MyKB.Clauses.Add (Fact1);

			HornClauseFactClass Fact2 = new HornClauseFactClass ();
			Fact2.Symbol = "B";
			MyKB.Clauses.Add (Fact2);

			HornClauseFactClass Fact3 = new HornClauseFactClass ();
			Fact3.Symbol = "A";
			MyKB.Clauses.Add (Fact3);

			QueryClass MyQuery = new QueryClass ();
			MyQuery.InferenceType = InferenceType.TT;
			MyQuery.PropositionSymbol = "A";


			string Result = MyKB.MakeQuery (MyQuery);

			

            

            int i = 1;

		}
	}
}
