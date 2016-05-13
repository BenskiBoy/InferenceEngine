using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    abstract class InferenceType
    {
        public List<HornClauseClass> KnowledgeBase;
        public List<String> Symbols; // List of symbols contained within the KB
        abstract public String EvaluateQuery(QueryClass Query);//?????????
        // Function to check whether the KB entails the Query
        // Returns a string response: YES or NO.
        // If the answer is YES, it is follwed by a colon (:) 
        // and the number of models of KB
        protected void LoadSymbolsList(QueryClass Query)
        {

            // First build a list of symbols in the KB and query
            List<String> AllSymbols = new List<string>();   // List of symbols contained within the KB and Query
            List<String> ClauseSymbols = new List<string>();
            List<String> QuerySymbols = new List<string>();

            // Check the KB clauses for symbols
            foreach (HornClauseClass Clause in this.KnowledgeBase)
            {
                ClauseSymbols = Clause.GetSymbols();

                foreach (String Symbol in ClauseSymbols)
                {
                    if (!AllSymbols.Contains(Symbol))
                    {
                        // only add symbols that aren't already contained
                        AllSymbols.Add(Symbol);
                        //Console.WriteLine("New Symbol found in KB: " + Symbol);
                    }
                    else
                    {
                        //Console.WriteLine("Repeated Symbol found in KB: " + Symbol);
                    }
                }
            }

            // Add the query symbols

            QuerySymbols = Query.QueryClause.GetSymbols();

            foreach (String Symbol in QuerySymbols)
            {
                if (!AllSymbols.Contains(Symbol))
                {
                    // only add symbols that aren't already contained
                    AllSymbols.Add(Symbol);
                    //Console.WriteLine("New Symbol found in Query: " + Symbol);
                }
                else
                {
                    //Console.WriteLine("Repeated Symbol found in Query: " + Symbol);
                }
            }
            // Save symbols to KB
            this.Symbols = AllSymbols;
        }
    }
}
