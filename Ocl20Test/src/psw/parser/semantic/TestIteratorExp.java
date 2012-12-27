/*
 * Created on Dec 20, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import java.util.Iterator;
import java.util.List;

import ocl20.expressions.AssociationClassCallExp;
import ocl20.expressions.AssociationEndCallExp;
import ocl20.expressions.AttributeCallExp;
import ocl20.expressions.IteratorExp;
import ocl20.expressions.ModelPropertyCallExp;
import ocl20.expressions.OclExpression;
import ocl20.expressions.OperationCallExp;
import ocl20.expressions.PropertyCallExp;
import ocl20.expressions.TupleLiteralExp;
import ocl20.expressions.VariableDeclaration;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestIteratorExp extends TestPropertyCallExp  {

	public void testIteratorExp_01() {
		List constraints	= doTestContextOK("context Film inv: self.tapes.theFilm = self.tapes.theFilm",     
				getCurrentMethodName());
	
			OclExpression oclExpression = getConstraintExpression(constraints);
			IteratorExp exp  = checkIteratorExp(((OperationCallExp)oclExpression).getSource(), "Bag(Film)", "collect", "Tape", "iterator");

			checkAssociationEndCallExp(exp.getBody(), "theFilm", "Film");
			checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Tape");
			
			checkAssociationEndCallExp(exp.getSource(), "tapes", "Set(Tape)");
			checkImplicitSource((PropertyCallExp) exp.getSource(), "self", "Film");
	}


	public void testIteratorExp_02() {
		List constraints	= doTestContextOK("context Rental inv: self.itens.Rental = self.itens.Rental",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp  = checkIteratorExp(((OperationCallExp)oclExpression).getSource(), "Sequence(Rental)", "collect", "RentalItem", "iterator");

		checkAssociationEndCallExp(exp.getBody(), "Rental", "Rental");
		checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "RentalItem");
			
		checkAssociationEndCallExp(exp.getSource(), "itens", "OrderedSet(RentalItem)");
		checkImplicitSource((PropertyCallExp) exp.getSource(), "self", "Rental");
	}

	public void testIteratorExp_03() {
		List constraints	= doTestContextOK("context Film inv: self.tapes.theFilm.tapes = self.tapes.theFilm.tapes",     
				getCurrentMethodName());

		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp  = checkIteratorExp(((OperationCallExp)oclExpression).getSource(), "Bag(Tape)", "collect", "Film", "iterator");

		checkAssociationEndCallExp(exp.getBody(), "tapes", "Set(Tape)");
		checkImplicitSource((PropertyCallExp) exp.getBody(), "iterator", "Film");
			
		checkIteratorExp(exp.getSource(), "Bag(Film)", "collect", "Tape", "iterator");
	}

	public void testExists_01() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->exists(number > 1)",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "iterator"); 
		
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), ">", "Boolean");
	}

	public void testExists_02() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->exists(number = 1)",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "iterator"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), "=", "Boolean");
	}


	public void testExists_03() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->exists(t | t.number = 1 and self.name = \"alex\")",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "t"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), "and", "Boolean");
	}

	public void testExists_04() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->exists(t : Tape | t.number = 1 and self.name = \"alex\")",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "t"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), "and", "Boolean");
	}

	public void testExists_05() {
		doTestContextNotOK("context Film inv: self.tapes->exists(t1 , t2 | t1.number = 1 and self.name = \"alex\")",     
				getCurrentMethodName());
	}


	public void testForAll_01() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->forAll(t1 : Tape, t2 : Tape | t1 <> t2 and t1.number = 1 and self.name = \"alex\")",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		assertTrue(oclExpression instanceof IteratorExp);
		IteratorExp exp = (IteratorExp) oclExpression;
		assertEquals("Boolean", exp.getType().getName());
		assertEquals("forAll", exp.getName());
		assertEquals(2, exp.getIterators().size());
		Iterator iter = exp.getIterators().iterator();
		VariableDeclaration v1 = (VariableDeclaration) iter.next();
		VariableDeclaration v2 = (VariableDeclaration) iter.next();
		
		assertEquals("t1", v1.getName());
		assertEquals("t2", v2.getName());
		assertEquals("Tape", v1.getType().getName());
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), "and", "Boolean");
	}

	public void testExists_06() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->exists(t : Tape | t.theFilm.tapes->forAll(t | t.number = 1 and self.name=\"alex\"))",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "t"); 
			
		assertTrue(exp.getBody() instanceof IteratorExp);
	}

	public void testExists_07() {
		List constraints	= doTestContextOK("context Film inv: " +
			"Set{ 1, 2, 3}->forAll(i | i > 0)", 	getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		checkIteratorExp(oclExpression, "Boolean", "forAll", "Integer", "i"); 
	}

	public void testExists_08() {
		doTestContextNotOK("context Film inv: self.tapes->exists(t : Film | t.name = \"alex\")",     
				getCurrentMethodName());
	}

	public void testExists_09() {
		doTestContextNotOK("context Film inv: self.tapes[1]->exists(t : Film | t.name = \"alex\")",     
				getCurrentMethodName());
	}

	public void testExists_10() {
		doTestContextNotOK("context Film inv: self.tapes->exists(t : Film = self.tapes[1] | t.number = 1)",     
				getCurrentMethodName());
	}

	public void testCollect_01() {
		List constraints	= doTestContextOK("context Film inv: self.tapes->collect(number)->notEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp( ((ModelPropertyCallExp) oclExpression).getSource(), "Bag(Integer)", "collect", "Tape", "iterator"); 
			
		assertTrue(((ModelPropertyCallExp) oclExpression).getSource() instanceof IteratorExp);
	}

	public void testCollect_02() {
		List constraints	= doTestContextOK("context Rental inv: self.itens->collect(number)->notEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp( ((ModelPropertyCallExp) oclExpression).getSource(), "Sequence(String)", "collect", "RentalItem", "iterator"); 
		System.out.println("source = " + exp.getSource().getType().getName());	
		
		
		assertTrue(((ModelPropertyCallExp) oclExpression).getSource() instanceof IteratorExp);
	}

	public void testSelect_01() { 
		List constraints	= doTestContextOK("context Film inv: self.tapes->select(t2 | number = 10)->notEmpty()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp( ((ModelPropertyCallExp) oclExpression).getSource(), "Set(Tape)", "select", "Tape", "t2"); 
			
		assertTrue(((ModelPropertyCallExp) oclExpression).getSource() instanceof IteratorExp);
	}

	public void testSelect_02() {
		doTestContextNotOK("context Film inv: self.tapes->select(t2 | t1.number = 10)->notEmpty()",     
				getCurrentMethodName());
	}

	public void testForAll_IncorrectIterators() {
		doTestContextNotOK("context Film inv: self.tapes->forAll(t1, t2 : Tape = self.tapes[1] | t1 = t2)",     
				getCurrentMethodName());
	}

	public void testNestedExpression_01() {
		doTestContextOK("context Client inv: Rental->forAll(r | r.returned = true and itens->forAll(Rental::maxDaysToReturn > Rental.Client.name.size() and number = 10 and name->notEmpty()))",     
				getCurrentMethodName());
	}
	
	public void testImplicitCollect_01() {
		List constraints	= doTestContextOK("context Client inv : self.Rental.itens->exists(i | i.number = 1)",      
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "RentalItem", "i"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), "=", "Boolean");
		
		checkIteratorExp(exp.getSource(), "Sequence(RentalItem)", "collect", "Rental", "iterator"); 
		assertTrue( ((IteratorExp)exp.getSource()).getBody() instanceof AssociationEndCallExp);
	}

	public void testImplicitCollect_02() {
		List constraints	= doTestContextOK("context Client inv : self.Rental.maxDaysToReturn->exists(x | x > 5)",      
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Integer", "x"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
		checkIteratorExp(exp.getSource(), "Bag(Integer)", "collect", "Rental", "iterator"); 
		assertTrue( ((IteratorExp)exp.getSource()).getBody() instanceof AttributeCallExp);
		
	}

	public void testImplicitCollect_03() {
		List constraints	= doTestContextOK("context Person inv: Reservation.Film.getTapes()->exists(x | x.number > 5)",      
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "x"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
		checkIteratorExp(exp.getSource(), "Sequence(Tape)", "collect", "Film", "iterator"); 
		assertTrue( ((IteratorExp)exp.getSource()).getBody() instanceof OperationCallExp);
	}

	public void testImplicitCollect_04() {
		List constraints	= doTestContextOK("context Tape inv: self.theFilm.getTapes().theFilm.getTapes()->exists( x | x.number > 5)",      
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tape", "x"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
		checkIteratorExp(exp.getSource(), "Bag(Tape)", "collect", "Film", "iterator"); 
		assertTrue( ((IteratorExp)exp.getSource()).getBody() instanceof OperationCallExp);
	}

	public void testImplicitCollect_05() {
		List constraints	= doTestContextOK("context Film inv: self.Reservation.Person->exists( x | x.age > 5)",      
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Person", "x"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
		checkIteratorExp(exp.getSource(), "Bag(Person)", "collect", "Reservation", "iterator"); 
		assertTrue( ((IteratorExp)exp.getSource()).getBody() instanceof AssociationEndCallExp);
	}

	public void testIncorrectIterator_01() { 
		doTestContextNotOK("context Film inv: self.tapes->select(t2 | number = 10)->select(t3 | t2.number = 10)->notEmpty()",     
				getCurrentMethodName());
	}

	public void testImplicitCollect_06() {
		List constraints	= doTestContextOK("context Film inv: self.Reservation.Person.EmployeeRanking[bosses]->exists( x | x.score > 5)",      
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "EmployeeRanking", "x"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
		checkIteratorExp(exp.getSource(), "Bag(EmployeeRanking)", "collect", "Person", "iterator"); 
		assertTrue( ((IteratorExp)exp.getSource()).getBody() instanceof AssociationClassCallExp);
	}

	public void testImplicitCollect_08() {
		doTestContextNotOK("context Film inv: self.Reservation.Person.EmployeeRanking->exists( x | x.score > 5)",      
				getCurrentMethodName());
	}

	public void testImplicitCollect_09() {
		doTestContextOK("context SpecialFilm inv : dist.films.dist.films.dist.Allocation->exists(x | x.abc > 10)",      
				getCurrentMethodName());
	}

	public void testImplicitCollect_07() {
		List constraints	= doTestContextOK("context Distributor  inv: self.films.doSomething(rentalFee, 10, 20.5)->size() > 5",      
				getCurrentMethodName());
	}

	public void testNestedIf_01() {
		List constraints	= doTestContextOK("context Distributor  inv: self.films->forAll(f | f.rentalFee > 10 and tapes->select(number > 10 and if number <=0 then theFilm.rentalFee > 10 else theFilm.rentalFee < 5 endif)->size() > 3 )",      
				getCurrentMethodName());
	}

	public void testCollectTuple_01() {
		List constraints	= doTestContextOK("context Film inv: self.Reservation.Person->collect(Tuple{a = age, b : Sequence(Integer) = bosses.age->asSequence()})->exists(x | x.a > 10)",      
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		IteratorExp exp = checkIteratorExp(oclExpression, "Boolean", "exists", "Tuple(a : Integer, b : Sequence(Integer))", "x"); 
			
		assertTrue(exp.getBody() instanceof OperationCallExp);
		checkOperationCallExp(exp.getBody(), ">", "Boolean");
		
		checkIteratorExp(exp.getSource(), "Bag(Tuple(a : Integer, b : Sequence(Integer)))", "collect", "Person", "iterator"); 
		assertTrue( ((IteratorExp)exp.getSource()).getBody() instanceof TupleLiteralExp);
	}
}
