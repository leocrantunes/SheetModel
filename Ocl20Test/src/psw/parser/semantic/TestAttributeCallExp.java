/*
 * Created on Dec 3, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;


import impl.ocl20.constraints.ExpressionInOclImpl;

import java.util.List;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.constraints.OclPostConstraint;
import ocl20.constraints.OclPrePostConstraint;
import ocl20.expressions.AssociationEndCallExp;
import ocl20.expressions.AttributeCallExp;
import ocl20.expressions.EnumLiteralExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;
import ocl20.expressions.VariableExp;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestAttributeCallExp extends TestPropertyCallExp {


	public void testAttributeCallExp_01() {
		List constraints	= doTestContextOK("context Film inv: name = name",     
				getCurrentMethodName());
	
			OclExpression oclExpression = getConstraintExpression(constraints);
			AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "name", "String");
			checkImplicitSource(attExp, "self", "Film");
	}

	public void testAttributeCallExp_02() {
		List constraints	= doTestContextOK("context Film inv: self.name = name",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "name", "String");
		checkImplicitSource(attExp, "self", "Film");
	}

	public void testDefAttributeCallExp_01() {
		doTestManyContextOK("context Film def: attrib : Integer = 10  context Film inv: self.rentalFee > attrib",     
				getCurrentMethodName());
	}


	public void testClassifierAttribute_01() {
		List constraints	= doTestContextOK("context Film inv: Rental::maxDaysToReturn > SpecialFilm::days",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
			
			assertTrue(((OperationCallExp)oclExpression).getSource() instanceof AttributeCallExp);
			AttributeCallExp exp = (AttributeCallExp) ((OperationCallExp)oclExpression).getSource();
			assertEquals("Rental", exp.getReferredAttribute().getFeatureOwner().getName());
			assertEquals("maxDaysToReturn", exp.getReferredAttribute().getName());
			assertEquals("Integer", exp.getType().getName());
	}


	public void testAttributeCall_Superclass() {
		doTestContextOK("context SpecialFilm inv: self.code = self.name",     
				getCurrentMethodName());
	}

	public void testAttributeCall_Superclass_02() {
		List constraints = doTestContextOK("context SpecialFilm inv: self.tapes[1].number > 10",     
				getCurrentMethodName());
				
		OclExpression oclExpression = getConstraintExpression(constraints);
			
		assertTrue(((OperationCallExp)oclExpression).getSource() instanceof AttributeCallExp);
		AttributeCallExp exp = (AttributeCallExp) ((OperationCallExp)oclExpression).getSource();
		assertEquals("Tape", exp.getReferredAttribute().getFeatureOwner().getName());
		assertEquals("number", exp.getReferredAttribute().getName());
		assertEquals("Integer", exp.getType().getName());
	}

	public void testClassifierAttribute_02() {
		doTestContextNotOK("context Rental inv: Rental::returned = true",     
				getCurrentMethodName());
	}

	public void testClassifierAttribute_03() {
		doTestContextNotOK("context Rental inv: Rental::return = true",     
				getCurrentMethodName());
	}

	public void testClassifierAttribute_04() {
		List constraints	= doTestContextOK("context Rental inv: self.maxDaysToReturn > 10",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "maxDaysToReturn", "Integer");
		checkImplicitSource(attExp, "self", "Rental");
	}

	public void testClassifierAttribute_05() {
		CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
		CoreOperation operation = film.lookupOperation("getTapes", null);
		assertEquals(0, operation.getSpecifications().size());

		List constraints	= doTestManyContextOK("context Film::getTapes() : Set(Tape) post: self.rentalFee@pre = 10 ",     
				getCurrentMethodName());

		film = (CoreClassifier) environment.lookup("Film");
		operation = film.lookupOperation("getTapes", null);
		assertEquals(1, operation.getSpecifications().size());
		OclPrePostConstraint	constraint = (OclPrePostConstraint) operation.getSpecifications().iterator().next();
		OclPostConstraint post = (OclPostConstraint) constraint.getPostConditions().iterator().next();

		OclExpression oclExpression = ((ExpressionInOclImpl) post.getExpression()).getBodyExpression();
		
		this.checkOperationCallExp(((OperationCallExp)oclExpression), "=", "Boolean");
		OperationCallExp  opCall = (OperationCallExp) ((OperationCallExp)oclExpression);
		AttributeCallExp attExp = checkAttributeCallExp(opCall.getSource(), "rentalFee", "Integer");
		
		opCall = (OperationCallExp) ((AttributeCallExp)opCall.getSource()).getSource();
		this.checkOperationCallExp(opCall, "atPre", "Film");
		checkImplicitSource(opCall, "self", "Film");
	}

	public void testEnumerationLiteral_01() {
		List constraints	= doTestContextOK("context Film inv: Situation::married = Situation::married",     
				getCurrentMethodName());
	
			OclExpression oclExpression = getConstraintExpression(constraints);
			
			assertTrue(((OperationCallExp)oclExpression).getSource() instanceof EnumLiteralExp);
			EnumLiteralExp exp = (EnumLiteralExp) ((OperationCallExp)oclExpression).getSource();
			assertEquals("Situation", exp.getType().getName());
			assertEquals("married", exp.getReferredEnumLiteral().getName());
	}


	public void testMoreThanOneNavigation_08() {
		List constraints	= doTestContextOK("context Tape inv: self.theFilm.name = self.theFilm.name",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		AttributeCallExp attExp = checkAttributeCallExp(((OperationCallExp)oclExpression).getSource(), "name", "String");
				
		assertTrue(attExp.getSource() instanceof AssociationEndCallExp);
		AssociationEndCallExp exp = (AssociationEndCallExp) attExp.getSource();
		assertEquals("Film", exp.getType().getName());
	}


	public void testVariableExp_01() {
		List constraints	= doTestContextOK("context Film inv: self = self",     
				getCurrentMethodName());
	
			OclExpression oclExpression = getConstraintExpression(constraints);
			
			assertTrue(((OperationCallExp)oclExpression).getSource() instanceof VariableExp);
			VariableExp varExp = (VariableExp) ((OperationCallExp)oclExpression).getSource();
			assertEquals("self", varExp.getReferredVariable().getVarName());
			assertEquals("Film", varExp.getType().getName());
			assertEquals("Film", varExp.getReferredVariable().getType().getName());
			assertNull(varExp.getReferredVariable().getInitExpression());
	}

	public void testAttributeCallExp_AttributesFromIterators_01() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->select(number > rentalFee)->size() = 0",     
				getCurrentMethodName());
	}

	public void testAttributeCallExp_AttributesFromIterators_02() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->select(t | t.number > self.rentalFee)->size() = 0",     
				getCurrentMethodName());
	}

	public void testAttributeCallExp_AttributesFromIterators_03() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->select(t | t.number > rentalFee)->size() = 0",     
				getCurrentMethodName());
	}

	public void testAttributeCallExp_AttributesFromIterators_04() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->select(t | t.number > t.theFilm.tapes->select(t2 | (t2.number > t.number) and (t2.number > self.rentalFee))->size())->size() = 0",     
				getCurrentMethodName());
	}

	public void testAttributeCallExp_AttributesFromIterators_05() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->select(t | t.number > t.theFilm.tapes->select(number > t.number and number > self.rentalFee)->size())->size() = 0",     
				getCurrentMethodName());
	}
	
	public void testInvalidAttributeCall01() {
		doTestContextNotOK("context Film inv: self.tapes->select(number > fee)->size() = 0",     
				getCurrentMethodName());
	}

	public void testInvalidAttributeCall02() {
		doTestContextNotOK("context Film inv: self.tapes->select(number1 > rentalFee)->size() = 0",     
				getCurrentMethodName());
	}
	
	public void testInvalidArrow_02() {
		doTestContextNotOK("context Film inv: self->name",     
				getCurrentMethodName());
	}

	public void testInvalidAttributeExp_01() {
		doTestManyContextNotOK("context Film inv: self.tapes.num = 10  context Film inv: self.tapes->size()",     
				getCurrentMethodName());
	}
}
