/*
 * Created on 10/10/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestEvalDateOperations extends TestEval {

	public	void	testGetData() {
		String	name = "testGetData";
		
		
		evaluateIntegerResult(name, "\"01/10/2005\".toDate().getDay()", 1);
		evaluateIntegerResult(name, "\"01/10/2005\".toDate().getMonth()", 10);
		evaluateIntegerResult(name, "\"01/10/2005\".toDate().getYear()", 2005);
		evaluateIntegerResult(name, "\"03/10/2005\".toDate().getDow()", 2);
	}
	
	public	void	testInvalidDate() {
		String	name = "testInvalidDate";
		
		evaluateInvalidResult(name, "\"35/10/2005\".toDate()");
		evaluateInvalidResult(name, "\"31/11/2005\".toDate()");
		evaluateInvalidResult(name, "\"01/13/2005\".toDate()");
	}

	public	void	testComparisonOperations() {
		String	name = "testComparisonOperations";
		
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().isBefore(\"30/08/2005\".toDate())", false);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().isBefore(\"01/10/2005\".toDate())", false);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().isBefore(\"02/10/2005\".toDate())", true);
		evaluateBooleanResult(name, "\"10/10/2005\".toDate().isBefore(\"01/11/2005\".toDate())", true);
		
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().isAfter(\"30/08/2005\".toDate())", true);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().isAfter(\"01/10/2005\".toDate())", false);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().isAfter(\"02/10/2005\".toDate())", false);
		evaluateBooleanResult(name, "\"10/10/2005\".toDate().isAfter(\"01/11/2005\".toDate())", false);
		
		evaluateBooleanResult(name, "\"01/10/2005\".toDate() = \"01/10/2005\".toDate()", true);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate() <> \"01/10/2005\".toDate()", false);
		
		evaluateBooleanResult(name, "\"01/10/2005\".toDate() = \"02/10/2005\".toDate()", false);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate() <> \"02/10/2005\".toDate()", true);
	}
	
	public	void	testAddOperations() {
		String	name = "testAddOperations";
		
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().addDay(1) = \"02/10/2005\".toDate()", true);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().addDay(31) = \"01/11/2005\".toDate()", true);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().addMonth(1) = \"01/11/2005\".toDate()", true);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().addMonth(3) = \"01/01/2006\".toDate()", true);
		evaluateBooleanResult(name, "\"01/10/2005\".toDate().addYear(1) = \"01/10/2006\".toDate()", true);
	}

}
