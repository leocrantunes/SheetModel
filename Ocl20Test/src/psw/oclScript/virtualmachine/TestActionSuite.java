/*
 * Created on Apr 16, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine;

import junit.framework.Test;
import junit.framework.TestSuite;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestActionSuite extends TestSuite {

	public TestActionSuite() {
	}
	
	public static Test suite()  {
		TestSuite suite = new TestSuite();

		suite.addTestSuite(TestPrimitiveActions.class);
		suite.addTestSuite(TestCompositeActions.class);
		suite.addTestSuite(TestCollectionAction.class);
		suite.addTestSuite(TestJumpActions.class);
		
		return	suite;
	}
}
