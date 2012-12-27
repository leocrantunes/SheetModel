/*
 * Created on 21/12/2004
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.persistence;

import java.beans.XMLDecoder;
import java.beans.XMLEncoder;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreEnumLiteral;
import ocl20.common.CoreEnumeration;
import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.MDRRepository.models.OclTypesDefinition;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.GUIDGenerator;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.IObjectSpace;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PSWObjectSpace;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PSWOclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestPSWOclEvaluator;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBagValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclEnumValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclOrderedSetValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclSequenceValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclSetValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclTupleValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValuesFactory;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.ValuesFactory;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestOclValuePersistence extends TestPSWOclEvaluator {

    
	private IObjectSpace objectSpace;

	

	/* (non-Javadoc)
	 * @see junit.framework.TestCase#setUp()
	 */
	protected void setUp()
		throws Exception {
		super.setUp();
		objectSpace = new PSWObjectSpace();
		GUIDGenerator.generateGUID();
	}


	public void testObjectValue() throws Exception {
	    long beforeCreating = System.currentTimeMillis();

		CoreClassifier cls = (CoreClassifier) environment.lookup("SpecialFilm");
		assertNotNull(cls);
		OclObjectValue obj1 = new PSWOclObjectValue(GUIDGenerator.generateGUID(), cls, objectSpace);
		assertNotNull(obj1);
		
		long beforeAttribSetting  = System.currentTimeMillis();
	    System.out.println("before defining attributes - " + (beforeAttribSetting - beforeCreating));

		obj1.setValueOf(cls.lookupAttribute("lateReturnFee"), new OclRealValue("10.45"));
		obj1.setValueOf(cls.lookupAttribute("name"), new OclStringValue("Titanic"));
		obj1.setValueOf(cls.lookupAttribute("rentalFee"), new OclIntegerValue(10));
		obj1.setValueOf(cls.lookupAttribute("days"), new OclIntegerValue(23));
		obj1.setValueOf(cls.lookupAttribute("code"), new OclStringValue("ABC123"));
		
		OclValue values[] = { obj1 };

		long beforeWriting = System.currentTimeMillis();
	    System.out.println("afterCreating - " + (beforeWriting - beforeAttribSetting));
		
	    saveValues(values, "testeObjects.xml");

	    long afterWriting = System.currentTimeMillis();
	    System.out.println("object written - " + (afterWriting - beforeWriting));
	    
		OclObjectValue objRead01 = null;
		OclValue readValues[] = readValues(1, "testeObjects.xml");
		
		long afterReading = System.currentTimeMillis();
		System.out.println("object read - " + (afterReading - afterWriting));
		
        objRead01 = (OclObjectValue) readValues[0];
        
		long afterConverting = System.currentTimeMillis();
		System.out.println("object converted -  " + (afterConverting - afterReading));

        assertNotNull(objRead01);
        assertNotNull(cls);

		assertEquals(10.45, ( (OclRealValue) objRead01.getValueOf(cls.lookupAttribute("lateReturnFee"))).doubleValue().doubleValue(), 0.00001);
		assertEquals("Titanic", ( (OclStringValue) objRead01.getValueOf(cls.lookupAttribute("name"))).stringValue());
		assertEquals(10, ( (OclIntegerValue) objRead01.getValueOf(cls.lookupAttribute("rentalFee"))).intValue());
		assertEquals(23, ( (OclIntegerValue) objRead01.getValueOf(cls.lookupAttribute("days"))).intValue());
		assertEquals("ABC123", ( (OclStringValue) objRead01.getValueOf(cls.lookupAttribute("code"))).stringValue());
		assertTrue(objRead01.equals(obj1));
		
		long afterAssertions= System.currentTimeMillis();
		System.out.println("after assertions -  " + (afterAssertions - afterConverting));
		System.out.println("total time -  " + ((afterAssertions - beforeCreating) / 1000.));

		objRead01.setValueOf(cls.lookupAttribute("lateReturnFee"), new OclRealValue("11.45"));
		objRead01.setValueOf(cls.lookupAttribute("name"), new OclStringValue("Fame"));
		objRead01.setValueOf(cls.lookupAttribute("rentalFee"), new OclIntegerValue(20));
		objRead01.setValueOf(cls.lookupAttribute("days"), new OclIntegerValue(45));
		objRead01.setValueOf(cls.lookupAttribute("code"), new OclStringValue("BDE456"));

		OclValue newvalues[] = { obj1, objRead01 };
		
	    saveValues(newvalues, "testeObjects.xml");
	}
	
	

	public	void	testPrimitiveValues() throws Exception {
		
	    OclBooleanValue		val01 = ValuesFactory.createBooleanValue(true);
	    OclBooleanValue		val02 = ValuesFactory.createBooleanValue(false);
	    OclRealValue  		real01 = ValuesFactory.createRealValue("345.67");
	    OclIntegerValue		integer01 = ValuesFactory.createIntegerValue(258);
	    OclStringValue		string01 = ValuesFactory.createStringValue("alexandre");

	    OclValue	values[] = { val01, val02, real01, integer01, string01 };
	    
	    saveValues(values, "teste.xml");
		
		OclBooleanValue obj3 = null;		
		OclBooleanValue obj4 = null;
		OclRealValue	obj5 = null;
		OclIntegerValue obj6 = null;
		OclStringValue obj7 = null;

		OclValue readValues[] = readValues(5, "teste.xml");
		
                // Read object.
	                obj3 = (OclBooleanValue) readValues[0];
	                obj4 = (OclBooleanValue) readValues[1];
	                obj5 = (OclRealValue) readValues[2];
	                obj6 = (OclIntegerValue) readValues[3];
	                obj7 = (OclStringValue) readValues[4];
	        
	        assertNotNull(obj3);
	        assertNotNull(obj4);
	        assertNotNull(obj5);
	        assertNotNull(obj6);
	        assertNotNull(obj7);
	        
	        assertEquals(obj3.booleanValue(), val01.booleanValue());
	        assertEquals(obj4.booleanValue(), val02.booleanValue());
	        assertEquals(obj5.doubleValue().doubleValue(), real01.doubleValue().doubleValue(), 0.00001);
	        assertEquals(obj6.intValue(), integer01.intValue());
	        assertEquals(obj7.stringValue(), string01.stringValue());
	        assertEquals(obj3.getClassifierName(), val01.getClassifierName());
	        assertEquals(obj4.getClassifierName(), val02.getClassifierName());
	}



	public	void	testCollectionValues() throws Exception {
		
	    int intValues[] = { 2, 3, 4, 5, 6, 7, 8, 9 };
	    int setOfSetsValues[] = { 444, 555, 666, 888, 999 };
	    
	    int bagValues[] = { 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8,  8, 9, 9 };
	    int seqValues[] = { 3, 5, 7, 9, 11, 13, 15 };
	    int ordValues[] = { 15, 13, 11, 9, 7, 5, 3 };
	    
	    OclSetValue		setValue = createIntegerSet(intValues);
	    OclSetValue		setOfSets = createSetOfSetOfIntegers(setOfSetsValues);
	    OclBagValue	    bagValue = createIntegerBag(bagValues);
	    OclSequenceValue seqValue = createIntegerSequence(seqValues);
	    OclOrderedSetValue ordValue = createIntegerOrderedSet(ordValues);
	    
	    OclValue values[] = { setValue, setOfSets, bagValue, seqValue, ordValue };

	    
        System.out.println("setValue type = " + setValue.getType().getName());
        System.out.println("setValue element type = " + setValue.getElementType().getName());

	    saveValues(values, "testeSet.xml");
		
		OclSetValue objRead01 = null;
		OclSetValue objRead02 = null;
		OclBagValue objRead03 = null;
		OclSequenceValue objRead04 = null;
		OclOrderedSetValue objRead05 = null;
		
		OclValue readValues[] = readValues(5, "testeSet.xml");
		
         // Read object.
         objRead01 = (OclSetValue) readValues[0];
         objRead02 = (OclSetValue) readValues[1];
         objRead03 = (OclBagValue) readValues[2];
         objRead04 = (OclSequenceValue) readValues[3];
         objRead05 = (OclOrderedSetValue) readValues[4];
	                
	        
	        assertNotNull(objRead01);
	        assertNotNull(objRead02);
	        assertNotNull(objRead03);
	        assertNotNull(objRead04);
	        assertNotNull(objRead05);;
	        
	        assertTrue(objRead01.getSetElements().containsAll(setValue.getElements()));
	        assertTrue(setValue.getElements().containsAll(objRead01.getElements()));
	        
	        System.out.println("objRead 01 type = " + objRead01.getType().getName());
	        System.out.println("objRead 01 element type = " + objRead01.getElementType().getName());
	        
	        assertTrue(objRead01.getElementType().conformsTo(setValue.getElementType()));
	        assertEquals(objRead01.getType().getName(), setValue.getType().getName());

			OclSetValue	resultSet1 = (OclSetValue) setOfSets.executeOperation("flatten", null);
			assertEquals( 10671, ((OclIntegerValue) resultSet1.executeOperation("sum", null)).intValue());
			
			OclSetValue	resultSet = (OclSetValue) objRead02.executeOperation("flatten", null);
			assertEquals( 10671, ((OclIntegerValue) resultSet.executeOperation("sum", null)).intValue());
			
			assertEquals(88, ((OclIntegerValue) objRead03.executeOperation("sum", null)).intValue());
			
			assertEquals(63, ((OclIntegerValue) objRead04.executeOperation("sum", null)).intValue());
			assertEquals(63, ((OclIntegerValue) objRead05.executeOperation("sum", null)).intValue());
	}
	

	public	void	testEnumValues() throws Exception {
		
	    CoreEnumeration enumeration = (CoreEnumeration) environment.lookup("Situation");
	    CoreEnumLiteral literal1 = enumeration.lookupEnumLiteral("married");
	    CoreEnumLiteral literal2 = enumeration.lookupEnumLiteral("single");
	    
	    OclEnumValue value1 = OclValuesFactory.getInstance().createEnumValue(literal1);
	    OclEnumValue value2 = OclValuesFactory.getInstance().createEnumValue(literal2);
	    
	    OclValue values[] = { value1, value2  };
	    
	    saveValues(values, "testeEnum.xml");
		
		OclEnumValue objRead01 = null;
		OclEnumValue objRead02 = null;
		
		OclValue readValues[] = readValues(2, "testeEnum.xml");
		
         // Read object.
         objRead01 = (OclEnumValue) readValues[0];
         objRead02 = (OclEnumValue) readValues[1];
	                
	        
	        assertNotNull(objRead01);
	        assertNotNull(objRead02);

	        assertEquals(objRead01, value1);
	        assertEquals(objRead02, value2);
	}

	public	void	testTupleValues() throws Exception {
		
		Object element = OclTypesDefinition.getType("PrimitiveTypes::String");
		assertNotNull(element);
		
		element = environment.lookupPathName("PrimitiveTypes::String");
		assertNotNull(element);
		
	    CoreClassifier tupleType = OclTypesFactory.createTypeFromString("Tuple(x:Integer, y:String)");	
	    assertNotNull(tupleType);
	    
	    CoreAttribute	attrX = tupleType.lookupAttribute("x");
	    CoreAttribute	attrY = tupleType.lookupAttribute("y");
	    assertNotNull(attrX);
	    assertNotNull(attrY);
	    
	    Map tupleParts = new HashMap();
	    tupleParts.put(attrX, OclValuesFactory.getInstance().createIntegerValue(10));
	    tupleParts.put(attrY, OclValuesFactory.getInstance().createStringValue("Alexandre"));
	    
	    OclTupleValue value1 = new OclTupleValue(tupleType, tupleParts);
	    
	    OclValue values[] = { value1 };
	    
	    saveValues(values, "testeTuple.xml");
		
		OclTupleValue objRead01 = null;
//		OclEnumValue objRead02 = null;
		
		OclValue readValues[] = readValues(1, "testeTuple.xml");
		
         // Read object.
         objRead01 = (OclTupleValue) readValues[0];
//         objRead02 = (OclEnumValue) readValues[1];
	                
	        
	        assertNotNull(objRead01);
//	        assertNotNull(objRead02);

	        assertEquals(objRead01, value1);
//	        assertEquals(objRead02, value2);
	}

	public void testObjectSpace() throws Exception {
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
	    
		ObjectSpacePersistenceManager.getInstance().saveObjectSpace(objectSpace, "testeObjectSpace.xml");
		IObjectSpace recoveredObjectSpace = null;
		recoveredObjectSpace = ObjectSpacePersistenceManager.getInstance().readObjectSpace("testeObjectSpace.xml");
        assertNotNull(recoveredObjectSpace);

        List personsRecovered = recoveredObjectSpace.getObjectsOfClass(personClass);
        assertEquals(1, personsRecovered.size());
        assertEquals(person.getGUID(), ((OclObjectValue) personsRecovered.get(0)).getGUID());
        
        List reservationsRecovered = recoveredObjectSpace.getObjectsOfClass((CoreClassifier) environment.lookup("Reservation"));
        assertEquals(5, reservationsRecovered.size());
        List filmsRecovered = recoveredObjectSpace.getObjectsOfClass((CoreClassifier) environment.lookup("Film"));
        assertEquals(2, filmsRecovered.size());
        List specialFilmsRecovered = recoveredObjectSpace.getObjectsOfClass((CoreClassifier) environment.lookup("SpecialFilm"));
        assertEquals(3, specialFilmsRecovered.size());

		List  linksOneR = recoveredObjectSpace.getAllLinks();
		assertEquals(11, linksOneR.size());
		
		List	linksTwoR = recoveredObjectSpace.getLinksForAssociation(personReservationAssoc);
		assertEquals(5, linksTwoR.size());

		List	linksThreeR = recoveredObjectSpace.getLinksForAssociation(filmReservationAssoc);
		assertEquals(6, linksThreeR.size());

		List	myReservationsR = recoveredObjectSpace.getLinkedObjects((OclObjectValue) personsRecovered.get(0), reservationRole);
		assertEquals(5, myReservationsR.size());
		assertEquals(reservations[0].getGUID(), ((OclObjectValue) myReservationsR.get(0)).getGUID());
		assertEquals(reservations[0].getGUID(), ((OclObjectValue) reservationsRecovered.get(0)).getGUID());
		List myPersonR = recoveredObjectSpace.getLinkedObjects((OclObjectValue) myReservationsR.get(0), personRole);
		assertEquals(1, myPersonR.size());
		assertTrue(myPersonR.contains((OclObjectValue) personsRecovered.get(0)));
		assertTrue(myReservationsR.contains((OclObjectValue) reservationsRecovered.get(0)));
		
		myReservationsR = recoveredObjectSpace.getLinkedObjects((OclObjectValue) filmsRecovered.get(0), filmReservationRole);
		assertEquals(2, myReservationsR.size());
		assertTrue(myReservationsR.contains((OclObjectValue) reservationsRecovered.get(0)));
		assertFalse(myReservationsR.contains((OclObjectValue) reservationsRecovered.get(2)));
		
		myReservationsR = recoveredObjectSpace.getLinkedObjects((OclObjectValue) filmsRecovered.get(1), filmReservationRole);
		assertEquals(2, myReservationsR.size());
		assertFalse(myReservationsR.contains((OclObjectValue) reservationsRecovered.get(0)));
		assertTrue(myReservationsR.contains((OclObjectValue) reservationsRecovered.get(2)));

		myReservationsR = recoveredObjectSpace.getLinkedObjects((OclObjectValue) specialFilmsRecovered.get(0), filmReservationRole);
		assertEquals(1, myReservationsR.size());
		assertTrue(myReservationsR.contains((OclObjectValue) reservationsRecovered.get(0)));
		assertFalse(myReservationsR.contains((OclObjectValue) reservationsRecovered.get(1)));

		List myFilmsR;
		myFilmsR = recoveredObjectSpace.getLinkedObjects((OclObjectValue) reservationsRecovered.get(0), filmRole);
		assertEquals(2, myFilmsR.size());
		assertTrue(myFilmsR.contains(filmsRecovered.get(0)));
		assertTrue(myFilmsR.contains(specialFilmsRecovered.get(0)));
		assertFalse(myFilmsR.contains(filmsRecovered.get(1)));

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

	
	
	private	OclSetValue	createIntegerSet(int[] values) {
		OclSetValue	aSet = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aSet.add(aNumber);
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aSet;
	}


	
	
	private	OclSetValue	createSetOfSetOfIntegers(int[] values) {
		OclSetValue	aSet = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType())));
		
	    for (int j = 0; j < 3; j++) {
			OclSetValue  elementSet = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType()));
	        try {
		        for (int i = 0; i < values.length; i++) {
		            OclIntegerValue aNumber = new OclIntegerValue(values[i] + j);
		            elementSet.add(aNumber);
		        } 
	        } catch (IllegalArgumentException e) {
	            fail();
	        }
	        aSet.add(elementSet);
	    }    	

		return	aSet;
	}

	private	OclBagValue	createIntegerBag(int[] values) {
		OclBagValue	aBag = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aBag.add(aNumber);
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aBag;
	}

	private	OclSequenceValue	createIntegerSequence(int[] values) {
		OclSequenceValue	aSeq = new OclSequenceValue(OclTypesFactory.createSequenceType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aSeq.add(aNumber);
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aSeq;
	}

	private	OclOrderedSetValue	createIntegerOrderedSet(int[] values) {
		OclOrderedSetValue	aSet = new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aSet.add(aNumber);
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aSet;
	}

	
	
	protected void  saveValues(OclValue values[], String fileName) throws Exception {
		     FileOutputStream fstream = new FileOutputStream(fileName);

		        try {
		            // Create XML encoder.
		            XMLEncoder ostream = new XMLEncoder(fstream);

		            try {
		                for (int i = 0; i < values.length; i++) {
			                ostream.writeObject(values[i]);
		                }
		                ostream.flush();
		            } finally {
		                // Close object stream.
		                ostream.close();
		            }
		        } finally {
		            // Close file stream.
		            fstream.close();
		        }
	}

	
	
	protected OclValue[]  readValues(int nValues, String fileName) throws Exception{
        // Create file input stream.

        OclValue values[] = new OclValue[nValues];
        
        FileInputStream finstream = new FileInputStream(fileName);
        
        try {
            // Create XML decoder.
            
            XMLDecoder istream = new XMLDecoder(finstream);

            try {
                // Read object.
                for (int i = 0; i < nValues; i++) {
                    values[i]= (OclValue) istream.readObject();
                }
            } finally {
                // Close object stream.
                istream.close();
            }
        } finally {
            // Close file stream.
            finstream.close();
        }

        return	values;
        
	}

	
}




