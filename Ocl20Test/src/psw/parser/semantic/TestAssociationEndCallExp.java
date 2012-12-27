/*
 * Created on Jul 23, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */

import impl.ocl20.constraints.ExpressionInOclImpl;

import java.util.List;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.constraints.OclPostConstraint;
import ocl20.constraints.OclPrePostConstraint;
import ocl20.expressions.AssociationEndCallExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestAssociationEndCallExp extends TestPropertyCallExp {

	/* (non-Javadoc)
	* @see junit.framework.TestCase#setUp()
	*/

	public void testAssociationEndCallExp_02() {
		List constraints	= doTestContextOK("context Film inv: tapes = tapes",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Set(Tape)");
		checkImplicitSource(exp, "self", "Film");
	}

	public void testAssociationEndCallExp_01() {
		List constraints	= doTestContextOK("context Tape inv: theFilm = theFilm ",     
				getCurrentMethodName());
	
			OclExpression oclExpression = getConstraintExpression(constraints);
			AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "theFilm", "Film");
			checkImplicitSource(exp, "self", "Tape");
	}

	

	public void testAssociationEndCallExp_03() {
		List constraints	= doTestContextOK("context Rental inv: itens->size() > 0",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp) ((OperationCallExp)oclExpression).getSource()).getSource(), "itens", "OrderedSet(RentalItem)");
		checkImplicitSource(exp, "self", "Rental");
	}


	public void testAssociationEndCallExp_04() {
		List constraints	= doTestContextOK("context Tape inv: self.theFilm = theFilm",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "theFilm", "Film");
		checkImplicitSource(exp, "self", "Tape");
	
	}

	public void testAssociationEndCallExp_05() {
		List constraints	= doTestContextOK("context Film inv: self.tapes = tapes",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Set(Tape)");
		checkImplicitSource(exp, "self", "Film");
	}

	public void testAssociationEndCallExp_06() {
		List constraints	= doTestContextOK("context RentalItem inv: Rental = Rental",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "Rental", "Rental");
		checkImplicitSource(exp, "self", "RentalItem");
	}

	public void testAssociationEndCallExp_07() {
		List constraints	= doTestContextOK("context RentalItem inv: self.Rental.Client = self.Rental.Client",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "Client", "Client");
	}

	public void testAssociationEndCallExp_08() {
		List constraints	= doTestContextOK("context RentalItem inv: self.Rental.itens = self.Rental.itens",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "itens", "OrderedSet(RentalItem)");
	}

	public void testAssociationEndCallExp_09() {
		List constraints	= doTestContextOK("context SpecialFilm inv: self.tapes = tapes",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Set(Tape)");
	}

	public void testAssociationEndCallExp_10() {
		List constraints	= doTestContextOK("context SpecialFilm inv: self.tapes[1] = tapes[2]",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Tape");
	}
	
	public void testAssociationEndCallExp_11() {
		doTestContextNotOK("context SpecialFilm inv: self.tapes[true] = tapes[2]",     
				getCurrentMethodName());
	}

	public void testAssociationEndCallExp_12() {
		List constraints	= doTestManyContextOK("context Film::getTapes() : Set(Tape) post: self.tapes[1]@pre = 10 ",     
				getCurrentMethodName());

		CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
		CoreOperation operation = film.lookupOperation("getTapes", null);
		assertEquals(1, operation.getSpecifications().size());
		OclPrePostConstraint	constraint = (OclPrePostConstraint) operation.getSpecifications().iterator().next();
		OclPostConstraint post = (OclPostConstraint) constraint.getPostConditions().iterator().next();

		OclExpression oclExpression = ((ExpressionInOclImpl) post.getExpression()).getBodyExpression();
		
		this.checkOperationCallExp(((OperationCallExp)oclExpression), "=", "Boolean");
		OperationCallExp  opCall = (OperationCallExp) ((OperationCallExp)oclExpression);
		AssociationEndCallExp exp = checkAssociationEndCallExp(((OperationCallExp)oclExpression).getSource(), "tapes", "Tape");
				
		opCall = (OperationCallExp) ((AssociationEndCallExp)opCall.getSource()).getSource();
		this.checkOperationCallExp(opCall, "atPre", "Film");
		checkImplicitSource(opCall, "self", "Film");
		
	}

	public void testAssociationEndCallExp_13() {
		List constraints	= doTestManyContextOK("context Film::getTapes() : Set(Tape) post: tapes[1]@pre = 10 ",     
				getCurrentMethodName());
		
		doTestManyContextNotOK("context Film inv: tapes[1]@pre = 10 ",     
				getCurrentMethodName());
	}

	public void testAssociationEndCallExp_14() {
		doTestContextOK("context Rental inv: self.itens->select(i | i.Rental = self) = Set{}",     
				getCurrentMethodName());
		doTestContextOK("context Rental inv: self.itens->select(Rental = self) = Set{}",     
				getCurrentMethodName());
		doTestContextOK("context Rental inv: self.itens->select(Rental.itens = itens) = Set{}",     
				getCurrentMethodName());
		doTestContextNotOK("context Rental inv: self.itens->select(i | i.client = self) = Set{}",     
				getCurrentMethodName());
	}


}
