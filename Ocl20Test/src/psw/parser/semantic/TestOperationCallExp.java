/*
 * Created on Dec 3, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;


import java.util.List;

import ocl20.expressions.CollectionLiteralExp;
import ocl20.expressions.IntegerLiteralExp;
import ocl20.expressions.IteratorExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;
import ocl20.expressions.PropertyCallExp;
import ocl20.expressions.VariableExp;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestOperationCallExp extends TestPropertyCallExp {

	protected void checkOperationCallType(OclExpression oclExpression, String typeName, String operationName, Class sourceClass, String sourceType, Object[] argTypes) {
		assertTrue(oclExpression instanceof OperationCallExp);
		OperationCallExp exp = (OperationCallExp) oclExpression;
			
		assertEquals(typeName, exp.getType().getName());
		assertTrue(exp.getReferredOperation().operationNameMatches(operationName));
		
		if (sourceClass != null) {
			assertTrue(sourceClass.isAssignableFrom(exp.getSource().getClass()));
			assertEquals(exp.getSource().getType().getName(), sourceType);
		}
		else {
			assertFalse(exp.getReferredOperation().isInstanceScope());	
		}
		if (argTypes == null)
			assertEquals(0, exp.getArguments().size());
		else
			assertEquals(argTypes.length, exp.getArguments().size());	
	}



	public void testOperationCall_01() {
		List constraints	= doTestContextOK("context Film inv: getRentalFee(1) = getRentalFee(1)",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getRentalFee", "Real");
		checkImplicitSource(exp, "self", "Film");
	}

	public void testOperationCall_02() {
		doTestContextNotOK("context Film inv: setDaysForReturn(1)",     
				getCurrentMethodName());
	}

	public void testOperationCall_03() {
		List constraints	= doTestContextOK("context SpecialFilm inv: getRentalFee(1) = getRentalFee(1)",     
				getCurrentMethodName());
				
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getRentalFee", "Real");
		checkImplicitSource(exp, "self", "SpecialFilm");
	}

	public void testOperationCall_04() {
		List constraints	= doTestContextOK("context SpecialFilm inv: getTapes() = getTapes()",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getTapes", "Set(Tape)");
		checkImplicitSource(exp, "self", "SpecialFilm");
	}


	public void testOperationCall_05() {
		List constraints	= doTestContextOK("context Film inv: self.getRentalFee(1) = self.getRentalFee(1)",     
				getCurrentMethodName());
				
		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getRentalFee", "Real");
		checkImplicitSource(exp, "self", "Film");
	}

	public void testOperationCall_06() {
		List constraints	= doTestContextOK("context Tape inv: self.theFilm.getRentalFee(1) = self.theFilm.getRentalFee(1)",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		OperationCallExp exp = checkOperationCallExp(((OperationCallExp)oclExpression).getSource(), "getRentalFee", "Real");
		checkAssociationEndCallExp(exp.getSource(), "theFilm", "Film");
	}

	public void testOperationCall_07() {
		List constraints	= doTestContextOK("context Film inv: self.getTapes().number = self.getTapes().number",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(((OperationCallExp)oclExpression).getSource(), "Bag(Integer)", "collect", "Tape", "iterator");
		checkOperationCallExp(exp.getSource(), "getTapes", "Set(Tape)");
		checkAttributeCallExp(exp.getBody(), "number", "Integer");
		checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Tape");
	}		

	public void testOperationCall_08() {
		List constraints	= doTestContextOK("context Film inv: self.getTapes().theFilm = self.getTapes().theFilm",     
				getCurrentMethodName());

		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		IteratorExp exp = checkIteratorExp(oclExpression, "Bag(Film)", "collect", "Tape", "iterator");
		checkOperationCallExp(exp.getSource(), "getTapes", "Set(Tape)");
		checkAssociationEndCallExp(exp.getBody(), "theFilm", "Film");
		checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Tape");
	
	}

	public void testOperationCall_085() {
		List constraints	= doTestContextOK("context Film inv: self.getTapes().theFilm.getTapes() = self.getTapes().theFilm.getTapes()",     
				getCurrentMethodName());

		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkIteratorExp(((IteratorExp)oclExpression).getSource(), "Bag(Film)", "collect", "Tape", "iterator");
		checkIteratorExp(oclExpression, "Bag(Tape)", "collect", "Film", "iterator");
		
	}


	public void testOperationCall_09() {
		List constraints	= doTestContextOK("context Film inv: self.getTapes().theFilm.getTapes().number = self.getTapes().theFilm.getTapes().number",     
				getCurrentMethodName());

		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		IteratorExp exp = checkIteratorExp(oclExpression, "Bag(Integer)", "collect", "Tape", "iterator");
		checkIteratorExp(exp.getSource(), "Bag(Tape)", "collect", "Film", "iterator");
		checkIteratorExp(((IteratorExp)exp.getSource()).getSource(), "Bag(Film)", "collect", "Tape", "iterator");
		checkAttributeCallExp(exp.getBody(), "number", "Integer");
		checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Tape");
	}

	public void testOperationCall_10() {
			List constraints	= doTestContextOK("context Film inv: Tape::tapesQty() = Tape::tapesQty()",     
					getCurrentMethodName());

			OclExpression oclExpression = getConstraintExpression(constraints);
			checkOperationCallType(((OperationCallExp) oclExpression).getSource(), "Integer", "tapesQty", null, null, null);
	}


	public void testOperationCall_11() {
			List constraints	= doTestContextOK("context Film inv: self.oclIsKindOf(Film)",     
					getCurrentMethodName());
					
			OclExpression oclExpression = getConstraintExpression(constraints);
			checkOperationCallType(oclExpression, "Boolean", "oclIsKindOf", VariableExp.class, "Film", new Object[] { "Film" });
	}

	public void testUnaryOperation_01() {
		List constraints	= doTestContextOK("context Film inv: -10 = -10",     
				getCurrentMethodName());

		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Integer", "-", IntegerLiteralExp.class, "Integer", null);			
	}
	
	public void testUnaryOperation_02() {
		List constraints	= doTestContextOK("context Film inv: not self.oclIsKindOf(Film)",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallType(oclExpression, "Boolean", "not", OperationCallExp.class, "Boolean", null);			
	}	

	public void testMultiplicativeOperation_01() {
		List constraints	= doTestContextOK("context Film inv: 100 * 200 = 500",     
				getCurrentMethodName());

		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Integer", "*", IntegerLiteralExp.class, "Integer", new Object[] { "Integer" });			
	}

	public void testMultiplicativeOperation_02() {
		List constraints	= doTestContextOK("context Film inv: 100 * 200.50 = 200.3",     
				getCurrentMethodName());

		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Real", "*", IntegerLiteralExp.class, "Integer", new Object[] { "Real" });			
	}

	public void testMultiplicativeOperation_03() {
		System.out.println("\n multiplicative operation 03");
		List constraints	= doTestContextOK("context Film inv: -100 * -200.50 = 200.3",     
				getCurrentMethodName());

		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Real", "*", OperationCallExp.class, "Integer", new Object[] { "Real" });			
	}

	public void testAdditiveOperation_01() {
		List constraints	= doTestContextOK("context Film inv: 100 + 200  = 300",     
				getCurrentMethodName());
	
		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Integer", "+", IntegerLiteralExp.class, "Integer", new Object[] { "Integer" });			

	}

	public void testAdditiveOperation_02() {
		List constraints	= doTestContextOK("context Film inv: 100 * 300.32 + 200 * 500 / 200 = 300",     
				getCurrentMethodName());

		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Real", "+", OperationCallExp.class, "Real", new Object[] { "Integer" });			
	}

	public void testRelationalExpression_01() {
		List constraints	= doTestContextOK("context Film inv: 100 > 200",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallType(oclExpression, "Boolean", ">", IntegerLiteralExp.class, "Integer", new Object[] { "Integer" });			
	}

	public void testRelationalPrecedence_01() {
		List constraints	= doTestContextOK("context Film inv: 100 > 200 and 200 < 100 or  100 = 200 xor 90 <> 67",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallType(oclExpression, "Boolean", "xor", OperationCallExp.class, "Boolean", new Object[] { "Boolean" });			
	}

	public void testRelationalPrecedence_02() {
		List constraints	= doTestContextOK("context Film inv: 100 > 200 and 200 < 100 or  100 = 200",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallType(oclExpression, "Boolean", "or", OperationCallExp.class, "Boolean", new Object[] { "Boolean" });			
	}

	public void testRelationalPrecedence_03() {
		List constraints	= doTestContextOK("context Film inv: 100 > 200 or 200 < 100 and  100 = 200",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallType(oclExpression, "Boolean", "and", OperationCallExp.class, "Boolean", new Object[] { "Boolean" });			
	}

	public void testRelationalPrecedence_04() {
		List constraints	= doTestContextOK("context Film inv: 100 > 200 or (200 < 100 and  100 = 200)",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallType(oclExpression, "Boolean", "or", OperationCallExp.class, "Boolean", new Object[] { "Boolean" });			
	}

	public void testRelationalPrecedence_05() {
			List constraints	= doTestContextOK("context Film inv: (100 > 200 and 300 < 400) or (200 < 100 and  100 = 200)",     
					getCurrentMethodName());

			OclExpression oclExpression = getConstraintExpression(constraints);
			checkOperationCallType(oclExpression, "Boolean", "or", OperationCallExp.class, "Boolean", new Object[] { "Boolean" });			
		}


	public void testAdditivePrecedence_01() {
		List constraints	= doTestContextOK("context Film inv: 100 + 200 - 500 = 100",     
				getCurrentMethodName());
	
		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Integer", "-", OperationCallExp.class, "Integer", new Object[] { "Integer" });			

	}

	public void testAdditivePrecedence_02() {
		List constraints	= doTestContextOK("context Film inv: 100 - 200 + 500 = 100",     
				getCurrentMethodName());
	
		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Integer", "+", OperationCallExp.class, "Integer", new Object[] { "Integer" });			

	}

	public void testAdditivePrecedence_03() {
		List constraints	= doTestContextOK("context Film inv: 100 + (200 - 500) = 100",     
				getCurrentMethodName());
	
		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Integer", "+", IntegerLiteralExp.class, "Integer", new Object[] { "Integer" });			
	}


	public void testMultiplicativePrecedence_01() {
		List constraints	= doTestContextOK("context Film inv: 100 * 200 / 500 = 100",     
				getCurrentMethodName());
	
		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Real", "/", OperationCallExp.class, "Integer", new Object[] { "Real" });			

	}

	public void testMultiplicativePrecedence_02() {
		List constraints	= doTestContextOK("context Film inv: 100 / 200 * 500 = 100",     
				getCurrentMethodName());
	
		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Real", "*", OperationCallExp.class, "Real", new Object[] { "Real" });			
	}

	public void testMultiplicativePrecedence_03() {
		List constraints	= doTestContextOK("context Film inv: 100 / (200 * 500) = 100",     
				getCurrentMethodName());
	
		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Real", "/", IntegerLiteralExp.class, "Integer", new Object[] { "Real" });			
	}

	public void testSetEquals_01() {
		List constraints	= doTestContextOK("context Film inv: Set{1, 2, 3} = Set { 2, 3 }",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallType(oclExpression, "Boolean", "=", CollectionLiteralExp.class, "Set(Integer)", new Object[] { "Set(Integer)" });			
	}


	public void testSequenceEquals_01() {
		List constraints	= doTestContextOK("context Film inv: Sequence{1, 2, 3} = Sequence { 2, 3 }",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallType(oclExpression, "Boolean", "=", CollectionLiteralExp.class, "Sequence(Integer)", new Object[] { "Sequence(Integer)" });			
	}


	public void testSetDifference_01() {
		List constraints	= doTestContextOK("context Film inv: Set{1, 2, 3} - Set { 2, 3 } = Set { 1, 2 }",     
				getCurrentMethodName());
	
		OclExpression oclExpression = ((OperationCallExp) getConstraintExpression(constraints)).getSource();
		checkOperationCallType(oclExpression, "Set(Integer)", "-", CollectionLiteralExp.class, "Set(Integer)", new Object[] { "Set(Integer)" });			
	}



	public void testError_01() {
		doTestContextNotOK("context Tape inv: self.theFilm.size()",     
				getCurrentMethodName());
	}

	public void testError_02() {
		doTestContextNotOK("context context Film inv: Set{1, 2, 3} + Set { 2, 3 }",     
				getCurrentMethodName());
	}

	public void testClassOperationCallError_01() {
		doTestContextNotOK("context Film inv: Film::getRentalFee(1) = 10",     
				getCurrentMethodName());
	}

	public void testClassOperationCallError_02() {
		doTestContextNotOK("context Film inv: Film::getRentalFee1(1) = 10",     
				getCurrentMethodName());
	}

	public void testClassOperationCallError_03() {
		doTestContextNotOK("context Film inv: name.Film::getRentalFee(1) = 10",     
				getCurrentMethodName());
	}

	public void testClassOperationCall_01() {
		doTestContextOK("context Film inv: getRentalFee(Film::allInstances()->size()) = 10",     
				getCurrentMethodName());
	}

}