/*
 * Created on Jul 9, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import ocl20.expressions.OclExpression;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestEvalIterator extends TestEval {
	private	IEvalEnvironment	evalEnv;
	
	private	String tuplesSet = 
		"Set { Tuple{ name : String = \"John\", age : Integer = 18, grade : Real = 84.7 }, " +
		 		"Tuple{ name : String = \"Daniel\", age : Integer = 21, grade : Real = 88.7 }, " +
				"Tuple{ name : String = \"Alex\", age : Integer = 21, grade : Real = 94.7 }, "+
				"Tuple{ name : String = \"Mary\", age : Integer = 16, grade : Real = 64.7 }, " +
				"Tuple{ name : String = \"Paul\", age : Integer = 25, grade : Real = 54.7 } }";

	private	String specialTuplesSet = 
		"Set { Tuple{ name : String = \"John\", age : Integer = 18, grade : Real = 84.7 }, " +
				"Tuple{ name : String = \"Daniel\", age : Integer = 21, grade : Real = 88.7 }, " +
				"Tuple{ name : String = \"Alex\", age : Integer = 21, grade : Real = 94.7 }, "+
				"Tuple{ name : String = \"Mary\", age : Integer = null, grade : Real = 64.7 }, " +
				"Tuple{ name : String = \"Paul\", age : Integer = 25, grade : Real = 54.7 } }";

	private String emptyIntegerSet = "(Set{1}-Set{1})";
	private String nullIntegerSet = "(Set{1, null}-Set{1})";
	
	
	public void	testExists() {
		String name = "testExists";
		evaluateBooleanResult(name, "Set{1, 2, 3}->exists(x | x > 2)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->exists(x | x > 5)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3}->exists(x | Set{3, 4, 5}->exists(y | y = x))", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->exists(x | Set{6, 4, 5}->includes(x))", false);
		evaluateBooleanResult(name, tuplesSet + "->exists(x | x.age > 20 and x.age < 30)", true);
		evaluateBooleanResult(name, tuplesSet + "->exists(x | x.grade < 50.0)", false);
		
		evaluateBooleanResult(name, "Set{1, 2, 3, null}->exists(x | x > 2)", true);
		evaluateBooleanResult(name, "Set{1, 2, null, 5}->exists(x | x > 2)", true);
		evaluateBooleanResult(name, emptyIntegerSet + "->exists(x | x > 5)", false);
		evaluateNullResult(name, nullIntegerSet + "->exists(x | x > 5)");
		evaluateNullResult(name, "Set{1, 2, 3, null}->exists(x | x > 5)");
		evaluateInvalidResult(name, "Set{1, 2 / 0, 3, null}->exists(x | x > 5)");
	}

	public void	testForAll() {
		String name = "testForAll";
		evaluateBooleanResult(name, "Set{1, 2, 3}->forAll(x | x > 0)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->forAll(x | x >= 2)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3}->forAll(x | x = 1)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->forAll(x | x < 15 and x > 0)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->forAll(x1, x2 | x1 + x2 >= 2)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->forAll(x1, x2 | x1 + x2 > 3)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->forAll(x1, x2 | x1 <> x2 implies x1 + x2 >= 3)", true);
		
		evaluateNullResult(name, "Set{1, 2, 3, null}->forAll(x | x > 0)");
		evaluateBooleanResult(name, "Set{1, 2, 3, null}->forAll(x | x > 1)", false);
		evaluateBooleanResult(name, "Set{null, 1, 2, 3}->forAll(x | x > 1)", false);
		evaluateBooleanResult(name, emptyIntegerSet + "->forAll(x | x > 0)", true);
		evaluateNullResult(name, nullIntegerSet + "->forAll(x | x > 0)");
		evaluateInvalidResult(name, "Set{1, 2, 3, 1.div(0)}->forAll(x | x > 0)");
		evaluateInvalidResult(name, "Set{1, 2, 3, 4 }->forAll(x | x/0 > 0)");
		evaluateBooleanResult(name, emptyIntegerSet + "->forAll(x | x/0 > 0)", true);
	}

	public void	testIsUnique() {
		String name = "testIsUnique";
		evaluateBooleanResult(name, "Set{1, 2, 3}->isUnique(x | x)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->isUnique(x | x - x)", false);
		evaluateBooleanResult(name, "Bag{1, 2, 3}->isUnique(x | x)", true);
		evaluateBooleanResult(name, "Bag{1, 1, 2, 3}->isUnique(x | x)", false);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 1}->isUnique(x | x)", false);
		evaluateBooleanResult(name, tuplesSet + "->isUnique(age)", false);
		evaluateBooleanResult(name, tuplesSet + "->isUnique(grade)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3, null}->isUnique(x | x).oclIsUndefined()", true);
		evaluateBooleanResult(name, emptyIntegerSet + "->isUnique(x | x)", true);
		evaluateNullResult(name, nullIntegerSet + "->isUnique(x | x > 0)");
		evaluateInvalidResult(name, "Set{1, 2 / 0, 3}->isUnique(x | x)");
	}

	public void	testAny() throws Exception {
		String name = "testAny";
		evaluateBooleanResult(name, "Set{1, 2}->includes(Set{1, 2, 3}->any(x | x < 3))", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->any(x | x > 5).oclIsUndefined()", true);
		evaluateBooleanResult(name, "Set{6}->includes(Set{1, 2, 3, 4, 5, 6}->any(x | x > 5))", true);
		evaluateBooleanResult(name, "Set{5}->includes(Set{1, 2, null, 5}->any(x | x > 2))", true);
		evaluateBooleanResult(name, "Set{1, 2, 3, null}->any(x | x > 5).oclIsUndefined()", true);

		String expression = "Set{1..20}->any(x | x >= 10 and x <= 15)";
		exp = compileExpression(name, expression);
		assertNotNull(exp);
		for (int i = 0; i < 40; i++) {
			OclIntegerValue value = (OclIntegerValue) oclEvaluator.evaluate(exp, evalEnv, null);
			assertTrue(value.intValue() >= 10 && value.intValue() <= 15);
//			System.out.println("value = " + value.intValue());
		}
	}

	public void	testOne() {
		String name = "testOne";
		evaluateBooleanResult(name, "Set{1, 2, 3}->one(x | x > 2)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->one(x | x < 2)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->one(x | x >= 2)", false);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 3}->one(x | x >= 3)", false);
		evaluateBooleanResult(name, "Bag{1, 2, 3, null, 3}->one(x | x >= 3)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3, null}->one(x | x >= 2)", false);
		evaluateBooleanResult(name, "Set{null, 1, 2, 3}->one(x | x >= 2)", false);
		evaluateNullResult(name, "Set{1, 2, null, 5}->one(x | x > 2))");
		evaluateBooleanResult(name, "Set{1, 2, 3, null}->one(x | x > 5).oclIsUndefined()", true);
		evaluateInvalidResult(name, "Set{1, 2, 1/0, 5}->one(x | x > 2))");
		
		evaluateBooleanResult(name, tuplesSet + "->one(age = 21)", false);
		evaluateBooleanResult(name, tuplesSet + "->one(age = 18)", true);
	}

	public void	testSelect() {
		String name = "testSelect";
		evaluateBooleanResult(name, "Set{1, 2, 3}->select(x | x > 2) = Set{3}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->select(x | x < 2) = Set{1}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->select(x | x >= 2) = Set{2,3}", true);
		
		evaluateBooleanResult(name, "Set{1, 2, 3}->reject(x | x > 2) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->reject(x | x < 2) = Set{2,3}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->reject(x | x >= 2) = Set{1}", true);

		evaluateBooleanResult(name, "Bag{1, 2, 3}->select(x | x > 2) = Bag{3}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3}->select(x | x < 2) = Bag{1}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3}->select(x | x >= 2) = Bag{2,3}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 1, 2, 3, 1, 3}->select(x | x > 2) = Bag{3, 3, 3}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 1, 2, 3, 1, 3}->select(x | x < 2) = Bag{1, 1, 1}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 1, 2, 3, 1, 2, 3}->select(x | x >= 2) = Bag{2,2,2,3,3,3}", true);

		evaluateBooleanResult(name, "Sequence{1, 2, 3}->select(x | x > 2) = Sequence{3}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3}->select(x | x < 2) = Sequence{1}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3}->select(x | x >= 2) = Sequence{2,3}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3, 1, 2, 3, 1, 3}->select(x | x > 2) = Sequence{3, 3, 3}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3, 1, 2, 3, 1, 3}->select(x | x < 2) = Sequence{1, 1, 1}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3, 1, 2, 3, 1, 2, 3}->select(x | x >= 2) = Sequence{2,3,2,3,2,3}", true);
		
		evaluateBooleanResult(name, tuplesSet + "->select(age > 20)->size() = 3", true);
		try {
			OclExpression exp = compileExpression(name, tuplesSet);
			System.out.println("exp type = " + exp.getType().getName());
		} catch (Exception e) {
			e.printStackTrace();
		}

		try {
			OclExpression exp = compileExpression(name, tuplesSet + "->select(age > 20)");
			System.out.println("exp type = " + exp.getType().getName());
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		evaluateBooleanResult(name, tuplesSet + "->select(age > 20)->select(grade > 85.0)->size() = 2", true);
		evaluateBooleanResult(name, tuplesSet + "->select(age > 20)->collect(grade)->size() = 3", true);
		evaluateBooleanResult(name, specialTuplesSet + "->collect(age)->size() = 5", true);
		evaluateBooleanResult(name, specialTuplesSet + "->collect(age)->sum() = 85", true);
	}

	public void	testCollectNested() {
		String name = "testCollectNested";
		evaluateBooleanResult(name, "Set{1, 2, 3}->collectNested(x | x) = Bag{1, 2, 3}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->collectNested(x | x + 2) = Bag{3, 4, 5}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->collectNested(x | x * 2) = Bag{2, 4, 6}", true);
		evaluateBooleanResult(name, "Set{Set{1, 2}, Set{1, 2, 3}}->collectNested(x | x->sum()) = Bag{3, 6}", true);
		
		evaluateBooleanResult(name, "Bag{1, 2, 3}->collectNested(x | x) = Bag{1, 2, 3}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3}->collectNested(x | x + 2) = Bag{3, 4, 5}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3}->collectNested(x | x * 2) = Bag{2,4,6}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 1, 2, 3, 1, 3}->collectNested(x | x + 2) = Bag{3, 3, 3, 4, 4, 5, 5, 5}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 1, 2, 3, 1, 3}->collectNested(x | x * 2) = Bag{2, 2, 2, 4, 4, 6, 6, 6}", true);


		evaluateBooleanResult(name, "Sequence{1, 2, 3}->collectNested(x | x) = Sequence{1, 2, 3}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3}->collectNested(x | x + 2) = Sequence{3, 4, 5}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3}->collectNested(x | x * 2) = Sequence{2, 4, 6}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3, 1, 2, 3, 1, 3}->collectNested(x | x + 2) = Sequence{3, 4, 5, 3, 4, 5, 3, 5}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3, 1, 2, 3, 1, 3}->collectNested(x | x * 2) = Sequence{2, 4, 6, 2, 4, 6, 2, 6}", true);

		evaluateBooleanResult(name, "OrderedSet{1, 2, 3}->collectNested(x | x) = Sequence{1, 2, 3}", true);
		evaluateBooleanResult(name, "OrderedSet{1, 2, 3}->collectNested(x | x + 2) = Sequence{3, 4, 5}", true);
		evaluateBooleanResult(name, "OrderedSet{1, 2, 3}->collectNested(x | x * 2) = Sequence{2, 4, 6}", true);
		evaluateBooleanResult(name, "OrderedSet{Set{1, 2}, Set{1, 2, 3}}->collectNested(x | x->sum()) = Sequence{3, 6}", true);

	}

	public void	testCollect() {
		String name = "testCollect";
		evaluateBooleanResult(name, "Set{1, 2, 3}->collect(x | x) = Bag{1, 2, 3}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->collect(x | x + 2) = Bag{3, 4, 5}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->collect(x | x * 2) = Bag{2, 4, 6}", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->collect(a | Tuple{x : Integer = a, y : Integer = a * 2}) = Bag{Tuple{x = 1, y = 3}, Tuple{x = 2, y = 4}, Tuple{x = 3, y = 6}}", false);
		evaluateBooleanResult(name, "Set{1, 2, 3}->collect(a | Tuple{x : Integer = a, y : Integer = a * 2}) = Bag{Tuple{x = 1, y = 2}, Tuple{x = 3, y = 6}, Tuple{x = 2, y = 4}}", true);
		evaluateBooleanResult(name, "Set{Set{1, 2}, Set{1, 2, 3}}->collect(x | x->sum()) = Bag{3, 6}", true);
		
		evaluateBooleanResult(name, "Bag{1, 2, 3}->collect(x | x) = Bag{1, 2, 3}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3}->collect(x | x + 2) = Bag{3, 4, 5}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3}->collect(x | x * 2) = Bag{2,4,6}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 1, 2, 3, 1, 3}->collect(x | x + 2) = Bag{3, 3, 3, 4, 4, 5, 5, 5}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 1, 2, 3, 1, 3}->collect(x | x * 2) = Bag{2, 2, 2, 4, 4, 6, 6, 6}", true);
		
		evaluateBooleanResult(name, "Sequence{1, 2, 3}->collect(x | x + 20) = Sequence{21,22,23}", true);
		evaluateBooleanResult(name, "OrderedSet{1, 2, 3}->collect(x | x + 20) = Sequence{21,22,23}", true);
		
		evaluateBooleanResult(name, tuplesSet + "->collect(age) = Bag{16, 18, 21, 21, 25}", true);
	}

	public void	testSortedBy() {
		String name = "testSortedBy";
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 10, 5}->sortedBy(x | x) = OrderedSet{1, 2, 3, 5, 8, 10}", true);
		evaluateBooleanResult(name, "Bag{1, 2, 3, 8, 10, 5}->sortedBy(x | x) = Sequence{1, 2, 3, 5, 8, 10}", true);
		evaluateBooleanResult(name, "Sequence{1, 2, 3, 8, 10, 5}->sortedBy(x | 20 - x) = Sequence{10, 8, 5, 3, 2, 1}", true);
		evaluateBooleanResult(name, tuplesSet + "->sortedBy(age)->collect(age) = Sequence{16, 18, 21, 21, 25} ", true);
		evaluateBooleanResult(name, tuplesSet + "->sortedBy(name)->collect(age) = Sequence{21, 21, 18, 16, 25} ", true);
		evaluateBooleanResult(name, "Set{1, 2, null, 3, 8, 10, 5}->sortedBy(x | x) = OrderedSet{1, 2, 3, 5, 8, 10, null}", true);
		evaluateBooleanResult(name, "Bag{null, 1, 2, 3, null, 8, 10, null, 5}->sortedBy(x | x) = Sequence{1, 2, 3, 5, 8, 10, null, null, null}", true);
		
		
		evaluateBooleanResult(name, "Set{\"Joao\", \"Alex\", \"Maria\", \"Carla\"}->sortedBy(x | x) = OrderedSet{\"Alex\", \"Carla\", \"Joao\", \"Maria\"}", true);
		evaluateBooleanResult(name, "Set{\"10/01/2005\".toDate()	, \"08/01/2005\".toDate(), \"01/01/2005\".toDate(), \"20/01/2005\".toDate()}->sortedBy(x|x) =  " +
				"OrderedSet{\"01/01/2005\".toDate(), \"08/01/2005\".toDate(), \"10/01/2005\".toDate(), \"20/01/2005\".toDate()}", true);
	}

	public	void testExistsWithIterate() {
		String	name = "testExistsWithIterate";
		
		evaluateBooleanResult(name, "Set{1, 2, 3}->iterate(x; result : Boolean = false | result or x > 5)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3}->iterate(x; result : Boolean = false | result or x > 2)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3, null}->iterate(x; result : Boolean = false | result or x > 2)", true);
		evaluateBooleanResult(name, "Set{1, 2, null, 5}->iterate(x; result : Boolean = false | result or x > 2)", true);
		evaluateNullResult(name, "Set{1, 2, 3, null}->iterate(x; result : Boolean = false | result or x > 5)");
		evaluateBooleanResult(name, "Set{1, 2, 3}->iterate(x; result : Boolean = false | result or Set{3, 4, 5}->iterate(y; result : Boolean = false | result or y = x))", true);
		evaluateBooleanResult(name, "Set{1, 2, 3}->iterate(x; result : Boolean = false | result or Set{6, 4, 5}->includes(x))", false);
		evaluateBooleanResult(name, tuplesSet + "->iterate(x; result : Boolean = false | result or (x.age > 20 and x.age < 30))", true);
		evaluateBooleanResult(name, tuplesSet + "->iterate(x; result : Boolean = false | result or x.grade < 50.0)", false);
	}

	public void	testForAllWithIterate() {
		String name = "testForAllWithIterate";
		evaluateBooleanResult(name, "Set{1, 2, 3}->iterate(x; result : Boolean = true | result and x > 0)", true);
		evaluateNullResult(name, "Set{1, 2, 3, null}->iterate(x; result : Boolean = true | result and x > 0)");
		evaluateBooleanResult(name, "Set{1, 2, 3}->iterate(x; result : Boolean = true | result and x >= 2)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3}->iterate(x; result : Boolean = true | result and x = 1)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->iterate(x; result : Boolean = true | result and x < 15 and x > 0)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->iterate(x1, x2; result : Boolean = true | result and x1 + x2 >= 2)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->iterate(x1, x2; result : Boolean = true | result and x1 + x2 > 3)", false);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->iterate(x1, x2; result : Boolean = true | result and x1 <> x2 implies x1 + x2 >= 3)", true);
		evaluateBooleanResult(name, "Set{1, 2, 3, 8, 9, 10}->iterate(x1, x2; result : Boolean = true | result and x1 - x2 = 0 implies x1 = x2)", true);
		evaluateIntegerResult(name, "Set{1, 2, 3, 8, 9, 10}->iterate(x1, x2; result : Integer = 0 | result + 1)", 36);
		evaluateIntegerResult(name, "Set{1, 2, 3, 8, 9, 10}->iterate(x1, x2; result : Integer = 0 | result + x1 - x2)", 0);
		evaluateIntegerResult(name, "Set{1, 2, 3, 8, 9, 10}->iterate(x1; result : Integer = 0 | result + x1)", 33);
		evaluateIntegerResult(name, emptyIntegerSet + "->iterate(x1; result : Integer = 0 | result + x1)", 0);
		evaluateNullResult(name, "Set{1, 2, 3, 8, 9, 10, null}->iterate(x1; result : Integer = 0 | result + x1)");
		evaluateInvalidResult(name, "Set{1, 2, 3, 8, 9, 10, 1/0}->iterate(x1; result : Real = 0 | result + x1)");
	}

	
	

	public	void 	setUp() throws Exception {
		evalEnv = new EvalEnvironment();
		super.setUp();
	}

	protected	void  evaluateBooleanResult(String sourceStream, String expression, boolean expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);

			assertNotNull(exp);

			OclBooleanValue value;
			value = (OclBooleanValue) oclEvaluator.evaluate(exp, evalEnv, null);
			assertEquals("Boolean",  value.getType().getName());
			assertEquals(expectedResult, value.booleanValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
	
	protected	void  evaluateIntegerResult(String sourceStream, String expression, int expectedResult) {
		try {
			exp = compileExpression(sourceStream, expression);

			assertNotNull(exp);

			OclIntegerValue value;
			value = (OclIntegerValue) oclEvaluator.evaluate(exp, evalEnv, null);
			assertEquals("Integer",  value.getType().getName());
			assertEquals(expectedResult, value.intValue());
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
}
