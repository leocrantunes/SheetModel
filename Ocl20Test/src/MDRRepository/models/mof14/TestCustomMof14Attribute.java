/*
 * Created on 04/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.mof14;

import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCustomMof14Attribute extends TestCustomMof14ModelElement {

	/**
	 * 
	 */
	public TestCustomMof14Attribute(String arg0) throws Exception {
		super(arg0);
		// TODO Auto-generated constructor stub
	}
	
	private CoreAttribute getAttribute(String classifierName, String attributeName) {
		CoreClassifier aClassifier = getClassifier("Core", classifierName);
		CoreAttribute attribute = (CoreAttribute) getModelElement(aClassifier.getClassifierFeatures(), attributeName, CoreAttribute.class);
		assertNotNull(attribute);
		return attribute;
	}

	public void testInstanceAttribute() {
		CoreClassifier operation = getClassifier("Core", "Operation");
		CoreAttribute aAtribute = getAttribute("Operation", "isRoot");		
		assertEquals("Boolean", aAtribute.getFeatureType().getName());
		assertTrue(aAtribute.isInstanceScope());
		assertEquals(operation, aAtribute.getFeatureOwner());
		assertEquals(operation, aAtribute.getElemOwner());
		assertFalse(aAtribute.isDerived());
	}

}
