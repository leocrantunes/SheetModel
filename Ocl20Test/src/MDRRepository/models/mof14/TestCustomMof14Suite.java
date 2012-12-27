/*
 * Created on 04/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.mof14;

import junit.framework.Test;
import junit.framework.TestSuite;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCustomMof14Suite extends TestSuite {

		/**
		 * Constructor for TestUml13JmiSuite.
		 * @param arg0
		 */
		public TestCustomMof14Suite(String arg0) throws Exception {
			super(arg0);
		}

		public static Test suite() throws MOFMetaModelRepositoryException {
			TestSuite suite = new TestSuite();
			
			suite.addTestSuite(TestCustomMof14Model.class);
			suite.addTestSuite(TestCustomMof14Package.class);
			suite.addTestSuite(TestCustomMof14Classifier.class);
			suite.addTestSuite(TestCustomMof14Attribute.class);
//			suite.addTestSuite(TestCustomMof14Operation.class);
			suite.addTestSuite(TestCustomMof14Association.class);
			
			return	suite;
		}

}
