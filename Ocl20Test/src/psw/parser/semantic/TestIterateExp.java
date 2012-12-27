/*
 * Created on Dec 21, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import java.util.List;

import ocl20.expressions.IterateExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;
import ocl20.expressions.VariableDeclaration;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestIterateExp extends TestPropertyCallExp  {

	protected IterateExp	checkIterateExp(OclExpression oclExpression, String typeName, String resultName, String iteratorName, String iteratorType, String bodyType, int iteratorsQty) {
		assertTrue(oclExpression instanceof IterateExp);
		IterateExp exp = (IterateExp) oclExpression;
		assertEquals(typeName, exp.getType().getName());
		assertEquals("iterate", exp.getName());
		assertEquals(iteratorsQty, exp.getIterators().size());
		if (iteratorName == null)
			iteratorName = "iterator";
		VariableDeclaration varDecl = (VariableDeclaration) exp.getIterators().iterator().next();
		assertEquals(iteratorName, varDecl.getName());
		assertEquals(iteratorType, varDecl.getType().getName());
		assertEquals(resultName, ((VariableDeclaration) exp.getResult()).getName());
		assertEquals(typeName, ((VariableDeclaration) exp.getResult()).getType().getName());

		assertEquals(bodyType, exp.getBody().getType().getName());
		
		return	exp;
	}

	public void testIterate_01() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->iterate(total : Integer = 0 | total + number) > 5",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		assertTrue(oclExpression instanceof OperationCallExp);
		
		OperationCallExp opCallExp = (OperationCallExp) oclExpression;
		
		checkIterateExp(opCallExp.getSource(), "Integer",  "total", null, "Tape", "Integer", 1);
	}

	public void testIterate_02() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->iterate(x : Tape; total : Integer = 0 | total + x.number) > 5",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		assertTrue(oclExpression instanceof OperationCallExp);
		
		OperationCallExp opCallExp = (OperationCallExp) oclExpression;
		
		checkIterateExp(opCallExp.getSource(), "Integer",  "total", "x", "Tape", "Integer", 1);
	}

	public void testIterate_03() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->iterate(x : Tape; result : Set(Tape) = Set{} | if x.number > 5 then result->including(x) else result endif)->size() > 5",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		assertTrue(oclExpression instanceof OperationCallExp);
		
		OperationCallExp opCallExp = (OperationCallExp) oclExpression;
		
		checkIterateExp( ((OperationCallExp)(opCallExp.getSource())).getSource(), "Set(Tape)",  "result", "x", "Tape", "Set(Tape)", 1);
	}


	public void testIterateWithError_03() {
		List constraints = doTestContextOK("context Film inv: self.tapes->iterate(x : Tape, y : Tape; total : Integer = 0 | total + x.number) > 5",     
				getCurrentMethodName());
				
		OclExpression oclExpression = getConstraintExpression(constraints);
		assertTrue(oclExpression instanceof OperationCallExp);
		
		OperationCallExp opCallExp = (OperationCallExp) oclExpression;
		
		checkIterateExp(opCallExp.getSource(), "Integer",  "total", "x", "Tape", "Integer", 2);

	}

	public void testIterateWithError_04() {
		doTestContextNotOK("context Film inv: self.tapes->iterate(x : Tape; total : Integer | total + x.number) > 5",     
				getCurrentMethodName());
	}

	public void testIterateWithError_05() {
		doTestContextNotOK("context Film inv: self.tapes->iterate(x : Tape; total = 10 | total + x.number) > 5",     
				getCurrentMethodName());
	}

	public void testIterateWithError_06() {
		doTestContextNotOK("context Film inv: self.tapes->iterate(x : Tape; total : Integer = 10 | total + x.theFilm.name) > 5",     
				getCurrentMethodName());
	}

	public void testIterateWithError_07() {
		doTestContextNotOK("context Film inv: self.tapes->iterate(x : Integer = 5; total : Integer = 10 | total + x.theFilm.name) > 5",     
				getCurrentMethodName());
	}

	public void testIterateWithError_08() {
		doTestContextNotOK("context Film inv: self.tapes->iterate(x : Integer; total : Integer = 10 | total + x.theFilm.name) > 5",     
				getCurrentMethodName());
	}

	public void testIterateWithError_09() {
		doTestContextNotOK("context Film inv: self.tapes->iterate(x : Tape; total : Integer = 10 | x.theFilm) > 5",     
				getCurrentMethodName());
	}

}
