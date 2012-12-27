/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml14;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;



/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml14CoreAssociation extends TestUml14CoreModelElement {
	
	public TestUml14CoreAssociation(String arg0) throws Exception {
		super(arg0);
	}
	
	private CoreAssociation getAssociation(CoreClassifier classifier1, CoreClassifier classifier2) {
		Collection allAssociations = model.getAllAssociations();
		
		for (Iterator iter = allAssociations.iterator(); iter.hasNext();) {
			CoreAssociation association = (CoreAssociation) iter.next();

			if (association.isClassifierInAssociation(classifier1) && 
				association.isClassifierInAssociation(classifier2)) {
					return	association;		
				}
		}
		return	null;
	}

	private CoreAssociation getSelfAssociation(CoreClassifier classifier) {
		Collection allAssociations = model.getAllAssociations();
		
		for (Iterator iter = allAssociations.iterator(); iter.hasNext();) {
			CoreAssociation association = (CoreAssociation) iter.next();
	
			List assocEnds = new ArrayList(association.getTheAssociationEnds(classifier));
			if (assocEnds.size() == 2 &&
				((CoreAssociationEnd) assocEnds.get(0)).getTheParticipant().getName().equals(classifier.getName()) &&
				((CoreAssociationEnd) assocEnds.get(1)).getTheParticipant().getName().equals(classifier.getName())) {
					return	association;		
				}
		}
		return	null;
	}



	public void testBinaryNToNAssociation() {
		CoreClassifier	specialFilm = getClassifier(null, "SpecialFilm");
		CoreClassifier	distributor = getClassifier("MyExample", "Distributor");
		
		assertNotNull(specialFilm);
		assertNotNull(distributor);

		CoreAssociation association = getAssociation(specialFilm, distributor);
		assertNotNull(association);
		
		List assocEnds = new ArrayList(association.getTheAssociationEnds(specialFilm));
		assertEquals(1, assocEnds.size());
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, false, false, "Distributor", "dist", false);
		assertEquals(association, assocEnd.getTheAssociation());
		
		assocEnds = new ArrayList(association.getTheAssociationEnds(distributor));
		assertEquals(1, assocEnds.size());
		assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, false, false, "SpecialFilm", "films", false);
		assertEquals(association, assocEnd.getTheAssociation());
		
		assocEnd = association.getAssociationEnd("films");
		assertAssocEnd(assocEnd, false, false, "SpecialFilm", "films", false);
		assocEnd = association.getAssociationEnd("dist");
		assertAssocEnd(assocEnd, false, false, "Distributor", "dist", false);
		assertNull(association.getAssociationEnd("distribuitor"));
		
		assertTrue(association.isClassifierInAssociation(specialFilm));
		assertTrue(association.isClassifierInAssociation(distributor));
		
		CoreClassifier film = getClassifier("MyExample", "Film");
		assertNotNull(film);
		assertFalse(association.isClassifierInAssociation(film));
	}

	public void testBinary1ToNAssociation() {
		CoreClassifier	reservation = getClassifier(null, "Reservation");
		CoreClassifier	person = getClassifier(null, "Person");
		
		assertNotNull(reservation);
		assertNotNull(person);

		CoreAssociation association = getAssociation(reservation, person);
		assertNotNull(association);
		
		List assocEnds = new ArrayList(association.getTheAssociationEnds(person));
		assertEquals(1, assocEnds.size());
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, false, true, "Reservation", "", true);

		assocEnds = new ArrayList(association.getTheAssociationEnds(reservation));
		assertEquals(1, assocEnds.size());
		assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, true, false, "Person", "", true);
	}

	public void testSelfAssociation() {
		CoreClassifier	person = getClassifier(null, "Person");
		assertNotNull(person);

		CoreAssociation association = getSelfAssociation(person);
		assertNotNull(association);
		
		List assocEnds = new ArrayList(association.getTheAssociationEnds(person));
		assertEquals(2, assocEnds.size());
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, false, false, "Person", "bosses", false);

		assocEnd = (CoreAssociationEnd) assocEnds.get(1);
		assertAssocEnd(assocEnd, false, false, "Person", "employees", false);
	}

	public void testQualifiers() {
		CoreClassifier	film = getClassifier("MyExample", "Film");
		CoreClassifier	tape = getClassifier("MyExample", "Tape");
		
		assertNotNull(film);
		assertNotNull(tape);

		CoreAssociation association = getAssociation(film, tape);
		assertNotNull(association);
		
		List assocEnds = new ArrayList(association.getTheAssociationEnds(tape));
		assertEquals(1, assocEnds.size());
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, true, false , "Film", "theFilm", true);
		List qualifiers = assocEnd.getTheQualifiers();
		assertEquals(0, qualifiers.size());
		
		assocEnds = new ArrayList(association.getTheAssociationEnds(film));
		assertEquals(1, assocEnds.size());
		assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, false, false , "Tape", "tapes", false);
//		qualifiers = assocEnd.getTheQualifiers();
//		assertEquals(1, qualifiers.size());
//		CoreAttribute qualifier = (CoreAttribute) qualifiers.get(0);
//		assertEquals("n", qualifier.getName());
//		assertEquals("Integer", qualifier.getFeatureType().getName());
		
		assertEquals("is available in::tapes", assocEnd.getFullPathName());
	}


	private	void assertAssocEnd(CoreAssociationEnd assocEnd, boolean isOneMultiplicity, boolean isOrdered, String participantName, String roleName, boolean isMandatory) {
		assertEquals(isOneMultiplicity, assocEnd.isOneMultiplicity());
		assertEquals(isOrdered, assocEnd.isOrdered());
		assertEquals(participantName, assocEnd.getTheParticipant().getName());
		if (roleName.length() == 0)
			roleName = assocEnd.getTheParticipant().getName();
		assertEquals(roleName, assocEnd.getName());
	}

}
