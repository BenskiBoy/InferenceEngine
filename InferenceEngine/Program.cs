﻿using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            // args[0] method
            // args[1] filename
            String inference = "BC";
            String fileName = "test1.txt";

            Parser P = new Parser();
            InferenceType iEngine;

            List<HornClauseClass> parsedKB = P.GetKB(fileName);
            QueryClass parsedQuery =  new QueryClass(P.GetQuery(fileName));

            foreach (HornClauseClass h in parsedKB)
            {
                if(h.GetType().Name == "HornClauseImplicationClass")
                {
                    Console.WriteLine("implication");
                }
                else if(h.GetType().Name == "HornClauseFactClass")
                {
                    Console.WriteLine("FACT");
                }
            }

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

            

            int i = 1;

		}
	}
}
