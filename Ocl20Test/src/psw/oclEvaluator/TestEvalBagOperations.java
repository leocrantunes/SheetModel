/*
 * Created on May 15, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalBagOperations extends TestEval {
	
	public void	testSize() {
		String	name = "testSize";
		
		evaluateIntegerResult(name, "Bag {1}->size() ", 1);
		evaluateIntegerResult(name, "Bag {1, 1}->size() ", 2);
		evaluateIntegerResult(name, "Bag {1, 2}->size() ", 2);
		evaluateIntegerResult(name, "Bag {1, 2, 2, 1}->size() ", 4);
		evaluateIntegerResult(name, "Bag {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}->size() ", 10);
		evaluateIntegerResult(name, "Bag { }->size() ", 0);
		evaluateIntegerResult(name, "Bag { null}->size()", 1);
		evaluateIntegerResult(name, "Bag { null, null}->size()", 2);
		evaluateIntegerResult(name, "Bag { null, 1}->size()", 2);
		evaluateIntegerResult(name, "Bag { 2, null, 1}->size()", 3);
		evaluateInvalidResult(name, "Bag { 10.div(0) }->size()");
	}
	
	public void	testIncludes() {
		String	name = "testIncludes";
		
		evaluateBooleanResult(name, "Bag {1}->includes(1) ", true);
		evaluateBooleanResult(name, "Bag {1, 1}->includes(1) ", true);
		evaluateBooleanResult(name, "Bag {1}->includes(2) ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->includes(1) ", true);
		evaluateBooleanResult(name, "Bag {2, 1, 1, 2}->includes(1) ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->includes(2) ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->includes(3) ", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(1) ", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(10) ", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(40) ", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(45) ", false);
		
		evaluateBooleanResult(name, "Bag {1, 2, null}->includes(1)", true);
		evaluateBooleanResult(name, "Bag {1, 2, null}->includes(null)", true);
		evaluateBooleanResult(name, "Bag {1, 2}->includes(null)", false);
		evaluateBooleanResult(name, "Bag { }->includes(0) ", false);
		
		evaluateInvalidResult(name, "Bag {1, 2, 10.div(0)}->includes(1)");
		evaluateInvalidResult(name, "Bag {1, 2, 3}->includes(10.div(0))");
	}

	public void	testExcludes() {
		String	name = "testExcludes";
		
		evaluateBooleanResult(name, "Bag {1}->excludes(1) ", false);
		evaluateBooleanResult(name, "Bag {1}->excludes(2) ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->excludes(1) ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->excludes(2) ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->excludes(3) ", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(1) ", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(10) ", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(40) ", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(45) ", true);
		evaluateBooleanResult(name, "Bag { }->excludes(0) ", true);
		
		evaluateBooleanResult(name, "Bag {1, 2, null}->excludes(1)", false);
		evaluateBooleanResult(name, "Bag {1, 2, null}->excludes(3)", true);
		evaluateBooleanResult(name, "Bag {1, 2}->excludes(null)", true);
		evaluateBooleanResult(name, "Bag {1, 2, null}->excludes(null)", false);
		
		evaluateInvalidResult(name, "Bag {1, 2, 10.div(0)}->excludes(1)");
		evaluateInvalidResult(name, "Bag {1, 2, 3}->excludes(10.div(0))");

	}

	public void	testCount() {
		String	name = "testCount";
		
		evaluateIntegerResult(name, "Bag {1}->count(1) ", 1);
		evaluateIntegerResult(name, "Bag {1, 1}->count(1) ", 2);
		evaluateIntegerResult(name, "Bag {1}->count(2) ", 0);
		evaluateIntegerResult(name, "Bag {1, 2}->count(1) ", 1);
		evaluateIntegerResult(name, "Bag {1, 2, 1, 2, 1}->count(1) ", 3);
		evaluateIntegerResult(name, "Bag {1, 2}->count(2) ", 1);
		evaluateIntegerResult(name, "Bag {1, 2}->count(3) ", 0);
		evaluateIntegerResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(1) ", 1);
		evaluateIntegerResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(10) ", 1);
		evaluateIntegerResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(40) ", 1);
		evaluateIntegerResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(45) ", 0);
		evaluateIntegerResult(name, "Bag { }->count(1) ", 0);
		
		evaluateIntegerResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(null) ", 0);
		evaluateIntegerResult(name, "Bag { null }->count(1) ", 0);
		evaluateIntegerResult(name, "Bag { null }->count(null) ", 1);
		evaluateIntegerResult(name, "Bag { null, null }->count( null ) ", 2);
		
		evaluateInvalidResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(10.div(0)) ");
		evaluateInvalidResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10, 10.div(0) }->count(1) ");
		evaluateInvalidResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10, 10.div(0) }->count(10.div(0)) ");
	}

	public void	testIncludesAll() {
		String	name = "testIncludesAll";
		
		evaluateBooleanResult(name, "Bag {1}->includesAll(Bag{1}) ", true);
		evaluateBooleanResult(name, "Bag {1, 1}->includesAll(Bag{1, 1}) ", true);
		evaluateBooleanResult(name, "Bag {1, 1}->includesAll(Bag{1}) ", true);
		evaluateBooleanResult(name, "Bag {1, 1}->includesAll(Bag{1, 1, 1}) ", true);
		
		evaluateBooleanResult(name, "Bag {1}->includesAll(Bag{2}) ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->includesAll(Bag{1,2}) ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->includesAll(Bag{1}) ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->includesAll(Bag{2}) ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->includesAll(Bag{1,2,3}) ", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includesAll(Bag{2, 3, 40, 70, 60, 50, 10, 9, 8, 1}) ", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includesAll(Bag{2, 3, 40, 45, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "Bag { }->includesAll(Bag{1}) ", false);
		evaluateBooleanResult(name, "Bag { }->includesAll(Bag{}) ", true);
	}

	public void	testExcludesAll() {
		String	name = "testEncludesAll";
		
		evaluateBooleanResult(name, "Bag {1}->excludesAll(Bag{1}) ", false);
		evaluateBooleanResult(name, "Bag {1}->excludesAll(Bag{2}) ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->excludesAll(Bag{1,2}) ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->excludesAll(Bag{1}) ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->excludesAll(Bag{2}) ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->excludesAll(Bag{1,2,3}) ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->excludesAll(Bag{3}) ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->excludesAll(Bag{3, 4, 5}) ", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Bag{2, 3, 40, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Bag{2, 3, 40, 45, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Bag{100, 200, 300, 400, 500}) ", true);
		evaluateBooleanResult(name, "Bag { }->excludesAll(Bag{1}) ", true);
		evaluateBooleanResult(name, "Bag { }->excludesAll(Bag{}) ", true);
	}

	public void	testIsEmpty() {
		String	name = "testIsEmpty";
		
		evaluateBooleanResult(name, "Bag {1}->isEmpty() ", false);
		evaluateBooleanResult(name, "Bag {1, 2}->isEmpty() ", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->isEmpty() ", false);
		evaluateBooleanResult(name, "Bag { }->isEmpty() ", true);
		evaluateBooleanResult(name, "Bag {null}->isEmpty() ", false);
	}

	public void	testNotEmpty() {
		String	name = "testNotEmpty";
		
		evaluateBooleanResult(name, "Bag {1}->notEmpty() ", true);
		evaluateBooleanResult(name, "Bag {1, 2}->notEmpty() ", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->notEmpty() ", true);
		evaluateBooleanResult(name, "Bag { }->notEmpty() ", false);
		evaluateBooleanResult(name, "Bag {null}->notEmpty() ", true);
	}

	public void	testSum() {
		String	name = "testSum";
		
		evaluateIntegerResult(name, "Bag {0}->sum() ", 0);
		evaluateIntegerResult(name, "Bag {1}->sum() ", 1);
		evaluateIntegerResult(name, "Bag {1, 1}->sum() ", 2);
		evaluateIntegerResult(name, "Bag {1, 2}->sum() ", 3);
		evaluateIntegerResult(name, "Bag {-1, -2, 0, 1, 2, -1, -2, 1, 2, 5, 10}->sum() ", 15);
		evaluateIntegerResult(name, "Bag {0..10}->sum() ", 55);
		evaluateIntegerResult(name, "Bag {0..10, null, 5..10}->sum() ", 100);
	}
	
	
	public	void	testBagEquality() {
		String	name = "testBagEquality";
		
		evaluateBooleanResult(name, "Bag {} = Bag {}", true);
		evaluateBooleanResult(name, "Bag {} = Bag {1}", false);
		evaluateBooleanResult(name, "Bag {1} = Bag {}", false);
		evaluateBooleanResult(name, "Bag {1} = Bag {1}", true);
		evaluateBooleanResult(name, "Bag {2} = Bag {1}", false);
		evaluateBooleanResult(name, "Bag {1} = Bag {2}", false);
		evaluateBooleanResult(name, "Bag {1, 2} = Bag {1}", false);
		evaluateBooleanResult(name, "Bag {1, 2} = Bag {1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 2} = Bag {2, 1}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 1, 2, 1, 1, 3, 3} = Bag {3, 3, 2, 2, 1, 1, 1, 1}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 1, 2, 1, 1, 3, 3} = Bag {3, 3, 2, 2, 1, 1, 1}", false);
		evaluateBooleanResult(name, "Bag {1, 2, 1, 2, 1, 1 } = Bag {3, 3, 2, 2, 1, 1, 1, 1}", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6} = Bag {2, 1, 5, 4, 3, 6}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6} = Bag {2, 1, 5, 4, 3}", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6, null} = Bag {2, 1, 5, 4, 3, 6, null}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6, null, null} = Bag {null, 2, 1, 5, 4, 3, 6, null}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6, null, null} = Bag {2, 1, 5, 4, 3, 6, null}", false);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6, null, null} = Bag {null, 2, 1, 5, 4, 3, 6, null, null}", false);
		
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4}->union(Bag{})->includesAll(Bag{1, 2, 3, 4}) ", true);
	}

	
	public	void	testBagUnion() {
		String	name = "testBagUnion";
		
		evaluateBooleanResult(name, "Bag {}->union(Bag{})->isEmpty() ", true);
		evaluateBooleanResult(name, "Bag {}->union(Bag{1}) = Bag{1} ", true);
		evaluateBooleanResult(name, "Bag {}->union(Bag{1, 2}) = Bag{1, 2} ", true);
		evaluateBooleanResult(name, "Bag {1}->union(Bag{2}) = Bag{1, 2} ", true);
		evaluateBooleanResult(name, "Bag {1}->union(Bag{1}) = Bag{1, 1} ", true);
		evaluateBooleanResult(name, "Bag {1}->union(Bag{1, 2}) = Bag{1, 1, 2} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Bag{2}) = Bag{1, 2, 3} ", true);
		evaluateBooleanResult(name, "Bag {1, 2,  3}->union(Bag{2}) = Bag{1, 2, 2, 3} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Bag{2, 4, 5}) = Bag{1, 2, 3, 4, 5} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Bag{1, 3}) = Bag{1, 1,  3,  3} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Bag{1, 1, 3, 3}) = Bag{1, 1, 1, 3,  3,  3} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Bag{1, 2, 3, 4}) = Bag{1, 1, 2, 3, 3, 4} ", true);
  	    evaluateBooleanResult(name, "Bag {1, 2, 3, 4}->union(Bag{})->includesAll(Bag{1, 2, 3, 4}) ", true);
	}
	
	public	void	testSetUnion() {
		String	name = "testBagUnion";

		evaluateBooleanResult(name, "Bag {}->union(Set{})->isEmpty() ", true);
		evaluateBooleanResult(name, "Bag {}->union(Set{1}) = Bag{1} ", true);
		evaluateBooleanResult(name, "Bag {}->union(Set{1, 2}) = Bag{1, 2} ", true);
		evaluateBooleanResult(name, "Bag {1}->union(Set{2}) = Bag{1, 2} ", true);
		evaluateBooleanResult(name, "Bag {1}->union(Set{1}) = Bag{1, 1} ", true);
		evaluateBooleanResult(name, "Bag {1}->union(Set{1, 2}) = Bag{1, 1, 2} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Set{2}) = Bag{1, 2, 3} ", true);
		evaluateBooleanResult(name, "Bag {1, 2,  3}->union(Set{2}) = Bag{1, 2, 2, 3} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Set{2, 4, 5}) = Bag{1, 2, 3, 4, 5} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Set{1, 3}) = Bag{1, 1,  3,  3} ", true);
		evaluateBooleanResult(name, "Bag {1, 3}->union(Set{1, 2, 3, 4}) = Bag{1, 1, 2, 3, 3, 4} ", true);
		evaluateBooleanResult(name, "Bag {1, 1, 3, 3}->union(Set{1, 2, 3, 4}) = Bag{1, 1, 1,  2, 3, 3, 3, 4} ", true);
	}


	public	void	testBagIntersection() {
		String	name = "testBagUnion";
		
		evaluateBooleanResult(name, "Bag {}->intersection(Bag {})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {}->intersection(Bag {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {1}->intersection(Bag {})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {1}->intersection(Bag {1}) = Bag{1}", true);
		evaluateBooleanResult(name, "Bag {2}->intersection(Bag {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {1}->intersection(Bag {2})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {1, 2}->intersection(Bag {1}) = Bag{1}", true);
		evaluateBooleanResult(name, "Bag {1, 2}->intersection(Bag {1, 2}) = Bag{1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 2}->intersection(Bag {2, 1}) = Bag{1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 2}->intersection(Bag {1, 2, 2, 2, 1}) = Bag{1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 1, 2, 2, 2, 2}->intersection(Bag {1, 2, 2, 2, 1}) = Bag{1, 1, 2, 2,  2}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->intersection(Bag {2, 1, 5, 4, 3, 6}) = Bag { 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->intersection(Bag {2, 1, 5, 4, 3}) = Bag { 1, 2, 3, 4, 5}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 2, 2, 7, 7, 3, 4, 5, 6}->intersection(Bag {2, 2, 7, 8, 1, 9, 4, 3}) = Bag { 1, 2, 2, 7, 3, 4}", true);
	}


	public	void	testSetIntersection() {
		String	name = "testBagIntersection";
		
		evaluateBooleanResult(name, "Bag {}->intersection(Set {})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {}->intersection(Set {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {1}->intersection(Set {1}) = Set {1}", true);
		evaluateBooleanResult(name, "Bag {2}->intersection(Set {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {1}->intersection(Set {2})->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {1, 2}->intersection(Set {1}) = Set{1}", true);
		evaluateBooleanResult(name, "Bag {1, 2}->intersection(Set {1, 2}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 1, 1, 2, 2, 2, 2}->intersection(Set {1, 2}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 2, 2, 1, 1, 3, 3, 2}->intersection(Set {2, 1}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->intersection(Set {2, 1, 2, 1, 5, 4, 3, 6}) = Set { 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->intersection(Set {2, 1, 5, 4, 4, 3, 3}) = Set { 1, 2, 3, 4, 5}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->intersection(Set {2, 7, 8, 1, 9, 1, 9, 4, 3}) = Set { 1, 2, 3, 4}", true);
	}


	public	void	testBagIncluding() {
		String	name = "testBagIncluding";
		
		evaluateBooleanResult(name, "Bag {}->including(1) = Bag{1}", true);
		evaluateBooleanResult(name, "Bag {1}->including(1) = Bag{1, 1}", true);
		evaluateBooleanResult(name, "Bag {2}->including(1) = Bag{1, 2}", true);
		evaluateBooleanResult(name, "Bag {1}->including(2) = Bag{1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 2}->including(1) = Bag{1, 1, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 2}->including(2) = Bag{1, 2, 2 }", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->including(3) = Bag { 6, 5, 4, 3, 3, 2, 1}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->including(7) = Bag { 1, 2, 3, 4, 5, 6, 7 }", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->including(null) = Bag { 1, 2, 3, 4, 5, 6, null }", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6, null}->including(null) = Bag { 1, 2, 3, 4, 5, 6, null, null }", true);
		evaluateInvalidResult(name, "Bag {1, 2, 3, 4, 5, 6}->including(10.div(0)) = Bag { 1, 2, 3, 4, 5, 6}");
	}


	public	void	testBagExcluding() {
		String	name = "testBagExcluding";
		
		evaluateBooleanResult(name, "(Bag {}->excluding(1))->isEmpty()", true);
		evaluateBooleanResult(name, "(Bag {1}->excluding(1))->isEmpty()", true);
		evaluateBooleanResult(name, "Bag {2}->excluding(1) = Bag{2}", true);
		evaluateBooleanResult(name, "Bag {1}->excluding(2) = Bag{1}", true);
		evaluateBooleanResult(name, "Bag {1, 2}->excluding(1) = Bag{2}", true);
		evaluateBooleanResult(name, "Bag {1, 2}->excluding(2) = Bag{1}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 1, 1, 2, 2}->excluding(1) = Bag{2, 2, 2}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->excluding(3) = Bag { 6, 5, 4, 2, 1}", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->excluding(7) = Bag { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6}->excluding(null) = Bag { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6, null}->excluding(null) = Bag { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Bag {1, 2, 3, 4, 5, 6, null, null, null}->excluding(null) = Bag { 1, 2, 3, 4, 5, 6 }", true);
		evaluateInvalidResult(name, "Bag {1, 2, 3, 4, 5, 6}->excluding(10.div(0)) = Bag { 1, 2, 3, 4, 5, 6, 7}");
	}

	public	void	testBagCount() {
		String	name = "testBagCount";
		
		evaluateIntegerResult(name, "Bag {}->count(1)", 0);
		evaluateIntegerResult(name, "Bag {1}->count(1)", 1);
		evaluateIntegerResult(name, "Bag {2}->count(1)", 0);
		evaluateIntegerResult(name, "Bag {1}->count(2)", 0);
		evaluateIntegerResult(name, "Bag {1, 2}->count(1)", 1);
		evaluateIntegerResult(name, "Bag {1, 2}->count(2)", 1);
		evaluateIntegerResult(name, "Bag {1, 1, 1, 2, 2, 2}->count(2)", 3);
		evaluateIntegerResult(name, "Bag {1, 1, 1, 2, 2, 2}->count(1)", 3);		evaluateIntegerResult(name, "Bag {1, 2, 3, 4, 5, 6}->count(3)", 1);
		evaluateIntegerResult(name, "Bag {1, 2, 3, 4, 5, 6}->count(7)", 0);
		evaluateIntegerResult(name, "Bag {1, 2, 3, 4, 5, 6, null}->count(null)", 1);
		evaluateIntegerResult(name, "Bag {1, 2, null, null, 3, 4, 5, 6, null}->count(null)", 3);
		evaluateInvalidResult(name, "Bag {1, 2, null, null, 3, 4, 5, 6, 10.div(0)}->count(null)");
	}

	public	void	testFlatten() {
		String	name = "testFlatten";

		evaluateBooleanResult(name, "Bag { Bag{1}, Bag{2}, Bag{3} }->flatten() = Bag{ 1, 2, 3 }", true);
		evaluateBooleanResult(name, "Bag { Bag{1}, Bag{1}, Bag{1} }->flatten() = Bag{ 1, 1, 1 }", true);
		evaluateBooleanResult(name, "Bag { Bag{1}, Bag{1, 2, 3, 4 }, Bag{2, 1, 3} }->flatten() = Bag{ 1, 1, 1, 2, 2, 3, 3, 4 }", true);
		evaluateBooleanResult(name, "Bag { Bag{ Bag{1}, Bag{2}, Bag{9} }, Bag{1, 2, 3, 4 }, Bag{2, 1, 3} }->flatten() = Bag{ 1, 1, 1, 2, 2, 2, 3, 3, 4, 9 }", true);
		evaluateBooleanResult(name, "Bag { Bag{1}, Bag{1, 2, 3, 4 }, Bag{2, 1, 3}, 4, 5 }->flatten() = Bag{ 1, 1, 1, 2, 2, 3, 3, 4, 4, 5 }", true);
		evaluateBooleanResult(name, "Bag { Bag{1}, Bag{1, 2, 3, 4 }, Bag{2, 1, 3}, Bag{null} }->flatten()  = Bag{ 1, 1, 1, 2, 2, 3, 3, 4, null }", true);
		evaluateIntegerResult(name, "Bag { Bag{1}, Bag{1, 2, 3, 4 }, Bag{2, 1, 3}, Bag{null} }->flatten()->size()", 9); 
	}

	public	void	testConversions() {
		String	name = "testConversions";

		evaluateBooleanResult(name, "Bag { Bag{1}, Bag{1, 2, 3, 4 }, Bag{2, 1, 3} }->flatten() = Bag{ 1, 1, 1, 2, 2, 3, 3, 4 }", true);
		evaluateBooleanResult(name, "Bag{ 1, 1, 1, 2, 2, 3, 3, 4 }->asSet() = Set{ 1, 2, 3, 4 }", true);
		evaluateBooleanResult(name, "Bag { Bag{1}, Bag{1, 2, 3, 4 }, Bag{2, 1, 3} }->flatten()->asSet() = Set{ 1, 2, 3, 4 }", true);
		evaluateBooleanResult(name, "Bag { 1,1, 2,2,2,2, 3,3,3, 4}->asSet() = Set{ 1, 2, 3, 4 }", true);
	}


}
