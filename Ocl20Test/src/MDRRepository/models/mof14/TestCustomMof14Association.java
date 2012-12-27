/*
 * Created on 04/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.mof14;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;



/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCustomMof14Association extends TestCustomMof14ModelElement {

	/**
	 * 
	 */
	public TestCustomMof14Association(String arg0) throws Exception {
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
		CoreClassifier	modelElement = getClassifier("Core", "ModelElement");
		CoreClassifier	constraint = getClassifier("Core", "Constraint");
		
		assertNotNull(modelElement);
		assertNotNull(constraint);

		CoreAssociation association = getAssociation(modelElement, constraint);
		assertNotNull(association);
		
		List assocEnds = new ArrayList(association.getTheAssociationEnds(constraint));
		assertEquals(1, assocEnds.size());
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, false, true, "ModelElement", "constrainedElement", true);
		assertEquals(association, assocEnd.getTheAssociation());
		
		assocEnds = new ArrayList(association.getTheAssociationEnds(modelElement));
		assertEquals(1, assocEnds.size());
		assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, false, false, "Constraint", "constraint", false);
		assertEquals(association, assocEnd.getTheAssociation());
		
		assocEnd = association.getAssociationEnd("constrainedElement");
		assertAssocEnd(assocEnd, false, true, "ModelElement", "constrainedElement", true);
		assocEnd = association.getAssociationEnd("constraint");
		assertAssocEnd(assocEnd, false, false, "Constraint", "constraint", false);
		
		assertNull(association.getAssociationEnd("feature"));
		assertTrue(association.isClassifierInAssociation(modelElement));
		assertTrue(association.isClassifierInAssociation(constraint));
		
		CoreClassifier operation = getClassifier("Core", "Operation");
		assertNotNull(operation);
		assertFalse(association.isClassifierInAssociation(operation));
	}

	public void testBinary1ToNAssociation() {
		CoreClassifier	classifier = getClassifier(null, "Classifier");
		CoreClassifier	feature = getClassifier(null, "Feature");
		
		assertNotNull(classifier);
		assertNotNull(feature);

		CoreAssociation association = getAssociation(classifier, feature);
		assertNotNull(association);
		
		List assocEnds = new ArrayList(association.getTheAssociationEnds(feature));
		assertEquals(1, assocEnds.size());
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, true, false, "Classifier", "owner", true);

		assocEnds = new ArrayList(association.getTheAssociationEnds(classifier));
		assertEquals(1, assocEnds.size());
		assocEnd = (CoreAssociationEnd) assocEnds.get(0);
		assertAssocEnd(assocEnd, false, true, "Feature", "feature", false);
	}

	private	void assertAssocEnd(CoreAssociationEnd assocEnd, boolean isOneMultiplicity, boolean isOrdered, String participantName, String roleName, boolean isMandatory) {
		assertEquals(isOneMultiplicity, assocEnd.isOneMultiplicity());
//		assertEquals(isOrdered, assocEnd.isOrdered());
		assertEquals(participantName, assocEnd.getTheParticipant().getName());
		if (roleName.length() == 0)
			roleName = assocEnd.getTheParticipant().getName();
		assertEquals(roleName, assocEnd.getName());
	}

}
