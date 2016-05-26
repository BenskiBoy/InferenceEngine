using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Main Input Arguments 	 //
            // args[0] inference method	 //
            // args[1] filename			 //

			String fileName = "default";
			String inference = "default";
			Boolean Debug = false;

			if (args.Length < 2) {
				Console.WriteLine ("ERROR: NOT ENOUGH INPUT PARAMETERS");
				return;
			} else if (args.Length == 2) {
				inference = args [0];
				fileName = args [1];
			} else if (args.Length == 3) {
				inference = args [0];
				fileName = args [1];
				if (args[2] == "DEBUG") {
					Debug = true;
				}
			} else {
				Console.WriteLine ("ERROR: TOO MANY INPUT PARAMETERS");
				return;
			}

			if(Debug) Console.WriteLine ("Program begin");



			//String inference = args[0];
			//String fileName = args [1];

			// debugging stuff for 3rd argument, error checking in input?

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
			String Result = iEngine.EvaluateQuery(parsedQuery, Debug);
            Console.WriteLine (Result);
		}
	}
}
