/*
 * Created on 11/08/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import impl.ocl20.constraints.ExpressionInOclImpl;

import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.constraints.OclBodyConstraint;
import ocl20.evaluation.OclValue;
import ocl20.expressions.OclExpression;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalOperationCallExp extends TestObjectEval {
	private	CoreClassifier personClass;
	private	CoreClassifier	filmClass;
		
	private	OclObjectValue person;
	private	OclObjectValue[] reservations;
	private	OclObjectValue[] films; 
	private	OclObjectValue[] specialFilms;

	private	CoreAssociation	personReservationAssoc;
	private	CoreAssociationEnd	personRole;
	private	CoreAssociationEnd   reservationRole;
		
	private	CoreAssociation	filmReservationAssoc;
	private	CoreAssociationEnd	filmRole;
	private	CoreAssociationEnd   filmReservationRole;

	public	void	testSimpleOperation() {
		try {
			String	sourceStream = "testOperation";
			
			oclCompiler.compileOclStream("context Film::getRentalFee(dayOfWeek : Integer) : Real 	body : dayOfWeek * self.rentalFee * 1.0", sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::doSomething(param1 : Integer, param2: Integer, param3: Real) : Integer body : param1 + param2", sourceStream + "-a", new PrintWriter(System.out));
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			evaluateRealExpression(sourceStream, "self.getRentalFee(1)", 20);
			evaluateRealExpression(sourceStream, "self.getRentalFee(2)", 40);
			evaluateIntegerExpression(sourceStream, "self.doSomething(10, 20, 30.4)", 30);

		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testRedefinedOperation() {
		try {
			String	sourceStream = "testOperation";
			
			oclCompiler.compileOclStream("context SpecialFilm def : getRentalFee(dayOfWeek : Integer) : Real = 50.0 + dayOfWeek", sourceStream + "-b", new PrintWriter(System.out));
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(10));

			OclObjectValue	aFilm = createInstanceOf("Film");
			aFilm.setValueOf("name", new OclStringValue("Legends"));
			aFilm.setValueOf("days", new OclIntegerValue(20));
			aFilm.setValueOf("code", new OclStringValue("2345"));
			aFilm.setValueOf("rentalFee", new OclIntegerValue(20));

			evalEnv.add("special", obj);
			insertObjectInEnvironment("special", "SpecialFilm");		
			evalEnv.add("aFilm", aFilm);
			insertObjectInEnvironment("aFilm", "Film");		
			
			evaluateRealExpression(sourceStream, "aFilm.getRentalFee(1)", 20);
			evaluateRealExpression(sourceStream, "aFilm.getRentalFee(2)", 40);
			evaluateRealExpression(sourceStream, "special.getRentalFee(2)", 52);
			evaluateIntegerExpression(sourceStream, "special.doSomething(10, 20, 30.4)", 30);
			evaluateRealExpression(sourceStream, "SpecialFilm::allInstances().getRentalFee(2)->sum()", 52);
			evaluateRealExpression(sourceStream, "Film::allInstances().getRentalFee(2)->sum()", 40 + 52);
			evaluateRealExpression(sourceStream, "Film::allInstances()->select(oclIsKindOf(SpecialFilm)).getRentalFee(2)->sum()", 52);
			evaluateRealExpression(sourceStream, "Film::allInstances()->select(oclIsKindOf(Film)).getRentalFee(2)->sum()", 40 + 52);
			evaluateRealExpression(sourceStream, "Film::allInstances()->select(oclIsTypeOf(Film)).getRentalFee(2)->sum()", 40);

		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}


	public	void	testDefOperation() {
		try {
			String	sourceStream = "testDefOperation";
			
			CoreClassifier classifier1 = (CoreClassifier) environment.lookup("Film");
			CoreClassifier classifier2 = (CoreClassifier) environment.lookup("SpecialFilm");
			
			oclCompiler.compileOclStream("context Film def : isGreaterThan10(a : Integer) : Boolean = a > 10", sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context Film def : myNewOperation(a : Integer) : Integer = (self.rentalFee + a) * 100", sourceStream + "-a", new PrintWriter(System.out));
			
			assertEquals(0, oclCompiler.getErrorsCount());

			{
				List param = new ArrayList();
				param.add((CoreClassifier) environment.lookup("Integer"));
				CoreOperation oper = classifier1.lookupOperation("myNewOperation", param);
				assertNotNull(oper);

				OclBodyConstraint constraint = oper.getBodyDefinition();
				if (constraint != null) {
					if (constraint.getExpression() != null) {
						OclExpression exp = ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression();
						if (exp != null) {
							System.out.println("exp = " + exp.toString());
						} else {
							System.out.println("exp = null");
						}
					} else {
						System.out.println("getExpression = null");
					}
				} else {
					System.out.println("constraint = null");
				}
				}

			
			System.out.println("compile 1");
			oclCompiler.compileOclStream("context SpecialFilm def : myNewOperation(a : Integer) : Integer = (self.rentalFee + a) * 10", sourceStream + "-b", new PrintWriter(System.out));
			System.out.println("compile 2");
			oclCompiler.compileOclStream("context SpecialFilm def : setOperation(a : Integer) : Set(Integer) = Set{a, (self.rentalFee + a) * 10}", sourceStream + "-c", new PrintWriter(System.out));
			System.out.println("compile 3");
			oclCompiler.compileOclStream("context SpecialFilm def : timesTen(x : Set(Integer)) : Set(Integer) = x->iterate(a; result : Set(Integer) = Set{} | result->including(a * 10))", sourceStream + "-d", new PrintWriter(System.out));
			System.out.println("compile 4");
			List param = new ArrayList();
			param.add((CoreClassifier) environment.lookup("Integer"));
			CoreOperation oper = classifier2.lookupOperation("myNewOperation", param);
			assertNotNull(oper);
			
			OclBodyConstraint constraint = oper.getBodyDefinition();
			if (constraint != null) {
				if (constraint.getExpression() != null) {
					OclExpression exp = ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression();
					if (exp != null) {
						System.out.println("exp = " + exp.toString());
					} else {
						System.out.println("exp = null");
					}
				} else {
					System.out.println("getExpression = null");
				}
			} else {
				System.out.println("constraint = null");
			}
				
			
			System.out.println("compile 5");
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			System.out.println("compile 6");
			
			OclObjectValue	obj2 = createInstanceOf("Film");
			obj2.setValueOf("name", new OclStringValue("AI"));
			obj2.setValueOf("days", new OclIntegerValue(10));
			obj2.setValueOf("code", new OclStringValue("1234"));
			obj2.setValueOf("rentalFee", new OclIntegerValue(20));
			
			System.out.println("compile 7");
			evalEnv.add("obj1", obj);
			evalEnv.add("obj2", obj2);
			insertObjectInEnvironment("obj1", "SpecialFilm");		
			insertObjectInEnvironment("obj2", "Film");
			
			
			System.out.println("compile 8");
			
			evaluateIntegerExpression(sourceStream, "obj1.myNewOperation(30)", 500);
			evaluateBooleanExpression(sourceStream, "obj1.isGreaterThan10(30)", true);
			evaluateBooleanExpression(sourceStream, "obj1.isGreaterThan10(5)", false);
			evaluateIntegerExpression(sourceStream, "obj1.setOperation(30)->sum()", 530);
			evaluateIntegerExpression(sourceStream, "obj2.myNewOperation(30)", 5000);
			
			OclValue val = evaluateExpression(sourceStream, "obj1.timesTen(Set{1, 2, 3})", objectSpace);
			System.out.println("val = " + val);
			
			val = evaluateExpression(sourceStream, "Set{30,10,20}->sum()", objectSpace);
			System.out.println("val = " + val);

			val = evaluateExpression(sourceStream, "obj1.timesTen(Set{1, 2, 3})->size()", objectSpace);
			System.out.println("val = " + val);
			
			evaluateIntegerExpression(sourceStream, "obj1.timesTen(Set{1, 2, 3})->sum()", 60);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
	
	
	public	void	testAllInstances() {
		String	sourceStream = "testAllInstances";
		
		createObjectsAndLinks();
		
		evaluateIntegerExpression(sourceStream, "Film::allInstances()->size()",  5);
		evaluateIntegerExpression(sourceStream, "SpecialFilm::allInstances()->size()", 3);
		evaluateIntegerExpression(sourceStream, "Reservation::allInstances()->size()", 6);
		evaluateIntegerExpression(sourceStream, "Person::allInstances()->size()", 1);
		evaluateIntegerExpression(sourceStream, "Person::allInstances()->size()", 1);
		evaluateIntegerExpression(sourceStream, "MyExample::Product::allInstances()->size()", 5);
		evaluateIntegerExpression(sourceStream, "IProduct::allInstances()->size()", 5);
		evaluateIntegerExpression(sourceStream, "Client::allInstances()->size()", 0);
		evaluateBooleanExpression(sourceStream, "IProduct::allInstances()->forAll(p | p.oclIsTypeOf(Film) or p.oclIsTypeOf(SpecialFilm))", true);
	}

	public	void	testBodyDefinedClassOperation() {
		try {
			String	sourceStream = "testBodyDefinedClassOperation";
			
			oclCompiler.compileOclStream("context Tape::tapesQty() : Integer body: 200 * 10", sourceStream, new PrintWriter(System.out));
			evaluateIntegerExpression(sourceStream, "Tape::tapesQty()",  2000);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}


	protected	void createObjectsAndLinks() {
		personClass = (CoreClassifier) environment.lookup("Person");
		filmClass = (CoreClassifier) environment.lookup("Film");
		
		person = objectSpace.createObject(personClass);
		reservations = createInstances("Reservation", 6);
		films = createInstances("Film", 2); 
		specialFilms = createInstances("SpecialFilm", 3);
		
		personReservationAssoc = personClass.lookupAssociationEnd("Reservation").getTheAssociation();
		personRole = personReservationAssoc.getAssociationEnd("Person");
		reservationRole = personReservationAssoc.getAssociationEnd("Reservation");
		
		filmReservationAssoc = filmClass.lookupAssociationEnd("Reservation").getTheAssociation();
		filmRole = filmReservationAssoc.getAssociationEnd("Film");
		filmReservationRole = filmReservationAssoc.getAssociationEnd("Reservation");
		
		for (int i = 0; i < reservations.length; i++) {
			objectSpace.createLink(personReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { person, reservations[i] } )), 
																							   new  ArrayList(Arrays.asList(new Object[] { personRole, reservationRole } )));
		}
		
		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[0], reservations[0] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[0], reservations[1] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[1], reservations[2] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[1], reservations[3] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], reservations[4] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[1], reservations[5] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));
	}


	
}
