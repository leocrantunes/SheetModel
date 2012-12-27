/*
 * Created on Dec 5, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import junit.framework.Test;
import junit.framework.TestSuite;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestSemanticAnalysisSuite extends TestSuite {

		public static Test suite() { 
			TestSuite suite= new TestSuite();
		 
			suite.addTestSuite(TestArgument.class);
			suite.addTestSuite(TestAssociationClassCallExp.class);
			suite.addTestSuite(TestAssociationEndCallExp.class);
			suite.addTestSuite(TestAttributeCallExp.class);
			suite.addTestSuite(TestCollectionLiteralExp.class);
			suite.addTestSuite(TestCollectionOperationsExp.class);
			suite.addTestSuite(TestDefConstraints.class);
			suite.addTestSuite(TestEnumLiteralExp.class);
			suite.addTestSuite(TestIfExp.class);
			suite.addTestSuite(TestInitConstraints.class);
			suite.addTestSuite(TestInvConstraints.class);
			suite.addTestSuite(TestIterateExp.class);
			suite.addTestSuite(TestIteratorExp.class);
			suite.addTestSuite(TestLetExp.class);
			suite.addTestSuite(TestOclAnyOperations.class);
			suite.addTestSuite(TestOperationCallExp.class);
			suite.addTestSuite(TestPrePostConstraints.class);
			suite.addTestSuite(TestPrimitiveLiteralExp.class);
			suite.addTestSuite(TestTupleLiteralExp.class);
			suite.addTestSuite(TestTypeCS.class);
			suite.addTestSuite(TestVariableDeclaration.class);
			
			return suite;
	}

}
