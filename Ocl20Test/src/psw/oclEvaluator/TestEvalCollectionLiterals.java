/*
 * Created on May 14, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;


import ocl20.evaluation.OclValue;
import ocl20.expressions.CollectionLiteralExp;
import ocl20.expressions.OclExpression;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBagValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclOrderedSetValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclSequenceValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclSetValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalCollectionLiterals extends TestEval {

	public void testSetIntegerLiteral() {
		String	name = "testSetIntegerLiteral";
		doTestSetIntegerLiteral(name, "Set { 1, 2, 8, 3, 4, 5, 6, 7, 9, 0 } ", 10, 45);
		doTestSetIntegerLiteral(name, "Set { 1 } ", 1, 1);
		doTestSetIntegerLiteral(name, "Set { 0 } ", 1, 0);
		doTestSetIntegerLiteral(name, "Set { 1, 2 } ", 2, 3);
		doTestSetIntegerLiteral(name, "Set { 0..9 } ", 10, 45);
		doTestSetIntegerLiteral(name, "Set { 0..9, 10} ", 11, 55);
		doTestSetIntegerLiteral(name, "Set{ 1, 1}", 1, 1);
		doTestSetIntegerLiteral(name, "Set { 10, null, 20}", 3, 30);
		doTestSetIntegerLiteral(name, "Set { null, 10, 20}", 3, 30);
		doTestSetIntegerLiteral(name, "Set { null }", 1, 0);
		doTestInvalidCollectionLiteral(name, "Set { 10/0, 1.0, 2}", "Set(Real)");
		doTestInvalidCollectionLiteral(name, "Set { 1.0, 2, 10/0 }", "Set(Real)" );
		doTestSetIntegerLiteral(name, "Set {  }", 0, 0);
	}
	
	public	void	testEmptySet() {
		
		try {
			exp = compileExpression("testEmptySet", "Set { }");

			assertNotNull(exp);
			assertTrue(exp instanceof CollectionLiteralExp);
		
			OclSetValue value;
			value = (OclSetValue) oclEvaluator.evaluate(exp, null, null);
			assertEquals(0,  value.getElements().size());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}

	}

	public void testBagIntegerLiteral() {
		String	name = "testBagIntegerLiteral";
		doTestBagIntegerLiteral(name, "Bag { 1, 2, 8, 3, 4, 5, 6, 7, 9, 0, 3, 7, 4, 8, 5, 6, 9, 1, 2, 0 } ", 20, 90);
		doTestBagIntegerLiteral(name, "Bag { 1 } ", 1, 1);
		doTestBagIntegerLiteral(name, "Bag { 0 } ", 1, 0);
		doTestBagIntegerLiteral(name, "Bag { 2, 2 } ", 2, 4);
		doTestBagIntegerLiteral(name, "Bag { null } ", 1, 0);
		doTestBagIntegerLiteral(name, "Bag { null, null, null } ", 3, 0);
		doTestBagIntegerLiteral(name, "Bag { null, 1, 2, 1, 2, null, null } ", 7, 6);
		doTestBagIntegerLiteral(name, "Bag { 1, 2, null, 1, 2, null, null } ", 7, 6);
		
		doTestInvalidCollectionLiteral(name, "Bag { 10/0, 1.0, 2}", "Bag(Real)");
		doTestInvalidCollectionLiteral(name, "Bag { 1.0, 2, 10/0 }", "Bag(Real)" );

	}

	public void testSequenceIntegerLiteral() {
		String	name = "testSequenceIntegerLiteral";
		doTestSequenceIntegerLiteral(name, "Sequence { 1, 2, 8, 3, 4, 5, 6, 7, 9, 0, 3, 7, 4, 8, 5, 6, 9, 1, 2, 0 } ", 20, 90, getInt(1));
		doTestSequenceIntegerLiteral(name, "Sequence { 1 } ", 1, 1, getInt(1));
		doTestSequenceIntegerLiteral(name, "Sequence { 0 } ", 1, 0, getInt(0));
		doTestSequenceIntegerLiteral(name, "Sequence { 3, 3 } ", 2, 6, getInt(3));
		doTestSequenceIntegerLiteral(name, "Sequence { null } ", 1, 0, getNull());
		doTestSequenceIntegerLiteral(name, "Sequence{ null, null, null } ", 3, 0, getNull());
		doTestSequenceIntegerLiteral(name, "Sequence{ null, 1, 2, 1, 2, null, null } ", 7, 6, getNull());
		doTestSequenceIntegerLiteral(name, "Sequence{ 1, 2, 1, 2, null, null, null } ", 7, 6, getInt(1));
		
		doTestInvalidCollectionLiteral(name, "Sequence { 10/0, 1.0, 2}", "Sequence(Real)");
		doTestInvalidCollectionLiteral(name, "Sequence { 1.0, 2, 10/0 }", "Sequence(Real)" );

	}

	public void testOrderedSetIntegerLiteral() {
		String	name = "testOrderedSetIntegerLiteral";
		doTestOrderedSetIntegerLiteral(name, "OrderedSet { 1, 2, 8, 3, 4, 5, 6, 7, 9, 0 } ", 10, 45, getInt(1));
		doTestOrderedSetIntegerLiteral(name, "OrderedSet { 1 } ", 1, 1, getInt(1));
		doTestOrderedSetIntegerLiteral(name, "OrderedSet { 0 } ", 1, 0, getInt(0));
		doTestOrderedSetIntegerLiteral(name, "OrderedSet { 3, 4 } ", 2, 7, getInt(3));
		doTestOrderedSetIntegerLiteral(name, "OrderedSet { 10, null, 20}", 3, 30, getInt(10));
		doTestOrderedSetIntegerLiteral(name, "OrderedSet { null, 10, 20}", 3, 30, getNull());
		doTestOrderedSetIntegerLiteral(name, "OrderedSet { null }", 1, 0, getNull());
		
		doTestInvalidCollectionLiteral(name, "OrderedSet { 10/0, 1.0, 2}", "OrderedSet(Real)");
		doTestInvalidCollectionLiteral(name, "OrderedSet { 1.0, 2, 10/0 }", "OrderedSet(Real)" );
	}

	public void testNestedBagIntegerLiteral() {
		String	name = "testNestedBagIntegerLiteral";
		String	expression =  "Bag { Bag { 1, 2, 8, 3, 4, 5, 6, 7, 9, 0 }, Bag { 1, 2, 8, 3, 4, 5, 6, 7, 9, 0 }, Bag{ 1, 2, 8, 3, 4, 5, 6, 7, 9, 0 } } ";
		
		try {
			exp = compileExpression(name, expression);

			assertNotNull(exp);
			assertTrue(exp instanceof CollectionLiteralExp);
		
			OclBagValue value;
			value = (OclBagValue) oclEvaluator.evaluate(exp, null, null);
			assertEquals(3,  value.getElements().size());
			value = (OclBagValue) value.executeOperation("flatten", null);
			assertEquals(135, ((OclIntegerValue) value.executeOperation("sum", null)).intValue() );
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}


	private	void  doTestSetIntegerLiteral(String sourceStream, String expression, long expectedSetSize, long expectedSum) {
		try {
			exp = compileExpression(sourceStream, expression);

			assertNotNull(exp);
			assertTrue(exp instanceof CollectionLiteralExp);

			OclSetValue value;
			value = (OclSetValue) oclEvaluator.evaluate(exp, null, null);
			assertEquals(expectedSetSize,  value.getElements().size());
			assertEquals(expectedSum, ((OclIntegerValue) value.executeOperation("sum", null)).intValue() );
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	private	void  doTestBagIntegerLiteral(String sourceStream, String expression, long expectedSetSize, long expectedSum) {
		OclExpression exp;
		try {
			exp = compileExpression(sourceStream, expression);
			assertNotNull(exp);
			assertTrue(exp instanceof CollectionLiteralExp);

			OclBagValue value;
			value = (OclBagValue) oclEvaluator.evaluate(exp, null, null);
			assertEquals(expectedSetSize,  value.getElements().size());
			assertEquals(expectedSum, ((OclIntegerValue) value.executeOperation("sum", null)).intValue() );
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	private	void  doTestInvalidCollectionLiteral(String sourceStream, String expression, String collectionType) {
		try {
			exp = compileExpression(sourceStream, expression);

			assertNotNull(exp);
			assertTrue(exp instanceof CollectionLiteralExp);
			assertEquals(collectionType, exp.getType().getName());

			OclValue value;
			value = oclEvaluator.evaluate(exp, null, null);
			assertSame(value, getInvalid());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	private	void  doTestSequenceIntegerLiteral(String sourceStream, String expression, long expectedSetSize, long expectedSum, OclValue firstElement) {
		try {
			exp = compileExpression(sourceStream, expression);

			assertNotNull(exp);
			assertTrue(exp instanceof CollectionLiteralExp);

			OclSequenceValue value;
			value = (OclSequenceValue) oclEvaluator.evaluate(exp, null, null);
			assertEquals(expectedSetSize,  value.getElements().size());
			assertEquals(expectedSum, ((OclIntegerValue) value.executeOperation("sum", null)).intValue() );
			checkResult(firstElement, value.executeOperation("first", null));
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	private	void  doTestOrderedSetIntegerLiteral(String sourceStream, String expression, long expectedSetSize, long expectedSum, OclValue firstElement) {
		try {
			exp = compileExpression(sourceStream, expression);

			assertNotNull(exp);
			assertTrue(exp instanceof CollectionLiteralExp);

			OclOrderedSetValue value;
			value = (OclOrderedSetValue) oclEvaluator.evaluate(exp, null, null);
			assertEquals(expectedSetSize,  value.getElements().size());
			assertEquals(expectedSum, ((OclIntegerValue) value.executeOperation("sum", null)).intValue() );
			checkResult(firstElement, value.executeOperation("first", null));
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
}
