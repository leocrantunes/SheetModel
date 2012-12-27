/*
 * Created on Nov 28, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import ocl20.expressions.EnumLiteralExp;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.expression.CSTClassifierAttributeCallExpCS;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEnumLiteralExp extends TestLiteralExp {

	public void testEnumOK_01() {
		doTestEnumOK("Situation", "Situation", "married", this.getCurrentMethodName());
	}

	public void testEnumOK_02() {
		doTestEnumOK("Situation", "Situation", "single", this.getCurrentMethodName());
	}
	
	public void testEnumOK_03() {
		doTestEnumOK("MyExample::Situation", "Situation", "married", this.getCurrentMethodName());
	}

	public void testEnumError_01() {
		parseWithError("Situation::other", this.getCurrentMethodName());
	}

	public void testEnumError_02() {
		parseWithError("Foo::married", this.getCurrentMethodName());
	}

	public void testEnumError_03() {
		parseWithError("married", this.getCurrentMethodName());
	}
	
	public void testEnumError_04() {
		parseWithError("Situation::married@pre", this.getCurrentMethodName());
	}

	public void doTestEnumOK(String enumFullName, String enumName, String enumLiteral, String source) {
		CSTNode node = parseOK(enumFullName + "::" +  enumLiteral, source);
		assertTrue(node instanceof CSTClassifierAttributeCallExpCS);
		CSTClassifierAttributeCallExpCS literalExp = (CSTClassifierAttributeCallExpCS) node;
		
		assertNotNull(literalExp.getAst());
		assertTrue(literalExp.getAst() instanceof EnumLiteralExp);
		EnumLiteralExp ast = (EnumLiteralExp) literalExp.getAst();
		
		assertEquals(enumLiteral, ast.getReferredEnumLiteral().getName());
		
		assertNotNull(ast.getType());
		assertEquals(enumName, ast.getType().getName()); 
	}
	
	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic.TestLiteralExp#getSpecificNodeClass()
	 */
	protected Class getSpecificNodeClass() throws Exception {
		return CSTClassifierAttributeCallExpCS.class;
	}

}
