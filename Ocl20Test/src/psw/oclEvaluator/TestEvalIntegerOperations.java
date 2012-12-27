/*
 * Created on May 14, 2004
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
public class TestEvalIntegerOperations extends TestEval {

	public void	testSum() {
		String name = "testSum";
		evaluateIntegerResult(name, "5 + 10", 15);
		evaluateIntegerResult(name, "1000000000 + 2000000000", 3000000000L);
		evaluateIntegerResult(name, "5 + 10 + 20 + 30", 65);
		evaluateIntegerResult(name, "0 + 0", 0);
		evaluateIntegerResult(name, "-5 + -10", -15);
		evaluateNullResult(name, "5 + null");
		evaluateInvalidResult(name, "5 + invalid");
	}

	public void	testSubtraction() {
		String name = "testSubtraction";
		evaluateIntegerResult(name, "10 - 4", 6);
		evaluateIntegerResult(name, "4000000000 - 1500000000", 2500000000L);
		evaluateIntegerResult(name, "0 - 0", 0);
		evaluateIntegerResult(name, "100 - 10 - 20 - 15", 55);
		evaluateIntegerResult(name, "-5 - -10", 5);
		evaluateIntegerResult(name, "-10 - 10", -20);
	}
	
	public void	testPlus() {
		String name = "testPlus";
		
		evaluateIntegerResult(name, "10 * 4", 40);
		evaluateIntegerResult(name, "200000 * 50000", 10000000000L);
		evaluateIntegerResult(name, "0 * 0", 0);
		evaluateIntegerResult(name, "10 * 2 * 3 * 4", 240);
		evaluateIntegerResult(name, "-5 * -10", 50);
		evaluateIntegerResult(name, "-10 * 10", -100);
	}

	public void	testDivision() {
		String name = "testDivision";
		
		evaluateRealResult(name, "10 / 5", 2);
		evaluateRealResult(name, "10 / 4", 2.5);
		evaluateRealResult(name, "100 / 5 / 4", 5);
		evaluateInvalidResult(name, "100 / 0");
	}

	public void	testDiv() {
		String name = "testDiv";
		
		evaluateIntegerResult(name, "10.div(5) ", 2);
		evaluateIntegerResult(name, "10.div(4)", 2);
		evaluateIntegerResult(name, "100.div(5).div(4)", 5);
	}

	public void	testMinus() {
		String name = "testMinus";
		
		evaluateIntegerResult(name, "-10", -10);
		evaluateIntegerResult(name, "(-10)", -10);
		evaluateIntegerResult(name, "- (-10)", 10);
		evaluateIntegerResult(name, "(10 * 2) + 4", 24);
	}

	public void	testMod() {
		String name = "testMod";
		
		evaluateIntegerResult(name, "10.mod(3)", 1);
		evaluateIntegerResult(name, "20.mod(7)", 6);
		evaluateIntegerResult(name, "-20.mod(7)", -6);
	}

	public void	testMax() {
		String name = "testMax";
		
		evaluateIntegerResult(name, "(5).max(100)", 100);
		evaluateIntegerResult(name, "(-8).max(5)", 5);
		evaluateIntegerResult(name, "100.max(5)", 100);
		evaluateIntegerResult(name, "5.max(-8)", 5);
		evaluateIntegerResult(name, "5.max(100).max(200)", 200);
		evaluateIntegerResult(name, "5.max(100).max(200).max(30)", 200);
	}

	public void	testMin() {
		String name = "testMin";
		
		evaluateIntegerResult(name, "5.min(100)", 5);
		evaluateIntegerResult(name, "(-8).min(5)", -8);
		evaluateIntegerResult(name, "-8.min(3.max(5))", -5);
		evaluateIntegerResult(name, "100.min(5)", 5);
		evaluateIntegerResult(name, "5.min(-8)", -8);
	}


	public void	testAbs() {
		String name = "testAbs";
		
		evaluateIntegerResult(name, "(-10).abs()", 10);
		evaluateIntegerResult(name, "((-10).abs()).max(20)", 20);
		evaluateIntegerResult(name, "10.abs()", 10);
		evaluateIntegerResult(name, "((((-((10))))).abs())", 10);
	}

	public void	testComparisons() {
		String name = "testComparisons";
		
		evaluateBooleanResult(name, "549 < 548", false);
		evaluateBooleanResult(name, "549 < 549", false);
		evaluateBooleanResult(name, "549 < 550", true);

		evaluateBooleanResult(name, "549 <= 550", true);
		evaluateBooleanResult(name, "549 <= 549", true);
		evaluateBooleanResult(name, "549 <= 548", false);

		evaluateBooleanResult(name, "549 > 548", true);
		evaluateBooleanResult(name, "549 > 549", false);
		evaluateBooleanResult(name, "549 > 550", false);
		
		evaluateBooleanResult(name, "549 >= 550", false);
		evaluateBooleanResult(name, "549 >= 549", true);
		evaluateBooleanResult(name, "549 >= 548", true);

		evaluateBooleanResult(name, "100 = 100", true);
		evaluateBooleanResult(name, "100 = 99", false);
		evaluateBooleanResult(name, "-100 = -100", true);
		evaluateBooleanResult(name, "0 = 0", true);
		evaluateInvalidResult(name, "100 = invalid");


		evaluateBooleanResult(name, "100 <> 100", false);
		evaluateBooleanResult(name, "100 <> 99", true);
		evaluateBooleanResult(name, "-100 <> -100", false);
		evaluateBooleanResult(name, "0 <> 0", false);
	}

	public void	testComplexOperation() {
		String name = "testComplexOperation";
		
		evaluateIntegerResult(name, "(10 * 2) + 4", 24);
		evaluateIntegerResult(name, "4 + 2 * 10", 24);
		evaluateIntegerResult(name, "4 + 2 * 10 + 6", 30);
		evaluateIntegerResult(name, "(4 + 2) * (10 + 6)", 96);
		evaluateRealResult(name, "((10 * 2) / (5 - 3)) + ((3 * 5 * 4) /  2)" , 40.0);
	}

	public void	testToString() {
		String name = "testToString";
		
		evaluateStringResult(name, "40.toString()", "40");
		evaluateStringResult(name, "(-1).toString()", "-1");
	}


	public	void	testInvalidOperations() {
		String name = "testInvalidOperations";
			
		compileInvalidOperation("10 + true", name);
		compileInvalidOperation("10 - \"alex\"", name);
	}

	
	public void	compileInvalidOperation(String expression, String sourceStream) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNull(exp);
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
}
