/*
 * Created on Nov 27, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import ocl20.common.CoreClassifier;
import ocl20.expressions.BooleanLiteralExp;
import ocl20.expressions.IntegerLiteralExp;
import ocl20.expressions.RealLiteralExp;
import ocl20.expressions.StringLiteralExp;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTBooleanLiteralExpCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTIntegerLiteralExpCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTRealLiteralExpCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTStringLiteralExpCS;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestPrimitiveLiteralExp extends TestLiteralExp {
	


	/* (non-Javadoc)
	* @see junit.framework.TestCase#setUp()
	*/

	public void testBooleanExpOK_01() {
		doTestBoolean(true);
	}

	public void testBooleanExpOK_02() {
		doTestBoolean(false);
	}

	public void testIntegerExpOK_01() {
		doTestInteger(3000);
	}

	public void testIntegerExpOK_02() {
		doTestInteger(0);
	}

	public void testRealExpOK_01() {
		doTestReal("100.0", 100.0);
	}

	public void testRealExpOK_02() {
		doTestReal("100.0E2", 10000.0);
	}

	public void testRealExpOK_03() {
		doTestReal("100.0e2", 10000.0);
	}

	public void testRealExpOK_04() {
		doTestReal("100.0e+2", 10000.0);
	}

	public void testRealExpOK_05() {
		doTestReal("100.0e-2", 1.0);
	}

	public void testRealExpOK_06() {
		doTestReal("100e2", 10000.0);
	}

	public void testStringExpOK_01() {
		doTestString("\"alex\"", "alex");
	}

	public void testStringExpOK_02() {
		doTestString("\"\"", "");
	}

	private void doTestBoolean(boolean expectedValue) {
		CSTNode node = parseOK (String.valueOf(expectedValue), this.getCurrentMethodName());
		assertTrue(node instanceof CSTBooleanLiteralExpCS);
		CSTBooleanLiteralExpCS literalExp = (CSTBooleanLiteralExpCS) node;
		
		assertNotNull(literalExp.getAst());
		assertTrue(literalExp.getAst() instanceof BooleanLiteralExp);
		BooleanLiteralExp ast = (BooleanLiteralExp) literalExp.getAst();
		assertEquals(expectedValue, ast.isBooleanSymbol());
		CoreClassifier type = ast.getType();
		assertNotNull(type);
		assertEquals("Boolean", type.getName()); 
	}

	private void doTestInteger(int expectedValue) {
		CSTNode node = parseOK (String.valueOf(expectedValue), this.getCurrentMethodName());
		assertTrue(node instanceof CSTIntegerLiteralExpCS);
		CSTIntegerLiteralExpCS literalExp = (CSTIntegerLiteralExpCS) node;
		assertNotNull(literalExp.getAst());
		assertTrue(literalExp.getAst() instanceof IntegerLiteralExp);
		IntegerLiteralExp ast = (IntegerLiteralExp) literalExp.getAst();
		assertEquals(expectedValue, ast.getIntegerSymbol());
		CoreClassifier type = ast.getType();
		assertNotNull(type);
		assertEquals("Integer", type.getName()); 
	}

	private void doTestReal(String expectedValueStr, double expectedValue) {
		CSTNode node = parseOK (expectedValueStr, this.getCurrentMethodName());
		assertTrue(node instanceof CSTRealLiteralExpCS);
		CSTRealLiteralExpCS literalExp = (CSTRealLiteralExpCS) node;
		assertNotNull(literalExp.getAst());
		assertTrue(literalExp.getAst() instanceof RealLiteralExp);
		RealLiteralExp ast = (RealLiteralExp) literalExp.getAst();
		assertEquals(expectedValue, Double.parseDouble(ast.getRealSymbol()), 0.01);
		CoreClassifier type = ast.getType();
		assertNotNull(type);
		assertEquals("Real", type.getName()); 
	}

	private void doTestString(String input, String expectedValue) {
		CSTNode node = parseOK (input, this.getCurrentMethodName());
		assertTrue(node instanceof CSTStringLiteralExpCS);
		CSTStringLiteralExpCS literalExp = (CSTStringLiteralExpCS) node;
		
		assertNotNull(literalExp.getAst());
		assertTrue(literalExp.getAst() instanceof StringLiteralExp);
		StringLiteralExp ast = (StringLiteralExp) literalExp.getAst();
		assertEquals(expectedValue, ast.getStringSymbol());
		CoreClassifier type = ast.getType();
		assertNotNull(type);
		assertEquals("String", type.getName()); 
	}
}
