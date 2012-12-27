/*
 * Created on 27/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.ArrayList;
import java.util.List;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestPSWOclEvaluator;

import ocl20.evaluation.OclValue;

/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestBooleanValue extends TestPSWOclEvaluator {
	
	public static OclBooleanValue trueValue;	
	public static OclBooleanValue falseValue;
	public static OclValue nullValue;
	public static OclValue invalidValue;
	
	public void setUp() throws Exception {
		super.setUp();
		trueValue = OclValuesFactory.getInstance().createBooleanValue(true);	
		falseValue = OclValuesFactory.getInstance().createBooleanValue(false);
		nullValue = OclValuesFactory.getInstance().createNullValue();
		invalidValue = OclValuesFactory.getInstance().createInvalidValue();
	}
	
	public void	testOr() {
		executeBinaryOperation("or", falseValue, falseValue, falseValue);
		executeBinaryOperation("or", falseValue, trueValue, trueValue);
		executeBinaryOperation("or", falseValue, nullValue, nullValue);
		executeBinaryOperation("or", falseValue, invalidValue, invalidValue);
		
		executeBinaryOperation("or", trueValue, falseValue, trueValue);
		executeBinaryOperation("or", trueValue, trueValue, trueValue);
		executeBinaryOperation("or", trueValue, nullValue, trueValue);
		executeBinaryOperation("or", trueValue, invalidValue, trueValue);
		
		executeBinaryOperation("or", nullValue, trueValue, trueValue);
		executeBinaryOperation("or", nullValue, falseValue, nullValue);
		executeBinaryOperation("or", nullValue, nullValue, nullValue);
		executeBinaryOperation("or", nullValue, invalidValue, invalidValue);
		
		executeBinaryOperation("or", invalidValue, trueValue, trueValue);
		executeBinaryOperation("or", invalidValue, falseValue, invalidValue);
		executeBinaryOperation("or", invalidValue, nullValue, invalidValue);
		executeBinaryOperation("or", invalidValue, invalidValue, invalidValue);
	}

	public void	testAnd() {
		executeBinaryOperation("and", falseValue, falseValue, falseValue);
		executeBinaryOperation("and", falseValue, trueValue, falseValue);
		executeBinaryOperation("and", falseValue, nullValue, falseValue);
		executeBinaryOperation("and", falseValue, invalidValue, falseValue);
		
		executeBinaryOperation("and", trueValue, falseValue, falseValue);
		executeBinaryOperation("and", trueValue, trueValue, trueValue);
		executeBinaryOperation("and", trueValue, nullValue, nullValue);
		executeBinaryOperation("and", trueValue, invalidValue, invalidValue);
		
		executeBinaryOperation("and", nullValue, trueValue, nullValue);
		executeBinaryOperation("and", nullValue, falseValue, falseValue);
		executeBinaryOperation("and", nullValue, nullValue, nullValue);
		executeBinaryOperation("and", nullValue, invalidValue, invalidValue);
		
		executeBinaryOperation("and", invalidValue, trueValue, invalidValue);
		executeBinaryOperation("and", invalidValue, falseValue, falseValue);
		executeBinaryOperation("and", invalidValue, nullValue, invalidValue);
		executeBinaryOperation("and", invalidValue, invalidValue, invalidValue);

	}

	public void	testXor() {
		executeBinaryOperation("xor", falseValue, falseValue, falseValue);
		executeBinaryOperation("xor", falseValue, trueValue, trueValue);
		executeBinaryOperation("xor", falseValue, nullValue, nullValue);
		executeBinaryOperation("xor", falseValue, invalidValue, invalidValue);
		
		executeBinaryOperation("xor", trueValue, falseValue, trueValue);
		executeBinaryOperation("xor", trueValue, trueValue, falseValue);
		executeBinaryOperation("xor", trueValue, nullValue, nullValue);
		executeBinaryOperation("xor", trueValue, invalidValue, invalidValue);
		
		executeBinaryOperation("xor", nullValue, trueValue, nullValue);
		executeBinaryOperation("xor", nullValue, falseValue, nullValue);
		executeBinaryOperation("xor", nullValue, nullValue, nullValue);
		executeBinaryOperation("xor", nullValue, invalidValue, invalidValue);
		
		executeBinaryOperation("xor", invalidValue, trueValue, invalidValue);
		executeBinaryOperation("xor", invalidValue, falseValue, invalidValue);
		executeBinaryOperation("xor", invalidValue, nullValue, invalidValue);
		executeBinaryOperation("xor", invalidValue, invalidValue, invalidValue);
	}

	public void	testImplies() {
		executeBinaryOperation("implies", falseValue, falseValue, trueValue);
		executeBinaryOperation("implies", falseValue, trueValue, trueValue);
		executeBinaryOperation("implies", falseValue, nullValue, trueValue);
		executeBinaryOperation("implies", falseValue, invalidValue, trueValue);

		executeBinaryOperation("implies", trueValue, falseValue, falseValue);
		executeBinaryOperation("implies", trueValue, trueValue, trueValue);
		executeBinaryOperation("implies", trueValue, nullValue, nullValue);
		executeBinaryOperation("implies", trueValue, invalidValue, invalidValue);
		
		executeBinaryOperation("implies", nullValue, falseValue, nullValue);
		executeBinaryOperation("implies", nullValue, trueValue, trueValue);
		executeBinaryOperation("implies", nullValue, nullValue, nullValue);
		executeBinaryOperation("implies", nullValue, invalidValue, invalidValue);
		
		executeBinaryOperation("implies", invalidValue, falseValue, invalidValue);
		executeBinaryOperation("implies", invalidValue, trueValue, trueValue);
		executeBinaryOperation("implies", invalidValue, nullValue, invalidValue);
		executeBinaryOperation("implies", invalidValue, invalidValue, invalidValue);
	}

	public void	testNot() {
		executeUnaryOperation("not", falseValue, trueValue);
		executeUnaryOperation("not", trueValue, falseValue);
		executeUnaryOperation("not", nullValue, nullValue);
		executeUnaryOperation("not", invalidValue, invalidValue);
	}

	public void	testEquality() {
		executeBinaryOperation("=", falseValue, falseValue, trueValue);
		executeBinaryOperation("=", falseValue, trueValue, falseValue);
		executeBinaryOperation("=", falseValue, nullValue, nullValue);
		executeBinaryOperation("=", falseValue, invalidValue, invalidValue);

		executeBinaryOperation("=", trueValue, falseValue, falseValue);
		executeBinaryOperation("=", trueValue, trueValue, trueValue);
		executeBinaryOperation("=", trueValue, nullValue, nullValue);
		executeBinaryOperation("=", trueValue, invalidValue, invalidValue);
		
		executeBinaryOperation("=", nullValue, falseValue, nullValue);
		executeBinaryOperation("=", nullValue, trueValue, nullValue);
		executeBinaryOperation("=", nullValue, nullValue, nullValue);
		executeBinaryOperation("=", nullValue, invalidValue, invalidValue);

		executeBinaryOperation("=", invalidValue, falseValue, invalidValue);
		executeBinaryOperation("=", invalidValue, trueValue, invalidValue);
		executeBinaryOperation("=", invalidValue, nullValue, invalidValue);
		executeBinaryOperation("=", invalidValue, invalidValue, invalidValue);
	}

	public void	testDifferent() {
		executeBinaryOperation("<>", falseValue, falseValue, falseValue);
		executeBinaryOperation("<>", falseValue, trueValue, trueValue);
		executeBinaryOperation("<>", falseValue, nullValue, nullValue);
		executeBinaryOperation("<>", falseValue, invalidValue, invalidValue);

		executeBinaryOperation("<>", trueValue, falseValue, trueValue);
		executeBinaryOperation("<>", trueValue, trueValue, falseValue);
		executeBinaryOperation("<>", trueValue, nullValue, nullValue);
		executeBinaryOperation("<>", trueValue, invalidValue, invalidValue);

		executeBinaryOperation("<>", nullValue, falseValue, nullValue);
		executeBinaryOperation("<>", nullValue, trueValue, nullValue);
		executeBinaryOperation("<>", nullValue, nullValue, nullValue);
		executeBinaryOperation("<>", nullValue, invalidValue, invalidValue);
		
		executeBinaryOperation("<>", invalidValue, falseValue, invalidValue);
		executeBinaryOperation("<>", invalidValue, trueValue, invalidValue);
		executeBinaryOperation("<>", invalidValue, nullValue, invalidValue);
		executeBinaryOperation("<>", invalidValue, invalidValue, invalidValue);
	}

	public void	testEquals() {
		executeEqualsTest(false, false, true);
		executeEqualsTest(false, true, false);
		executeEqualsTest(true, false, false);
		executeEqualsTest(true, true, true);
		
		OclBooleanValue val = ValuesFactory.createBooleanValue(true);
		assertTrue(val.equals(val));
		OclIntegerValue valInt = ValuesFactory.createIntegerValue(1);
		assertFalse(val.equals(valInt));
	}

	
	private	void	executeBinaryOperation(String op, OclValue val1, OclValue val2, OclValue expectedResult) {
		List args = new ArrayList();
		args.add(val2);
		OclValue result = (OclValue) val1.executeOperation(op, args);
		assertTrue(result == expectedResult);			
	}

	private	void	executeUnaryOperation(String op, OclValue val1, OclValue expectedResult) {
		OclValue result = (OclValue) val1.executeOperation(op, null);
		assertTrue(result == expectedResult);			
	}

	private	void	executeEqualsTest(boolean val1, boolean val2, boolean expectedResult) {
		OclBooleanValue num1 = ValuesFactory.createBooleanValue(val1);
		OclBooleanValue num2 = ValuesFactory.createBooleanValue(val2);
		assertNotNull(num1);
		assertNotNull(num2);
		assertEquals(expectedResult, num1.equals(num2));			
	}

}
