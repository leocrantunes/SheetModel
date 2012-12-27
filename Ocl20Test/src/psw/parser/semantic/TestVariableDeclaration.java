/*
 * Created on Nov 30, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import ocl20.expressions.VariableDeclaration;
import ocl20.types.TupleType;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.type.CSTVariableDeclarationCS;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestVariableDeclaration extends TestNodeCS {

	public void testPrimitiveVariableOK() {
		doTestPrimitiveOK("a : Boolean = true", "a", "Boolean");
		doTestPrimitiveOK("a : String = \"joao\"", "a", "String");
		doTestPrimitiveOK("a : Integer = 2", "a", "Integer");
		doTestPrimitiveOK("a : Real = 2.3", "a", "Real");
		doTestPrimitiveOK("a : Real = 2", "a", "Real");
		doTestPrimitiveOK("a = 2", "a", "Integer");
		doTestPrimitiveOK("a : Integer", "a", "Integer");
	}
	
	public void testEnumerationVariableOK() {
		doTestPrimitiveOK("a : Situation = Situation::married", "a", "Situation");
	}

	public void testSetVariableOK_01() {
		doTestSetOK("a : Set(Integer) = Set { 1, 2, 3} ", "a", "Set(Integer)");
	}

	public void testSetVariableOK_02() {
		doTestSetOK("a : Set(Real) = Set { 1.4, 2.3, 3.8} ", "a", "Set(Real)");
	}

	public void testSetVariableOK_03() {
		doTestSetOK("a : Set(Real) = Set { 1.4, 21, 3.8} ", "a", "Set(Real)");
	}

	public void testSetVariableOK_04() {
		doTestSetOK("a : Set(Set(Integer)) = Set {Set { 1, 2, 3}, Set { 4, 5, 6}} ", "a", "Set(Set(Integer))");
	}

	public void testSetVariableOK_05() {
		doTestSetOK("a : Set(Bag(Set(Integer))) = Set {Bag{Set { 1, 2, 3}, Set { 4, 5, 6}}, Bag{Set{4,  8, 9}}} ", "a", "Set(Bag(Set(Integer)))");
	}
	
	public void testSetVariableOK_06() {
		doTestSetOK("a : Collection(Real) = Set { 1.4, 21, 3.8} ", "a", "Collection(Real)");
	}

	public void testSetVariableOK_07() {
		doTestSetOK("a : Collection(Real) = Sequence { 1.4, 21, 3.8} ", "a", "Collection(Real)");
	}
	
	public void testSetVariableOK_08() {
		doTestSetOK("a : Collection(Real) = Bag { 1.4, 21, 3.8} ", "a", "Collection(Real)");
	}

	public void testSetVariableOK_09() {
		doTestSetOK("a = Bag { 1.4, 21, 3.8} ", "a", "Bag(Real)");
	}

	public void testTupleVariableOK_01() {
		doTestTupleOK("x : Tuple(a: Integer, b: Boolean) = Tuple { a : Integer = 10, b : Boolean = true} ");
	}

	public void testTupleVariableOK_02() {
		doTestTupleOK("x : Tuple(a: Real, b: Boolean) = Tuple {  b : Boolean = true, a : Integer = 5} ");
	}

	public void testTupleVariableOK_03() {
		doTestTupleOK("x : Tuple(a: Set(Integer), b: Bag(Boolean)) = Tuple {  a : Set(Integer) = Set { 1, 3, 5}, b : Bag(Boolean) = Bag { true, false} } ");
	}

	public void testTupleVariableOK_04() {
		doTestTupleOK("x : Tuple(a: Set(Real), b: Bag(Boolean)) = Tuple {  a : Set(Integer) = Set { 1, 3, 5}, b : Bag(Boolean) = Bag { true, false} } ");
	}

	public void testTupleVariableOK_05() {
		doTestTupleOK("x : Tuple(a: Set(Real), b: Bag(Boolean)) = Tuple {  b : Bag(Boolean) = Bag { true, false}, a : Set(Integer) = Set { 1, 3, 5}} ");
	}


	public void testPrimitiveVariableError_01() {
		doTestWithError("a : Integer = true", this.getCurrentMethodName());
	}

	public void testPrimitiveVariableError_02() {
		doTestWithError("a : Integer = 2.3", this.getCurrentMethodName());
	}

	public void testPrimitiveVariableError_03() {
		doTestWithError("a : Integer = \"joao\"", this.getCurrentMethodName());
	}

	public void testEnumerationVariableError_01() {
		doTestWithError("a : Situation = 2", this.getCurrentMethodName());
	}

	public void testEnumerationVariableError_02() {
		doTestWithError("a : Situation = Situation::other", this.getCurrentMethodName());
	}

	public void testSetVariableError_01() {
		doTestWithError("a : Set(Real) = Set { 1.4, \"alex\", 3.8} ", this.getCurrentMethodName());
	}

	public void testSetVariableError_02() {
		doTestWithError("a : Set(String) = Set { 1.4, \"alex\", 3.8} ", this.getCurrentMethodName());
	}

	public void testSetVariableError_03() {
		doTestWithError("a : Set(Set(Integer)) = Set {Set { 1, 2, 3}, Set { false, 5, 6}} ", this.getCurrentMethodName());
	}

	public void testSetVariableError_04() {
		doTestWithError("a : Set(Bag(Set(Integer))) = Set {Set { 1, 2, 3}, Set { 4, 5, 6}} ", this.getCurrentMethodName());
	}

	public void testSetVariableError_05() {
		doTestWithError("a : Collection(String) = Set { 1.4, \"alex\", 3.8} ", this.getCurrentMethodName());
	}

	public void testTupleVariableError_01() {
		doTestWithError("x : Tuple(a: Set(Integer), b: Bag(Boolean)) = Tuple {  x : Set(Integer) = Set { 1, 3, 5}, b : Bag(Boolean) = Bag { true, false} } ", this.getCurrentMethodName());
	}

	public void testTupleVariableError_02() {
		doTestWithError("a : Tuple(a: Set(Integer), b: Bag(Boolean)) = Tuple {  a : Set(Real) = Set { 1.0, 3.0, 5.0}, b : Bag(Boolean) = Bag { true, false} } ", this.getCurrentMethodName());
	}

	public void testTupleVariableError_03() {
		doTestWithError("a : Tuple(a: Set(Integer), b: Bag(Boolean)) = Tuple {  a : Set(Real) = Set { 1.0, 3.0, 5.0}} ", this.getCurrentMethodName());
	}

	protected VariableDeclaration doTestPrimitiveOK(String literal, String varName, String varType) {
		VariableDeclaration varDecl = getVariableDeclaration(literal);
		
		assertEquals(varName, varDecl.getVarName());
		assertEquals(environment.lookup(varType), varDecl.getType());
		
		return varDecl;
	}

	protected VariableDeclaration doTestSetOK(String literal, String varName, String varType) {
		VariableDeclaration varDecl = getVariableDeclaration(literal);
		
		assertEquals(varName, varDecl.getVarName());
		assertEquals(varType, varDecl.getType().getName());
		
		return	varDecl;
	}

	protected VariableDeclaration doTestTupleOK(String literal) {
		VariableDeclaration varDecl = getVariableDeclaration(literal);
		assertTrue(varDecl.getInitExpression().getType() instanceof TupleType);
		return varDecl;
	}

	protected void doTestWithError(String literal, String source) {
		parseWithError(literal, source);
	}

	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic.TestNodeCS#getSpecificNodeClass()
	 */
	protected Class getSpecificNodeClass() throws Exception {
		return CSTVariableDeclarationCS.class;
	}


	protected	VariableDeclaration	getVariableDeclaration(String literal) {
		CSTNode node = parseOK(literal, this.getCurrentMethodName());
		assertTrue(node instanceof CSTVariableDeclarationCS);
		CSTVariableDeclarationCS variable = (CSTVariableDeclarationCS) node;
		return (VariableDeclaration) variable.getAst();
	}
}
