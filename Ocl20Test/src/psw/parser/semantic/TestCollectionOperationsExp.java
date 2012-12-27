/*
 * Created on Dec 18, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import java.io.PrintWriter;
import java.util.List;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTExpressionInOclCS;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.PSWOclCompiler;

import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestCollectionOperationsExp extends TestPropertyCallExp  {

	public void testSize_01() {
		List constraints	= doTestContextOK("context Tape inv: self.theFilm->size() > 10",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
		checkOperationCallExp(exp.getSource(), "asSet", "Set(Film)");
	}

	public void testSize_02() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->size() > 10",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
		checkAssociationEndCallExp(exp.getSource(), "tapes", "Set(Tape)");
	}


	public void testSize_03() {
		List constraints	= doTestContextOK("context Film inv: self.getTapes()->size() > 10",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
		checkOperationCallExp(exp.getSource(), "getTapes", "Set(Tape)");
	}

	public void testSize_04() {
		List constraints	= doTestContextOK("context Film inv: self.getTapes().theFilm->size() > 10",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
		checkIteratorExp(exp.getSource(), "Bag(Film)", "collect", "Tape", "iterator");
	}

	public void testSize_04A() {
		List constraints	= doTestContextOK("context Film inv: (self.getTapes().theFilm)->size() > 10",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
		checkIteratorExp(exp.getSource(), "Bag(Film)", "collect", "Tape", "iterator");
	}

	public void testSize_04B() {
		List constraints	= doTestContextOK("context Film inv: (self.getTapes()).theFilm->size() > 10",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
		checkIteratorExp(exp.getSource(), "Bag(Film)", "collect", "Tape", "iterator");
	}

	public void testSize_05() {
		List constraints	= doTestContextOK("context Film inv: Set{1, 2}->size() > 10",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp.getSource(), "size", "Integer");
		checkCollectionLiteralExp(exp.getSource(), "Set(Integer)");
	}

	public void testSize_06() {
		List constraints	= doTestContextOK("context Film inv: self.tapes[1]->isEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
		checkOperationCallExp(exp.getSource(), "asSet", "Set(Tape)");
	}

	public void testSize_07() {
		List constraints	= doTestContextOK("context Film inv: self.name->isEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
		checkOperationCallExp(exp.getSource(), "asSet", "Set(String)");
	}

	public void testSize_08() {
		List constraints	= doTestContextOK("context Film inv: self.name->asOrderedSet()->isEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
		checkOperationCallExp(exp.getSource(), "asOrderedSet", "OrderedSet(String)");
	}


	public void testProduct_01() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->product(self.Reservation)->product(self.tapes)->isEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
		System.out.println("type = " + exp.getSource().getType().getName());
		checkOperationCallExp(exp.getSource(), "product", "Set(Tuple(first : Tuple(first : Tape, second : Reservation), second : Tape))");
	}

	public void testProduct_02() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->product(self.Reservation)->isEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
		System.out.println("type = " + exp.getSource().getType().getName());
		checkOperationCallExp(exp.getSource(), "product", "Set(Tuple(first : Tape, second : Reservation))");
	}

	public void testProduct_03() {
		List constraints	= doTestContextOK("context Film inv: self.Reservation->product(self.tapes)->isEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp finalExp = (OperationCallExp) oclExpression;
		OperationCallExp exp = checkOperationCallExp(finalExp, "isEmpty", "Boolean");
		checkOperationCallExp(exp.getSource(), "product", "Set(Tuple(first : Reservation, second : Tape))");
	}
	public void testNotDefinedOperation_01() {
		doTestContextNotOK("context Film inv: self.getTapes()->foo()", getCurrentMethodName());	
	}
	
	public void testNotDefinedOperation_02() {
		doTestContextNotOK("context Film inv: self.getTapes()->size(1) > 0", getCurrentMethodName());	
	}

	public void testNotAOperation_01() {
		doTestContextNotOK("context Tape inv: self.theFilm->abcde", getCurrentMethodName());	
	}
	
	public void testSelect_08() throws Exception {
		oclCompiler = new PSWOclCompiler(environment, tracker);
		CSTExpressionInOclCS rootNode = oclCompiler.compileOclExpression("Film::allInstances()->select(name.toInteger() > 10)",     
				getCurrentMethodName(), new PrintWriter(System.out));
		source = getCurrentMethodName();
		assertNotNull(rootNode);
		assertEquals("Set(Film)", rootNode.getAst().getBodyExpression().getType().getName());
	}

}
