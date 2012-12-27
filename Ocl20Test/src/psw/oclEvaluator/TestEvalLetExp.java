/*
 * Created on 07/07/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */

public class TestEvalLetExp extends TestEval{
	private	IEvalEnvironment	evalEnv;
	
	public void	testLetInteger() {
		String name = "testLetInteger";
		
		evaluateIntegerLetExpression(name, "let x : Integer = 5 in x + 10", 15);
		evaluateIntegerLetExpression(name, "let x : Integer = 5, y : Integer = 10 in x + y + 10", 25);
		evaluateIntegerLetExpression(name, "let x : Set(Integer) = Set { 1, 2, 3 }, y : Bag(Integer) = Bag { 4, 5, 6 } in x->sum() + y->size()", 6 + 3);
		evaluateIntegerLetExpression(name, "let x : Integer = 5 in let y : Integer = 10 in x + y + 10", 25);
	}

	public	void 	setUp() throws Exception {
		evalEnv = new EvalEnvironment();
		super.setUp();
	}

	protected	void	evaluateIntegerLetExpression(String sourceStream, String expression, int expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclIntegerValue value;
			value = (OclIntegerValue) oclEvaluator.evaluate(exp, evalEnv, null);
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
}
