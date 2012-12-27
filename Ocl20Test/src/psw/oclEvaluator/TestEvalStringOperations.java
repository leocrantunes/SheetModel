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
public class TestEvalStringOperations extends TestEval {
	public void	testSize() {
		String	name = "testSize";
		
		evaluateIntegerResult(name, "\"\".size()", 0);
		evaluateIntegerResult(name, "\"a\".size()", 1);
		evaluateIntegerResult(name, "\"abcdefghij\".size()", 10);
	}

	public void	testConcat() {
		String	name = "testConcat";
		
		evaluateStringResult(name, "\"\".concat(\"\")", "");
		evaluateStringResult(name, "\"\".concat(\"a\")", "a");
		evaluateStringResult(name, "\"a\".concat(\"\")", "a");
		evaluateStringResult(name, "\"a\".concat(\"b\")", "ab");
		evaluateStringResult(name, "\"abc\".concat(\"defg\")", "abcdefg");
	}

	public void testSubstring() {
		String	name = "testSubstring";
		
		evaluateStringResult(name, "\"a\".substring(1, 1)", "a");
		evaluateStringResult(name, "\"alex\".substring(1, 1)", "a");
		evaluateStringResult(name, "\"alex\".substring(4, 4)", "x");
		evaluateStringResult(name, "\"alex\".substring(1, 4)", "alex");
		evaluateStringResult(name, "\"alex\".substring(2, 3)", "le");
		evaluateInvalidResult(name, "\"a\".substring(1, 2)");
		evaluateInvalidResult(name, "\"alex\".substring(5, 6)");
	}

	public void	testToInteger() {
		String	name = "testToInteger";
		
		evaluateIntegerResult(name, "\"0\".toInteger()", 0);
		evaluateIntegerResult(name, "\"1\".toInteger()", 1);
		evaluateIntegerResult(name, "\"100\".toInteger()", 100);
		evaluateIntegerResult(name, "\"-50\".toInteger()", -50);
		evaluateInvalidResult(name, "\"john\".toInteger()");
	}

	public void	testToReal() {
		String	name = "testToReal";
		
		evaluateRealResult(name, "\"0.0\".toReal()", 0.0);
		evaluateRealResult(name, "\"1.0\".toReal()", 1.0);
		evaluateRealResult(name, "\"100.2345\".toReal()", 100.2345);
		evaluateRealResult(name, "\"-50.9876\".toReal()", -50.9876);
		evaluateInvalidResult(name, "\"john\".toReal()");
	}
	
	public void	testEquality() {
		String	name = "testEquality";
		
		evaluateBooleanResult(name, " \"\" = \"\" ", true);
		evaluateBooleanResult(name, " \"a\" = \"a\" ", true);
		evaluateBooleanResult(name, " \"a\" = \"A\" ", false);
		evaluateBooleanResult(name, " \"alex\" = \"alex\" ", true);
		evaluateBooleanResult(name, " \"alex\" = \"john\" ", false);
		evaluateBooleanResult(name, " \"alex\" = 10", false);
	}

	public void	testDifferent() {
		String	name = "testDifferent";
		
		evaluateBooleanResult(name, " \"\" <> \"\" ", false);
		evaluateBooleanResult(name, " \"a\" <> \"a\" ", false);
		evaluateBooleanResult(name, " \"a\" <> \"A\" ", true);
		evaluateBooleanResult(name, " \"alex\" <> \"alex\" ", false);
		evaluateBooleanResult(name, " \"alex\" <> \"john\" ", true);
		evaluateBooleanResult(name, " \"alex\" <> 10", true);
	}
	
	public void	testSum() {
		String	name = "testSum";
		
		evaluateStringResult(name, "\"\" + \"\"", "");
		evaluateStringResult(name, "\"\" + \"a\"", "a");
		evaluateStringResult(name, "\"a\" + \"\"", "a");
		evaluateStringResult(name, "\"a\" + \"b\"", "ab");
		evaluateStringResult(name, "\"abc\" + \"defg\"", "abcdefg");
	}


	public void	testStartsWith() {
		String	name = "testStartsWith";
		
		evaluateBooleanResult(name, " \"\".startsWith(\"\")", false);
		evaluateBooleanResult(name, " \"\".startsWith(\"a\")", false);
		
		evaluateBooleanResult(name, " \"a\".startsWith(\"a\")", true);
		evaluateBooleanResult(name, " \"a\".startsWith(\"b\")", false);

		evaluateBooleanResult(name, " \"alex\".startsWith(\"a\")", true);
		evaluateBooleanResult(name, " \"alex\".startsWith(\"al\")", true);
		evaluateBooleanResult(name, " \"alex\".startsWith(\"alex\")", true);
		evaluateBooleanResult(name, " \"alex\".startsWith(\"alexa\")", false);
		evaluateBooleanResult(name, " \"alex\".startsWith(\"b\")", false);
	}

	public void	testEndsWith() {
		String	name = "testEndsWith";
		
		evaluateBooleanResult(name, " \"\".endsWith(\"\")", false);
		evaluateBooleanResult(name, " \"\".endsWith(\"a\")", false);
		
		evaluateBooleanResult(name, " \"a\".endsWith(\"a\")", true);
		evaluateBooleanResult(name, " \"a\".endsWith(\"b\")", false);

		evaluateBooleanResult(name, " \"alex\".endsWith(\"x\")", true);
		evaluateBooleanResult(name, " \"alex\".endsWith(\"ex\")", true);
		evaluateBooleanResult(name, " \"alex\".endsWith(\"alex\")", true);
		evaluateBooleanResult(name, " \"alex\".endsWith(\"alexa\")", false);
		evaluateBooleanResult(name, " \"alex\".endsWith(\"b\")", false);
	}


	public void	testIndexOf() {
		String	name = "testIndexOf";
		
		evaluateIntegerResult(name, " \"\".indexOf(\"\")", 0);
		evaluateIntegerResult(name, " \"\".indexOf(\"a\")", 0);
		evaluateIntegerResult(name, " \"alexandre\".indexOf(\"a\")", 1);
		evaluateIntegerResult(name, " \"alexandre\".indexOf(\"a\", 2)", 5);
		evaluateIntegerResult(name, " \"alexandre\".indexOf(\"a\", 4)", 5);
		evaluateIntegerResult(name, " \"alexandre\".indexOf(\"a\", 5)", 5);
		evaluateIntegerResult(name, " \"alexandre\".indexOf(\"b\", 2)", 0);
	}

	public void	testLastIndexOf() {
		String	name = "testLastIndexOf";
		
		evaluateIntegerResult(name, " \"\".lastIndexOf(\"\")", 0);
		evaluateIntegerResult(name, " \"\".lastIndexOf(\"a\")", 0);
		evaluateIntegerResult(name, " \"alexandre\".lastIndexOf(\"a\")", 5);
		evaluateIntegerResult(name, " \"alexandre\".lastIndexOf(\"e\", 6)", 3);
		evaluateIntegerResult(name, " \"alexandre\".lastIndexOf(\"b\", 2)", 0);
	}


	public void	testTrim() {
		String	name = "testTrim";
		
		evaluateStringResult(name, " \"\".trim()", "");
		
		evaluateStringResult(name, " \"a\".trim()", "a");
		evaluateStringResult(name, " \"    a\".trim()", "a");
		evaluateStringResult(name, " \"a     \".trim()", "a");
		evaluateStringResult(name, " \"      a     \".trim()", "a");
		evaluateStringResult(name, " \"      a l e x     \".trim()", "a l e x");
		evaluateStringResult(name, " \"           \".trim()", "");
		evaluateStringResult(name, " \"      a l e x     \".ltrim()", "a l e x     ");
		evaluateStringResult(name, " \"      a l e x     \".rtrim()", "      a l e x");
		evaluateStringResult(name, " \"          \".ltrim()", "");
		evaluateStringResult(name, " \"          \".rtrim()", "");
		evaluateStringResult(name, " \" \".ltrim()", "");
		evaluateStringResult(name, " \"\".ltrim()", "");
		evaluateStringResult(name, " \" \".rtrim()", "");
		evaluateStringResult(name, " \"\".rtrim()", "");
	}

	public	void	testLike() {
		String	name = "testLike";
		
		evaluateBooleanResult(name, "\"Rio de Janeiro\".like(\"R.*o\") ", true);
		evaluateBooleanResult(name, "\"Rio de Janeiro\".like(\"R.o.*o\") ", true);
		evaluateBooleanResult(name, "\"Rio de Janeiro\".like(\"R.*r\") ", false);
	}
}
