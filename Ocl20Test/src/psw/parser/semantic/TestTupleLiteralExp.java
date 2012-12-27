/*
 * Created on Nov 30, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import ocl20.expressions.TupleLiteralExp;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTTupleLiteralExpCS;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestTupleLiteralExp extends TestLiteralExp  {
	

	public void testTupleLiteralWithDuplicateNames_03() {
		parseWithError("Tuple{a:Integer = 10, b : Boolean = true, a: Integer = 9}", this.getCurrentMethodName());	
	}

	public void testTupleLiteralOK_01() {
		doTestOK("Tuple{a:Integer = 10, b:Boolean = true, c: Real = 9.5}", "Tuple(a : Integer, b : Boolean, c : Real)", this.getCurrentMethodName());	
	}
	
	public void testTupleLiteralOK_03() {
		doTestOK("Tuple{a:Integer = 10, s:Set(Integer) = Set {1, 5}}", "Tuple(a : Integer, s : Set(Integer))", this.getCurrentMethodName());	
	}

	public void testTupleLiteralOK_02() {
		doTestOK("Tuple{a:Integer = 10}", "Tuple(a : Integer)", this.getCurrentMethodName());	
	}

	public void testTupleLiteralWithoutType() {
		doTestOK("Tuple{a:Integer = 10, b = true, c: Real = 9.5}", "Tuple(a : Integer, b : Boolean, c : Real)", this.getCurrentMethodName());
	}

	public void testTupleLiteralWithoutType_02() {
		doTestOK("Tuple{a = 10, s = Set {1, 5}}", "Tuple(a : Integer, s : Set(Integer))", this.getCurrentMethodName());	
	}

	public void testTupleLiteralWithoutType_03() {
		doTestOK("Tuple{a = 10, s = Set {Set{1, 5}, Bag{4.9, 5.9}}}", "Tuple(a : Integer, s : Set(Collection(Real)))", this.getCurrentMethodName());	
	}


	public void testTupleLiteralWithTypeConformanceError() {
		parseWithError("Tuple{a:Integer = 10, b:Boolean = 10, c: Real = 9.5}", this.getCurrentMethodName());	
	}

	public void testTupleLiteralWithTypeConformanceError_02() {
		parseWithError("Tuple{a = 10, s : Set(Set(Real)) = Set {Set{1, 5}, Bag{4.9, 5.9}}}", this.getCurrentMethodName());	
	}

	public void testTupleLiteralWithoutInitialization() {
		parseWithError("Tuple{a:Integer = 10, b:Boolean, c: Real = 9.5}", this.getCurrentMethodName());	
	}

	public void testTupleLiteralWithDuplicateNames() {
		parseWithError("Tuple{a:Integer = 10, a : Boolean = true, c: Real = 9.5}", this.getCurrentMethodName());	
	}

	public void testTupleLiteralWithDuplicateNames_02() {
		parseWithError("Tuple{a:Integer = 10, b : Boolean = true, a: Real = 9.5}", this.getCurrentMethodName());	
	}

	
	public void testTupleLiteralWithoutTypeAndInitialization() {
		parseWithError("Tuple{a:Integer = 10, d, c: Real = 9.5}", this.getCurrentMethodName());	
	}

	
	public TupleLiteralExp doTestOK(String literal, String typeName, String callerMethodName) {
		CSTNode node = parseOK(literal, callerMethodName);
		assertTrue(node instanceof CSTTupleLiteralExpCS);
		CSTTupleLiteralExpCS literalExp = (CSTTupleLiteralExpCS) node;
		
		assertNotNull(literalExp.getAst());
		assertTrue(literalExp.getAst() instanceof TupleLiteralExp);
		assertEquals(typeName, literalExp.getAst().getType().getName());
		return (TupleLiteralExp) literalExp.getAst();
	}
}
