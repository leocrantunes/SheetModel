/*
 * Created on 27/04/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.compiler;

import junit.framework.Test;
import junit.framework.TestSuite;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestOclScriptCompilerSuite {
		
		public static Test suite()  {
			TestSuite suite = new TestSuite();

			suite.addTestSuite(TestPrimitiveStatements.class);
			suite.addTestSuite(TestRepetitiveStatements.class);
			suite.addTestSuite(TestInvalidPrimitiveStatements.class);
			suite.addTestSuite(TestInvalidRepetitiveStatements.class);
			suite.addTestSuite(TestOperationCallActions.class);
			
			return	suite;
		}
}
