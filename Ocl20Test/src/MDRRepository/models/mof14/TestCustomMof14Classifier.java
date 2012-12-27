/*
 * Created on 04/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.mof14;


import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreEnumLiteral;
import ocl20.common.CoreEnumeration;
import ocl20.common.CorePackage;




/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCustomMof14Classifier extends TestCustomMof14ModelElement {

	/**
	 * 
	 */
	public TestCustomMof14Classifier(String arg0) throws Exception {
		super(arg0);
	}

	
	public void testFeatures() {
		CoreClassifier aClassifier = getClassifier("Core", "StructuralFeature");
		
		assertNotNull(getModelElement(aClassifier.getClassifierFeatures(), "targetScope", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getClassifierFeatures(), "multiplicity", CoreAttribute.class));
		assertNull(getModelElement(aClassifier.getClassifierFeatures(), "code", CoreAttribute.class));
	}

	public void testAncestors() {
		CoreClassifier aClassifier = getClassifier("Core", "Classifier");
		assertNotNull(getModelElement(aClassifier.getAllAncestors(), "Namespace", CoreClassifier.class));
		assertNotNull(getModelElement(aClassifier.getAllAncestors(), "ModelElement", CoreClassifier.class));
		assertNull(getModelElement(aClassifier.getAllAncestors(), "Feature", CoreClassifier.class));
		assertEquals("Core", aClassifier.getElemOwner().getName());
		
		CoreClassifier attribute = getClassifier("Core", "Attribute");
		List ancestors = (List) attribute.getAllAncestors();
		assertEquals("StructuralFeature", ((CoreClassifier) ancestors.get(0)).getName());
		assertEquals("Feature", ((CoreClassifier) ancestors.get(1)).getName());
		assertEquals("ModelElement", ((CoreClassifier) ancestors.get(2)).getName());
		assertEquals("Element", ((CoreClassifier) ancestors.get(3)).getName());
	}		

	public void testSubClasses() {
		CoreClassifier aClassifier = getClassifier("Core", "Feature");
		assertNotNull(aClassifier);
		assertTrue(aClassifier.getAllSubClasses().size() > 0);
		assertNotNull(getModelElement(aClassifier.getAllSubClasses(), "BehavioralFeature", CoreClassifier.class));
		assertNotNull(getModelElement(aClassifier.getAllSubClasses(), "StructuralFeature", CoreClassifier.class));
		assertNotNull(getModelElement(aClassifier.getAllSubClasses(), "Operation", CoreClassifier.class));
		assertNotNull(getModelElement(aClassifier.getAllSubClasses(), "Attribute", CoreClassifier.class));
		assertNull(getModelElement(aClassifier.getAllSubClasses(), "ElementOwnership", CoreClassifier.class));
		
		assertEquals("Core", aClassifier.getElemOwner().getName());
	}		

	public void testModel() {
		CoreClassifier aClassifier = getClassifier("Core", "Classifier");
		assertEquals(model, aClassifier.getModel());
	}
	
	public void testAllAttributes() {
		CoreClassifier aClassifier = getClassifier("Core", "Operation");
		
		assertNotNull(getModelElement(aClassifier.getAllAttributes(), "concurrency", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getAllAttributes(), "isRoot", CoreAttribute.class));
		assertNull(getModelElement(aClassifier.getAllAttributes(), "isQuery", CoreAttribute.class));
	}

	public void testGetElementsForEnv() {
		CoreClassifier aClassifier = getClassifier("Core", "Operation");
		
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "concurrency", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(),  "isRoot", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "isQuery", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "ownerScope", CoreAttribute.class));
		assertNotNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "name", CoreAttribute.class));
		assertNull(getModelElement(aClassifier.getEnvironmentWithoutParents(), "body", CoreAttribute.class));
	}


	public void testEnumeration() {
		CoreClassifier aClassifier = getClassifier(null, "VisibilityKind");
		assertTrue(aClassifier.isEnumeration());
		
		CoreEnumLiteral publicLiteral01 = ((CoreEnumeration) aClassifier).lookupEnumLiteral("vk_public");
		CoreEnumLiteral publicLiteral02 = ((CoreEnumeration) aClassifier).lookupEnumLiteral("vk_public");
		CoreEnumLiteral privateLiteral = ((CoreEnumeration) aClassifier).lookupEnumLiteral("vk_private");
		
		assertNotNull(publicLiteral01);
		assertNotNull(publicLiteral02);
		assertNotNull(privateLiteral);

		assertTrue(publicLiteral01.equals(publicLiteral02));
		assertFalse(publicLiteral01.equals(privateLiteral));
	}
	
	public void testConformsTo() {
		
		CoreClassifier feature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Feature", CoreClassifier.class);
		CoreClassifier structuralFeature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "StructuralFeature", CoreClassifier.class);
		CorePackage core = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "Core", CorePackage.class);
		CoreClassifier modelElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "ModelElement", CoreClassifier.class);
		CoreClassifier attribute = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Attribute", CoreClassifier.class);
		CoreClassifier operation = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Operation", CoreClassifier.class);
		
		assertNotNull(structuralFeature);
		assertNotNull(feature);
		assertNotNull(modelElement);
		assertNotNull(modelElement);
		assertNotNull(attribute);
		
		assertTrue(attribute.conformsTo(structuralFeature));
		assertTrue(attribute.conformsTo(feature));
		assertTrue(attribute.conformsTo(modelElement));
		
		assertTrue(structuralFeature.conformsTo(modelElement));
		assertTrue(feature.conformsTo(modelElement));
		
		assertFalse(structuralFeature.conformsTo(attribute));
		assertFalse(modelElement.conformsTo(attribute));
		assertFalse(modelElement.conformsTo(operation));
		assertFalse(attribute.conformsTo(operation));
	}

	
	public void testFindAttribute() {
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Operation", CoreClassifier.class);
		assertNotNull(specialFilm);
		
		
		doPositiveTestLookupAttribute(specialFilm, "concurrency", "CallConcurrencyKind");
		doPositiveTestLookupAttribute(specialFilm, "isRoot", "Boolean");
		doPositiveTestLookupAttribute(specialFilm, "isLeaf", "Boolean");
		doPositiveTestLookupAttribute(specialFilm, "specification", "String");
		doPositiveTestLookupAttribute(specialFilm, "isQuery", "Boolean");
		doPositiveTestLookupAttribute(specialFilm, "name", "Name");
		doNegativeTestLookupAttribute(specialFilm, "multiplicity");
	}

	private void doPositiveTestLookupAttribute(CoreClassifier cls, String attName, String typeName) {
		CoreAttribute att = cls.lookupAttribute(attName);
		assertNotNull(att);
		assertEquals(attName, att.getName());
		assertEquals(typeName, att.getFeatureType().getName());
	}

	private void doNegativeTestLookupAttribute(CoreClassifier cls, String name) {
		CoreAttribute att = cls.lookupAttribute(name);
		assertNull(att);
	}

	
	public void testFindAssociationEnd() {
		CoreClassifier cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Operation", CoreClassifier.class);
		assertNotNull(cls);
		
		doPositiveTestLookupAssociationEnd(cls, "method", "Method");
		doPositiveTestLookupAssociationEnd(cls, "parameter", "Parameter");
		doPositiveTestLookupAssociationEnd(cls, "owner", "Classifier");
		doPositiveTestLookupAssociationEnd(cls, "namespace", "Namespace");
		doNegativeTestLookupAssociationEnd(cls, "feature");
		
	}

	
	private void doPositiveTestLookupAssociationEnd(CoreClassifier cls, String name, String assocTypeName) {
		CoreAssociationEnd assocEnd = cls.lookupAssociationEnd(name);
		assertNotNull(name);
		assertNotNull(assocEnd);
		assertEquals(name, assocEnd.getName());
		assertEquals(assocTypeName, assocEnd.getTheParticipant().getName());
	}

	private void doNegativeTestLookupAssociationEnd(CoreClassifier cls, String name) {
		CoreAssociationEnd assocEnd = cls.lookupAssociationEnd(name);
		assertNull(assocEnd);
	}

	
	public void testAssociation() {
		CoreClassifier op = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Operation", CoreClassifier.class);
		assertNotNull(op);
		
		CoreAssociationEnd assocEnd = op.lookupAssociationEnd("owner");
		assertNotNull(assocEnd);
		CoreAssociation assoc = assocEnd.getTheAssociation();
		assertNotNull(assoc);
		
		CoreClassifier cls = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Classifier", CoreClassifier.class);
		assertNotNull(cls);
		CoreAssociationEnd	otherAssocEnd = cls.lookupAssociationEnd("feature");
		assertNotNull(otherAssocEnd);
		assertEquals(assoc, otherAssocEnd.getTheAssociation());
		
		List assocEnds = assoc.getTheAssociationEnds();
		assertTrue(assocEnds.contains(assocEnd));
		assertEquals(2, assocEnds.size());
		for (int i = 0; i < assocEnds.size(); i++) {
			CoreAssociationEnd theAssocEnd = (CoreAssociationEnd) assocEnds.get(i);
			assertTrue(theAssocEnd.getTheParticipant().getName().equals("Classifier") ||
					theAssocEnd.getTheParticipant().getName().equals("Feature") );
		}
	}


	public void testAllDirectSubclasses() {
		CorePackage core = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "core", CorePackage.class);
		CoreClassifier modelElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "ModelElement", CoreClassifier.class);
		CoreClassifier structuralFeature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "StructuralFeature", CoreClassifier.class);
		CoreClassifier feature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Feature", CoreClassifier.class);
		CoreClassifier attribute = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Attribute", CoreClassifier.class);

		assertTrue(modelElement.getAllDirectSubClasses().contains(feature));
		assertFalse(modelElement.getAllDirectSubClasses().contains(structuralFeature));
		assertFalse(modelElement.getAllDirectSubClasses().contains(attribute));
		assertTrue(structuralFeature.getAllDirectSubClasses().contains(attribute));
		assertFalse(feature.getAllDirectSubClasses().contains(attribute));
		assertTrue(feature.getAllDirectSubClasses().contains(structuralFeature));
	}

	public void testAllSubclasses() {
		CorePackage core = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "core", CorePackage.class);
		CoreClassifier modelElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "ModelElement", CoreClassifier.class);
		CoreClassifier structuralFeature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "StructuralFeature", CoreClassifier.class);
		CoreClassifier feature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Feature", CoreClassifier.class);
		CoreClassifier attribute = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Attribute", CoreClassifier.class);
		CoreClassifier operation = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Operation", CoreClassifier.class);

		assertTrue(modelElement.getAllSubClasses().contains(feature));
		assertTrue(modelElement.getAllSubClasses().contains(structuralFeature));
		assertTrue(modelElement.getAllSubClasses().contains(attribute));
		assertTrue(structuralFeature.getAllSubClasses().contains(attribute));
		assertTrue(feature.getAllSubClasses().contains(attribute));
		assertTrue(feature.getAllSubClasses().contains(structuralFeature));
		assertTrue(feature.getAllSubClasses().contains(operation));
		assertFalse(attribute.getAllSubClasses().contains(operation));
	}


	public void testGetFullPathName() {
		CorePackage core = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "core", CorePackage.class);
		CoreClassifier modelElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "ModelElement", CoreClassifier.class);
		CoreClassifier structuralFeature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "StructuralFeature", CoreClassifier.class);
		CoreClassifier feature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Feature", CoreClassifier.class);
		CoreClassifier attribute = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Attribute", CoreClassifier.class);

		assertEquals("Foundation::Core::Feature", feature.getFullPathName());		
		assertEquals("Foundation::Core::StructuralFeature", structuralFeature.getFullPathName());
		assertEquals("Foundation::Core::Attribute", attribute.getFullPathName());
	}
	
	
	public void testGetMostSpecificSuperType() {
		CorePackage core = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "core", CorePackage.class);
		CoreClassifier modelElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "ModelElement", CoreClassifier.class);
		CoreClassifier structuralFeature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "StructuralFeature", CoreClassifier.class);
		CoreClassifier feature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Feature", CoreClassifier.class);
		CoreClassifier attribute = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Attribute", CoreClassifier.class);
		CoreClassifier operation = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Operation", CoreClassifier.class);
		CoreClassifier genElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "GeneralizableElement", CoreClassifier.class);
		
		assertTrue(attribute.equals(attribute));
		assertFalse(operation.equals(attribute));
		
		assertEquals(structuralFeature, attribute.getMostSpecificCommonSuperType(structuralFeature));
		assertEquals(attribute, attribute.getMostSpecificCommonSuperType(attribute));
		assertEquals(modelElement, attribute.getMostSpecificCommonSuperType(genElement));
		assertEquals(feature, attribute.getMostSpecificCommonSuperType(operation));
	}

	public void testIsConcrete() {
		CorePackage core = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "core", CorePackage.class);
		CoreClassifier modelElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "ModelElement", CoreClassifier.class);
		CoreClassifier structuralFeature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "StructuralFeature", CoreClassifier.class);
		CoreClassifier feature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Feature", CoreClassifier.class);
		CoreClassifier attribute = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Attribute", CoreClassifier.class);
		
		assertTrue(attribute.isConcrete());
		assertFalse(structuralFeature.isConcrete());
		assertFalse(feature.isConcrete());
		assertFalse(modelElement.isConcrete());
	}
	
	public void testIsDescendantOf() {
		CorePackage core = (CorePackage) getModelElement(model.getEnvironmentWithoutParents(), "core", CorePackage.class);
		CoreClassifier modelElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "ModelElement", CoreClassifier.class);
		CoreClassifier structuralFeature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "StructuralFeature", CoreClassifier.class);
		CoreClassifier feature = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Feature", CoreClassifier.class);
		CoreClassifier attribute = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Attribute", CoreClassifier.class);
		CoreClassifier operation = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "Operation", CoreClassifier.class);
		CoreClassifier genElement = (CoreClassifier) getModelElement(model.getEnvironmentWithoutParents(), "GeneralizableElement", CoreClassifier.class);
		
		assertTrue(attribute.isClassifierDescendantOf(structuralFeature));
		assertTrue(attribute.isClassifierDescendantOf(feature));
		assertTrue(attribute.isClassifierDescendantOf(modelElement));
		assertFalse(attribute.isClassifierDescendantOf(operation));
		assertFalse(attribute.isClassifierDescendantOf(genElement));
		
		assertTrue(structuralFeature.isClassifierDescendantOf(feature));
		assertTrue(feature.isClassifierDescendantOf(modelElement));
		
		assertFalse(attribute.isClassifierDescendantOf(attribute));
		assertFalse(modelElement.isClassifierDescendantOf(modelElement));
		
		assertEquals("Foundation::Core::Operation", operation.getFullPathName());
	}

}
