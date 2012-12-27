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
public class TestEvalDateTimeOperations extends TestEvalDateOperations {
	public	void	testGetData() {
		String	name = "testGetData";
		
		
		evaluateIntegerResult(name, "\"01/10/2005 10:00:00\".toDateTime().getDay()", 1);
		evaluateIntegerResult(name, "\"01/10/2005 10:00:00\".toDateTime().getMonth()", 10);
		evaluateIntegerResult(name, "\"01/10/2005 10:00:00\".toDateTime().getYear()", 2005);

		evaluateIntegerResult(name, "\"01/10/2005 10:05:56\".toDateTime().getHour()", 10);
		evaluateIntegerResult(name, "\"01/10/2005 10:05:56\".toDateTime().getMinute()", 05);
		evaluateIntegerResult(name, "\"01/10/2005 10:05:56\".toDateTime().getSecond()", 56);
		
		evaluateIntegerResult(name, "\"01/10/2005 10:00:00\".toDateTime().getDow()", 7);
	}
	
	public	void	testInvalidDate() {
		String	name = "testInvalidDate";
		
		evaluateInvalidResult(name, "\"35/10/2005\".toDateTime()");
		evaluateInvalidResult(name, "\"31/11/2005\".toDateTime()");
		evaluateInvalidResult(name, "\"01/13/2005\".toDateTime()");
		evaluateInvalidResult(name, "\"35/10/2005 10:00:00\".toDateTime()");
		evaluateInvalidResult(name, "\"31/11/2005 10:00:00\".toDateTime()");
		evaluateInvalidResult(name, "\"01/13/2005 10:00:00\".toDateTime()");

		evaluateInvalidResult(name, "\"01/10/2005 25:00:00\".toDateTime()");
		evaluateInvalidResult(name, "\"01/10/2005 10:60:00\".toDateTime()");
		evaluateInvalidResult(name, "\"01/10/2005 10:00:60\".toDateTime()");

	}

	public	void	testComparisonOperations() {
		String	name = "testComparisonOperations";
		
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isBefore(\"30/08/2005 13:00:00\".toDateTime())", false);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isBefore(\"01/10/2005 09:00:00\".toDateTime())", false);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isBefore(\"01/10/2005 10:00:00\".toDateTime())", false);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isBefore(\"01/10/2005 10:00:01\".toDateTime())", true);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isBefore(\"02/10/2005 03:00:00\".toDateTime())", true);
		evaluateBooleanResult(name, "\"10/10/2005 23:59:00\".toDateTime().isBefore(\"01/11/2005 02:00:00\".toDateTime())", true);

		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isAfter(\"30/08/2005 13:00:00\".toDateTime())", true);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isAfter(\"01/10/2005 09:59:59\".toDateTime())", true);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isAfter(\"01/10/2005 10:00:00\".toDateTime())", false);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isAfter(\"01/10/2005 10:00:01\".toDateTime())", false);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().isAfter(\"02/10/2005 03:00:00\".toDateTime())", false);
		evaluateBooleanResult(name, "\"10/10/2005 23:59:00\".toDateTime().isAfter(\"01/11/2005 02:00:00\".toDateTime())", false);
	}
	
	public	void	testAddOperations() {
		String	name = "testAddOperations";
		
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().addDay(1) = \"02/10/2005 10:00:00\".toDateTime()", true);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().addDay(31) = \"01/11/2005 10:00:00\".toDateTime()", true);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().addMonth(1) = \"01/11/2005 10:00:00\".toDateTime()", true);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().addMonth(3) = \"01/01/2006 10:00:00\".toDateTime()", true);
		evaluateBooleanResult(name, "\"01/10/2005 10:00:00\".toDateTime().addYear(1) = \"01/10/2006 10:00:00\".toDateTime()", true);
		evaluateBooleanResult(name, "\"31/12/2005 23:59:00\".toDateTime().addSecond(61) = \"01/01/2006 00:00:01\".toDateTime()", true);
		evaluateBooleanResult(name, "\"31/12/2005 23:59:00\".toDateTime().addMinute(1) = \"01/01/2006 00:00:00\".toDateTime()", true);
		evaluateBooleanResult(name, "\"31/12/2005 23:59:00\".toDateTime().addHour(1) = \"01/01/2006 00:59:00\".toDateTime()", true);
	}
}
