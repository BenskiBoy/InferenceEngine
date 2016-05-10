﻿using System;

namespace InferenceEngine
{
	// Class to model the behaviour of a KB Query
	public class QueryClass
	{
		// Attributes
		public InferenceType InferenceType; // The Type of Inference to be used by the KB in response to the query 
		public HornClauseClass QueryClause;	// Clause to represent the query being asked of the KB

		// Methods
		public QueryClass (HornClauseClass Clause, InferenceType InferenceType)
		{
			this.QueryClause = Clause;
			this.InferenceType = InferenceType;
		}
	}
}

