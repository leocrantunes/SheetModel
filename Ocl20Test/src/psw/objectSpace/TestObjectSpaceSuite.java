/*
 * Created on May 13, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.objectSpace;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.persistence.TestOclValuePersistence;
import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestObjectSpaceSuite extends TestCase {

	public static Test	suite() {
		TestSuite	suite = new TestSuite();
		suite.addTestSuite(TestPSWObjectValue.class);
		suite.addTestSuite(TestPSWObjectSpace.class);
		suite.addTestSuite(TestOclValuePersistence.class);

		return	suite;
	}
}
