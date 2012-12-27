/*
 * Created on Jul 23, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import java.util.List;

import ocl20.expressions.AssociationEndCallExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;



/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestAssociationClassCallExp extends TestAttributeCallExp {
	public void testAssocClass_01() {
		List constraints	= doTestContextOK("context SpecialFilm inv: self.dist = self.dist",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "dist", "Set(Distributor)");
	}

	public void testAssocClass_02() {
		List constraints	= doTestContextOK("context SpecialFilm inv: self.Allocation = self.Allocation",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkAssociationClassCallExp(((OperationCallExp)oclExpression).getSource(), "Allocation", "Set(Allocation)");
	}


	public void testAssocClass_03() {
		List constraints	= doTestContextOK("context Allocation inv: self.dist= self.dist",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "dist", "Distributor");
	}

	public void testAssocClass_04() {
		List constraints	= doTestContextOK("context Distributor inv: self.Allocation = self.Allocation",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkAssociationClassCallExp(((OperationCallExp)oclExpression).getSource(), "Allocation", "Set(Allocation)");
	}


	public void testAssocClass_05() {
		List constraints	= doTestContextOK("context Person inv: EmployeeRanking[self.bosses] = EmployeeRanking[self.bosses]",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkAssociationClassCallExp(((OperationCallExp)oclExpression).getSource(), "EmployeeRanking", "Set(EmployeeRanking)");
	}

	public void testAssocClass_06() {
		List constraints	= doTestContextOK("context Person inv: EmployeeRanking[bosses] = EmployeeRanking[employees]",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkAssociationClassCallExp(((OperationCallExp)oclExpression).getSource(), "EmployeeRanking", "Set(EmployeeRanking)");
	}


	public void testQualifier_01() {
		List constraints	= doTestContextOK("context Film inv: self.tapes[1] = self.tapes[2]",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp attExp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Tape");
		assertEquals(1, attExp.getQualifiers().size());
	}

	public void testQualifier_02() {
		doTestContextNotOK("context Film inv: self.tapes[true] = self.tapes[2]",     
				getCurrentMethodName());
	}

	public void testInvalidArrow_01() {
		doTestContextNotOK("context Rental inv: self.itens->rental",     
				getCurrentMethodName());
	}


}
