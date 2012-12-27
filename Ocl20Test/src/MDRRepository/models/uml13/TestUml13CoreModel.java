/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml13;

import ocl20.environment.Environment;

import java.util.Collection;

import ocl20.common.CoreAssociationClass;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreModelElement;
import ocl20.common.CorePackage;
import ocl20.common.CoreStereotype;




/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml13CoreModel extends TestUml13CoreModelElement {

	
	/**
	 * Constructor for TestUml13Model.
	 * @param arg0
	 */
	public TestUml13CoreModel(String arg0) throws Exception {
		super(arg0);
	}
	
	public void testUmlModelName() {
		assertEquals("myExampleRose", model.getName());
	}
	
	public void testUmlGetModel() {
		assertEquals(model, model.getModel());
	}

	public void testGetOwnedElements() {
		Collection elements = model.getElemOwnedElements();

		assertNotNull(getModelElement(elements, "Person", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "SpecialFilm", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "RentalItem", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "MyExample", CorePackage.class));
		assertNotNull(getModelElement(elements, "EmployeeRanking", CoreModelElement.class));
		assertNotNull(getModelElement(elements, "EmployeeRanking", CoreAssociationClass.class));
		assertNull(getModelElement(elements, "Distributor", CoreClassifier.class));
	}


	public void testgetEnvironmentWithoutParents() {
		Environment elements = model.getEnvironmentWithoutParents();
		
//		for (Iterator iter = elements.iterator(); iter.hasNext();) {
//			CoreModelElement element = (CoreModelElement) iter.next();
//			System.out.println("element = " + element.getName() + " - " + element.getClass().getName() + " owner: " + element.getElemOwner().getName());
//		}
		
		assertNotNull(getModelElement(elements, "Person", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "SpecialFilm", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "RentalItem", CoreClassifier.class));
		assertNotNull(getModelElement(elements, "MyExample", CorePackage.class));
		assertNotNull(getModelElement(elements, "EmployeeRanking", CoreModelElement.class));
		assertNotNull(getModelElement(elements, "EmployeeRanking", CoreAssociationClass.class));
		assertNotNull(getModelElement(elements, "Distributor", CoreClassifier.class));
	}
	
	public void testGetStereotypes() {
		Collection elements = model.getAllStereotypes();
		assertNotNull(getModelElement(elements, "enumeration", CoreStereotype.class));
		
//		assertNotNull(model.getStereotype("enumeration"));
	}
	
	
	public void testGetAssociationEnds() {
		Environment elements = model.getEnvironmentWithoutParents();
		
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(elements, "SpecialFilm", CoreClassifier.class);
		CoreClassifier allocation = (CoreClassifier) getModelElement(elements, "Allocation", CoreClassifier.class);
		CoreClassifier distributor = (CoreClassifier) getModelElement(elements, "Distributor", CoreClassifier.class);

		CoreClassifier client = (CoreClassifier) getModelElement(elements, "Client", CoreClassifier.class);
		CoreClassifier rental = (CoreClassifier) getModelElement(elements, "Rental", CoreClassifier.class);
		
		Collection result = model.getAssociationEndsForClassifier(specialFilm);
		CoreAssociationEnd assocEnd = (CoreAssociationEnd) result.iterator().next();
		assertEquals(assocEnd.getTheParticipant(), distributor);
		
		result = model.getAssociationClassesForClassifier(specialFilm);
		CoreAssociationClass assocClass = (CoreAssociationClass) result.iterator().next();
		assertEquals(assocClass, allocation);
		
		result = model.getAssociationEndsForClassifier(client);
		assocEnd = (CoreAssociationEnd) result.iterator().next();
		assertEquals(assocEnd.getTheParticipant(), rental);
	}	
}
