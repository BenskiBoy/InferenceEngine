using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class ForwardChaining : InferenceType
    {

        public ForwardChaining(List<HornClauseClass> KB)
        {
            KnowledgeBase = KB;
        }
        override public string EvaluateQuery(QueryClass Query)
        {
            // TODO Implement Forward Chaining Query Here

            // first load in all of the symbols (clauses and query)
            LoadSymbolsList(Query);
            string Result;
            // create a list to keep track of the number of unknown premises
            // remaining for each clause
            // Indexed by clause number
            // Initialised to the number of premises in the clause
            List<int> NumPremisesRemaining = new List<int>();
            int TempInt = 0;
            for (int ClauseNum = 0; ClauseNum < this.KnowledgeBase.Count; ClauseNum++)
            {
                // TODO make the symbols hornclause classes so we can 
                // see how many premise symbols they have
                TempInt = this.KnowledgeBase[ClauseNum].GetPremiseSymbols().Count;
                NumPremisesRemaining.Add(TempInt);
            }

            // create a list to keep track of which symbols have been inferred
            List<Boolean> SymbolIsInferred = new List<Boolean>();
            // initialise this list
            for (int SymbolNum = 0; SymbolNum < this.Symbols.Count; SymbolNum++)
            {
                SymbolIsInferred.Add(false);
            }

            // create the agenda of symbols which are to be tested 
            List<String> Agenda = new List<String>();
            String TempString;
            // initially, we populate the agenda with any known symbols

            foreach (HornClauseClass Clause in this.KnowledgeBase)
            {
                if (Clause.Type == HornClauseClassType.Fact)
                {
                    TempString = Clause.GetSymbols()[0];
                    Agenda.Add(TempString);
                }
            }

            String P;
            List<String> CPremiseSymbols = new List<String>();
            Boolean TempBool = false;

            String ResultSymbols = "";

            while (Agenda.Count > 0)
            {
                // while agenda is not empty
                // assign the first item of the agenda to P
                P = Agenda[0];
                // then remove it from the Agenda
                Agenda.RemoveAt(0);

                // return true if P = Q 
                if (P == Query.QueryClause.GetPremiseSymbols()[0])
                {
                    Result = "YES: " + ResultSymbols + P;
                    return Result;
                }

                int SymbolIndex = 0;
                // find the corresponding index for the symbol P
                for (int SymbolNum = 0; SymbolNum < this.Symbols.Count; SymbolNum++)
                {
                    if (this.Symbols[SymbolNum] == P)
                    {
                        SymbolIndex = SymbolNum;
                    }
                }

                // if inferred[p] = false
                // ie. if the symbol P is known to be inferred
                if (SymbolIsInferred[SymbolIndex] == false)
                {
                    // set the inferred[p] to true;
                    SymbolIsInferred[SymbolIndex] = true;
                    ResultSymbols += Symbols[SymbolIndex] + ", ";

                    for (int ClauseNum = 0; ClauseNum < this.KnowledgeBase.Count; ClauseNum++)
                    {
                        // check if P is in C.premise
                        CPremiseSymbols = new List<String>();
                        CPremiseSymbols = this.KnowledgeBase[ClauseNum].GetPremiseSymbols();
                        TempBool = false;
                        foreach (String Symbol in CPremiseSymbols)
                        {
                            if (Symbol == P)
                            {
                                TempBool = true;
                            }
                        }
                        if (TempBool == true)
                        {
                            // we've found P in this clause
                            // decrement count[ClauseNum]
                            NumPremisesRemaining[ClauseNum] = NumPremisesRemaining[ClauseNum] - 1;
                            // if count[c] = 0 then add c.CONCLUSION to agenda
                            if (NumPremisesRemaining[ClauseNum] == 0)
                            {
                                Agenda.Add(this.KnowledgeBase[ClauseNum].GetConclusionSymbol());
                            }
                        }

                    }
                }
            }
            Result = "NO";
            return Result;
            // end of FC procedure
        }
    }
}
