/*
 * Created on 17/05/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import ocl20.expressions.OclExpression;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestExpressionClone extends TestObjectEval {

	public void testLiterals() throws Exception {
		String	testName = "testLiteral";
		
		clone(testName, "10");
		clone(testName, "true");
		clone(testName, "\'Alex\'");
		clone(testName, "20.45");
		clone(testName, "Situation::married");
		clone(testName, "Set{1, 2, 3}");
		clone(testName, "Set{1..2}");
		clone(testName, "Bag{1..2}");
		clone(testName, "Bag{1, 2, 4, 2, 4, 1}");
		clone(testName, "Set{ Set{1, 2, 3}, Set{ 4, 5, 6} }");
		clone(testName, "Tuple{a : Integer = 10, b : Real = 20.40, c : Boolean = false, d : String = \'Alex\'}");
	}

	public void testModelPropertyExp() throws Exception {
		String	testName = "testModelPropertyExp";
		
		insertObjectInEnvironment("self", "SpecialFilm");
		clone(testName, "self.name");
		clone(testName, "self.days");
		clone(testName, "self.dist");
		clone(testName, "self.getRentalFee(1)");
	}

	public void testIteratorsExp() throws Exception {
		String	testName = "testIteratorsExp";
		
		clone(testName, "SpecialFilm::allInstances()->size()");
		clone(testName, "SpecialFilm::allInstances()->select(x | x.days > 10 and x.dist->size() < 3)");
		clone(testName, "SpecialFilm::allInstances()->iterate(x ; result : Integer = 0 | result + x.days)");
	}
	
	
	public void testOthersExp() throws Exception {
		String	testName = "testOthersExp";

		clone(testName, "if 10 > 30 then 40 else SpecialFilm::allInstances()->size() endif");
		clone(testName, "let a : Integer = 20 in a + 30 * a");
		clone(testName, "let a : Integer = 30, b : Real = 20.40 in a + b");
		clone(testName, "let a : Integer = 30 in let b : Real = 20.40 in a + b");
	}
	
	public void clone(String name, String expression) throws Exception {
		OclExpression exp = compileExpression(name, expression);
		
		OclExpression theClone = (OclExpression) exp.clone();
		assertEquals(exp.toString(), theClone.toString());
	}
	
	
	
}
