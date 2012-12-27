/*
 * Created on Dec 5, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestObjectSpaceSuite;
import br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator.TestEvalSuite;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.TestValuesSuite;
import junit.framework.Test;
import junit.framework.TestSuite;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class AllTestsEvaluator extends TestSuite {

		public static Test suite() throws Exception {
			TestSuite suite= new TestSuite();
	 
			suite.addTest(TestValuesSuite.suite());
			suite.addTest(TestObjectSpaceSuite.suite());
			suite.addTest(TestEvalSuite.suite());
			
			return suite;
	}

}
