/*
 * Created on 27/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.ArrayList;
import java.util.List;

import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestPSWOclEvaluator;

/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestRealValue extends TestPSWOclEvaluator {

	public void	testSum() {
		executeSum("5.5", "2.2", "7.7");
		executeSum("1000.90", "100.90", "1101.80");
		executeSum("1000.123456", "100.12340", "1100.246856");
		executeSum("-5.3", "-10.6", "-15.9");
		executeSum("0.0", "0.0", "0.0");
		
		executeBinaryOperationWithUndefinedValues("+", getReal("10"));
	}

	public void	testSubtraction() {
		executeSubtraction("5.5", "2.2", "3.3");
		executeSubtraction("1000.90", "100.90", "900.00");
		executeSubtraction("-5.3", "-10.7", "5.4");
		executeSubtraction("0.0", "0.0", "0.0");
		
		executeBinaryOperationWithUndefinedValues("-", getReal("10"));
	}

	public void	testPlus() {
		executePlus("10.4", "2.8", "29.12");
		executePlus("2000.74", "500.29", "1000950.2146");
		executePlus("-5.9", "-10.3", "60.77");
		executePlus("0", "0", "0");
		executePlus("-10.4", "10.3", "-107.12");
		
		executeBinaryOperationWithUndefinedValues("*", getReal("10"));
	}

	public void	testDiv() {
		executeDiv("12.4", "2.4", "5.166666666666667");
		executeDiv("100.75", "40.5", "2.4876543209876543");
		executeDiv("-10.0", "-2.5", "4.0");
		
		executeBinaryOperationWithUndefinedValues("/", getReal("10"));
	}

	public void	testAbs() {
		executeAbs("5.5", "5.5");
		executeAbs("-5.6", "5.6");
		executeAbs("0.0", "0.0");
		
		executeUnaryOperationWithUndefinedValues("abs");
	}

	public void	testMinus() {
		executeMinus("5.3", "-5.3");
		executeMinus("-5.89", "5.89");
		executeMinus("0.0", "0.0");
		
		executeUnaryOperationWithUndefinedValues("-");
	}

	public void	testFloor() {
		executeFloor("10.1", 10);
		executeFloor("10.9", 10);
		executeFloor("10.5", 10);
		
		executeUnaryOperationWithUndefinedValues("floor");
	}

	public void	testRound() {
		executeRound("10.1", 10);
		executeRound("10.8", 11);
		executeRound("10.5", 11);
		executeRound("10.49", 10);
		
		executeUnaryOperationWithUndefinedValues("round");
	}


	public void	testMax() {
		executeMax("5.49", "5.48", "5.49");
		executeMax("8.90", "700.34", "700.34");
		executeMax("5.48", "5.49", "5.49");
		executeMax("700.34", "8.90", "700.34");
		
		executeBinaryOperationWithUndefinedValues("max", getReal("10"));
	}

	public void	testMin() {
		executeMin("5.49", "5.48", "5.48");
		executeMin("8.90", "700.34", "8.90");
		executeMin("5.48", "5.49", "5.48");
		executeMin("700.34", "8.90", "8.90");
		
		executeBinaryOperationWithUndefinedValues("min", getReal("10"));
	}

	public void	testComparisons() {
		executeComparison(5.49, "<", 5.48, false);
		executeComparison(5.49, "<", 5.49, false);
		executeComparison(5.49, "<", 5.491, true);

		executeComparison(5.49, "<=", 5.50, true);
		executeComparison(5.49, "<=", 5.49, true);
		executeComparison(5.49, "<=", 5.485, false);
		
		executeComparison(5.49, ">", 5.48, true);
		executeComparison(5.49, ">", 5.49, false);
		executeComparison(5.49, ">", 5.50, false);
		
		executeComparison(5.49, ">=", 5.48, true);
		executeComparison(5.49, ">=", 5.49, true);
		executeComparison(5.49, ">=", 5.50, false);
		
		executeBinaryOperationWithUndefinedValues(">", getReal("10"));
		executeBinaryOperationWithUndefinedValues(">=", getReal("10"));
		executeBinaryOperationWithUndefinedValues("<", getReal("10"));
		executeBinaryOperationWithUndefinedValues("<=", getReal("10"));
	}

	public void	testEquality() {
		executeComparison(100.45, "=", 100.45, true);
		executeComparison(100.34, "=", 99.98, false);
		executeComparison(-100.00, "=", -100.00, true);
		executeComparison(0.0, "=", 0.0, true);
		
		executeBinaryOperationWithUndefinedValues("=", getReal("10"));
	}

	public void	testDifferent() {
		executeComparison(100.1, "<>", 100.1, false);
		executeComparison(100.3, "<>", 99.4, true);
		executeComparison(-100.0, "<>", -100.0, false);
		executeComparison(0.0, "<>", 0.0, false);
		
		executeBinaryOperationWithUndefinedValues("<>", getReal("10"));
	}

	public void	testEquals() {
		executeEqualsTest(100.23, 100.23, true);
		executeEqualsTest(100.23, 99.23, false);
		
		OclRealValue val = ValuesFactory.createRealValue(String.valueOf(1.0));
		assertTrue(val.equals(val));
		OclIntegerValue valInt = ValuesFactory.createIntegerValue(1);
		assertTrue(val.equals(valInt));
		OclBooleanValue valBoolean = ValuesFactory.createBooleanValue(true);
		assertFalse(val.equals(valBoolean));
	}

	private	void	executeUnaryOperation(String op, String val1, String expectedResult) {
		OclRealValue num1 = ValuesFactory.createRealValue(val1);
		assertNotNull(num1);
		OclRealValue result = (OclRealValue) num1.executeOperation(op, null);
		assertEquals(expectedResult, result.doubleValue().toString());		
	}

	private	void	executeUnaryOperationIntegerResult(String op, String  val1, long expectedResult) {
		OclRealValue num1 = ValuesFactory.createRealValue(val1);
		assertNotNull(num1);
		OclIntegerValue result = (OclIntegerValue) num1.executeOperation(op, null);
		assertEquals(expectedResult, result.intValue());		
	}


	private	void	executeBinaryOperation(String op, String val1, String val2, String expectedResult) {
		OclRealValue num1 = ValuesFactory.createRealValue(val1);
		OclRealValue num2 = ValuesFactory.createRealValue(val2);
		OclRealValue result = ValuesFactory.createRealValue(expectedResult);
		executeBinaryOperation(op, num1, num2, result);
	}
	

	private	void	executeUnaryOperation(String op, OclValue num1, OclValue expectedResult) {
		assertNotNull(num1);
		assertNotNull(expectedResult);
		
		OclValue result = (OclValue) num1.executeOperation(op, null);
		if (! expectedResult.oclIsUndefined())
			assertTrue(expectedResult.equals(result));
		else {
			assertTrue(expectedResult == result);
		}	
	}

	private	void	executeBinaryOperation(String op, OclValue num1, OclValue num2, OclValue expectedResult) {
		assertNotNull(num1);
		assertNotNull(num2);
		assertNotNull(expectedResult);
		
		List args = new ArrayList();
		args.add(num2);
		OclValue result = (OclValue) num1.executeOperation( op, args);
		if (! expectedResult.oclIsUndefined())
			assertTrue(expectedResult.equals(result));
		else {
			assertTrue(expectedResult == result);
		}	
	}

	private	void	executeBinaryOperationWithUndefinedValues(String op, OclValue num) {
		executeBinaryOperation(op, num, getNull(), getNull());
		executeBinaryOperation(op, getNull(),  num, getNull());
		executeBinaryOperation(op, getNull(),  getNull(), getNull());
		
		executeBinaryOperation(op, num, getInvalid(), getInvalid());
		executeBinaryOperation(op, getInvalid(),  num, getInvalid());
		executeBinaryOperation(op, getInvalid(),  getInvalid(), getInvalid());

		executeBinaryOperation(op, getInvalid(),  getNull(), getInvalid());
		executeBinaryOperation(op, getNull(),  getInvalid(), getInvalid());
	}

	private	void	executeUnaryOperationWithUndefinedValues(String op) {
		executeUnaryOperation(op, getNull(), getNull());
		executeUnaryOperation(op, getInvalid(), getInvalid());
	}

	private	void	executeSum(String val1, String val2, String expectedResult) {
		executeBinaryOperation("+", val1, val2, expectedResult);		
	}

	private	void	executeSubtraction(String val1, String val2, String expectedResult) {
		executeBinaryOperation("-", val1, val2, expectedResult);		
	}

	private	void	executePlus(String val1, String val2, String expectedResult) {
		executeBinaryOperation("*", val1, val2, expectedResult);		
	}

	private	void	executeDiv(String val1, String val2, String expectedResult) {
		executeBinaryOperation("/", val1, val2, expectedResult);		
	}

	private	void	executeAbs(String val1, String expectedResult) {
		executeUnaryOperation("abs", val1, expectedResult);		
	}

	private	void	executeMinus(String val1, String expectedResult) {
		executeUnaryOperation("-", val1, expectedResult);		
	}

	private	void	executeFloor(String val1, long expectedResult) {
		executeUnaryOperationIntegerResult("floor", val1, expectedResult);		
	}
	
	private	void	executeRound(String val1, long expectedResult) {
		executeUnaryOperationIntegerResult("round", val1, expectedResult);		
	}


	private	void	executeMax(String val1, String val2, String expectedResult) {
		executeBinaryOperation("max", val1, val2, expectedResult);		
	}

	private	void	executeMin(String val1, String val2, String expectedResult) {
		executeBinaryOperation("min", val1, val2, expectedResult);		
	}

	private	void	executeComparison(double val1, String op, double val2, boolean expectedResult) {
		OclRealValue num1 = ValuesFactory.createRealValue(String.valueOf(val1));
		OclRealValue num2 = ValuesFactory.createRealValue(String.valueOf(val2));
		assertNotNull(num1);
		assertNotNull(num2);
		List args = new ArrayList();
		args.add(num2);
		OclBooleanValue result = (OclBooleanValue) num1.executeOperation(op, args);
		assertEquals(expectedResult, result.booleanValue());		
	}
	
	private	void	executeEqualsTest(double val1, double val2, boolean expectedResult) {
		OclRealValue num1 = ValuesFactory.createRealValue(String.valueOf(val1));
		OclRealValue num2 = ValuesFactory.createRealValue(String.valueOf(val2));
		assertNotNull(num1);
		assertNotNull(num2);
		assertEquals(expectedResult, num1.equals(num2));			
	}


}
