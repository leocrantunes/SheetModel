/*
 * Created on 04/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.mof14;

import java.util.Collection;
import java.util.Iterator;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreEnumeration;
import ocl20.common.CoreModelElement;
import ocl20.common.CorePackage;
import ocl20.environment.Environment;




/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCustomMof14Model extends TestCustomMof14ModelElement {

	/**
	 * 
	 */
	public TestCustomMof14Model(String arg0) throws Exception {
		super(arg0);
	}

	public void testUmlModelName() {
		assertEquals("UML13", model.getName());
	}
	
	public void testUmlGetModel() {
		assertEquals(model, model.getModel());
	}

	public void testGetOwnedElements() {
		Collection elements = model.getElemOwnedElements();

		for (Iterator iter = elements.iterator(); iter.hasNext();) {
			CoreModelElement element = (CoreModelElement) iter.next();
		}
		
		assertNotNull(getModelElement(elements, "Classifier", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "AssociationClass", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "State", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "Core", CorePackage.class));
		assertNotNull(getModelElement(elements, "VisibilityKind", CoreEnumeration.class));
		assertNotNull(getModelElement(elements, "isRoot", CoreAttribute.class));
		assertNotNull(getModelElement(elements, "partition", CoreAssociationEnd.class));
		assertNotNull(getModelElement(elements, "A_package_elementImport", CoreAssociation.class));
	}

	
	public void testgetEnvironmentWithoutParents() {
		Environment elements = model.getEnvironmentWithoutParents();
		
		assertNotNull(getModelElement(elements, "Classifier", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "Foundation", CorePackage.class));
		
		CorePackage foundation = (CorePackage) getModelElement(elements, "Foundation", CorePackage.class);
		assertNotNull(getModelElement(foundation.getEnvironmentWithoutParents(), "Core", CorePackage.class));
		assertNotNull(getModelElement(elements, "Core", CorePackage.class));
		assertNotNull(getModelElement(elements, "Use_Cases", CorePackage.class));
//		assertNotNull(getModelElement(elements, "VisibilityKind", CoreEnumeration.class));
		assertNull(getModelElement(elements, "isRoot", CoreAttribute.class));
		assertNull(getModelElement(elements, "partition", CoreAssociationEnd.class));
		assertNull(getModelElement(elements, "A_package_elementImport", CoreAssociation.class));
	}
	
	public void testGetAssociationEndsForClassifier() {
		Environment elements = model.getEnvironmentWithoutParents();
		
		CoreClassifier  classifier = (CoreClassifier) getModelElement(elements, "Classifier", CoreClassifier.class);
		CoreClassifier  modelelement = (CoreClassifier) getModelElement(elements, "ModelElement", CoreClassifier.class);
		
		Collection assocEnds = model.getAssociationEndsForClassifier(classifier);
		
		assertNotNull(getModelElement(assocEnds, "feature", CoreAssociationEnd.class));
		assertNotNull(getModelElement(assocEnds, "parameter", CoreAssociationEnd.class));
		assertNull(getModelElement(assocEnds, "namespace", CoreAssociationEnd.class));
		assertNull(getModelElement(assocEnds, "specification", CoreAssociationEnd.class));
		
		assocEnds = model.getAssociationEndsForClassifier(modelelement);
		assertNotNull(getModelElement(assocEnds, "namespace", CoreAssociationEnd.class));
	}
	
	
	public void testGetAllAssociations() {
		Collection elements = model.getAllAssociations();
		
		assertNotNull(getModelElement(elements, "A_package_elementImport", CoreAssociation.class));
		assertNotNull(getModelElement(elements, "A_stateMachine_transitions", CoreAssociation.class));
		assertNull(getModelElement(elements, "A_package_elementImport2", CoreAssociation.class));
	}

	
}
