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
public class TestIntegerValue extends TestPSWOclEvaluator {

	public void	testSum() {
		String	op = "+";
		
		executeBinaryOperation(op, 5, 10, 15);
		executeBinaryOperation(op, 1000, 2000, 3000);
		executeBinaryOperation(op, -5, -10, -15);
		executeBinaryOperation(op, 0, 0, 0);
		
		executeBinaryOperationDouble(op, 5, 10.43, 15.43);
		
		executeBinaryOperationWithUndefinedValues(op, getInt(10));
	}

	public void	testSubtraction() {
		executeSubtraction(10, 4, 6);
		executeSubtraction(2000, 500, 1500);
		executeSubtraction(-5, -10, 5);
		executeSubtraction(0, 0, 0);
		executeSubtraction(-10, 10, -20);
		
		executeBinaryOperationWithUndefinedValues("-", getInt(10));
	}

	public void	testPlus() {
		executePlus(10, 4, 40);
		executePlus(2000, 500, 1000000);
		executePlus(-5, -10, 50);
		executePlus(0, 0, 0);
		executePlus(-10, 10, -100);
		
		executeBinaryOperationWithUndefinedValues("*", getInt(10));
	}

	public void	testDiv() {
		executeDiv(10, 5, 2);
		executeDiv(100, 40, 2);
		executeDiv(-10, -5, 2);
		
		executeBinaryOperationWithUndefinedValues("/", getInt(10));
	}

	public void	testDivide() {
		executeDivide(10, 5.5, 1.8181818181818181);
		executeDivide(100, 40.73, 2.4551927326295115);
		executeDivide(-10, -3.78, 2.6455026455026456);
		
		executeBinaryOperationWithUndefinedValues("div" , getInt(10));
	}

	public void	testAbs() {
		executeAbs(5, 5);
		executeAbs(-5, 5);
		executeAbs(0, 0);
		executeUnaryOperationWithUndefinedValues("abs");
	}

	public void	testMinus() {
		executeMinus(5, -5);
		executeMinus(-5, 5);
		executeMinus(0, 0);
		executeUnaryOperationWithUndefinedValues("-");
	}

	public void	testMod() {
		executeMod(10, 3, 1);
		executeMod(20, 7, 6);
		executeMod(-20, 7, -6);
		executeBinaryOperationWithUndefinedValues("mod", getInt(10));
	}

	public void	testMax() {
		executeMax(5, 100, 100);
		executeMax(-8, 5, 5);
		executeMax(100, 5, 100);
		executeMax(5, -8, 5);
		executeBinaryOperationWithUndefinedValues("max", getInt(10));
	}

	public void	testMin() {
		executeMin(5, 100, 5);
		executeMin(-8, 5, -8);
		executeMin(100, 5, 5);
		executeMin(5, -8, -8);
		executeBinaryOperationWithUndefinedValues("min", getInt(10));
	}

	public void	testComparisons() {
		executeComparison(549, "<", 548, false);
		executeComparison(549, "<", 549, false);
		executeComparison(549, "<", 550, true);

		executeComparison(549, "<=", 550, true);
		executeComparison(549, "<=", 549, true);
		executeComparison(549, "<=", 548, false);
		
		executeComparison(549, ">", 548, true);
		executeComparison(549, ">", 549, false);
		executeComparison(549, ">", 550, false);
		
		executeComparison(549, ">=", 548, true);
		executeComparison(549, ">=", 549, true);
		executeComparison(549, ">=", 550, false);
		
		
		executeBinaryOperationWithUndefinedValues(">", getInt(10));
		executeBinaryOperationWithUndefinedValues(">=", getInt(10));
		executeBinaryOperationWithUndefinedValues("<", getInt(10));
		executeBinaryOperationWithUndefinedValues("<=", getInt(10));
	}

	public void	testEquality() {
		executeComparison(100, "=", 100, true);
		executeComparison(100, "=", 99, false);
		executeComparison(-100, "=", -100, true);
		executeComparison(0, "=", 0, true);
		
		executeBinaryOperationWithUndefinedValues("=", getInt(10));
	}

	public void	testDifferent() {
		executeComparison(100, "<>", 100, false);
		executeComparison(100, "<>", 99, true);
		executeComparison(-100, "<>", -100, false);
		executeComparison(0, "<>", 0, false);
		
		executeBinaryOperationWithUndefinedValues("<>", getInt(10));
	}

	public void	testEquals() {
		executeEqualsTest(100, 100, true);
		executeEqualsTest(100, 99, false);
		
		OclIntegerValue val = ValuesFactory.createIntegerValue(1);
		assertTrue(val.equals(val));
		OclRealValue valReal = ValuesFactory.createRealValue("1.0");
		assertTrue(val.equals(valReal));
		OclBooleanValue valBoolean = ValuesFactory.createBooleanValue(true);
		assertFalse(val.equals(valBoolean));
	}


	private	void	executeUnaryOperation(String op, int val1, int expectedResult) {
		OclIntegerValue num1 = ValuesFactory.createIntegerValue(val1);
		assertNotNull(num1);
		OclIntegerValue result = (OclIntegerValue) num1.executeOperation(op, null);
		assertEquals(expectedResult, result.intValue());		
	}

	private	void	executeBinaryOperation(String op, int val1, int val2, int expectedResult) {
		OclIntegerValue num1 = ValuesFactory.createIntegerValue(val1);
		OclIntegerValue num2 = ValuesFactory.createIntegerValue(val2);
		OclIntegerValue result = ValuesFactory.createIntegerValue(expectedResult);
		executeBinaryOperation(op, num1, num2, result);
	}

	private	void	executeBinaryOperationDouble(String op, int val1, double val2, double expectedResult) {
		OclIntegerValue num1 = ValuesFactory.createIntegerValue(val1);
		OclRealValue num2 = ValuesFactory.createRealValue(String.valueOf(val2));
		OclRealValue result = ValuesFactory.createRealValue(String.valueOf(expectedResult));
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


	private	void	executeSubtraction(int val1, int val2, int expectedResult) {
		executeBinaryOperation("-", val1, val2, expectedResult);		
	}

	private	void	executePlus(int val1, int val2, int expectedResult) {
		executeBinaryOperation("*", val1, val2, expectedResult);		
	}

	private	void	executeDiv(int val1, int val2, int expectedResult) {
		executeBinaryOperation("div", val1, val2, expectedResult);		
	}

	private	void	executeDivide(int val1, double val2, double expectedResult) {
		executeBinaryOperationDouble("/", val1, val2, expectedResult);		
	}

	private	void	executeAbs(int val1, int expectedResult) {
		executeUnaryOperation("abs", val1, expectedResult);		
	}

	private	void	executeMinus(int val1, int expectedResult) {
		executeUnaryOperation("-", val1, expectedResult);		
	}

	private	void	executeMod(int val1, int val2, int expectedResult) {
		executeBinaryOperation("mod", val1, val2, expectedResult);		
	}

	private	void	executeMax(int val1, int val2, int expectedResult) {
		executeBinaryOperation("max", val1, val2, expectedResult);		
	}

	private	void	executeMin(int val1, int val2, int expectedResult) {
		executeBinaryOperation("min", val1, val2, expectedResult);		
	}

	private	void	executeComparison(long val1, String op, long val2, boolean expectedResult) {
		OclIntegerValue num1 = ValuesFactory.createIntegerValue(val1);
		OclIntegerValue num2 = ValuesFactory.createIntegerValue(val2);
		OclBooleanValue	result = ValuesFactory.createBooleanValue(expectedResult);
		assertNotNull(num1);
		assertNotNull(num2);
		assertNotNull(result);
		executeBinaryOperation(op, num1, num2, result);
	}

	private	void	executeEqualsTest(long val1, long val2, boolean expectedResult) {
		OclIntegerValue num1 = ValuesFactory.createIntegerValue(val1);
		OclIntegerValue num2 = ValuesFactory.createIntegerValue(val2);
		assertNotNull(num1);
		assertNotNull(num2);
		assertEquals(expectedResult, num1.equals(num2));			
	}
}
