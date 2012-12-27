/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml13;

import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreEnumLiteral;
import ocl20.common.CoreEnumeration;



/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml13CoreAttribute extends TestUml13CoreModelElement {

	public TestUml13CoreAttribute(String arg0) throws Exception {
		super(arg0);
	}
	
	private CoreAttribute getAttribute(String classifierName, String attributeName) {
		CoreClassifier aClassifier = getClassifier("MyExample", classifierName);
		CoreAttribute attribute = (CoreAttribute) getModelElement(aClassifier.getClassifierFeatures(), attributeName, CoreAttribute.class);
		assertNotNull(attribute);
		return attribute;
	}

	public void testInstanceAttribute() {
		CoreClassifier film = getClassifier("MyExample", "Film");
		CoreAttribute aAtribute = getAttribute("Film", "name");		
		assertEquals("String", aAtribute.getFeatureType().getName());
		assertTrue(aAtribute.isInstanceScope());
		assertEquals(film, aAtribute.getFeatureOwner());
		assertEquals(film, aAtribute.getElemOwner());
		assertFalse(aAtribute.isDerived());
		
		aAtribute = getAttribute("Film", "att3");
		assertNotNull(aAtribute);
		assertTrue(aAtribute.isDerived());
	}
	
	public void testClassAttribute() {
		CoreAttribute aAtribute = getAttribute("Film", "days");		
		assertEquals("Integer", aAtribute.getFeatureType().getName());
		assertFalse(aAtribute.isInstanceScope());
	}

	
	public void testQualifiers() {
		CoreClassifier tape = getClassifier("MyExample", "Film");
		assertNotNull(tape);
		
		Collection assocEnds = tape.getAllAssociationEnds();
		CoreAssociationEnd assocEndWithQualifiers = null;
		
		for (Iterator iter = assocEnds.iterator(); iter.hasNext();) {
			CoreAssociationEnd aEnd = (CoreAssociationEnd) iter.next();
			
			if (aEnd.getTheParticipant().getName().equals("Tape")) {
				assocEndWithQualifiers = aEnd;
			}
		}
		
		assertNotNull(assocEndWithQualifiers);
		List qualifiers = assocEndWithQualifiers.getTheQualifiers();
		assertTrue(qualifiers != null);
		assertTrue(qualifiers.size() > 0);
		
		CoreAttribute attrib = (CoreAttribute) qualifiers.iterator().next();
		assertEquals("n", attrib.getName());
		assertEquals(assocEndWithQualifiers, attrib.getTheAssociationEnd());
	}

	public void testEnumeration() {
		CoreClassifier aClassifier = getClassifier("MyExample", "Situation");
		assertTrue(aClassifier.isEnumeration());
		CoreEnumeration aEnum = (CoreEnumeration) aClassifier;
		CoreEnumLiteral literal = (CoreEnumLiteral) aEnum.getTheLiterals().get(0);
		assertTrue(literal.getName().equals("single") || literal.getName().equals("married"));
		assertEquals(aEnum, literal.getTheEnumeration());
	}
	
	public void testStereotype() {
		CoreClassifier film = getClassifier("MyExample", "Film");
		CoreAttribute aAtribute = getAttribute("Film", "name");
		assertTrue(aAtribute.hasStereotype("id"));
	}
}
