/*
 * Created on 16/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.ocl20;

import junit.framework.Test;
import junit.framework.TestSuite;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestOcl20Suite extends TestSuite {

	/**
	 * Constructor for TestUml13JmiSuite.
	 * @param arg0
	 */
	public TestOcl20Suite(String arg0) throws Exception {
		super(arg0);
	}

	public static Test suite() throws MOFMetaModelRepositoryException {
		TestSuite suite = new TestSuite();
		
		suite.addTestSuite(TestOclConstraints.class);
		suite.addTestSuite(TestOclModelElements.class);
		suite.addTestSuite(TestOclTypesDefinition.class);
		
		return	suite;
	}
}
