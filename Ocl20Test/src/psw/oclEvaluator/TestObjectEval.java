/*
 * Created on 11/08/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import impl.ocl20.environment.NameClashException;
import impl.ocl20.environment.NullNameException;

import ocl20.common.CoreClassifier;
import ocl20.evaluation.OclValue;
import ocl20.expressions.OclExpression;
import ocl20.expressions.VariableDeclaration;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.IObjectSpace;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PSWObjectSpace;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public abstract class TestObjectEval extends TestEval {
	protected	IEvalEnvironment 	evalEnv;
	protected	IObjectSpace	objectSpace;

	public	void 	setUp() throws Exception {
		objectSpace = new PSWObjectSpace();
		evalEnv = new EvalEnvironment();
		super.setUp();
	}

	protected	OclObjectValue	createInstanceOf(String className) {
		return	createInstanceOf(objectSpace, className);
	}

	protected	OclObjectValue	createInstanceOf(IObjectSpace objSpace, String className) {
		CoreClassifier	classifier;
		if (className.indexOf("::") > 0)
			classifier = (CoreClassifier) environment.lookupPathName(className);
		else	
			classifier = (CoreClassifier) environment.lookup(className);
		return	objSpace.createObject(classifier);
	}

	protected	void	evaluateIntegerExpression(String sourceStream, String expression, int expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclIntegerValue value;
			value = (OclIntegerValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
			assertEquals("Integer",  value.getType().getName());
			assertEquals(expectedResult, value.intValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	protected	void	evaluateNullExpression(String sourceStream, String expression) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclValue value;
			value = (OclValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
			assertTrue(value.oclIsNull());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	protected	void	evaluateBooleanExpression(String sourceStream, String expression, boolean expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclBooleanValue value;
			value = (OclBooleanValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
			assertEquals("Boolean",  value.getType().getName());
			assertEquals(expectedResult, value.booleanValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	protected	void	evaluateRealExpression(String sourceStream, String expression, double expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclRealValue value;
			value = (OclRealValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
			assertEquals("Real",  value.getType().getName());
			assertEquals(expectedResult, value.doubleValue().doubleValue(), 0.000001);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}


	protected	void	evaluateStringExpression(String sourceStream, String expression, String expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclStringValue value;
			value = (OclStringValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
			assertEquals("String",  value.getType().getName());
			assertEquals(expectedResult, value.stringValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	
	protected	void	rawEvaluateRealExpression(String sourceStream, String expression, double expectedResult) throws Exception {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclRealValue value;
			value = (OclRealValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
			assertEquals("Real",  value.getType().getName());
			assertEquals(expectedResult, value.doubleValue().doubleValue(), 0.000001);
	}

	protected	void	rawEvaluateIntegerExpression(String sourceStream, String expression, int expectedResult) throws Exception {
		exp = compileExpression(sourceStream, expression);
		assertNotNull(exp);

		OclIntegerValue value;
		value = (OclIntegerValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
		assertEquals("Integer",  value.getType().getName());
		assertEquals(expectedResult, value.intValue());
	}

	protected	OclExpression	evaluateIntegerExpression(String sourceStream, String expression, int expectedResult, IObjectSpace before, IObjectSpace after) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);
			System.out.println("expresion = " + exp);
			OclIntegerValue value;
			value = (OclIntegerValue) oclEvaluator.evaluate(exp, evalEnv, before, after);
			assertEquals("Integer",  value.getType().getName());
			assertEquals(expectedResult, value.intValue());
			return	exp;
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
		return	null;
	}

	protected	OclExpression	evaluateExpression(String sourceStream, String expression, OclValue expectedResult, IObjectSpace before, IObjectSpace after) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);
			System.out.print("expression = " + exp);
			OclValue value;
			value =  oclEvaluator.evaluate(exp, evalEnv, before, after);
			System.out.println(" - result = " + value);
			checkResult(value, expectedResult);
			return	exp;
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
		return	null;
	}

	
	protected	void	evaluateBooleanExpression(String sourceStream, String expression, boolean expectedResult, IObjectSpace before, IObjectSpace after) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclBooleanValue value;
			value = (OclBooleanValue) oclEvaluator.evaluate(exp, evalEnv, before, after);
			assertEquals("Boolean",  value.getType().getName());
			assertEquals(expectedResult, value.booleanValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	protected	OclValue evaluateExpression(String sourceStream, String expression, IObjectSpace before) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);
			return  oclEvaluator.evaluate(exp, evalEnv, before);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
		return	null;
	}

	protected	OclValue evaluateExpression(String sourceStream, String expression, IObjectSpace before, IObjectSpace after) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);
			OclValue result = oclEvaluator.evaluate(exp, evalEnv, before, after);
			System.out.println("exp: " + exp + "  - value: " + result);
			return	result;
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
		return	null;
	}

	
	protected	void	insertObjectInEnvironment(String objectName, String className) {
		try {
			VariableDeclaration selfVariable = getFactory().createVariableDeclaration(objectName, (CoreClassifier) environment.lookup(className), null);
			environment.addElement(selfVariable.getName(), selfVariable, false);
			
		} catch (NullNameException e) {
			e.printStackTrace();
			fail();
		} catch (NameClashException e) {
			e.printStackTrace();
			fail();
		}	
	}

	protected	OclObjectValue[] createInstances(String className, int numberOfInstances) {
		CoreClassifier	classifier;
		if (className.indexOf("::") > 0)
			classifier = (CoreClassifier) environment.lookupPathName(className);
		else	
			classifier = (CoreClassifier) environment.lookup(className);
		OclObjectValue[] instances = new OclObjectValue[numberOfInstances];
		for (int i = 0; i < instances.length; i++) {
			instances[i] = objectSpace.createObject(classifier);			
		}
		return	instances;
	}

	
}
