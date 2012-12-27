/*
 * Created on May 14, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import ocl20.expressions.BooleanLiteralExp;
import ocl20.expressions.IntegerLiteralExp;
import ocl20.expressions.InvalidLiteralExp;
import ocl20.expressions.NullLiteralExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.RealLiteralExp;
import ocl20.expressions.StringLiteralExp;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclInvalidValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclNullValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalPrimitiveLiterals extends TestEval {
    public void testIntegerLiteral() {
    	String	name = "testIntegerLiteral";
		doTestIntegerLiteral(name, "0", 0);
		doTestIntegerLiteral(name, "10", 10);
		doTestIntegerLiteral(name, "40000000", 40000000);
		doTestIntegerLiteral(name, "40000000000000", 40000000000000L);
    }

	public void testRealLiteral() {
		String	name = "testRealLiteral";
		doTestRealLiteral(name, "0.0", 0.0);
		doTestRealLiteral(name, "0.0004", 0.0004);
		doTestRealLiteral(name, "10.45", 10.45);
		doTestRealLiteral(name, "1234567.1234", 1234567.1234);
		doTestRealLiteral(name, "1234567.1234", 1234567.1234);
		doTestRealLiteral(name, "100.421e+2", 10042.1);
		doTestRealLiteral(name, "100.421e-2", 1.00421);
		doTestRealLiteral(name, "100.421e2", 10042.1);
	}

	public void testBooleanLiteral() {
		String	name = "testBooleanLiteral";
		doTestBooleanLiteral(name, "true", true);
		doTestBooleanLiteral(name, "false", false);
	}

	public void testStringLiteral() {
		String	name = "testStringLiteral";
		doTestStringLiteral(name, "\"true\"", "true");
		doTestStringLiteral(name, "\"false\"", "false");
		doTestStringLiteral(name, "\"\"", "");
	}

	public void testNullLiteral() {
		String	name = "testNullLiteral";
		doTestNullLiteral(name, "null" );
	}	

	public void testInvalidLiteral() {
		String	name = "testInvalidLiteral";
		doTestInvalidLiteral(name, "invalid" );
	}	

	private	void  doTestIntegerLiteral(String sourceStream, String expression, long expectedValue) {
		try {
			OclExpression literalExp = compileExpression(sourceStream, expression);
			
			assertNotNull(literalExp);
			assertTrue(literalExp instanceof IntegerLiteralExp);
			
			OclIntegerValue	value = (OclIntegerValue) oclEvaluator.evaluate(literalExp, null, null);
			assertEquals(expectedValue,  value.intValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	private	void  doTestRealLiteral(String sourceStream, String expression, double expectedValue) {
		try {
			OclExpression literalExp = compileExpression(sourceStream, expression);
			
			assertNotNull(literalExp);
			assertTrue(literalExp instanceof RealLiteralExp);
			
			OclRealValue	value = (OclRealValue) oclEvaluator.evaluate(literalExp, null, null);
			assertEquals(expectedValue,  value.doubleValue().doubleValue(), 0.0000001);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	private	void  doTestBooleanLiteral(String sourceStream, String expression, boolean expectedValue) {
		try {
			OclExpression literalExp = compileExpression(sourceStream, expression);
			
			assertNotNull(literalExp);
			assertTrue(literalExp instanceof BooleanLiteralExp);
			
			OclBooleanValue	value = (OclBooleanValue) oclEvaluator.evaluate(literalExp, null, null);
			assertEquals(expectedValue,  value.booleanValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	private	void  doTestStringLiteral(String sourceStream, String expression, String expectedValue) {
		try {
			OclExpression literalExp = compileExpression(sourceStream, expression);
			
			assertNotNull(literalExp);
			assertTrue(literalExp instanceof StringLiteralExp);
			
			OclStringValue	value = (OclStringValue) oclEvaluator.evaluate(literalExp, null, null);
			assertEquals(expectedValue,  value.stringValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
	
	private	void  doTestNullLiteral(String sourceStream, String expression) {
		try {
			OclExpression literalExp = compileExpression(sourceStream, expression);
			
			assertNotNull(literalExp);
			assertTrue(literalExp instanceof NullLiteralExp);
			
			OclNullValue	value = (OclNullValue) oclEvaluator.evaluate(literalExp, null, null);
			assertSame(getNull(),  value);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	private	void  doTestInvalidLiteral(String sourceStream, String expression) {
		try {
			OclExpression literalExp = compileExpression(sourceStream, expression);
			
			assertNotNull(literalExp);
			assertTrue(literalExp instanceof InvalidLiteralExp);
			
			OclInvalidValue	value = (OclInvalidValue) oclEvaluator.evaluate(literalExp, null, null);
			assertSame(getInvalid(),  value);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

}


