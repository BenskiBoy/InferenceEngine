using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Parser
    {
        public Parser()
        {

        }
        public List<HornClauseClass> GetKB(String file)
        {
            List<HornClauseClass> result = new List<HornClauseClass>();
            String[] precidenceArray = { "=>", "|", "&", "!" };
            String[] fileData = System.IO.File.ReadAllLines(file);
            int ClauseLine = 0;
            for(int i = 0; i < fileData.Count(); i++)
            {
                if (fileData[i].ToUpper() == "TELL")
                {
                    ClauseLine = i + 1;
                    break;
                }
            }
            fileData[ClauseLine] = fileData[ClauseLine].Replace(" ", string.Empty);
            String[] line = fileData[ClauseLine].Split(';');
            foreach (String sentence in line.Where(n=>n != ""))
            {

                temp = Sentence2Clause(sentence);
                if (temp != null)
                {

                }
                
            }
            return result;
        }
        public QueryClass GetQuerySymbol(String file)
        {
            String[] fileData = System.IO.File.ReadAllLines(file);
            int QueryLine = 0;
            for (int i = 0; i < fileData.Count(); i++)
            {
                if (fileData[i].ToUpper() == "ASK")
                {
                    QueryLine = 1+i;
                    break;
                }
            }
            QueryClass result = new QueryClass();
            result.PropositionSymbol = fileData[QueryLine];
            return result;
        }
        private HornClauseClass Sentence2Clause(String sentence)
        {
            HornClauseClass result;
            String[] symbols = GetSymbols(sentence);
            List<HornClauseFactClass> facts = new List<HornClauseFactClass>();
            foreach (String str in symbols)
            {
                HornClauseFactClass temp = new HornClauseFactClass();
                temp.Symbol = symbols[0];
                facts.Add(temp);
            }
            String[] operators = GetOperator(sentence);
            if (operators.Count() == 0)
            {
                result =facts[0];
            }
            else
            {
                result = null;
            }
            return result;
        }
        private String[] GetSymbols(String str)
        {
            String[] sym = { "" };
            String[] result;
            sym[0] = Regex.Replace(str, "[^a-zA-Z0-9]", ";");
            sym = sym[0].Split(';');
            result = new String[sym.Where(s => s != "").Count()];
            int count = 0;
            foreach(String st in sym.Where(s => s != ""))
            {
                result[count] = st;
                count++;
            }

            return result;
        }
        private String[] GetOperator(String str)
        {
            String[] sym = { "" };
            String[] result;
            sym[0] = Regex.Replace(str, "[^=>&|]", ";");
            sym = sym[0].Split(';');
            result = new String[sym.Where(s => s != "").Count()];
            int count = 0;
            foreach (String st in sym.Where(s => s != ""))
            {
                result[count] = st;
                count++;
            }

            return result;
        }
    }
}
