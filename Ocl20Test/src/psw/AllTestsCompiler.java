package br.ufrj.cos.lens.odyssey.tools.psw;

/*
 * Created on Dec 5, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */


import br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic.TestSemanticAnalysisSuite;
import br.ufrj.cos.lens.odyssey.tools.psw.project.TestPswProject;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.TestPswOclCompiler;
import junit.framework.Test;
import junit.framework.TestSuite;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class AllTestsCompiler extends TestSuite {

		public static Test suite() throws Exception {
			TestSuite suite= new TestSuite();
	 
			suite.addTest(TestSemanticAnalysisSuite.suite());
			suite.addTestSuite(TestPswOclCompiler.class);
			suite.addTestSuite(TestPswProject.class);
			
			return suite;
	}

}
