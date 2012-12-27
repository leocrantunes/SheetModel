/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml14;

import junit.framework.Test;
import junit.framework.TestSuite;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml14ModelSuite extends TestSuite {


		/**
		 * Constructor for TestUml13JmiSuite.
		 * @param arg0
		 */
		public TestUml14ModelSuite(String arg0) {
			super(arg0);
		}

		public static Test suite() throws MOFMetaModelRepositoryException {
			TestSuite suite = new TestSuite();
			
			suite.addTestSuite(TestUml14CoreModel.class);
			suite.addTestSuite(TestUml14CorePackage.class);
			suite.addTestSuite(TestUml14CoreClassifier.class);
			suite.addTestSuite(TestUml14CoreAttribute.class);
			suite.addTestSuite(TestUml14CoreOperation.class);
			suite.addTestSuite(TestUml14CoreAssociation.class);
			
			TestUml14CoreModelElement.setUmlModelsRepository();
			
			return	suite;
		}
}
