using System;
using System.Collections.Generic;

namespace InferenceEngine
{
	// Class to model behaviour of a Knowledge Base (KB)
	public class KnowledgeBaseClass
	{	

		// Attributes
		public List <HornClauseClass> Clauses = new List<HornClauseClass>();	// list of clauses contained within the KB
		public List <String> Symbols = new List<String>(); // List of symbols contained within the KB

		// Methods
		public KnowledgeBaseClass ()
		{
		}

		// Function to allow for Querying of the KB
		public String MakeQuery (QueryClass Query)
		{
		}
	}
}

