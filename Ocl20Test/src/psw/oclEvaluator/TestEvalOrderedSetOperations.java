/*
 * Created on May 15, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

public class TestEvalOrderedSetOperations extends TestEval {

	public void	testSize() {
		String	name = "testSize";
		
		evaluateIntegerResult(name, "OrderedSet {1}->size() ", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->size() ", 2);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}->size() ", 10);
		evaluateIntegerResult(name, "OrderedSet { }->size() ", 0);
		evaluateIntegerResult(name, "OrderedSet {null}->size() ", 1);
		evaluateIntegerResult(name, "OrderedSet {null, 1}->size() ", 2);
		evaluateIntegerResult(name, "OrderedSet {null, 1, null, 1}->size() ", 2);
		evaluateInvalidResult(name, "OrderedSet {null, 10.div(0)}->size()");
	}
	
	public void	testIncludes() {
		String	name = "testIncludes";
		
		evaluateBooleanResult(name, "OrderedSet {1}->includes(1) ", true);
		evaluateBooleanResult(name, "OrderedSet {1}->includes(2) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->includes(1) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->includes(2) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->includes(3) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(1) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(10) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(40) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(45) ", false);
		evaluateBooleanResult(name, "OrderedSet { }->includes(0) ", false);
		
		evaluateBooleanResult(name, "OrderedSet {null}->includes(null)", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, null}->includes(null)", true);
		evaluateBooleanResult(name, "OrderedSet {null, 1, 2, 3}->includes(null)", true);
		evaluateInvalidResult(name, "OrderedSet {1.0, 10/0, 2, 3}->includes(null)");
		evaluateInvalidResult(name, "OrderedSet {1.0, 2.0, 3.0}->includes(10/0)");

	}

	public void	testExcludes() {
		String	name = "testExcludes";
		
		evaluateBooleanResult(name, "OrderedSet {1}->excludes(1) ", false);
		evaluateBooleanResult(name, "OrderedSet {1}->excludes(2) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludes(1) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludes(2) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludes(3) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(1) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(10) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(40) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(45) ", true);
		evaluateBooleanResult(name, "OrderedSet { }->excludes(0) ", true);
		
		evaluateBooleanResult(name, "OrderedSet {null}->excludes(null)", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, null}->excludes(null)", false);
		evaluateBooleanResult(name, "OrderedSet {null, 1, 2, 3}->excludes(null)", false);
		evaluateInvalidResult(name, "OrderedSet {1.0, 10/0, 2, 3}->excludes(null)");
		evaluateInvalidResult(name, "OrderedSet {1.0, 2.0, 3.0}->excludes(10/0)");
	}

	public void	testCount() {
		String	name = "testCount";
		
		evaluateIntegerResult(name, "OrderedSet {1}->count(1) ", 1);
		evaluateIntegerResult(name, "OrderedSet {1}->count(2) ", 0);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->count(1) ", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->count(2) ", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->count(3) ", 0);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(1) ", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(10) ", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(40) ", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(45) ", 0);
		evaluateIntegerResult(name, "OrderedSet { }->count(1) ", 0);
		
		evaluateIntegerResult(name, "OrderedSet {null}->count(null)", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->count(null)", 0);
		evaluateIntegerResult(name, "OrderedSet {1, 2, null}->count(null)", 1);
		evaluateIntegerResult(name, "OrderedSet {null, 1, 2}->count(null)", 1);
		evaluateInvalidResult(name, "OrderedSet {1.0, 10/0, 2, 3}->count(10/0)");
		evaluateInvalidResult(name, "OrderedSet {1.0, 2.0, 3.0}->count(10/0)");
	}

	public void	testIncludesAll() {
		String	name = "testIncludesAll";
		
		evaluateBooleanResult(name, "OrderedSet {1}->includesAll(OrderedSet{1}) ", true);
		evaluateBooleanResult(name, "OrderedSet {1}->includesAll(OrderedSet{2}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->includesAll(OrderedSet{1,2}) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->includesAll(OrderedSet{1}) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->includesAll(OrderedSet{2}) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->includesAll(OrderedSet{1,2,3}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includesAll(OrderedSet{2, 3, 40, 70, 60, 50, 10, 9, 8, 1}) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includesAll(OrderedSet{2, 3, 40, 45, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "OrderedSet { }->includesAll(OrderedSet{1}) ", false);
		evaluateBooleanResult(name, "OrderedSet { }->includesAll(OrderedSet{}) ", true);
	}

	public void	testExcludesAll() {
		String	name = "testEncludesAll";
		
		evaluateBooleanResult(name, "OrderedSet {1}->excludesAll(OrderedSet{1}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1}->excludesAll(OrderedSet{2}) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludesAll(OrderedSet{1,2}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludesAll(OrderedSet{1}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludesAll(OrderedSet{2}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludesAll(OrderedSet{1,2,3}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludesAll(OrderedSet{3}) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->excludesAll(OrderedSet{3, 4, 5}) ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(OrderedSet{2, 3, 40, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(OrderedSet{2, 3, 40, 45, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(OrderedSet{100, 200, 300, 400, 500}) ", true);
		evaluateBooleanResult(name, "OrderedSet { }->excludesAll(OrderedSet{1}) ", true);
		evaluateBooleanResult(name, "OrderedSet { }->excludesAll(OrderedSet{}) ", true);
	}

	public void	testIsEmpty() {
		String	name = "testIsEmpty";
		
		evaluateBooleanResult(name, "OrderedSet {1}->isEmpty() ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->isEmpty() ", false);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->isEmpty() ", false);
		evaluateBooleanResult(name, "OrderedSet { }->isEmpty() ", true);
		
		evaluateBooleanResult(name, "OrderedSet { null }->isEmpty() ", false);
		evaluateInvalidResult(name, "OrderedSet { 10/0 }->isEmpty() ");
	}

	public void	testNotEmpty() {
		String	name = "testNotEmpty";
		
		evaluateBooleanResult(name, "OrderedSet {1}->notEmpty() ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->notEmpty() ", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->notEmpty() ", true);
		evaluateBooleanResult(name, "OrderedSet { }->notEmpty() ", false);
		
		evaluateBooleanResult(name, "OrderedSet { null }->notEmpty() ", true);
		evaluateInvalidResult(name, "OrderedSet { 10/0 }->notEmpty() ");
	}

	public void	testSum() {
		String	name = "testSum";
		
		evaluateIntegerResult(name, "OrderedSet {0}->sum() ", 0);
		evaluateIntegerResult(name, "OrderedSet {1}->sum() ", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->sum() ", 3);
		evaluateIntegerResult(name, "OrderedSet {-1, -2, 0, 1, 2}->sum() ", 0);
		evaluateIntegerResult(name, "OrderedSet {0..10}->sum() ", 55);
		
		evaluateIntegerResult(name, "OrderedSet {-1, -2, 0, null, 1, 2}->sum() ", 0);
		evaluateIntegerResult(name, "OrderedSet {-1, -2, 0, null, 1, 2}->size() ", 6);
		evaluateInvalidResult(name, "OrderedSet {10/0, 1.0, 2.0, 3.0}->sum()");
	}
	
	public	void	testOrderedSetAppend() {
		String	name = "testOrderedSetAppend";
		
		evaluateBooleanResult(name, "OrderedSet {}->append(1) = OrderedSet{1}", true);
		evaluateBooleanResult(name, "OrderedSet {1}->append(1) = OrderedSet{1, 1}", true);
		evaluateBooleanResult(name, "OrderedSet {2}->append(1) = OrderedSet{2, 1}", true);
		evaluateBooleanResult(name, "OrderedSet {1}->append(2) = OrderedSet{1, 2}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->append(1) = OrderedSet{1, 2, 1}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->append(2) = OrderedSet{1, 2, 2 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->append(3) = OrderedSet { 1, 2, 3, 4, 5, 6, 3}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->append(7) = OrderedSet { 1, 2, 3, 4, 5, 6, 7 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->append(null) = OrderedSet { 1, 2, 3, 4, 5, 6, null }", true);
	}

	public	void	testOrderedSetPrepend() {
		String	name = "testOrderedSetPrepend";
		
		evaluateBooleanResult(name, "OrderedSet {}->prepend(1) = OrderedSet{1}", true);
		evaluateBooleanResult(name, "OrderedSet {1}->prepend(1) = OrderedSet{1, 1}", true);
		evaluateBooleanResult(name, "OrderedSet {2}->prepend(1) = OrderedSet{1, 2}", true);
		evaluateBooleanResult(name, "OrderedSet {1}->prepend(2) = OrderedSet{2, 1}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->prepend(1) = OrderedSet{1, 2}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->prepend(2) = OrderedSet{1, 2 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2}->prepend(3) = OrderedSet{3, 1, 2 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->prepend(8) = OrderedSet { 8, 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->prepend(null) = OrderedSet { null, 1, 2, 3, 4, 5, 6}", true);
	}

	public	void	testOrderedSetInsertAt() {
		String	name = "testOrderedSetInsertAt";
		
		evaluateBooleanResult(name, "OrderedSet {}->insertAt(1, 9) = OrderedSet{9}", true);
		evaluateBooleanResult(name, "OrderedSet {1}->insertAt(1, 9) = OrderedSet{9, 1}", true);
		evaluateBooleanResult(name, "OrderedSet {2}->insertAt(1, 9) = OrderedSet{9, 2 }", true);
		evaluateBooleanResult(name, "OrderedSet {8}->insertAt(2, 9) = OrderedSet{8, 9}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->insertAt(1, 9) = OrderedSet { 9, 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->insertAt(7, 9) = OrderedSet { 1, 2, 3, 4, 5, 6, 9}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->insertAt(2, 9) = OrderedSet { 1, 9, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->insertAt(6, 9) = OrderedSet { 1, 2, 3, 4, 5, 9, 6}", true);
		evaluateInvalidResult(name, "OrderedSet {}->insertAt(2, 0) = OrderedSet{0}");
	}

	public	void	testSubOrderedSet() {
		String	name = "testSubOrderedSet";
		
		evaluateInvalidResult(name, "OrderedSet {}->subOrderedSet(1, 1) = OrderedSet{}");
		evaluateBooleanResult(name, "OrderedSet {1}->subOrderedSet(1, 1) = OrderedSet{1}", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->subOrderedSet(1, 1) = OrderedSet { 1 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->subOrderedSet(1, 2) = OrderedSet { 1, 2 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->subOrderedSet(1, 6) = OrderedSet { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->subOrderedSet(6, 6) = OrderedSet { 6 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->subOrderedSet(4, 6) = OrderedSet { 4, 5, 6 }", true);
		evaluateBooleanResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->subOrderedSet(2, 4) = OrderedSet { 2, 3, 4 }", true);
		
		evaluateBooleanResult(name, "OrderedSet {}->subOrderedSet(2, 0) = OrderedSet{0}", false);
		evaluateInvalidResult(name, "OrderedSet {8}->subOrderedSet(1, 2)");
		evaluateBooleanResult(name, "OrderedSet {8}->subOrderedSet(1, 2)->oclIsUndefined()", true);
	}

	public	void	testOrderedSetAt() {
		String	name = "testOrderedSetAt";
		
		evaluateInvalidResult(name, "OrderedSet {}->at(1) = OrderedSet{1}");
		evaluateIntegerResult(name, "OrderedSet {1}->at(1)", 1);
		evaluateIntegerResult(name, "OrderedSet {2}->at(1)", 2);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->at(1)", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->at(2)", 2);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->at(3)", 3);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->at(6)", 6);
	}

	public	void	testOrderedSetFirst() {
		String	name = "testOrderedSetFirst";
		
		evaluateInvalidResult(name, "OrderedSet {}->first() = OrderedSet{1}");
		evaluateIntegerResult(name, "OrderedSet {1}->first()", 1);
		evaluateIntegerResult(name, "OrderedSet {2}->first()", 2);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->first()", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->first()", 1);
	}

	public	void	testOrderedSetLast() {
		String	name = "testOrderedSetLast";
		
		evaluateInvalidResult(name, "OrderedSet {}->last() = OrderedSet{1}");
		evaluateIntegerResult(name, "OrderedSet {1}->last()", 1);
		evaluateIntegerResult(name, "OrderedSet {2}->last()", 2);
		evaluateIntegerResult(name, "OrderedSet {1, 2}->last()", 2);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->last()", 6);
	}

	public	void	testOrderedSetIndexOf() {
		String	name = "testOrderedSetIndexOf";
		 
		evaluateIntegerResult(name, "OrderedSet {8}->indexOf(8)", 1);
		evaluateIntegerResult(name, "OrderedSet {9}->indexOf(9)", 1);
		evaluateIntegerResult(name, "OrderedSet {8, 9}->indexOf(8)", 1);
		evaluateIntegerResult(name, "OrderedSet {8, 9}->indexOf(9)", 2);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->indexOf(1)", 1);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->indexOf(3)", 3);
		evaluateIntegerResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->indexOf(6)", 6);
		evaluateInvalidResult(name, "OrderedSet {1, 2, 3, 4, 5, 6}->indexOf(7)");
	}
}
