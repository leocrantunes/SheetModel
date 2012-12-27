/*
 * Created on Apr 30, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestValuesSuite extends TestCase {
	public static Test	suite() {
		TestSuite	suite = new TestSuite();
		suite.addTestSuite(TestBagValue.class);
		suite.addTestSuite(TestBooleanValue.class);
		suite.addTestSuite(TestDateTimeValue.class);
		suite.addTestSuite(TestDateValue.class);
		suite.addTestSuite(TestIntegerValue.class);
		suite.addTestSuite(TestOrderedSetValue.class);
		suite.addTestSuite(TestRealValue.class);
		suite.addTestSuite(TestSequenceValue.class);
		suite.addTestSuite(TestStringValue.class);
		suite.addTestSuite(TestSetValue.class);

		return	suite;
	}
}
