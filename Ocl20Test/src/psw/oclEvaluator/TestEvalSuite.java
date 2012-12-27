/*
 * Created on May 15, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalSuite extends TestCase {
	static	public	Test	suite() {
		TestSuite suite = new TestSuite();
		suite.addTestSuite(TestEvalEnvironment.class);
		suite.addTestSuite(TestEvalPrimitiveLiterals.class);
		suite.addTestSuite(TestEvalCollectionLiterals.class);
		suite.addTestSuite(TestEvalEnumLiterals.class);
		suite.addTestSuite(TestEvalOperationCallExp.class);
		suite.addTestSuite(TestEvalBooleanOperations.class);
		suite.addTestSuite(TestEvalIntegerOperations.class);
		suite.addTestSuite(TestEvalRealOperations.class);
		suite.addTestSuite(TestEvalStringOperations.class);
		suite.addTestSuite(TestEvalDateOperations.class);
		suite.addTestSuite(TestEvalDateTimeOperations.class);
		suite.addTestSuite(TestEvalSetOperations.class);
		suite.addTestSuite(TestEvalBagOperations.class);
		suite.addTestSuite(TestEvalOrderedSetOperations.class);
		suite.addTestSuite(TestEvalSequenceOperations.class);
		suite.addTestSuite(TestEvalTupleLiterals.class);
		suite.addTestSuite(TestEvalAttributeCallExp.class);
		suite.addTestSuite(TestEvalAssociationEndCallExp.class);
		suite.addTestSuite(TestEvalIfExp.class);
		suite.addTestSuite(TestEvalLetExp.class);
		suite.addTestSuite(TestEvalIterator.class);
		suite.addTestSuite(TestEvalCastingOperations.class);
		suite.addTestSuite(TestEvalInvariants.class);
		suite.addTestSuite(TestPreConditionsEvaluator.class);
		suite.addTestSuite(TestPostConditionEvaluator.class);
		
		
		return	suite;
	}

}
