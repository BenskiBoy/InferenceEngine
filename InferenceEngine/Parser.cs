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
        public void testSentence2Clause(String str)
        {
            HornClauseClass H = Sentence2Clause(str);
        }
        private HornClauseClass Sentence2Clause(String sentence)
        {
			/*if (sentence.Contains("("))
			{
				String[] separator = { "(" };
				String temp1 = sentence.Substring (0, sentence.IndexOf ('('));

				String temp2 = sentence.Substring (sentence.IndexOf ('(')+1,sentence.Length-sentence.IndexOf ('(')-1);
				String[] RHS;


				int OpenBracketCount = 0;
				int SplitIndex = 0;

				for (int charNum = 0; charNum < temp2.Length; charNum ++) {
					if (temp2[charNum] == ')') {
						if (OpenBracketCount == 0) {
							// we've reached the corresponding close bracket
							// save this as the SplitIndex
							SplitIndex = charNum;
							break;
						} else {
							// if not the corresponding bracket, then decrement the OpenBracketCount
							OpenBracketCount = OpenBracketCount - 1;
						}
					}
					else if(temp2[charNum] == '('){
						// then increment the OpenBracketCount
						OpenBracketCount = OpenBracketCount + 1;
					}
				}

				// split the left hand side and then add it 
				String temp3 = temp2.Substring (0, SplitIndex);
				String temp4 = temp2.Substring (SplitIndex+1,temp2.Length-SplitIndex-1);

				// append the temp3 to the temp1 string
				temp1 = temp1 + temp3;


				HornClauseClass tempHorn1 = Sentence2Clause(temp1);
				HornClauseClass tempHorn2 = Sentence2Clause(temp4);
				return new HornClauseBidirectionalClass(tempHorn1, tempHorn2);
			}
            else*/ if (sentence.Contains("<=>"))
            {
                String[] separator = { "<=>" };
                String[] temp = sentence.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
                HornClauseClass tempHorn1 = Sentence2Clause(temp[0]);
                HornClauseClass tempHorn2 = Sentence2Clause(temp[1]);
                return new HornClauseBidirectionalClass(tempHorn1, tempHorn2);
            }
            else if (sentence.Contains("=>"))
            {
                String[] separator = { "=>" };
                String[] temp = sentence.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
                HornClauseClass tempHorn1 = Sentence2Clause(temp[0]);
                HornClauseClass tempHorn2 = Sentence2Clause(temp[1]);
                return new HornClauseImplicationClass(tempHorn1, tempHorn2);
            }
			else if (sentence.Contains("\\/")){
				String[] separator = { "\\/" };
				String[] temp = sentence.Split(separator,2, System.StringSplitOptions.RemoveEmptyEntries);
				HornClauseClass tempHorn1 = Sentence2Clause(temp[0]);
				HornClauseClass tempHorn2 = Sentence2Clause(temp[1]);
				return new HornClauseOrClass(tempHorn1, tempHorn2);
			}
			else if (sentence.Contains("&")){
                String[] temp = sentence.Split(new char[] { '&' }, 2);
                HornClauseClass tempHorn1 = Sentence2Clause(temp[0]);
                HornClauseClass tempHorn2 = Sentence2Clause(temp[1]);
                return new HornClauseAndClass(tempHorn1, tempHorn2);
            }
			else if (sentence.Contains("~")){
				String[] separator = { "~" };
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
