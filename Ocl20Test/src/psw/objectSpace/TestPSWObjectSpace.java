/*
 * Created on May 11, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.objectSpace;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationClass;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreInterface;
import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestPSWObjectSpace extends TestPSWOclEvaluator {
	IObjectSpace objectSpace;

	/* (non-Javadoc)
	 * @see junit.framework.TestCase#setUp()
	 */
	protected void setUp()
		throws Exception {
		super.setUp();
		objectSpace = new PSWObjectSpace();
	}


	public	void 	testAllInstances() {
		OclObjectValue[]	films = createInstances("Film", 10);
		createInstances("Game", 5);
		createInstances("SpecialFilm", 20);

		
		CoreClassifier filmClass = (CoreClassifier) environment.lookup("Film");
		CoreClassifier gameClass = (CoreClassifier) environment.lookup("Game");
		CoreClassifier specialClass = (CoreClassifier) environment.lookup("SpecialFilm");
		CoreClassifier productClass = (CoreClassifier) environment.lookupPathName("MyExample::Product");
		CoreInterface iProductInterface = (CoreInterface) environment.lookup("IProduct");
		CoreInterface iThingInterface = (CoreInterface) environment.lookup("IThing");
				
		assertEquals(10, objectSpace.getObjectsOfClass(filmClass).size());
		assertEquals(5, objectSpace.getObjectsOfClass(gameClass).size());
		assertEquals(20, objectSpace.getObjectsOfClass(specialClass).size());

		assertEquals(30, objectSpace.getObjectsOfType(filmClass).size());
	   assertEquals(5, objectSpace.getObjectsOfType(gameClass).size());
	   assertEquals(20, objectSpace.getObjectsOfType(specialClass).size());
	   assertEquals(35, objectSpace.getObjectsOfType(productClass).size());
	   assertEquals(35, objectSpace.getObjectsOfType(iProductInterface).size());
	   assertEquals(35, objectSpace.getObjectsOfType(iThingInterface).size());
	   assertEquals(35, objectSpace.getAllObjects().size());
	   
	   for (int i = 0; i < films.length; i++)
	   		assertEquals(films[i], objectSpace.getObjectForId(films[i].getGUID()));
	   	
	   	objectSpace.deleteObject(films[0]);
		objectSpace.deleteObject(films[1]);
	   	
		assertEquals(8, objectSpace.getObjectsOfClass(filmClass).size());
		assertEquals(33, objectSpace.getObjectsOfType(productClass).size());
		assertEquals(33, objectSpace.getAllObjects().size());
	}

	public void	testAbstractClassInstantiation() {
		try {
			createInstances("MyExample::Product", 12);
			fail();
		} catch (RuntimeException e) {
		}
	}

	public void	testAssociationClassDirectInstantiation() {
		try {
			createInstances("EmployeeRanking", 12);
			fail();
		} catch (RuntimeException e) {
		}
	}

	
	public	void	testLink() {
		CoreClassifier personClass = (CoreClassifier) environment.lookup("Person");
		CoreClassifier	filmClass = (CoreClassifier) environment.lookup("Film");
		OclObjectValue person = objectSpace.createObject(personClass);
		OclObjectValue[] reservations = createInstances("Reservation", 5);
		OclObjectValue[] films = createInstances("Film", 2); 
		OclObjectValue[] specialFilms = createInstances("SpecialFilm", 3);
		
		CoreAssociation	personReservationAssoc = personClass.lookupAssociationEnd("Reservation").getTheAssociation();
		assertNotNull(personReservationAssoc);
		CoreAssociationEnd	personRole = personReservationAssoc.getAssociationEnd("Person");
		assertNotNull(personRole);
		CoreAssociationEnd   reservationRole = personReservationAssoc.getAssociationEnd("Reservation");
		assertNotNull(reservationRole);
		
		CoreAssociation	filmReservationAssoc = filmClass.lookupAssociationEnd("Reservation").getTheAssociation();
		assertNotNull(filmReservationAssoc);
		CoreAssociationEnd	filmRole = filmReservationAssoc.getAssociationEnd("Film");
		assertNotNull(filmRole);
		CoreAssociationEnd   filmReservationRole = filmReservationAssoc.getAssociationEnd("Reservation");
		assertNotNull(filmReservationRole);
		
		assertNotNull(personRole.getTheAssociation());
		assertTrue(personRole.getTheAssociation() == personReservationAssoc);
		assertTrue(reservationRole.getTheAssociation() == personReservationAssoc);
		
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

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], reservations[0] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		objectSpace.createLink(filmReservationAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[1], reservations[1] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, filmReservationRole } )));

		List  linksOne = objectSpace.getAllLinks();
		assertEquals(11, linksOne.size());
		
		List	linksTwo = objectSpace.getLinksForAssociation(personReservationAssoc);
		assertEquals(5, linksTwo.size());

		List	linksThree = objectSpace.getLinksForAssociation(filmReservationAssoc);
		assertEquals(6, linksThree.size());

		List	myReservations = objectSpace.getLinkedObjects(person, reservationRole);
		assertEquals(5, myReservations.size());
		List myPerson = objectSpace.getLinkedObjects(reservations[0], personRole);
		assertEquals(1, myPerson.size());
		assertTrue(myPerson.contains(person));
		assertTrue(myReservations.contains(reservations[0]));
		
		myReservations = objectSpace.getLinkedObjects(films[0], filmReservationRole);
		assertEquals(2, myReservations.size());
		assertTrue(myReservations.contains(reservations[0]));
		assertFalse(myReservations.contains(reservations[2]));
		
		myReservations = objectSpace.getLinkedObjects(films[1], filmReservationRole);
		assertEquals(2, myReservations.size());
		assertFalse(myReservations.contains(reservations[0]));
		assertTrue(myReservations.contains(reservations[2]));
		
		myReservations = objectSpace.getLinkedObjects(specialFilms[0], filmReservationRole);
		assertEquals(1, myReservations.size());
		assertTrue(myReservations.contains(reservations[0]));
		assertFalse(myReservations.contains(reservations[1]));

		
		List myFilms;
		myFilms = objectSpace.getLinkedObjects(reservations[0], filmRole);
		assertEquals(2, myFilms.size());
		assertTrue(myFilms.contains(films[0]));
		assertTrue(myFilms.contains(specialFilms[0]));
		assertFalse(myFilms.contains(films[1]));
		
		objectSpace.deleteObject(reservations[0]);

		myReservations = objectSpace.getLinkedObjects(person, reservationRole);
		assertEquals(4, myReservations.size());
		
		myReservations = objectSpace.getLinkedObjects(films[0], filmReservationRole);
		assertEquals(1, myReservations.size());
		assertFalse(myReservations.contains(reservations[0]));
		assertTrue(myReservations.contains(reservations[1]));
	}



	public	void	testAssociationClassLink() {
		OclObjectValue[] distributors = createInstances("Distributor", 5);
		OclObjectValue[] specialFilms = createInstances("SpecialFilm", 3);
		CoreClassifier	specialFilmsClass = (CoreClassifier) environment.lookup("SpecialFilm");
		
		CoreAssociation	distributionAssoc = specialFilmsClass.lookupAssociationEnd("dist").getTheAssociation();
		assertNotNull(distributionAssoc);
		CoreAssociationEnd	filmRole = distributionAssoc.getAssociationEnd("films");
		assertNotNull(filmRole);
		CoreAssociationEnd	distRole = distributionAssoc.getAssociationEnd("dist");
		assertNotNull(distRole);
		CoreAssociationClass	allocationRole = specialFilmsClass.lookupAssociationClass("Allocation");
		assertNotNull(allocationRole);

		objectSpace.createLink(distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], distributors[0] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

		objectSpace.createLink(distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], distributors[1] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

		objectSpace.createLink(distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[1], distributors[1] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

		List  linksOne = objectSpace.getAllLinks();
		assertEquals(3, linksOne.size());
		
		List	linksTwo = objectSpace.getLinksForAssociation(distributionAssoc);
		assertEquals(3, linksTwo.size());

		OclObjectValue[] allocations = new OclObjectValue[5];

		allocations[0] = objectSpace.createObject((CoreAssociationClass) distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], distributors[0] } ))); 
		allocations[1] = objectSpace.createObject((CoreAssociationClass) distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], distributors[1] } )));
		allocations[2] = objectSpace.createObject((CoreAssociationClass) distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[1], distributors[1] } )));																							
		allocations[3] = objectSpace.createObject((CoreAssociationClass) distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[2], distributors[1] } )));
		
		try {
			allocations[4] = objectSpace.createObject((CoreAssociationClass) distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], distributors[0] } )));
			fail();
		} catch (IllegalArgumentException e) {
		}
		
		assertNotNull(allocations[0]);
		assertNotNull(allocations[1]);
		assertNotNull(allocations[2]);
		assertNull(allocations[3]);
		
		
		assertEquals(11, objectSpace.getAllObjects().size());
		
		List	myDistributors; 
		myDistributors = objectSpace.getLinkedObjects(specialFilms[0], distRole, (CoreAssociationClass) distributionAssoc);
		assertEquals(2, myDistributors.size());
		myDistributors = objectSpace.getLinkedObjects(specialFilms[1], distRole, (CoreAssociationClass) distributionAssoc);
		assertEquals(1, myDistributors.size());
		myDistributors = objectSpace.getLinkedObjects(specialFilms[2], distRole, (CoreAssociationClass) distributionAssoc);
		assertEquals(0, myDistributors.size());
		
		List	myFilms;
		myFilms = objectSpace.getLinkedObjects(distributors[0], filmRole, (CoreAssociationClass) distributionAssoc);
		assertEquals(1, myFilms.size());
		myFilms = objectSpace.getLinkedObjects(distributors[1], filmRole, (CoreAssociationClass) distributionAssoc);
		assertEquals(2, myFilms.size());
		
		CoreClassifier	allocationClass = (CoreClassifier) environment.lookup("Allocation");
		
		CoreAssociationEnd	assocFilmRole = allocationClass.lookupAssociationEnd("films");
		assertNotNull(assocFilmRole);
		CoreAssociationEnd	assocDistRole = allocationClass.lookupAssociationEnd("dist");
		assertNotNull(assocDistRole);

		myFilms = objectSpace.getLinkedObjects(allocations[0], assocFilmRole);
		assertEquals(1, myFilms.size());
		assertTrue(myFilms.contains(specialFilms[0]));
		
		myFilms = objectSpace.getLinkedObjects(allocations[2], assocFilmRole);
		assertEquals(1, myFilms.size());
		assertTrue(myFilms.contains(specialFilms[1]));

		myFilms = objectSpace.getLinkedObjects(allocations[2], assocDistRole);
		assertEquals(1, myFilms.size());
		assertTrue(myFilms.contains(distributors[1]));

		assertEquals(3, objectSpace.getObjectsOfClass(allocationClass).size());
		objectSpace.deleteObject(allocations[0]);
		assertEquals(2, objectSpace.getObjectsOfClass(allocationClass).size());
		
		myDistributors = objectSpace.getLinkedObjects(specialFilms[0], distRole, (CoreAssociationClass) distributionAssoc);
		assertEquals(1, myDistributors.size());
		myDistributors = objectSpace.getLinkedObjects(specialFilms[0], distRole);
		assertEquals(2, myDistributors.size());

		myFilms = objectSpace.getLinkedObjects(distributors[0], filmRole, (CoreAssociationClass) distributionAssoc);
		assertEquals(0, myFilms.size());
		myFilms = objectSpace.getLinkedObjects(distributors[0], filmRole);
		assertEquals(1, myFilms.size());
		myFilms = objectSpace.getLinkedObjects(distributors[1], filmRole, (CoreAssociationClass) distributionAssoc);
		assertEquals(2, myFilms.size());

		assertEquals(10, objectSpace.getAllObjects().size());
	}

	public	void	testClassifierScopeAttribute() {
		
		CoreClassifier	filmClass = (CoreClassifier) environment.lookup("Film");
		CoreAttribute	days_1 = (CoreAttribute) filmClass.lookupAttribute("days");
		OclValue daysValue_1 = (OclValue) objectSpace.getValueForClassifierAttribute(days_1);
		assertTrue(((OclBooleanValue) daysValue_1.executeOperation("oclIsUndefined", null)).booleanValue());

		
		OclIntegerValue	daysValueInteger;
		objectSpace.setValueForClassifierAttribute(days_1, new OclIntegerValue(10));
		daysValueInteger = (OclIntegerValue) objectSpace.getValueForClassifierAttribute(days_1);
		assertEquals(10, daysValueInteger.intValue());
	}		


	public	void	testSelfAssociation() {
		CoreClassifier personClass = (CoreClassifier) environment.lookup("Person");
		OclObjectValue[] persons = createInstances("Person", 5);
		
		CoreAssociation	personSelfAssoc = personClass.lookupAssociationEnd("bosses").getTheAssociation();
		assertNotNull(personSelfAssoc);
		CoreAssociationEnd	bossesRole = personSelfAssoc.getAssociationEnd("bosses");
		assertNotNull(bossesRole);
		CoreAssociationEnd  employeesRole = personSelfAssoc.getAssociationEnd("employees");
		assertNotNull(employeesRole);
		
		for (int i = 1; i < persons.length; i++) {
			objectSpace.createLink(personSelfAssoc,  new ArrayList(Arrays.asList(new Object[] { persons[0], persons[i] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { bossesRole, employeesRole } )));
		}
		
		List  linksOne = objectSpace.getAllLinks();
		assertEquals(4, linksOne.size());
		
		List	linksTwo = objectSpace.getLinksForAssociation(personSelfAssoc);
		assertEquals(4, linksTwo.size());

		List	myEmployees = objectSpace.getLinkedObjects(persons[0], employeesRole);
		assertEquals(4, myEmployees.size());
		List myBoss = objectSpace.getLinkedObjects(persons[1], bossesRole);
		assertEquals(1, myBoss.size());
		assertTrue(myBoss.contains(persons[0]));
	}
	

	public	OclObjectValue[] createInstances(String className, int numberOfInstances) {
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
}
