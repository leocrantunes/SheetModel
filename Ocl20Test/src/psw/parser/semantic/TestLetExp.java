/*
 * Created on Jul 23, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import impl.ocl20.constraints.ExpressionInOclImpl;

import java.util.Iterator;
import java.util.List;

import ocl20.expressions.LetExp;
import ocl20.expressions.OclExpression;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTExpressionInOclCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTInvariantCS;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestLetExp extends TestPropertyCallExp {
	public void testLetOK() {
		List constraints	= doTestContextOK("context Film inv: let x : Integer = 20 in x > 10 endif",     
				getCurrentMethodName());
	
		for (Iterator iter = constraints.iterator(); iter.hasNext(); ) {
			CSTInvariantCS constraint = (CSTInvariantCS) iter.next();
			CSTExpressionInOclCS expression = constraint.getExpressionNodeCS();
			OclExpression oclExpression = ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
			
			assertTrue(oclExpression instanceof LetExp);
			LetExp exp = (LetExp) oclExpression;
			
			assertEquals("Boolean", exp.getType().getName());
		}
	}

	public void testLetOK_02() {
		List constraints	= doTestContextOK("context Film inv: let x : Integer = 20, y : Real = 40.10 in x > 10 and y < 30.2 endif",     
				getCurrentMethodName());
	
		for (Iterator iter = constraints.iterator(); iter.hasNext(); ) {
			CSTInvariantCS constraint = (CSTInvariantCS) iter.next();
			CSTExpressionInOclCS expression = constraint.getExpressionNodeCS();
			OclExpression oclExpression = ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
			
			assertTrue(oclExpression instanceof LetExp);
			LetExp exp = (LetExp) oclExpression;
			
			assertEquals("Boolean", exp.getType().getName());
		}
	}

	public void testLetOK_03() {
		List constraints	= doTestContextOK("context Film inv: let x : Integer = 20, y : Real = 40.10 in x > 0 ",     
				getCurrentMethodName());
	
		for (Iterator iter = constraints.iterator(); iter.hasNext(); ) {
			CSTInvariantCS constraint = (CSTInvariantCS) iter.next();
			CSTExpressionInOclCS expression = constraint.getExpressionNodeCS();
			OclExpression oclExpression = ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
			
			assertTrue(oclExpression instanceof LetExp);
			LetExp exp = (LetExp) oclExpression;
			
			assertEquals("Boolean", exp.getType().getName());
		}
	}

	public void testLetOK_04() {
		List constraints	= doTestContextOK("context Film inv: if let x : Integer = 20 in x > 0 then let a : Integer = 10 in a + 20 < 4 else let b : Integer = 20 in b * 10 > 5 endif",     
				getCurrentMethodName());
	}

	public void testLetOK_05() {
		doTestContextOK("context Film inv: if let x : Integer = 20 in x > 0 then let x : Integer = 10 in x + 20 < 4 else let b : Integer = 20 in b * 10 > 5 endif",     
				getCurrentMethodName());
	}

	public void testLetOK_06() {
		doTestContextOK("context SpecialFilm def: xpto : Integer = self.doSomething(let a : Integer = 10 in a + 10, let b : Integer = 20 in b * 20, 40.42)",  
			getCurrentMethodName());
	}

	public void testLetOK_07() {
		doTestContextOK("context SpecialFilm inv:  let a : Integer = 10 in self.tapes->forAll(t | let b : Integer = 30 in t.number > (let c : Integer = 40 in a + b + c))",  
			getCurrentMethodName());
	}

	public void testLetOK_08() {
		doTestContextOK("context SpecialFilm inv:  let a : Integer = 10 in let b : Integer = 20 in a + b > 20",  
			getCurrentMethodName());
	}

	public void testLetOK_09() {
		doTestManyContextNotOK("context SpecialFilm inv:  let a : Integer = 10, b : Integer = a + 10, c : Integer = b + a in a + b > 20   inv : let x : Integer = a in x + 10 > 20",  
			getCurrentMethodName());
	}


	public void testLetNotOK_01() {
		doTestContextNotOK("context Film inv: let x : Integer = 20, y : Real = 40.10 in z > 10 and y < 30.2 endif",     
				getCurrentMethodName());
	}
	
	public void testLetNotOK_02() {
		doTestContextNotOK("context Film inv: let x = 20, y : Real = 40.10 in x > 10 and y < 30.2 endif",     
				getCurrentMethodName());
	}

	public void testLetNotOK_03() {
		doTestContextNotOK("context Film inv: let x : Integer = 20, y : Real = 40.10, x : Boolean = true in z > 10 and y < 30.2 endif",     
				getCurrentMethodName());
	}

	public void testLetNotOK_04() {
		doTestContextNotOK("context Film inv: let x : Integer = 20, y : Real = 40.10 in let x : Boolean = true in z > 10 and y < 30.2 endif",     
				getCurrentMethodName());
	}

}
