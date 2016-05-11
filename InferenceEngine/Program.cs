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

			HornClauseFactClass Fact1 = new HornClauseFactClass ("A");
			//MyKB.Clauses.Add (Fact1);

			HornClauseFactClass Fact2 = new HornClauseFactClass ("B");
			//MyKB.Clauses.Add (Fact2);

			HornClauseFactClass Fact3 = new HornClauseFactClass ("C");
			MyKB.Clauses.Add (Fact3);

			HornClauseAndClass AndClause1 = new HornClauseAndClass (Fact1,Fact2);
			//MyKB.Clauses.Add (AndClause1);

			HornClauseImplicationClass ImplicationClause1 = new HornClauseImplicationClass (Fact1,Fact2);
			MyKB.Clauses.Add (ImplicationClause1);

			QueryClass MyQuery = new QueryClass (Fact1,InferenceType.FC);
			//QueryClass MyQuery = new QueryClass (Fact2,InferenceType.TT);

			string Result = MyKB.MakeQuery (MyQuery);

			Console.WriteLine (Result);

            

            int i = 1;

		}
	}
}
