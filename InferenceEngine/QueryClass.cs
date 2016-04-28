using System;

namespace InferenceEngine
{
	// Class to model the behaviour of a KB Query
	public class QueryClass
	{
		// Attributes
		public String PropositionSymbol;	// String to represent the propositional symbol being asked of the KB
		public InferenceType InferenceType; // The Type of Inference to be used by the KB in response to the query 

		// Methods
		public QueryClass ()
		{
		}
	}
}

