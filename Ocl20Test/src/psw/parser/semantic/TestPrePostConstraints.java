/*
 * Created on Dec 26, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import java.util.ArrayList;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.constraints.OclPostConstraint;
import ocl20.constraints.OclPreConstraint;
import ocl20.constraints.OclPrePostConstraint;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestPrePostConstraints extends TestPropertyCallExp {
	public void testPrePost_01() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 post myPost: result = tapes->select(rentalFee > 10)  pre anotherPre: tapes->size() < 10",     
				getCurrentMethodName());
				
		CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
		CoreOperation operation = film.lookupOperation("getTapes", null);
		assertEquals(1, new ArrayList(operation.getSpecifications()).size());
		OclPrePostConstraint	constraint = (OclPrePostConstraint) new ArrayList(operation.getSpecifications()).iterator().next();
		assertEquals(2, constraint.getPreConditions().size());
		assertEquals("myPre", ((OclPreConstraint) new ArrayList(constraint.getPreConditions()).get(0)).getName());
		assertEquals("anotherPre", ((OclPreConstraint) new ArrayList(constraint.getPreConditions()).get(1)).getName());
		assertEquals(1, new ArrayList(constraint.getPostConditions()).size());
		assertEquals("myPost", ((OclPostConstraint) new ArrayList(constraint.getPostConditions()).iterator().next()).getName());
	}

	public void testPrePost_02() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) pre: tapes->size() > 0 pre : tapes->select(t | t.number = 1)->size() > 0 post: result = tapes->select(rentalFee > 10) post: tapes->select(rentalFee = 0) = 0",     
				getCurrentMethodName());
	}

	public void testPrePost_03() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre0: tapes->size() > 0   pre anotherPre0: tapes->size() < 10   post myPost0: result = tapes->select(rentalFee > 10)   post anotherPost0: result = tapes->select(rentalFee > 10)  " +
											 "context Film::getTapes() : Set(Tape) pre myPre1: tapes->size() > 0 pre anotherPre1: tapes->size() < 10 post myPost1: result = tapes->select(rentalFee > 10)  post anotherPost1: result = tapes->select(rentalFee > 10)  ",     
				getCurrentMethodName());
				
		CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
		CoreOperation operation = film.lookupOperation("getTapes", null);
		assertEquals(2, new ArrayList(operation.getSpecifications()).size());
		for (int i = 0; i < 2; i++) {
			OclPrePostConstraint	constraint = (OclPrePostConstraint) new ArrayList(new ArrayList(operation.getSpecifications())).get(i);
			assertEquals(2, constraint.getPreConditions().size());
			assertEquals("myPre"+i, ((OclPreConstraint) new ArrayList(constraint.getPreConditions()).get(0)).getName());
			assertEquals("anotherPre"+i, ((OclPreConstraint) new ArrayList(constraint.getPreConditions()).get(1)).getName());
			assertEquals(2, new ArrayList(constraint.getPostConditions()).size());
			assertEquals("myPost"+i, ((OclPostConstraint) new ArrayList(constraint.getPostConditions()).get(0)).getName());
			assertEquals("anotherPost"+i, ((OclPostConstraint) new ArrayList(constraint.getPostConditions()).get(1)).getName());
		}
	}

	public void testPre_01() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 ",     
				getCurrentMethodName());
				
		CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
		CoreOperation operation = film.lookupOperation("getTapes", null);
		assertEquals(1, new ArrayList(operation.getSpecifications()).size());
		OclPrePostConstraint	constraint = (OclPrePostConstraint) new ArrayList(operation.getSpecifications()).get(0);
		assertEquals(1, new ArrayList(constraint.getPreConditions()).size());
		assertEquals("myPre", ((OclPreConstraint) new ArrayList(constraint.getPreConditions()).get(0)).getName());
	}

	public void testPost_01() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) post myPost: result = tapes->select(rentalFee > 10)  ",     
				getCurrentMethodName());
				
		CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
		CoreOperation operation = film.lookupOperation("getTapes", null);
		assertEquals(1, new ArrayList(operation.getSpecifications()).size());
		OclPrePostConstraint	constraint = (OclPrePostConstraint) new ArrayList(operation.getSpecifications()).get(0);
		assertEquals(1, new ArrayList(constraint.getPostConditions()).size());
		assertEquals("myPost", ((OclPostConstraint) new ArrayList(constraint.getPostConditions()).get(0)).getName());
	}

	public void testBody_01() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) body : self.tapes",     
				getCurrentMethodName());
	}

	public void testBody_02() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 post myPost: result = tapes->select(rentalFee > 10)  pre anotherPre: tapes->size() < 10 body: self.tapes",     
				getCurrentMethodName());
	}

	public void testBody_03() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 body: self.tapes post myPost: result = tapes->select(rentalFee > 10)  pre anotherPre: tapes->size() < 10 ",     
				getCurrentMethodName());
	}

	public void testBody_04() {
		doTestManyContextOK("context Film::getRentalFee(dayOfWeek : Integer) : Real  pre: dayOfWeek > 10 body: self.tapes->size() * 10.3 + dayOfWeek post myPost: result = tapes->select(rentalFee > dayOfWeek * 10)  pre anotherPre: tapes->size() < dayOfWeek * 10 ",     
				getCurrentMethodName());
	}

	public void testPrePost_PreNotBoolean01() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() ",     
				getCurrentMethodName());
	}

	public void testPrePost_PreNotBoolean02() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 post myPost: result = tapes->select(t | t.number > 10)  pre anotherPre: tapes->size()",     
				getCurrentMethodName());
	}

	public void testPrePost_PostNotBoolean() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) pre myPre: tapes->size() > 0 post myPost: tapes->select(rentalFee > 10)  pre anotherPre: tapes->size() > 0",     
				getCurrentMethodName());
	}
	
	public void testBody_TwoBodies() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) body: self.tapes  body: self.tapes->select(rentalFee > 10)->size() > 10",     
				getCurrentMethodName());
	}	
	
	public void testBody_BodyAlreadyDefined() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) body: self.tapes  context Film::getTapes() : Set(Tape) body: self.tapes->select(rentalFee > 10)->size() > 10",     
				getCurrentMethodName());
	}	

	public void testBody_IncompatibleBodyType() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) body: self.name",     
				getCurrentMethodName());
	}	

	public void testBody_UndefinedOperation() {
		doTestManyContextNotOK("context Film::getTapes() : String  body: self.name",     
				getCurrentMethodName());
	}	

	public void testPostOfOperationWithoutReturnType() {
		doTestManyContextOK("context Film::setDaysForReturn(days : Integer)  post myPost: rentalFee + days  > 20  ",     
				getCurrentMethodName());
	}

	public void testInvalidBody_01() {
		doTestManyContextNotOK("context Film::setDaysForReturn(days : Integer)  body: rentalFee + days  > 20  ",     
				getCurrentMethodName());
	}

	public void testInvalidBody_02() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) body : 10 ",     
				getCurrentMethodName());
	}

	public void testInvalidBody_03() {
		doTestManyContextNotOK("context Tape::tapesQty() : Integer body : number + 10 ",     
				getCurrentMethodName());
	}

	public void testPrePost_DefLocalVar_01() {
		doTestManyContextOK("context Film::getTapes() : Set(Tape) \r\n" +
				"def: theSize : Integer = tapes->size() \r\n" +
				"def: abc : Integer = theSize + 10 \r\n" +
				"pre myPre: theSize > 0 \r\n" +
				"post myPost: result = tapes->select(rentalFee > 10) \r\n" +
				"pre anotherPre: theSize < 10 and abc > 20",     
				getCurrentMethodName());
				
		CoreClassifier	film = (CoreClassifier) environment.lookup("Film");
		CoreOperation operation = film.lookupOperation("getTapes", null);
		assertEquals(1, new ArrayList(operation.getSpecifications()).size());
		OclPrePostConstraint	constraint = (OclPrePostConstraint) new ArrayList(operation.getSpecifications()).iterator().next();
		assertEquals(2, constraint.getPreConditions().size());
		assertEquals("myPre", ((OclPreConstraint) new ArrayList(constraint.getPreConditions()).get(0)).getName());
		assertEquals("anotherPre", ((OclPreConstraint) new ArrayList(constraint.getPreConditions()).get(1)).getName());
		assertEquals(1, new ArrayList(constraint.getPostConditions()).size());
		assertEquals("myPost", ((OclPostConstraint) new ArrayList(constraint.getPostConditions()).iterator().next()).getName());
		assertNotNull(operation.lookupLocalVariable("theSize"));
	}

	public void testPrePost_DefLocalVar_02() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) \r\n" +
				"def: aSize : Integer = tapes->size() \r\n" +
				"def: bef : Integer = aSize + 10 \r\n" +
				"def: aSize : Integer = 30 \r\n" +
				"pre myPre: aSize > 0 \r\n" +
				"post myPost: result = tapes->select(rentalFee > 10) \r\n" +
				"pre anotherPre: aSize < 10 and bef > 20",     
				getCurrentMethodName());
	}

	public void testPrePost_DefLocalVar_03() {
		doTestManyContextNotOK("context Film::getTapes() : Set(Tape) \r\n" +
				"def: bSize : Integer = tapes->size() \r\n" +
				"def: efg : Integer = bSize + 10 \r\n" +
				"def: bSize : Integer = 30 \r\n" +
				"pre myPre: bSize > 0 \r\n" +
				"post myPost: result = tapes->select(rentalFee > 10) \r\n" +
				"pre anotherPre: bSize < 10 and efg > 20",     
				getCurrentMethodName());
	}

	public void testPrePost_DefLocalVar_04() {
		doTestManyContextNotOK("context Film::setSpecialFee(fee : Integer) \r\n" +
				"def: bSize : Integer = tapes->size() \r\n" +
				"def: fee : Integer = bSize + 10 \r\n" +
				"pre myPre: bSize > 0 \r\n" +
				"post myPost: tapes->select(rentalFee > 10)->size() > 5 \r\n" +
				"pre anotherPre: fee > 2",     
				getCurrentMethodName());
	}

}
