/*
 * Created on Dec 7, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import ocl20.common.CoreClassifier;
import ocl20.expressions.OclExpression;
import ocl20.types.OclModelElementType;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTArgumentCS;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestArgument extends TestNodeCS {


	public void testArgument_01() {
		doTestOK("100", "Integer", this.getCurrentMethodName());
	}

	public void testArgument_02() {
		doTestOK("Set{1, 3, 5}", "Set(Integer)", this.getCurrentMethodName());
	}
	
	public void testArgument_03() {
		CoreClassifier c = doTestOK("Film", "OclModelElementType", this.getCurrentMethodName());
		checkReferredOclType(c, "Film");
	}

	public void testArgument_04() {
		CoreClassifier c = doTestOK("MyExample::Film", "OclModelElementType", this.getCurrentMethodName());
		checkReferredOclType(c, "Film");
	}

	public void testArgument_05() {
		doTestOK("Situation::married", "Situation", this.getCurrentMethodName());
	}

	public void testArgument_06() {
		doTestOK("Rental::maxDaysToReturn", "Integer", this.getCurrentMethodName());
	}


	protected CoreClassifier doTestOK(String expression, String expectedTypeName, String testName)  {
		CSTNode rootNode = parseOK(expression, testName);
		return checkResult(rootNode, expectedTypeName);
	}

	
	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic.TestNodeCS#getSpecificNodeClass()
	 */
	protected Class getSpecificNodeClass() throws Exception {
		return CSTArgumentCS.class;
	}


	private	CoreClassifier	checkResult(CSTNode rootNode, String expectedTypeName) {
		CSTArgumentCS argument = (CSTArgumentCS) rootNode;
		OclExpression ast = argument.getAst();
		assertEquals(expectedTypeName, ast.getType().getName());
		return	ast.getType();
	}

	private	void	checkReferredOclType(Object c, String expectedClassifierName) {
		assertTrue(c instanceof OclModelElementType);
		OclModelElementType ast = (OclModelElementType) c;
		assertEquals(expectedClassifierName, ast.getReferredModelElement().getName());
	}
}
