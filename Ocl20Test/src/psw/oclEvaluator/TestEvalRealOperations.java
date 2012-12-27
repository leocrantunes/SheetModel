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
public class TestEvalRealOperations extends TestEval {
	public void	testSum() {
		String	name = "testSum";
		
		evaluateRealResult(name, "5.5 + 2.2", 7.7);
		evaluateRealResult(name, "1000.90 + 100.90", 1101.80);
		evaluateRealResult(name, "-5.3 + -10.6", -15.9);
		evaluateRealResult(name, "0.0 + 0.0", 0.0);
		evaluateRealResult(name, "15.5 + 3", 18.5);
		evaluateRealResult(name, "15 + 3.5", 18.5);
		evaluateRealResult(name, "15.5 + 3 + 2.5", 21.0);
	}
	
	public void	testSubtraction() {
		String	name = "testSubtraction";
		
		evaluateRealResult(name, "5.5 - 2.2", 3.3);
		evaluateRealResult(name, "1000.90 - 100.90", 900.00);
		evaluateRealResult(name, "-5.3 - -10.7", 5.4);
		evaluateRealResult(name, "-(5.3) - (-10.7)", 5.4);
		evaluateRealResult(name, "-((5.3) - (-10.7))", -16.0);
		evaluateRealResult(name, "0.0 - 0.0", 0.0);
	}
	

	public void	testPlus() {
		String	name = "testPlus";
		
		evaluateRealResult(name, "10.4 * 2.8", 29.12);
		evaluateRealResult(name, "2000.74 * 500.29", 1000950.2146000001);
		evaluateRealResult(name, "-5.9 * -10.3", 60.77);
		evaluateRealResult(name, "0.0 * 0.0", 0.0);
		evaluateRealResult(name, "-10.4 * 10.3", -107.12);
	}

	public void	testDiv() {
		String	name = "testDiv";
		
		evaluateRealResult(name, "12.4 / 2.4", 5.166666666666);
		evaluateRealResult(name, "100.75 / 40.5", 2.48765432);
		evaluateRealResult(name, "-10.0 / -2.5", 4.0);
	}


	public void	testAbs() {
		String	name = "testAbs";
		
		evaluateRealResult(name, "5.5.abs()", 5.5);
		evaluateRealResult(name, "(-5.5).abs()", 5.5);
		evaluateRealResult(name, "-5.5.abs()", -5.5);
		evaluateRealResult(name, "0.0.abs()", 0.0);
	}

	public void	testMinus() {
		String	name = "testMinus";
		
		evaluateRealResult(name, "-5.3", -5.3);
		evaluateRealResult(name, "-(-5.89)", 5.89);
		evaluateRealResult(name, "-0.0", 0.0);
	}

	public void	testFloor() {
		String	name = "testFloor";
		
		evaluateIntegerResult(name, "10.1.floor()", 10);
		evaluateIntegerResult(name, "10.9.floor()", 10);
		evaluateIntegerResult(name, "10.5.floor()", 10);
	}

	public void	testRound() {
		String	name = "testRound";
		
		evaluateIntegerResult(name, "10.1.round()", 10);
		evaluateIntegerResult(name, "10.8.round()", 11);
		evaluateIntegerResult(name, "10.5.round()", 11);
		evaluateIntegerResult(name, "10.49.round()", 10);
	}


	public void	testMax() {
		String	name = "testMax";
		
		evaluateRealResult(name, "5.49.max(5.48)", 5.49);
		evaluateRealResult(name, "8.90.max(700.34)", 700.34);
		evaluateRealResult(name, "5.48.max(5.49)", 5.49);
		evaluateRealResult(name, "700.34.max(8.90)", 700.34);
	}

	public void	testMin() {
		String	name = "testMax";
		
		evaluateRealResult(name, "5.49.min(5.48)", 5.48);
		evaluateRealResult(name, "8.90.min(700.34)", 8.90);
		evaluateRealResult(name, "5.48.min(5.49)", 5.48);
		evaluateRealResult(name, "700.34.min(8.90)", 8.90);
		evaluateRealResult(name, "5.min(5.48)", 5.00);
	}

	public void	testComparisons() {
		String	name = "testComparisons";
		
		evaluateBooleanResult(name, "5.49 < 5.48", false);
		evaluateBooleanResult(name, "5.49 < 5.49", false);
		evaluateBooleanResult(name, "5.49 < 5.491", true);
		
		evaluateBooleanResult(name, "5.49 <= 5.50", true);
		evaluateBooleanResult(name, "5.49 <= 5.49", true);
		evaluateBooleanResult(name, "5.49 <= 5.485", false);

		evaluateBooleanResult(name, "5.49 > 5.48", true);
		evaluateBooleanResult(name, "5.49 > 5.49", false);
		evaluateBooleanResult(name, "5.49 > 5.50", false);
		evaluateBooleanResult(name, "5.48 > 5", true);
		evaluateBooleanResult(name, "5 < 5.48", true);

		evaluateBooleanResult(name, "5.49 >= 5.48", true);
		evaluateBooleanResult(name, "5.49 >= 5.49", true);
		evaluateBooleanResult(name, "5.49 >= 5.50", false);
		
		evaluateBooleanResult(name, "100.45 = 100.45", true);
		evaluateBooleanResult(name, "100.34 = 99.98", false);
		evaluateBooleanResult(name, "-100.00 = -100.00", true);
		evaluateBooleanResult(name, "0.0 = 0.0", true);
		
		evaluateBooleanResult(name, "100.1 = 100.1", true);
		evaluateBooleanResult(name, "100.34 <> 99.98", true);
		evaluateBooleanResult(name, "-100.00 = -100.00", true);
		evaluateBooleanResult(name, "-100.00 <> -100.00", false);
		evaluateBooleanResult(name, "0.0 <> 0.0", false);
		
		evaluateBooleanResult(name, "64.7 = 94.7", false);
		evaluateBooleanResult(name, "64.7 <> 94.7", true);
	}

	public void	testToString() {
		String name = "testToString";
		
		evaluateStringResult(name, "40.4.toString()", "40.4");
		evaluateStringResult(name, "(-1.92).toString()", "-1.92");
	}

}
