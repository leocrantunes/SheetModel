/*
 * Created on Dec 8, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import impl.ocl20.constraints.ExpressionInOclImpl;

import java.util.Iterator;
import java.util.List;

import ocl20.expressions.AttributeCallExp;
import ocl20.expressions.IfExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTExpressionInOclCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTInvariantCS;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestIfExp extends TestPropertyCallExp {

	public void testIfOK() {
		List constraints	= doTestContextOK("context Film inv: if 20 > 10 then self.name = self.name else self.name.concat(\"Alex\") = self.name endif",     
				getCurrentMethodName());
	
		for (Iterator iter = constraints.iterator(); iter.hasNext(); ) {
			CSTInvariantCS constraint = (CSTInvariantCS) iter.next();
			CSTExpressionInOclCS expression = constraint.getExpressionNodeCS();
			OclExpression oclExpression = ((ExpressionInOclImpl) expression.getAst()).getBodyExpression();
			
			assertTrue(oclExpression instanceof IfExp);
			IfExp exp = (IfExp) oclExpression;
			
			assertEquals("Boolean", exp.getType().getName());
			
			assertTrue(((OperationCallExp) exp.getThenExpression()).getSource() instanceof AttributeCallExp);
			assertTrue(((OperationCallExp) exp.getElseExpression()).getSource() instanceof OperationCallExp);
		}
	}

	public void testIfOK_02() {
		List constraints	= doTestContextOK("context Film inv: let a : OclAny = if 20 > 10 then 40 else true endif in a = 60.0",     
				getCurrentMethodName());

	}

	public void testIfError_01() {
		doTestContextNotOK("context Film inv: if 20 > 10 then 10 else self.name.concat(\"Alex\") endif",     
				getCurrentMethodName());
	}

	public void testIfError_02() {
		doTestContextNotOK("context Film inv: if  10 then 10 else self.name.concat(\"Alex\") endif",     
				getCurrentMethodName());
	}
	
	public void testIfError_03() {
		doTestContextNotOK("context Film inv: if 20 > 10 then 10 endif",     
				getCurrentMethodName());
	}

	public void testIfError_04() {
		doTestContextNotOK("context Film inv: if 20 > 10 then 10 else 20",     
				getCurrentMethodName());
	}
}
