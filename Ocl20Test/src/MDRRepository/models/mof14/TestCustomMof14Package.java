/*
 * Created on 04/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.mof14;

import ocl20.common.CoreClassifier;
import ocl20.common.CorePackage;


/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCustomMof14Package extends TestCustomMof14ModelElement {

	/**
	 * 
	 */
	public TestCustomMof14Package(String arg0) throws Exception {
		super(arg0);
		// TODO Auto-generated constructor stub
	}

	
	public void testInnerPackages() {
		CorePackage aPackage = (CorePackage) getModelElement(model.getElemOwnedElements(), "Behavioral_Elements", CorePackage.class);
		assertNotNull(aPackage);
		
		assertEquals(model, aPackage.getModel());
			
		CorePackage package1 = (CorePackage) getModelElement(aPackage.getElemOwnedElements(), "State_Machines", CorePackage.class);
		assertNotNull(package1);
		assertEquals(model, package1.getModel());	
	}
	
	public void testClassifiers() {
		CorePackage aPackage = (CorePackage) getModelElement(model.getElemOwnedElements(), "Behavioral_Elements", CorePackage.class);
		assertNotNull(aPackage);
		
		CorePackage package1 = (CorePackage) getModelElement(aPackage.getElemOwnedElements(), "State_Machines", CorePackage.class);
		assertNotNull(package1);

		assertNotNull(getModelElement(package1.getElemOwnedElements(), "State", CoreClassifier.class));	
		assertNotNull(getModelElement(package1.getElemOwnedElements(), "Transition", CoreClassifier.class));
		assertNull(getModelElement(package1.getElemOwnedElements(), "Classifier", CoreClassifier.class));

	}
	
}
