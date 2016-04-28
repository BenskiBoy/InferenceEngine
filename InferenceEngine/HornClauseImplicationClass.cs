using System;

namespace InferenceEngine
{	
	// Class to model the behaviour of an implication clause
	// E.g. A => B
	// E.g. Premise => Conclusion
	public class HornClauseImplicationClass : HornClauseClass
	{	
		// Atributes
		// In Horn form, the premise is called the body and the conclusion is called the head.
		public HornClauseClass Premise;	// Phrases can consist of other phrases
		public HornClauseClass Conclusion;

		public HornClauseImplicationClass ()
		{
		}
	}
}

