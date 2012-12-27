/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml13;

import ocl20.common.CoreClassifier;
import ocl20.common.CorePackage;


/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml13CorePackage extends TestUml13CoreModelElement {

	public TestUml13CorePackage(String arg0) throws Exception {
		super(arg0);
	}

	public void testInnerPackages() {
		CorePackage aPackage = (CorePackage) getModelElement(model.getElemOwnedElements(), "MyExample", CorePackage.class);
		assertNotNull(aPackage);
		assertEquals(model, aPackage.getModel());
			
		CorePackage package1 = (CorePackage) getModelElement(aPackage.getElemOwnedElements(), "package_1", CorePackage.class);
		assertNotNull(package1);
		assertEquals(model, package1.getModel());	

		CorePackage package1_1 = (CorePackage) getModelElement(package1.getElemOwnedElements(), "package_1_1", CorePackage.class);
		assertNotNull(package1_1);
		assertEquals(model, package1_1.getModel());	
	}
	
	public void testClassifiers() {
		CorePackage aPackage = (CorePackage) getModelElement(model.getElemOwnedElements(), "MyExample", CorePackage.class);
		assertNotNull(aPackage);
		
		CorePackage package1 = (CorePackage) getModelElement(aPackage.getElemOwnedElements(), "package_1", CorePackage.class);
		assertNotNull(package1);

		CorePackage package1_2 = (CorePackage) getModelElement(package1.getElemOwnedElements(), "package_1_2", CorePackage.class);
		assertNotNull(package1_2);
		
		assertNotNull(getModelElement(package1_2.getElemOwnedElements(), "Client", CoreClassifier.class));	
		assertNotNull(getModelElement(package1_2.getElemOwnedElements(), "Rental", CoreClassifier.class));
	}

}
