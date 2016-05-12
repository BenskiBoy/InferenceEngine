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

			HornClauseFactClass Fact1 = new HornClauseFactClass("P1");
            HornClauseFactClass Fact2 = new HornClauseFactClass("P2");
            HornClauseFactClass Fact3 = new HornClauseFactClass("P3");
            HornClauseFactClass Fact4 = new HornClauseFactClass("a");
            HornClauseFactClass Fact5 = new HornClauseFactClass("b");
            HornClauseFactClass Fact6 = new HornClauseFactClass("c");
            HornClauseFactClass Fact7 = new HornClauseFactClass("d");
            HornClauseFactClass Fact8 = new HornClauseFactClass("e");
            HornClauseFactClass Fact9 = new HornClauseFactClass("f");
            HornClauseFactClass Fact10 = new HornClauseFactClass("g");
            HornClauseFactClass Fact11 = new HornClauseFactClass("h");

            HornClauseAndClass AndClause1 = new HornClauseAndClass(Fact5, Fact8); //b and e
            HornClauseAndClass AndClause2 = new HornClauseAndClass(Fact9, Fact10); //f and g
            HornClauseAndClass AndClause3 = new HornClauseAndClass(Fact1, Fact3); //P1 and P3

            HornClauseImplicationClass ImpClause1 = new HornClauseImplicationClass(Fact2, Fact3); //P2 => P3
            HornClauseImplicationClass ImpClause2 = new HornClauseImplicationClass(Fact3, Fact1); //P3 => P1
            HornClauseImplicationClass ImpClause3 = new HornClauseImplicationClass(Fact6, Fact8); //c=> e
            HornClauseImplicationClass ImpClause4 = new HornClauseImplicationClass(AndClause1, Fact9); //b and e =>f
            HornClauseImplicationClass ImpClause5 = new HornClauseImplicationClass(AndClause2, Fact11); //f and g => h
            HornClauseImplicationClass ImpClause6 = new HornClauseImplicationClass(AndClause3, Fact6); //P1 and P3 => c
            HornClauseImplicationClass ImpClause7 = new HornClauseImplicationClass(Fact1, Fact7); //P1 => d

            MyKB.Clauses.Add(ImpClause1);//P2 => P3
            MyKB.Clauses.Add(ImpClause2);//P3 => P1
            MyKB.Clauses.Add(ImpClause3);//c=> e
            MyKB.Clauses.Add(ImpClause4);//b and e =>f
            MyKB.Clauses.Add(ImpClause5);//f and g => h
            MyKB.Clauses.Add(ImpClause7);//P1 => d
            MyKB.Clauses.Add(ImpClause6);//P1 and P3 => c

            MyKB.Clauses.Add(Fact4); //a
            MyKB.Clauses.Add(Fact5); //b
            MyKB.Clauses.Add(Fact2); //P2

            QueryClass MyQuery = new QueryClass (Fact7, InferenceType.FC);
			//QueryClass MyQuery = new QueryClass (Fact7,InferenceType.TT);

			string Result = MyKB.MakeQuery (MyQuery);

			Console.WriteLine (Result);

            

            int i = 1;

		}
	}
}
