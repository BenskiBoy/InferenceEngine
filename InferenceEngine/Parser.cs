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

                HornClauseClass temp = Sentence2Clause(sentence);
                if (temp != null)
                {
                    result.Add(temp);
                }
                
            }
            return result;
        }
        public HornClauseClass GetQuery(String file)
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
            return Sentence2Clause(fileData[QueryLine]);
        }
        private HornClauseClass Sentence2Clause(String sentence)
        {
			if (Regex.IsMatch(sentence, "<=>"))
			{
				String[] separator = { "<=>" };
				String[] temp = sentence.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
				HornClauseClass tempHorn1 = Sentence2Clause(temp[0]);
				HornClauseClass tempHorn2 = Sentence2Clause(temp[1]);
				return new HornClauseBidirectionalClass(tempHorn1, tempHorn2);
			}
			if (Regex.IsMatch(sentence, "=>"))
            {
                String[] separator = { "=>" };
                String[] temp = sentence.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
                HornClauseClass tempHorn1 = Sentence2Clause(temp[0]);
                HornClauseClass tempHorn2 = Sentence2Clause(temp[1]);
                return new HornClauseImplicationClass(tempHorn1, tempHorn2);
            }
			else if (Regex.IsMatch(sentence, "OR")){
				String[] separator = { "OR" };
				String[] temp = sentence.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
				HornClauseClass tempHorn1 = Sentence2Clause(temp[0]);
				HornClauseClass tempHorn2 = Sentence2Clause(temp[1]);
				return new HornClauseOrClass(tempHorn1, tempHorn2);
			}
			else if (Regex.IsMatch(sentence, "&")){
                String[] temp = sentence.Split('&');
                HornClauseClass tempHorn1 = Sentence2Clause(temp[0]);
                HornClauseClass tempHorn2 = Sentence2Clause(temp[1]);
                return new HornClauseAndClass(tempHorn1, tempHorn2);
            }
			else if (Regex.IsMatch(sentence, "NOT")){
				String[] separator = { "NOT" };
				String[] temp = sentence.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
				HornClauseClass tempHorn1 = Sentence2Clause (temp [0]);
				return new HornClauseNotClass(tempHorn1);
			}
            else if(Regex.IsMatch(sentence, "^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                return new HornClauseFactClass(sentence);
            }
            else
            {
                return null;
            }
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
