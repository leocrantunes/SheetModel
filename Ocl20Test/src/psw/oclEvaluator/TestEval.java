/*
 * Created on May 14, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import impl.ocl20.constraints.ExpressionInOclImpl;
import impl.ocl20.util.AstOclModelElementFactoryManager;

import java.io.PrintWriter;

import ocl20.evaluation.OclValue;
import ocl20.expressions.OclExpression;
import ocl20.util.AstOclModelElementFactory;

import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTrackerImpl;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.PSWOclCompiler;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestPSWOclEvaluator;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTExpressionInOclCS;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public abstract class TestEval extends TestPSWOclEvaluator {

	protected	static IPSWOclEvaluator	oclEvaluator;
	protected static PSWOclCompiler oclCompiler;
	protected static OclExpression exp;
	protected static CSTExpressionInOclCS  cstNode; 
	/* (non-Javadoc)
	 * @see junit.framework.TestCase#setUp()
	 */
	
	
	protected void setUp()
		throws Exception {
		super.setUp();
		oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
		oclEvaluator = new PSWOclEvaluator();
	}

	protected AstOclModelElementFactory  getFactory() {
		return AstOclModelElementFactoryManager.getInstance(model.getOclPackage());
	}

	protected OclExpression compileExpression(String sourceStream, String expression) throws Exception {
			cstNode = oclCompiler.compileOclExpression(expression,
				sourceStream, new PrintWriter(System.out));
			if (cstNode != null && cstNode.getAst() != null)
				return	((ExpressionInOclImpl) cstNode.getAst()).getBodyExpression();
			else
				return	null;
	}
	
	protected	void  evaluateIntegerResult(String sourceStream, String expression, long expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclIntegerValue value;
			value = (OclIntegerValue) oclEvaluator.evaluate(exp, null, null);
			assertEquals("Integer",  value.getType().getName());
			assertEquals(expectedResult, value.intValue());
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	
	protected	void  evaluateRealResult(String sourceStream, String expression, double expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclRealValue value;
			value = (OclRealValue) oclEvaluator.evaluate(exp, null, null);
			assertEquals("Real",  value.getType().getName());
			assertEquals(expectedResult, value.doubleValue().doubleValue(), 0.00001);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	protected	void  evaluateBooleanResult(String sourceStream, String expression, boolean expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclBooleanValue value;
			value = (OclBooleanValue) oclEvaluator.evaluate(exp, null, null);
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

	protected	void  evaluateStringResult(String sourceStream, String expression, String expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclStringValue value;
			value = (OclStringValue) oclEvaluator.evaluate(exp, null, null);
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

	protected	void  evaluateInvalidResult(String sourceStream, String expression) {
		try {
			exp = compileExpression(sourceStream, expression);
			
			assertNotNull(exp);

			OclValue value;
			value = (OclValue) oclEvaluator.evaluate(exp, null, null);
			assertTrue(value.oclIsInvalid());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	protected	void  evaluateNullResult(String sourceStream, String expression) {
		try {
			exp = compileExpression(sourceStream, expression);
			
			assertNotNull(exp);

			OclValue value;
			value = (OclValue) oclEvaluator.evaluate(exp, null, null);
			assertTrue(value.oclIsNull());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

}
