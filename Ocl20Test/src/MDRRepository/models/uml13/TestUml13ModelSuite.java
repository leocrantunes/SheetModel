/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml13;

import junit.framework.Test;
import junit.framework.TestSuite;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml13ModelSuite extends TestSuite {

	/**
	 * Constructor for TestUml13JmiSuite.
	 * @param arg0
	 */
	public TestUml13ModelSuite(String arg0) {
		super(arg0);
	}

	public static Test suite() throws MOFMetaModelRepositoryException {
		TestSuite suite = new TestSuite();
		
		suite.addTestSuite(TestUml13CoreModel.class);
		suite.addTestSuite(TestUml13CorePackage.class);
		suite.addTestSuite(TestUml13CoreClassifier.class);
		suite.addTestSuite(TestUml13CoreAttribute.class);
		suite.addTestSuite(TestUml13CoreOperation.class);
		suite.addTestSuite(TestUml13CoreAssociation.class);
		suite.addTestSuite(TestUml13CoreAssociationClass.class);
		suite.addTestSuite(TestCoreModelElementNameGenerator.class);
		
		TestUml13CoreModelElement.setUmlModelsRepository();
		
		return	suite;
	}
}
