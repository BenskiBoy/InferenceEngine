using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class BackwardChaining :InferenceType
    {
        public BackwardChaining(List<HornClauseClass> KB)
        {
            KnowledgeBase = KB;
        }
        public override string EvaluateQuery(QueryClass Query)
        {
            string Result = "YES:";
			// initialise our goals list to the symbol(s) contained within the query
            List<string> goals = Query.QueryClause.GetSymbols();
            List<string> searched = new List<string>();

			// starting from the symbol query, iteratively check if each symbol in the goals list
			// is a conclusion of another KB clause, or is a fact.
			// if the symbol is a conclusion of another KB clause, then we add the symbols of 
			// the premise of that clause to the goals symbols list
            while (goals.Count > 0)
            {
                String goal = goals[0];
                goals.RemoveAt(0);
                searched.Add(goal);
                HornClauseClass ex = isConclusionOrFact(goal);//Not sure why I called it ex. name can be changed - Thomas
                if(ex == null)
                {
                    //null should say that it wasn't a conclusion or a fact in the knowledge base
                    //and therefore should return null
                    return "NO";
                    
                }
                else if(ex.GetType().Name == "HornClauseImplicationClass")
                {
                    //probably should keep the clauses
                    List<String> temp = ex.GetPremiseSymbols();
                    foreach(String str in temp)
                    {
                        if (!goals.Contains(str)&&!searched.Contains(str))
                        {
                            goals.Add(str);
                        }
                    }

                }
            }
            searched.Reverse();
            for (int i = 0; i < searched.Count; i++)
            {
                Result += " " + searched[i];
                if (i < searched.Count - 1)
                {
                    Result += ",";
                }
            }
            return Result;
        }
        /// <summary>
        /// Checks if the symboo is the conclusion of an implication or fact
        /// </summary>
        /// <param name="symbol">The string you wish to look for in the KB</param>
        /// <returns>if symbol is found in as a conclusion or fact it will return the clause. 
        /// If it isn't found it returns null (error code)</returns>
        public HornClauseClass isConclusionOrFact(String symbol)
        {
            foreach(HornClauseClass clause in KnowledgeBase.Where(s=>s.GetType().Name == "HornClauseImplicationClass"|| s.GetType().Name == "HornClauseFactClass"))
            {
                if(clause.GetType().Name == "HornClauseImplicationClass")
                {
                    if(clause.GetConclusionSymbol() == symbol)
                    {
                        return clause;
                    }
                }
                else if(clause.GetType().Name == "HornClauseFactClass")
                {
                    if(clause.GetSymbols()[0] == symbol)
                    {
                        return clause;
                    }
                }
            }
            return null;
        }
        public bool isPremiseInKB(String symbol)
        {
            foreach(HornClauseClass clause in KnowledgeBase.Where(s=>s.GetType().Name != "HornClauseFactClass"))
            {
                List<String> temp = clause.GetPremiseSymbols();
                foreach(String str in temp)
                {
                    if(str == symbol)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
