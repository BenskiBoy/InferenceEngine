using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            // args[0] method
            // args[1] filename
            String inference = "FC";
            String fileName = "test1.txt";

            Parser P = new Parser();
            InferenceType iEngine;

            List<HornClauseClass> parsedKB = P.GetKB(fileName);
            QueryClass parsedQuery =  new QueryClass(P.GetQuery(fileName));

            switch (inference)
            {
                case "TT":
                    iEngine = new TruthTableClass(parsedKB, parsedQuery);
                    break;
                case "FC":
                    iEngine = new ForwardChaining(parsedKB);
                    break;
                case "BC":
                    iEngine = new BackwardChaining(parsedKB);
                    break;
                default:
                    Console.WriteLine("Inference Type code invalid");
                    return;
            }
            String Result = iEngine.EvaluateQuery(parsedQuery);
            Console.WriteLine (Result);
		}
	}
}
