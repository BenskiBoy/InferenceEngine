using System;

namespace InferenceEngine
{	
	// Class to model the behaviour of an AND clause
	// E.g. A & B => C
	// E.g. Premise1 & Premise2 => Conclusion
	public class HornClauseAndClass : HornClauseClass
	{	
		// Atributes
		// In Horn form, the premise is called the body and the conclusion is called the head.
		public HornClauseClass Premise1;	// Phrases can consist of other phrases
		public HornClauseClass Premise2;	
		public HornClauseClass Conclusion;

		public HornClauseAndClass ()
		{
		}
	}
}

