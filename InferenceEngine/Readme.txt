Readme.txt

You must include a single readme.txt file with your work with the following details:
  // Student Details: Your full student names, ids, and your group number (as allocated by ESP).
  Team Name : COS30019_A02_T018
  Student #1: Ricardo Cannizzaro, 	4916514
  Student #2: Thomas Cheah, 		9737405
  Student #3: Benjamin Thwaites, 	9739157

Features/Bugs/Missing: 
 // Include a list of the features you have implemented. Clearly state if a
 // required feature has not been implemented. Failure to do this will result in penalties. 
 // Include a list of any known bugs.
	 Features working 100%
	 + Parser Functionality
	 + TT Methods
	 + FC Methods
	 + BC Methods
	 + Research Component: General Inference Engine for TT Method. Added operators: OR (\/), Not (~) and Bidirectional (<=>).
	 + 3 Input operators work (i.e. A & B & C)
	 + The user can run the program with an optional 3rd argument. When this 3rd argument is DEBUG, and inferent method is TT, the truth table will be printed to console.

	 Known Bugs
	 - FC and BC methods don't work with extended operators (OR, NOT, Bidirectional Implication)
	 - Parser method fails when parenthesis (optional research component) are present in TELL clauses. Program throws an exception.
	 - Parser method fails when text file name is invalid.
	 - Query method fails when inference method is invalid.

	 Missing
	 - None 


Test cases: 
 // The test cases you have developed ton test your program. What bugs have you found?

	TELL
	a&b=>c; a;
	ASK
	c

	Result: works for TT, FC, BC

	TELL
	p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2;
	ASK
	d

	Result: works for TT, FC, BC

	TELL
	a\/b\/c=>d; a; b; c;
	ASK
	d

	Result: works for TT only

	TELL
	a&b&c=>d; a; b; c;
	ASK
	d

	Result: works for TT only

	TELL
	~a=>b; ~a;
	ASK
	b

	Result: works for TT only

	TELL
	a<=>b; b;
	ASK
	a

	Result: works for TT only

	TELL
	a&(b\/c); a; b;
	ASK
	a

	Result: Parser method fails due to presence of parenthesis. Program throws an exception.

Acknowledgements/Resources: 
 // Include in your readme.txt file a list of the resources you have used 
 // to create your work. A simple list of URL's is not enough. Include with each entry a basic 
 // description of how the person or website assisted you in your work.
 [1] STUART RUSSELL, P. N. 2010. Artificial Intelligence A Modern Approach - Third Edition.

Notes: 
 // Anything else you want to tell the marker, such as how to use the GUI version of your
 // program, and something particular about your implementation.
 Nothing.

Summary report: 
 // Summary of the teamwork in this assignment. You need to clearly indicate who
 // did what and how each team member gave feedback to other members. In this report, the overall 
 // percentage of contribution by each student to the project has to be clearly specified and summed to // 100%.
 All students contributed 1/3 = 33.333333...%
