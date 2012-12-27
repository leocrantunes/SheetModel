/*
 * Created on 04/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw;

import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.mof14.TestCustomMof14Suite;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.ocl20.TestOcl20Suite;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.uml13.TestUml13ModelSuite;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.uml14.TestUml14ModelSuite;
import junit.framework.Test;
import junit.framework.TestSuite;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class AllTestsModels extends TestSuite {

	
		/**
		 * Constructor for TestUml13JmiSuite.
		 * @param arg0
		 */
		public AllTestsModels(String arg0) throws Exception {
			super(arg0);
		}

		public static Test suite() throws MOFMetaModelRepositoryException {
			TestSuite suite = new TestSuite();
			
			suite.addTest(TestCustomMof14Suite.suite());
			suite.addTest(TestUml13ModelSuite.suite());
			suite.addTest(TestUml14ModelSuite.suite());
			suite.addTest(TestOcl20Suite.suite());
			
			return	suite;
		}


}
