/*
 * Created on 07/07/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import impl.ocl20.environment.NameClashException;
import impl.ocl20.environment.NullNameException;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;
import ocl20.evaluation.OclValue;
import ocl20.expressions.VariableDeclaration;
import ocl20.types.CollectionType;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclCollectionValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValuesFactory;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalAssociationEndCallExp extends TestObjectEval {
	private	CoreClassifier personClass;
	private	CoreClassifier	filmClass;
		
	private	OclObjectValue [] person;
	private	OclObjectValue[] reservations;
	private	OclObjectValue[] reservations2;
	private	OclObjectValue[] films; 
	private	OclObjectValue[] specialFilms;
	
	private 	OclObjectValue[]	a;
	private 	OclObjectValue[] b;
	private 	OclObjectValue[] c;
	private 	OclObjectValue[] d;
	private 	OclObjectValue[] e;
	private 	OclObjectValue[] f;
	private 	OclObjectValue[] g;
	private 	OclObjectValue[] h;
	private 	OclObjectValue[] i;
	private 	OclObjectValue[] j;
	private 	OclObjectValue[] k;
	private 	OclObjectValue[] l;
	private 	OclObjectValue[] m;
	private 	OclObjectValue[] n;
	private 	OclObjectValue[] o;

	private	CoreClassifier 	AClass;
	private	CoreClassifier	BClass;
	private	CoreClassifier	FClass;
	private	CoreClassifier	IClass;
	private	CoreClassifier	LClass;

	private	CoreAssociation	AB;
	private	CoreAssociationEnd AB_ARole;
	private	CoreAssociationEnd AB_BRole;
	
	private	CoreAssociation	BC;
	private	CoreAssociationEnd BC_BRole;
	private	CoreAssociationEnd BC_CRole;

	private	CoreAssociation	BD;
	private	CoreAssociationEnd BD_BRole;
	private	CoreAssociationEnd BD_DRole;

	private	CoreAssociation	BE;
	private	CoreAssociationEnd BE_BRole;
	private	CoreAssociationEnd BE_ERole;

	private	CoreAssociation	AF;
	private	CoreAssociationEnd AF_ARole;
	private	CoreAssociationEnd AF_FRole;

	private	CoreAssociation	FG;
	private	CoreAssociationEnd FG_FRole;
	private	CoreAssociationEnd FG_GRole;
	
	private	CoreAssociation	FH;
	private	CoreAssociationEnd FH_FRole;
	private	CoreAssociationEnd FH_HRole;

	private	CoreAssociation	FI;
	private	CoreAssociationEnd FI_FRole;
	private	CoreAssociationEnd FI_IRole;

	private	CoreAssociation	IJ;
	private	CoreAssociationEnd IJ_IRole;
	private	CoreAssociationEnd IJ_JRole;

	private	CoreAssociation	IK;
	private	CoreAssociationEnd IK_IRole;
	private	CoreAssociationEnd IK_KRole;

	private	CoreAssociation	AL;
	private	CoreAssociationEnd AL_ARole;
	private	CoreAssociationEnd AL_LRole;

	private	CoreAssociation	LM;
	private	CoreAssociationEnd LM_LRole;
	private	CoreAssociationEnd LM_MRole;

	private	CoreAssociation	LN;
	private	CoreAssociationEnd LN_LRole;
	private	CoreAssociationEnd LN_NRole;

	private	CoreAssociation	LO;
	private	CoreAssociationEnd LO_LRole;
	private	CoreAssociationEnd LO_ORole;

	
	private	CoreAssociation	personReservationAssoc;
	private	CoreAssociationEnd	personRole;
	private	CoreAssociationEnd   reservationRole;
		
	private	CoreAssociation	filmReservationAssoc;
	private	CoreAssociationEnd	filmRole;
	private	CoreAssociationEnd   filmReservationRole;

	public	void 	setUp() throws Exception {
		super.setUp();
		createObjectsAndLinks();
		createOtherObjectsAndLinks();
	}

	public void testNavigation() throws Exception {
		String	sourceStream = "testNavigation";
		
		evalEnv.add("self", person[0]);
		VariableDeclaration selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("Person"), null);
		environment.addElement(selfVariable.getName(), selfVariable, false);
		evalExpression(sourceStream, "self.Reservation");
		
		person[0].setValueOf("age", new OclIntegerValue(50));
		
		for (int i = 0; i < specialFilms.length; i++) {
			evalEnv.replace("self", specialFilms[i]);
			selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("SpecialFilm"), null);
			environment.removeElement(selfVariable.getName());
			environment.addElement(selfVariable.getName(), selfVariable, false);
			evalExpression(sourceStream, "self.Reservation");
			evalExpression(sourceStream, "self.Reservation.Person");
			evalExpression(sourceStream, "self.Reservation.Person.age");
			System.out.println("");
		}

		evalExpression(sourceStream, "SpecialFilm::allInstances().Reservation");
		evalExpression(sourceStream, "SpecialFilm::allInstances().Reservation.Person");
		evalExpression(sourceStream, "SpecialFilm::allInstances().Reservation.Person.age");
		System.out.println("");
		
		evalExpression(sourceStream, "Person::allInstances().Reservation.Film->size()");
		evalExpression(sourceStream, "Film::allInstances().Reservation.Person->size()");
		evalExpression(sourceStream, "Film::allInstances().Reservation.Person->asSet()->size()");
		System.out.println("");
		
		evalEnv.replace("self", films[1]);
		selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("Film"), null);
		environment.removeElement(selfVariable.getName());
		environment.addElement(selfVariable.getName(), selfVariable, false);
		evalExpression(sourceStream, "self.Reservation");
		evalExpression(sourceStream, "self.Reservation.Person");
		evalExpression(sourceStream, "self.Reservation.Person.age");
		System.out.println("");
		
		evalEnv.replace("self", films[2]);
		selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("Film"), null);
		environment.removeElement(selfVariable.getName());
		environment.addElement(selfVariable.getName(), selfVariable, false);
		evalExpression(sourceStream, "self.Reservation");
		evalExpression(sourceStream, "self.Reservation.Person");
		evalExpression(sourceStream, "self.Reservation.Person.age");
		evalExpression(sourceStream, "self.Reservation->collectNested(Person->asSet())");
		evalExpression(sourceStream, "self.Reservation->collect(Person->asSet())");
		evalExpression(sourceStream, "self.Reservation->collect(Person->asSet()).age");
		evalExpression(sourceStream, "self.Reservation->collect(Person.age)");
		System.out.println("");
		
	}
	
	
	public	void	testLinkToMany01() throws Exception {
		String	sourceStream = "testLinkToMany01";
		evalEnv.add("self", person[0]);
		
		VariableDeclaration selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("Person"), null);
		environment.addElement(selfVariable.getName(), selfVariable, false);
		evaluateCollectionExpression(sourceStream, "self.Reservation", reservations);
	}

	public	void	testLinkToMany02() throws Exception {
		String	sourceStream = "testLinkToMany02";
		evalEnv.add("self", specialFilms[0]);
		
		VariableDeclaration selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("SpecialFilm"), null);
		environment.addElement(selfVariable.getName(), selfVariable, false);
		evaluateCollectionExpression(sourceStream, "self.Reservation", new Object[] { reservations[4] });
	}

	public	void	testLinkToMany03() throws Exception {
		String	sourceStream = "testLinkToMany03";
		evalEnv.add("self", specialFilms[2]);
		
		VariableDeclaration selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("SpecialFilm"), null);
		environment.addElement(selfVariable.getName(), selfVariable, false);
		evaluateCollectionExpression(sourceStream, "self.Reservation", new Object[] {  });
	}

	public	void	testLinkToMany04() throws Exception {
		String	sourceStream = "testLinkToMany04";
		evalEnv.add("self", person[0]);
		
		VariableDeclaration selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("Person"), null);
		environment.addElement(selfVariable.getName(), selfVariable, false);
		evaluateCollectionExpression(sourceStream, "self.Reservation.Film", new Object[] { films[0], films[0], films[1], films[1], specialFilms[0], specialFilms[1] } );
	}

	public	void	testLinkToOne() {
		String	sourceStream = "testLinkToOne";
		evalEnv.add("self", reservations[0]);
		
		try {
			VariableDeclaration selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("Reservation"), null);
			environment.addElement(selfVariable.getName(), selfVariable, false);
			evaluateSingleObjectExpression(sourceStream, "self.Person", person[0]);
			
		} catch (NullNameException e) {
			e.printStackTrace();
			fail();
		} catch (NameClashException e) {
			e.printStackTrace();
			fail();
		}	
	}

	public	void	testLinkToOne_AsSet() {
		String	sourceStream = "testLinkToOne";
		evalEnv.add("self", reservations[0]);
		
		try {
			VariableDeclaration selfVariable = getFactory().createVariableDeclaration("self", (CoreClassifier) environment.lookup("Reservation"), null);
			environment.addElement(selfVariable.getName(), selfVariable, false);
			evaluateIntegerExpression(sourceStream, "self.Person->size()", 1);
			evaluateBooleanExpression(sourceStream, "self.Person->notEmpty()", true);
			
		} catch (NullNameException e) {
			e.printStackTrace();
			fail();
		} catch (NameClashException e) {
			e.printStackTrace();
			fail();
		}	
	}


	public void testNavigations() throws Exception {
		String	sourceStream = "testNavigations";
		
		evaluateBooleanExpression(sourceStream, "a1.B = b0", true);
		evaluateBooleanExpression(sourceStream, "a2.B = b1", true);
		evaluateBooleanExpression(sourceStream, "a2.B.C = c0", true);
		evaluateNullExpression(sourceStream, "a0.B");
		evaluateNullExpression(sourceStream, "a1.B.C");
		evaluateExpression(sourceStream, "a0.B.C", getInvalid(), null, objectSpace);
		
		evaluateBooleanExpression(sourceStream, "a2.B.D = Set{}", true);
		evaluateBooleanExpression(sourceStream, "a3.B = b2", true);
		evaluateBooleanExpression(sourceStream, "a3.B.D = Set{d0, d1}", true);
		evaluateExpression(sourceStream, "a0.B.D", getInvalid(), null, objectSpace);

		
		evaluateBooleanExpression(sourceStream, "a3.B.E = OrderedSet{e0,e1}", true);
		evaluateBooleanExpression(sourceStream, "a4.B.E = OrderedSet{e1, e0}", true);
		evaluateBooleanExpression(sourceStream, "a2.B.E = OrderedSet{}", true);
		evaluateExpression(sourceStream, "a0.B.E", getInvalid(), null, objectSpace);

		evaluateBooleanExpression(sourceStream, "a0.F = Set{}", true);
		evaluateBooleanExpression(sourceStream, "a1.F = Set{f0, f1}", true);
		evaluateBooleanExpression(sourceStream, "a2.F = Set{f0, f3}", true);
		evaluateBooleanExpression(sourceStream, "a3.F = Set{f3, f4}", true);
		evaluateBooleanExpression(sourceStream, "a4.F = Set{f0, f5}", true);
		evaluateBooleanExpression(sourceStream, "a0.F.G = Bag{}", true);
		evaluateBooleanExpression(sourceStream, "a1.F.G = Bag{g0, g1}", true);
		evaluateBooleanExpression(sourceStream, "a4.F.G = Bag{g0, g0}", true);
		
		evaluateIntegerExpression(sourceStream, "Bag{g0, null}->size()", 2);
		evaluateBooleanExpression(sourceStream, "a3.F.G = Bag{null, null}", true);
		evaluateBooleanExpression(sourceStream, "let v : Bag(G) = Bag{null, g0} in a2.F.G->includes(null)", true);
		evaluateBooleanExpression(sourceStream, "let v : Bag(G) = Bag{null, g0} in a2.F.G->includes(g0)", true);
		evaluateBooleanExpression(sourceStream, "let v : Bag(G) = Bag{null, g0} in a2.F.G->includesAll(Bag{g0, null})", true);
		evaluateBooleanExpression(sourceStream, "a2.F.G = Bag{null, g0}", true);
		
		evaluateBooleanExpression(sourceStream, "a0.F = Set{}", true);
		evaluateBooleanExpression(sourceStream, "a0.F.H = Bag{}", true);
		evaluateBooleanExpression(sourceStream, "a1.F.H = Bag{h0, h1, h2, h3}", true);
		evaluateBooleanExpression(sourceStream, "a2.F.H = Bag{h0, h1}", true);
		evaluateBooleanExpression(sourceStream, "a3.F.H = Bag{}", true);
		evaluateBooleanExpression(sourceStream, "a4.F.H = Bag{h0, h1, h1, h2}", true);
		
		evaluateBooleanExpression(sourceStream, "a0.F = Set{}", true);
		evaluateExpression(sourceStream, "a0.F.I", null, objectSpace);
		evaluateExpression(sourceStream, "a2.F.I", null, objectSpace);
		evaluateExpression(sourceStream, "a2.F.I.J", null, objectSpace);
		evaluateBooleanExpression(sourceStream, "a0.F.I = Sequence{}", true);
		evaluateBooleanExpression(sourceStream, "a1.F.I->includesAll(Sequence{i0, i1, i2, i3})", true);
		evaluateBooleanExpression(sourceStream, "a1.F.I.J->includesAll(Sequence{j1, j2, j3, j4, j5, j6})", true);
		evaluateBooleanExpression(sourceStream, "a2.F.I->includesAll(Sequence{i0, i1, i4}) and a2.F.I->size() = 4", true);
		evaluateBooleanExpression(sourceStream, "a2.F.I.J->includesAll(Sequence{j1, j2, j3}) and a2.F.I.J->size() = 6", true);
		evaluateBooleanExpression(sourceStream, "a3.F.I->includesAll(Sequence{i1, i4}) and a3.F.I->size() = 2", true);
		evaluateBooleanExpression(sourceStream, "a3.F.I.J->includesAll(Sequence{j1, j2, j3}) and a3.F.I.J->size() = 3", true);
		evaluateBooleanExpression(sourceStream, "a4.F.I->includesAll(Sequence{i0, i1}) and a4.F.I->size() = 4", true);
		evaluateBooleanExpression(sourceStream, "a4.F.I.J->includesAll(Sequence{j1, j2, j3}) and a4.F.I.J->size() = 6", true);
		
		 evaluateBooleanExpression(sourceStream, "a5.F.I->isEmpty()", true);
		 evaluateBooleanExpression(sourceStream, "a5.F.I.J->isEmpty()", true);
		 evaluateBooleanExpression(sourceStream, "a5.F.I = Sequence{}", true);
		 evaluateBooleanExpression(sourceStream, "a5.F.I.J = Sequence{}", true);

		 evaluateExpression(sourceStream, "a2.F.I.K", null, objectSpace);
		evaluateBooleanExpression(sourceStream, "a1.F.I.K->includesAll(Sequence{k0, k1, k2, k3})", true);
		evaluateBooleanExpression(sourceStream, "a2.F.I.K->includesAll(Sequence{k0, k1, null}) and a2.F.I.K->size() = 4", true);
		evaluateBooleanExpression(sourceStream, "a3.F.I.K->includesAll(Sequence{k1, null}) and a3.F.I.K->size() = 2", true);
		
		evaluateExpression(sourceStream, "a4.F.I", null, objectSpace);
		evaluateExpression(sourceStream, "a4.F.I.K", null, objectSpace);
		
		evaluateBooleanExpression(sourceStream, "a4.F.I.K->includesAll(Sequence{k0, k1}) and a4.F.I.K->size() = 4", true);
		evaluateBooleanExpression(sourceStream, "a5.F.I.K->isEmpty()", true);
		
		 evaluateBooleanExpression(sourceStream, "a0.L = OrderedSet{}", true);
		 evaluateBooleanExpression(sourceStream, "a1.L = OrderedSet{l1, l2}", true);
		 evaluateBooleanExpression(sourceStream, "a2.L = OrderedSet{l1, l3}", true);
		 evaluateBooleanExpression(sourceStream, "a3.L = OrderedSet{l1, l4}", true);			
		 evaluateBooleanExpression(sourceStream, "a4.L = OrderedSet{l4, l5}", true);
		 
		 evaluateBooleanExpression(sourceStream, "a0.L.M = Sequence{}", true);
		 evaluateBooleanExpression(sourceStream, "a1.L.M->includesAll(Sequence{m1, m2})", true);
		 evaluateBooleanExpression(sourceStream, "a2.L.M->includesAll(Sequence{m1}) and a2.L.M->size() = 2", true);
		 evaluateBooleanExpression(sourceStream, "a3.L.M->includesAll(Sequence{m1}) and a3.L.M->size() = 2", true);			
		 evaluateBooleanExpression(sourceStream, "a3.L.M->includesAll(Sequence{m1, null}) and a3.L.M->size() = 2", true);			
		 evaluateBooleanExpression(sourceStream, "a4.L.M->forAll(m | m.oclIsUndefined())", true);
		 
		 evaluateBooleanExpression(sourceStream, "a0.L.N = Sequence{}", true);
		 evaluateBooleanExpression(sourceStream, "a1.L.N->includesAll(Sequence{n1, n2, n3, n4})", true);
		 evaluateBooleanExpression(sourceStream, "a2.L.N->includesAll(Sequence{n1, n2, n4, n5}) and a2.L.N->size() = 5", true);
		 evaluateBooleanExpression(sourceStream, "a3.L.N->includesAll(Sequence{n1, n2}) and a3.L.N->size() = 2", true);			
		 evaluateBooleanExpression(sourceStream, "a4.L.N->isEmpty()", true);
		 
		 evaluateBooleanExpression(sourceStream, "a0.L.O = Sequence{}", true);
		 evaluateBooleanExpression(sourceStream, "a1.L.O->includesAll(Sequence{o1, o2, o3, o4})", true);
		 evaluateBooleanExpression(sourceStream, "a2.L.O->includesAll(Sequence{o1, o2, o4, o5}) and a2.L.O->size() = 5", true);
		 evaluateBooleanExpression(sourceStream, "a3.L.O->includesAll(Sequence{o1, o2}) and a3.L.O->size() = 2", true);			
		 evaluateBooleanExpression(sourceStream, "a4.L.O->isEmpty()", true);
		 
	}
	
	protected	void addVariable(String prefix, int n, OclObjectValue[] objArray) throws Exception {
		for (int i = 0; i < n; i++) {
			String varName = prefix + i;
			
			evalEnv.add(varName, objArray[i]);
			environment.addElement(varName, getFactory().createVariableDeclaration(varName, (CoreClassifier) environment.lookup(prefix.toUpperCase()), null), false);
		}
	}
	
	
	protected	OclObjectValue	createInstanceOf(String className) {
		CoreClassifier	classifier;
		if (className.indexOf("::") > 0)
			classifier = (CoreClassifier) environment.lookupPathName(className);
		else	
			classifier = (CoreClassifier) environment.lookup(className);
		return	objectSpace.createObject(classifier);
	}
	

	private	void	evalExpression(String sourceStream, String expression) throws Exception {
		exp = compileExpression(sourceStream, expression);
		assertNotNull(exp);

		OclValue value = (OclValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
		System.out.println("value = " + value);
	}

	
	protected	void	evaluateCollectionExpression(String sourceStream, String expression, Object[] expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclCollectionValue value = null;
			OclCollectionValue otherCollection = null;
			value = (OclCollectionValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
			otherCollection = OclValuesFactory.getInstance().createCollectionValue((CollectionType) value.getType()); 
			for (int i = 0; i < expectedResult.length; i++)
				otherCollection.add((OclValue) expectedResult[i]);
			List arguments = new ArrayList();
			arguments.add(otherCollection);
			assertEquals(expectedResult.length, ((OclIntegerValue) (value.executeOperation("size", null))).intValue());
			
			if (expectedResult.length > 0)
				assertTrue( ((OclBooleanValue) (value.executeOperation("includesAll", arguments))).booleanValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
	
	protected	void	evaluateSingleObjectExpression(String sourceStream, String expression, Object expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);

			OclObjectValue value = null;
			value = (OclObjectValue) oclEvaluator.evaluate(exp, evalEnv, objectSpace);
			OclObjectValue expectedObject = (OclObjectValue) expectedResult;
			assertEquals(expectedObject, value);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}


	protected	OclObjectValue[] createInstances(String className, int numberOfInstances) {
		CoreClassifier	classifier;
		if (className.indexOf("::") > 0)
			classifier = (CoreClassifier) environment.lookupPathName(className);
		else	
			classifier = (CoreClassifier) environment.lookup(className);
		OclObjectValue[] instances = new OclObjectValue[numberOfInstances];
		for (int i = 0; i < instances.length; i++) {
			instances[i] = objectSpace.createObject(classifier);			
		}
		return	instances;
	}

	protected	void createObjectsAndLinks() {
		personClass = (CoreClassifier) environment.lookup("Person");
		filmClass = (CoreClassifier) environment.lookup("Film");
		
		person = createInstances("Person", 3);
		reservations = createInstances("Reservation", 6);
		reservations2 = createInstances("Reservation", 4);
		films = createInstances("Film", 3); 
		specialFilms = createInstances("SpecialFilm", 3);
		
		personReservationAssoc = personClass.lookupAssociationEnd("Reservation").getTheAssociation();
		personRole = personReservationAssoc.getAssociationEnd("Person");
		reservationRole = personReservationAssoc.getAssociationEnd("Reservation");
		
		filmReservationAssoc = filmClass.lookupAssociationEnd("Reservation").getTheAssociation();
		filmRole = filmReservationAssoc.getAssociationEnd("Film");
		filmReservationRole = filmReservationAssoc.getAssociationEnd("Reservation");
		
		for (int i = 0; i < 6; i++) {
			objectSpace.createLink(personReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { person[0], reservations[i] } )), 
																							   new  ArrayList(Arrays.asList(new Object[] { personRole, reservationRole } )));
		}
		objectSpace.createLink(personReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { person[1], reservations2[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { personRole, reservationRole } )));
		objectSpace.createLink(personReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { person[1], reservations2[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { personRole, reservationRole } )));

		
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
		
		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[1], reservations2[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[1], reservations2[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));
		
		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[2], reservations2[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));
		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { films[2], reservations2[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));
	}

	
	protected	void createOtherObjectsAndLinks() throws Exception {
		AClass = (CoreClassifier) environment.lookup("A");
		BClass = (CoreClassifier) environment.lookup("B");
		FClass = (CoreClassifier) environment.lookup("F");
		IClass = (CoreClassifier) environment.lookup("I");
		LClass = (CoreClassifier) environment.lookup("L");
		
		a = createInstances("A", 6);
		b = createInstances("B", 4);
		c = createInstances("C", 1);
		d = createInstances("D", 2);
		e = createInstances("E", 2);
		f = createInstances("F", 6);
		g = createInstances("G", 2);
		h = createInstances("H", 4);
		i = createInstances("I", 5);
		j = createInstances("J", 7);
		k = createInstances("K", 4);
		l = createInstances("L", 6);
		m = createInstances("M", 3);
		n = createInstances("N", 6);
		o = createInstances("O", 6);

		addVariable("a", 6, a);
		addVariable("b", 4, b);
		addVariable("c", 1, c);
		addVariable("d", 2, d);
		addVariable("e", 2, e);
		addVariable("f", 6, f);
		addVariable("g", 2, g);
		addVariable("h", 4, h);
		addVariable("i", 5, i);
		addVariable("j", 7, j);
		addVariable("k", 4, k);
		addVariable("l", 6, l);
		addVariable("m", 3, m);
		addVariable("n", 6, n);
		addVariable("o", 6, o);

		AB = AClass.lookupAssociationEnd("B").getTheAssociation();
		AB_ARole = AB.getAssociationEnd("A");
		AB_BRole = AB.getAssociationEnd("B");

		objectSpace.createLink(AB,  new ArrayList(Arrays.asList(new Object[] { a[1], b[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AB_ARole, AB_BRole} )));
		objectSpace.createLink(AB,  new ArrayList(Arrays.asList(new Object[] { a[2], b[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AB_ARole, AB_BRole} )));
		objectSpace.createLink(AB,  new ArrayList(Arrays.asList(new Object[] { a[3], b[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AB_ARole, AB_BRole} )));
		objectSpace.createLink(AB,  new ArrayList(Arrays.asList(new Object[] { a[4], b[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AB_ARole, AB_BRole} )));

		BC = BClass.lookupAssociationEnd("C").getTheAssociation();
		BC_BRole = BC.getAssociationEnd("B");
		BC_CRole = BC.getAssociationEnd("C");
		
		objectSpace.createLink(BC,  new ArrayList(Arrays.asList(new Object[] { b[1], c[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { BC_BRole, BC_CRole} )));

		BD = BClass.lookupAssociationEnd("D").getTheAssociation();
		BD_BRole = BD.getAssociationEnd("B");
		BD_DRole = BD.getAssociationEnd("D");
		
		objectSpace.createLink(BD,  new ArrayList(Arrays.asList(new Object[] { b[2], d[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { BD_BRole, BD_DRole} )));
		objectSpace.createLink(BD,  new ArrayList(Arrays.asList(new Object[] { b[2], d[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { BD_BRole, BD_DRole} )));

		
		BE = BClass.lookupAssociationEnd("E").getTheAssociation();
		BE_BRole = BE.getAssociationEnd("B");
		BE_ERole = BE.getAssociationEnd("E");
		
		objectSpace.createLink(BE,  new ArrayList(Arrays.asList(new Object[] { b[2], e[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { BE_BRole, BE_ERole} )));
		objectSpace.createLink(BE,  new ArrayList(Arrays.asList(new Object[] { b[2], e[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { BE_BRole, BE_ERole} )));

		objectSpace.createLink(BE,  new ArrayList(Arrays.asList(new Object[] { b[3], e[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { BE_BRole, BE_ERole} )));
		objectSpace.createLink(BE,  new ArrayList(Arrays.asList(new Object[] { b[3], e[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { BE_BRole, BE_ERole} )));

		
		AF = AClass.lookupAssociationEnd("F").getTheAssociation();
		AF_ARole = AF.getAssociationEnd("A");
		AF_FRole = AF.getAssociationEnd("F");

		objectSpace.createLink(AF,  new ArrayList(Arrays.asList(new Object[] { a[1], f[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AF_ARole, AF_FRole} )));
		objectSpace.createLink(AF,  new ArrayList(Arrays.asList(new Object[] { a[1], f[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AF_ARole, AF_FRole} )));
		objectSpace.createLink(AF,  new ArrayList(Arrays.asList(new Object[] { a[2], f[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AF_ARole, AF_FRole} )));
		objectSpace.createLink(AF,  new ArrayList(Arrays.asList(new Object[] { a[2], f[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AF_ARole, AF_FRole} )));
		objectSpace.createLink(AF,  new ArrayList(Arrays.asList(new Object[] { a[3], f[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AF_ARole, AF_FRole} )));
		objectSpace.createLink(AF,  new ArrayList(Arrays.asList(new Object[] { a[3], f[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AF_ARole, AF_FRole} )));
		objectSpace.createLink(AF,  new ArrayList(Arrays.asList(new Object[] { a[4], f[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AF_ARole, AF_FRole} )));
		objectSpace.createLink(AF,  new ArrayList(Arrays.asList(new Object[] { a[4], f[5] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AF_ARole, AF_FRole} )));


		FG = FClass.lookupAssociationEnd("G").getTheAssociation();
		FG_FRole = FG.getAssociationEnd("F");
		FG_GRole = FG.getAssociationEnd("G");
		objectSpace.createLink(FG,  new ArrayList(Arrays.asList(new Object[] { f[0], g[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FG_FRole, FG_GRole} )));
		objectSpace.createLink(FG,  new ArrayList(Arrays.asList(new Object[] { f[1], g[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FG_FRole, FG_GRole} )));
		objectSpace.createLink(FG,  new ArrayList(Arrays.asList(new Object[] { f[5], g[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FG_FRole, FG_GRole} )));
		

		FH = FClass.lookupAssociationEnd("H").getTheAssociation();
		FH_FRole = FH.getAssociationEnd("F");
		FH_HRole = FH.getAssociationEnd("H");
		objectSpace.createLink(FH,  new ArrayList(Arrays.asList(new Object[] { f[0], h[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FH_FRole, FH_HRole} )));
		objectSpace.createLink(FH,  new ArrayList(Arrays.asList(new Object[] { f[0], h[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FH_FRole, FH_HRole} )));
		objectSpace.createLink(FH,  new ArrayList(Arrays.asList(new Object[] { f[1], h[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FH_FRole, FH_HRole} )));
		objectSpace.createLink(FH,  new ArrayList(Arrays.asList(new Object[] { f[1], h[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FH_FRole, FH_HRole} )));
		objectSpace.createLink(FH,  new ArrayList(Arrays.asList(new Object[] { f[5], h[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FH_FRole, FH_HRole} )));
		objectSpace.createLink(FH,  new ArrayList(Arrays.asList(new Object[] { f[5], h[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FH_FRole, FH_HRole} )));

		
		FI = FClass.lookupAssociationEnd("I").getTheAssociation();
		FI_FRole = FI.getAssociationEnd("F");
		FI_IRole = FI.getAssociationEnd("I");
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[0], i[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[0], i[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[1], i[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[1], i[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[3], i[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[3], i[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[5], i[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[5], i[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));

		IJ = IClass.lookupAssociationEnd("J").getTheAssociation();
		IJ_IRole = IJ.getAssociationEnd("I");
		IJ_JRole = IJ.getAssociationEnd("J");
		objectSpace.createLink(IJ,  new ArrayList(Arrays.asList(new Object[] { i[0], j[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IJ_IRole, IJ_JRole} )));
		objectSpace.createLink(IJ,  new ArrayList(Arrays.asList(new Object[] { i[1], j[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IJ_IRole, IJ_JRole} )));
		objectSpace.createLink(IJ,  new ArrayList(Arrays.asList(new Object[] { i[1], j[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IJ_IRole, IJ_JRole} )));
		objectSpace.createLink(IJ,  new ArrayList(Arrays.asList(new Object[] { i[2], j[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IJ_IRole, IJ_JRole} )));
		objectSpace.createLink(IJ,  new ArrayList(Arrays.asList(new Object[] { i[3], j[5] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IJ_IRole, IJ_JRole} )));
		objectSpace.createLink(IJ,  new ArrayList(Arrays.asList(new Object[] { i[3], j[6] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IJ_IRole, IJ_JRole} )));
		objectSpace.createLink(IJ,  new ArrayList(Arrays.asList(new Object[] { i[4], j[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IJ_IRole, IJ_JRole} )));

		
		IK = IClass.lookupAssociationEnd("K").getTheAssociation();
		IK_IRole = IK.getAssociationEnd("I");
		IK_KRole = IK.getAssociationEnd("K");
		objectSpace.createLink(IK,  new ArrayList(Arrays.asList(new Object[] { i[0], k[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IK_IRole, IK_KRole} )));
		objectSpace.createLink(IK,  new ArrayList(Arrays.asList(new Object[] { i[1], k[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IK_IRole, IK_KRole} )));
		objectSpace.createLink(IK,  new ArrayList(Arrays.asList(new Object[] { i[2], k[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IK_IRole, IK_KRole} )));
		objectSpace.createLink(IK,  new ArrayList(Arrays.asList(new Object[] { i[3], k[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { IK_IRole, IK_KRole} )));

		
		FI = FClass.lookupAssociationEnd("I").getTheAssociation();
		FI_FRole = FI.getAssociationEnd("F");
		FI_IRole = FI.getAssociationEnd("I");
		objectSpace.createLink(FI,  new ArrayList(Arrays.asList(new Object[] { f[0], i[0] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { FI_FRole, FI_IRole} )));


		AL = AClass.lookupAssociationEnd("L").getTheAssociation();
		AL_ARole = AL.getAssociationEnd("A");
		AL_LRole = AL.getAssociationEnd("L");
		objectSpace.createLink(AL,  new ArrayList(Arrays.asList(new Object[] {a[1], l[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AL_ARole, AL_LRole} )));
		objectSpace.createLink(AL,  new ArrayList(Arrays.asList(new Object[] {a[1], l[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AL_ARole, AL_LRole} )));
		objectSpace.createLink(AL,  new ArrayList(Arrays.asList(new Object[] {a[2], l[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AL_ARole, AL_LRole} )));
		objectSpace.createLink(AL,  new ArrayList(Arrays.asList(new Object[] {a[2], l[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AL_ARole, AL_LRole} )));
		objectSpace.createLink(AL,  new ArrayList(Arrays.asList(new Object[] {a[3], l[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AL_ARole, AL_LRole} )));
		objectSpace.createLink(AL,  new ArrayList(Arrays.asList(new Object[] {a[3], l[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AL_ARole, AL_LRole} )));
		objectSpace.createLink(AL,  new ArrayList(Arrays.asList(new Object[] {a[4], l[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AL_ARole, AL_LRole} )));
		objectSpace.createLink(AL,  new ArrayList(Arrays.asList(new Object[] {a[4], l[5] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { AL_ARole, AL_LRole} )));


		LM = LClass.lookupAssociationEnd("M").getTheAssociation();
		LM_LRole = LM.getAssociationEnd("L");
		LM_MRole = LM.getAssociationEnd("M");
		objectSpace.createLink(LM,  new ArrayList(Arrays.asList(new Object[] {l[1], m[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LM_LRole, LM_MRole} )));
		objectSpace.createLink(LM,  new ArrayList(Arrays.asList(new Object[] {l[2], m[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LM_LRole, LM_MRole} )));
		objectSpace.createLink(LM,  new ArrayList(Arrays.asList(new Object[] {l[3], m[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LM_LRole, LM_MRole} )));

		
		LN = LClass.lookupAssociationEnd("N").getTheAssociation();
		LN_LRole = LN.getAssociationEnd("L");
		LN_NRole = LN.getAssociationEnd("N");
		objectSpace.createLink(LN,  new ArrayList(Arrays.asList(new Object[] {l[1], n[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LN_LRole, LN_NRole} )));
		objectSpace.createLink(LN,  new ArrayList(Arrays.asList(new Object[] {l[1], n[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LN_LRole, LN_NRole} )));
		objectSpace.createLink(LN,  new ArrayList(Arrays.asList(new Object[] {l[2], n[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LN_LRole, LN_NRole} )));
		objectSpace.createLink(LN,  new ArrayList(Arrays.asList(new Object[] {l[2], n[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LN_LRole, LN_NRole} )));
		objectSpace.createLink(LN,  new ArrayList(Arrays.asList(new Object[] {l[3], n[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LN_LRole, LN_NRole} )));
		objectSpace.createLink(LN,  new ArrayList(Arrays.asList(new Object[] {l[3], n[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LN_LRole, LN_NRole} )));
		objectSpace.createLink(LN,  new ArrayList(Arrays.asList(new Object[] {l[3], n[5] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LN_LRole, LN_NRole} )));

		LO = LClass.lookupAssociationEnd("O").getTheAssociation();
		LO_LRole = LO.getAssociationEnd("L");
		LO_ORole = LO.getAssociationEnd("O");
		objectSpace.createLink(LO,  new ArrayList(Arrays.asList(new Object[] {l[1], o[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LO_LRole, LO_ORole} )));
		objectSpace.createLink(LO,  new ArrayList(Arrays.asList(new Object[] {l[1], o[2] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LO_LRole, LO_ORole} )));
		objectSpace.createLink(LO,  new ArrayList(Arrays.asList(new Object[] {l[2], o[3] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LO_LRole, LO_ORole} )));
		objectSpace.createLink(LO,  new ArrayList(Arrays.asList(new Object[] {l[2], o[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LO_LRole, LO_ORole} )));
		objectSpace.createLink(LO,  new ArrayList(Arrays.asList(new Object[] {l[3], o[1] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LO_LRole, LO_ORole} )));
		objectSpace.createLink(LO,  new ArrayList(Arrays.asList(new Object[] {l[3], o[4] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LO_LRole, LO_ORole} )));
		objectSpace.createLink(LO,  new ArrayList(Arrays.asList(new Object[] {l[3], o[5] } )), 
				   new  ArrayList(Arrays.asList(new Object[] { LO_LRole, LO_ORole} )));
		
		
		
		b[0].setValueOf("iatt", getInt(10));
		
	}

}
