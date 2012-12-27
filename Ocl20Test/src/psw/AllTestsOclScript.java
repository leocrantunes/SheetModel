package br.ufrj.cos.lens.odyssey.tools.psw;

import junit.framework.Test;
import junit.framework.TestSuite;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.compiler.TestOclScriptCompilerSuite;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.TestActionSuite;

public class AllTestsOclScript extends TestSuite {

	public static Test suite() throws Exception {
		TestSuite suite= new TestSuite();
 
		suite.addTest(TestOclScriptCompilerSuite.suite());
		suite.addTest(TestActionSuite.suite());
		
		return suite;
}

}
