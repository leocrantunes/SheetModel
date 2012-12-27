/*
 * Created on Nov 30, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;



import ocl20.common.CoreClassifier;
import ocl20.expressions.CollectionKindEnum;
import ocl20.expressions.CollectionLiteralExp;
import ocl20.types.BagType;
import ocl20.types.CollectionType;
import ocl20.types.SetType;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.literalExp.CSTCollectionLiteralExpCS;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestCollectionLiteralExp extends TestLiteralExp  {

	public void testSetLiteral_01() {
		testCollectionLiteral("Set{ 1, 2, 3} ", "Integer", "Set(Integer)", 3, CollectionKindEnum.SET);
		testCollectionLiteral("Bag{ 1, 2, 3} ", "Integer", "Bag(Integer)", 3, CollectionKindEnum.BAG);
		testCollectionLiteral("Sequence{ 1, 2, 3} ", "Integer", "Sequence(Integer)", 3, CollectionKindEnum.SEQUENCE);
		testCollectionLiteral("OrderedSet{ 1, 2, 3} ", "Integer", "OrderedSet(Integer)", 3, CollectionKindEnum.ORDERED_SET);
	}

	public void testSetLiteral_02() {
		testCollectionLiteral("Set{ 1.4, 2.3, 3.9} ", "Real", "Set(Real)", 3, CollectionKindEnum.SET);
		testCollectionLiteral("Bag{ 1.4, 2.3, 3.9} ", "Real", "Bag(Real)", 3, CollectionKindEnum.BAG);
		testCollectionLiteral("Sequence{ 1.4, 2.3, 3.9} ", "Real", "Sequence(Real)", 3, CollectionKindEnum.SEQUENCE);
		testCollectionLiteral("OrderedSet{ 1.4, 2.3, 3.9} ", "Real", "OrderedSet(Real)", 3, CollectionKindEnum.ORDERED_SET);
	}

	public void testSetLiteral_03() {
		testCollectionLiteral("Set{ \"alex\", \"john\"} ", "String", "Set(String)", 2, CollectionKindEnum.SET);
	}
	
	public void testSetLiteral_04() {
		testCollectionLiteral("Set{ true, false} ", "Boolean", "Set(Boolean)", 2, CollectionKindEnum.SET);
	}

	public void testSetLiteral_05() {
		testCollectionLiteral("Set{ 1 } ", "Integer", "Set(Integer)", 1, CollectionKindEnum.SET);
	}

	public void testSetLiteral_06() {
		testCollectionLiteral("Set{ 1..8 } ", "Integer", "Set(Integer)", 1, CollectionKindEnum.SET);
	}

	public void testSetLiteral_07() {
		CollectionLiteralExp literalExp = doTestOK("Bag{ Set{1, 2}, Set{4, 8} } ");
		CoreClassifier expectedType = (CoreClassifier) environment.lookup("Integer");
		assertNotNull(expectedType);
		assertTrue(literalExp.getType() instanceof BagType);
		
		CollectionType collectionType = (CollectionType) literalExp.getType();
		assertTrue(collectionType.getElementType() instanceof SetType);

		assertEquals(2, literalExp.getParts().size());
		assertEquals(CollectionKindEnum.BAG, literalExp.getKind());
	}

	public void testSetLiteral_08() {
		testCollectionLiteral("Set{ } ", "OclVoid", "Set(OclVoid)", 0, CollectionKindEnum.SET);
	}

	public void testSetLiteral_09() {
		testCollectionLiteral("Set{ 1, 3.9, 5 } ", "Real", "Set(Real)", 3, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ 1, false } ", "OclAny", "Set(OclAny)", 2, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ 1, false, 4.9, Set{1}, Bag{Set{1,2}, Set{4,5} } }", "OclAny", "Set(OclAny)", 5, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ Bag{Set{1,2}, Set{4,5} }, 1, false, 4.9, Set{1} }", "OclAny", "Set(OclAny)", 5, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ Bag{Set{1,2}, Set{}, Set{4,5} }, Set{}, false, 4.9, Set{1} }", "OclAny", "Set(OclAny)", 5, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ Set{1}, 1 } ", "OclAny", "Set(OclAny)", 2, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ Set{1}, Set{} } ", "Set(Integer)", "Set(Set(Integer))", 2, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ Set{3.9}, Set{1} } ", "Set(Real)", "Set(Set(Real))", 2, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ Set{3}, Bag{1} } ", "Collection(Integer)", "Set(Collection(Integer))", 2, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ Set{3.9}, Bag{1} } ", "Collection(Real)", "Set(Collection(Real))", 2, CollectionKindEnum.SET);
		testCollectionLiteral("Bag{ Set{3.9, 4.8}, Sequence{1, 4} } ", "Collection(Real)", "Bag(Collection(Real))", 2, CollectionKindEnum.BAG);
		testCollectionLiteral("Bag{ Set{3.9, 4.8}, Sequence{1, 4}, OrderedSet{false, true} } ", "Collection(OclAny)", "Bag(Collection(OclAny))", 3, CollectionKindEnum.BAG);
		testCollectionLiteral("Bag{ Sequence{3.9, 4.8}, Sequence{1, 4}, Sequence{false, true} } ", "Sequence(OclAny)", "Bag(Sequence(OclAny))", 3, CollectionKindEnum.BAG);
		testCollectionLiteral("Set{ 1, Set{1} } ", "OclAny", "Set(OclAny)", 2, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ false, Set{1} } ", "OclAny", "Set(OclAny)", 2, CollectionKindEnum.SET);
		testCollectionLiteral("Set{ Set{3.9}, Bag{ Set{ 1, 2} } }", "Collection(OclAny)", "Set(Collection(OclAny))", 2, CollectionKindEnum.SET);
	}
	
	public void testSetLiteralError_01() {
		this.parseWithError("Collection{ 1, 2} ", this.getCurrentMethodName());
		this.parseWithError("Set{ 1, 2, } ", this.getCurrentMethodName());
	}

	public void testSetLiteralError_02() {
		this.parseWithError("Set{ 1.. 2.9 } ", this.getCurrentMethodName());
		this.parseWithError("Set{ 3.9.. 5 } ", this.getCurrentMethodName());
		this.parseWithError("Set{ 3.9.. 5.4 } ", this.getCurrentMethodName());
		this.parseWithError("Set{ false..true } ", this.getCurrentMethodName());
	}


	public void testSameTypeInstance() {
		doTestSameCollectionTypeInstance("Bag{1, 2}",  "Bag{1, 3, 5}", true);
		doTestSameCollectionTypeInstance("Set{1, 2}",  "Set{1, 3, 5}", true);
		doTestSameCollectionTypeInstance("Sequence{1, 2}",  "Sequence{1, 3, 5}", true);
		doTestSameCollectionTypeInstance("OrderedSet{1, 2}",  "OrderedSet{1, 3, 5}", true);
		doTestSameCollectionTypeInstance("Bag{1, 2}",  "Set{1, 3, 5}", false);
		doTestSameCollectionTypeInstance("Sequence{1, 2}",  "Set{1, 3, 5}", false);
		doTestSameCollectionTypeInstance("OrderedSet{1, 2}",  "Set{1, 3, 5}", false);
	}

	private	void	doTestSameCollectionTypeInstance(String expression1, String expression2, boolean isEqual) {
		CollectionLiteralExp literalExp1 = doTestOK(expression1);
		assertTrue(literalExp1.getType() instanceof CollectionType);
		CollectionType collectionType1 = (CollectionType) literalExp1.getType();
		
		CollectionLiteralExp literalExp2 = doTestOK(expression2);
		assertTrue(literalExp2.getType() instanceof CollectionType);
		CollectionType collectionType2 = (CollectionType) literalExp2.getType();

		if (isEqual)
			assertEquals(collectionType1.getName(), collectionType2.getName());
		else
			assertFalse(collectionType1.getName().equals(collectionType2.getName()));
	
	}

	
	
	public CollectionLiteralExp doTestOK(String literal) {
		CSTNode node = parseOK(literal, this.getCurrentMethodName());
		assertTrue(node instanceof CSTCollectionLiteralExpCS);
		CSTCollectionLiteralExpCS literalExp = (CSTCollectionLiteralExpCS) node;
		
		assertNotNull(literalExp.getAst());
		assertTrue(literalExp.getAst() instanceof CollectionLiteralExp);
		return (CollectionLiteralExp) literalExp.getAst();
	}

	public void testCollectionLiteral(String expression, String expectedElementTypeName, String collectionName, int partsSize, CollectionKindEnum collectionKind) {
		CollectionLiteralExp literalExp = doTestOK(expression);
		assertTrue(literalExp.getType() instanceof CollectionType);
		CollectionType collectionType = (CollectionType) literalExp.getType();
		assertEquals(collectionName, collectionType.getName());
		assertEquals(expectedElementTypeName, collectionType.getElementType().getName());
		assertEquals(partsSize, literalExp.getParts().size());
		assertEquals(collectionKind, literalExp.getKind());
	}
	
}
