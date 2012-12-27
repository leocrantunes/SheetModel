/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml14;

import java.util.ArrayList;
import java.util.List;

import ocl20.common.CoreAssociationClass;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;




/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml14CoreAssociationClass extends TestUml14CoreModelElement {

	public TestUml14CoreAssociationClass(String arg0) throws Exception {
		super(arg0);
	}
	
	public void testSelfAssociation() {
		CoreAssociationClass employeeRanking = (CoreAssociationClass) getClassifier(null, "EmployeeRanking");
		
		assertNotNull(getModelElement(employeeRanking.getClassifierFeatures(), "score", CoreAttribute.class));
		
		List assocEnds = new ArrayList(employeeRanking.getTheAssociationEnds(employeeRanking));
		assertEquals(2, assocEnds.size());
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertFalse(assocEnd.isOneMultiplicity());
		assertFalse(assocEnd.isOrdered());
		assertEquals("Person", assocEnd.getTheParticipant().getName());
		assertEquals("employees", assocEnd.getName());
		assertFalse(assocEnd.isMandatory());

		assocEnd = (CoreAssociationEnd) assocEnds.get(1);
		assertFalse(assocEnd.isOneMultiplicity());
		assertFalse(assocEnd.isOrdered());
		assertEquals("Person", assocEnd.getTheParticipant().getName());
		assertEquals("bosses", assocEnd.getName());
		assertFalse(assocEnd.isMandatory());
	}


	public void testNormalAssociation() {
		CoreAssociationClass allocation = (CoreAssociationClass) getClassifier("MyExample", "Allocation");
		
		CoreClassifier distributor = (CoreClassifier) getClassifier("MyExample", "Distributor");
		CoreClassifier specialFilm = (CoreClassifier) getClassifier(null, "SpecialFilm");
		CoreClassifier person = (CoreClassifier) getClassifier(null, "Person");
			
		assertNotNull(getModelElement(allocation.getClassifierFeatures(), "abc", CoreAttribute.class));
		
		List assocEnds = new ArrayList(allocation.getTheAssociationEnds(allocation));
		assertEquals(2, assocEnds.size());
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertFalse(assocEnd.isOneMultiplicity());
		assertFalse(assocEnd.isOrdered());
		assertEquals("Distributor", assocEnd.getTheParticipant().getName());
		assertEquals("dist", assocEnd.getName());
		assertFalse(assocEnd.isMandatory());
		assertEquals(assocEnd, allocation.getAssociationEnd("dist"));
		
		assocEnd = (CoreAssociationEnd) assocEnds.get(1);
		assertFalse(assocEnd.isOneMultiplicity());
		assertFalse(assocEnd.isOrdered());
		assertEquals("SpecialFilm", assocEnd.getTheParticipant().getName());
		assertEquals("films", assocEnd.getName());
		assertFalse(assocEnd.isMandatory());
		assertEquals(assocEnd, allocation.getAssociationEnd("films"));
		
		assertTrue(allocation.isClassifierInAssociation(allocation));
		assertTrue(allocation.isClassifierInAssociation(distributor));
		assertTrue(allocation.isClassifierInAssociation(specialFilm));
		assertFalse(allocation.isClassifierInAssociation(person));
	}

}
