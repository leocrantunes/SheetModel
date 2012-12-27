/*
 * Created on May 15, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalBooleanOperations extends TestEval {

	public	void	testOr() {
		String	name = "testOr";
		
		evaluateBooleanResult(name, "false or false", false);
		evaluateBooleanResult(name, "false or true", true);
		evaluateInvalidResult(name, "false or invalid");
		evaluateNullResult(name, "false or null");
		
		evaluateBooleanResult(name, "true or false", true);
		evaluateBooleanResult(name, "true or true", true);
		evaluateBooleanResult(name, "true or null", true);
		evaluateBooleanResult(name, "true or invalid", true);
	}

		public	void	testAnd() {
			String	name = "testAnd";
		
			evaluateBooleanResult(name, "false and false", false);
			evaluateBooleanResult(name, "false and true", false);
			evaluateBooleanResult(name, "false and null", false);
			evaluateBooleanResult(name, "false and invalid", false);
			
			evaluateBooleanResult(name, "true and false", false);
			evaluateBooleanResult(name, "true and true", true);
			evaluateNullResult(name, "true and null");
			evaluateInvalidResult(name, "true and invalid");
		}

	public	void	testXor() {
		String	name = "testXor";
		
		evaluateBooleanResult(name, "false xor false", false);
		evaluateBooleanResult(name, "false xor true", true);
		evaluateNullResult(name, "false xor null");
		evaluateInvalidResult(name, "false xor invalid");
		
		evaluateBooleanResult(name, "true xor false", true);
		evaluateBooleanResult(name, "true xor true", false);
		evaluateNullResult(name, "true xor null");
		evaluateInvalidResult(name, "true xor invalid");
	}

	public	void	testImplies() {
		String	name = "testImplies";
		
		evaluateBooleanResult(name, "false implies false", true);
		evaluateBooleanResult(name, "false implies true", true);
		evaluateBooleanResult(name, "false implies null", true);
		evaluateBooleanResult(name, "false implies invalid", true);
		
		evaluateBooleanResult(name, "true implies false", false);
		evaluateBooleanResult(name, "true implies true", true);
		evaluateNullResult(name, "true implies null");
		evaluateInvalidResult(name, "true implies invalid ");
	}

	public	void	testNot() {
		String	name = "testNot";
		
		evaluateBooleanResult(name, "not false", true);
		evaluateBooleanResult(name, "not true", false);
	}

	public	void	testEquality() {
		String	name = "testEquality";
		
		evaluateBooleanResult(name, "false = false", true);
		evaluateBooleanResult(name, "false = true", false);
		evaluateNullResult(name, "false = null");
		evaluateInvalidResult(name, "false = invalid ");
		
		evaluateBooleanResult(name, "true = true", true);
		evaluateBooleanResult(name, "true = false", false);
		evaluateNullResult(name, "true = null");
		evaluateInvalidResult(name, "true = invalid ");
	}

	public	void	testDifference() {
		String	name = "testDifference";
		
		evaluateBooleanResult(name, "false <> false", false);
		evaluateBooleanResult(name, "false <> true", true);
		evaluateNullResult(name, "false <> null");
		evaluateInvalidResult(name, "false <> invalid ");
		
		evaluateBooleanResult(name, "true <> true", false);
		evaluateBooleanResult(name, "true <> false", true);
		evaluateNullResult(name, "true <> null");
		evaluateInvalidResult(name, "true <> invalid ");
	}

	public void	testToString() {
		String name = "testToString";
		
		evaluateStringResult(name, "true.toString()", "true");
		evaluateStringResult(name, "false.toString()", "false");
	}

}
