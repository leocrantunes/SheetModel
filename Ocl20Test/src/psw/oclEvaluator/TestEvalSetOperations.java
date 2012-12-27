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
public class TestEvalSetOperations extends TestEval {

	
	public void	testSize() {
		String	name = "testSize";
		
		evaluateIntegerResult(name, "Set {1}->size() ", 1);
		evaluateIntegerResult(name, "Set {1, 2}->size() ", 2);
		evaluateIntegerResult(name, "Set {1, 2, 1, 2}->size() ", 2);
		evaluateIntegerResult(name, "Set {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}->size() ", 10);
		evaluateIntegerResult(name, "Set { }->size() ", 0);
		evaluateIntegerResult(name, "Set {null}->size()", 1);
		evaluateIntegerResult(name, "Set {null, 2}->size()", 2);
		evaluateInvalidResult(name, "Set {10/0}->size()");
		evaluateInvalidResult(name, "Set {10/0}->size() + 10");
		evaluateInvalidResult(name, "Set {10/0, 1.0, 2.0, 3.0}->size() + 10");
	}
	
	public void	testIncludes() {
		String	name = "testIncludes";
		
		evaluateBooleanResult(name, "Set {1}->includes(1) ", true);
		evaluateBooleanResult(name, "Set {1}->includes(2) ", false);
		evaluateBooleanResult(name, "Set {1, 2}->includes(1) ", true);
		evaluateBooleanResult(name, "Set {1, 2}->includes(2) ", true);
		evaluateBooleanResult(name, "Set {1, 2}->includes(3) ", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(1) ", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(10) ", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(40) ", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(45) ", false);
		evaluateBooleanResult(name, "Set { }->includes(0) ", false);
		evaluateBooleanResult(name, "Set {null}->includes(null)", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, null}->includes(null)", true);
		evaluateBooleanResult(name, "Set {null, 1, 2, 3}->includes(null)", true);
		evaluateInvalidResult(name, "Set {1.0, 10/0, 2, 3}->includes(null)");
		evaluateInvalidResult(name, "Set {1.0, 2.0, 3.0}->includes(10/0)");
	}

	public void	testExcludes() {
		String	name = "testExcludes";
		
		evaluateBooleanResult(name, "Set {1}->excludes(1) ", false);
		evaluateBooleanResult(name, "Set {1}->excludes(2) ", true);
		evaluateBooleanResult(name, "Set {1, 2}->excludes(1) ", false);
		evaluateBooleanResult(name, "Set {1, 2}->excludes(2) ", false);
		evaluateBooleanResult(name, "Set {1, 2}->excludes(3) ", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(1) ", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(10) ", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(40) ", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(45) ", true);
		evaluateBooleanResult(name, "Set { }->excludes(0) ", true);
		
		evaluateBooleanResult(name, "Set {null}->excludes(null)", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, null}->excludes(null)", false);
		evaluateBooleanResult(name, "Set {null, 1, 2, 3}->excludes(null)", false);
		evaluateInvalidResult(name, "Set {1.0, 10/0, 2, 3}->excludes(null)");
		evaluateInvalidResult(name, "Set {1.0, 2.0, 3.0}->excludes(10/0)");

	}

	public void	testCount() {
		String	name = "testCount";
		
		evaluateIntegerResult(name, "Set {1}->count(1) ", 1);
		evaluateIntegerResult(name, "Set {1}->count(2) ", 0);
		evaluateIntegerResult(name, "Set {1, 2}->count(1) ", 1);
		evaluateIntegerResult(name, "Set {1, 2}->count(2) ", 1);
		evaluateIntegerResult(name, "Set {1, 2}->count(3) ", 0);
		evaluateIntegerResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(1) ", 1);
		evaluateIntegerResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(10) ", 1);
		evaluateIntegerResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(40) ", 1);
		evaluateIntegerResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(45) ", 0);
		evaluateIntegerResult(name, "Set { }->count(1) ", 0);
		
		evaluateIntegerResult(name, "Set {null}->count(null)", 1);
		evaluateIntegerResult(name, "Set {1, 2}->count(null)", 0);
		evaluateIntegerResult(name, "Set {1, 2, null}->count(null)", 1);
		evaluateIntegerResult(name, "Set {null, 1, 2}->count(null)", 1);
		evaluateInvalidResult(name, "Set {1.0, 10/0, 2, 3}->count(10/0)");
		evaluateInvalidResult(name, "Set {1.0, 2.0, 3.0}->count(10/0)");

	}

	public void	testIncludesAll() {
		String	name = "testIncludesAll";
		
		evaluateBooleanResult(name, "Set {1}->includesAll(Set{1}) ", true);
		evaluateBooleanResult(name, "Set {1}->includesAll(Set{2}) ", false);
		evaluateBooleanResult(name, "Set {1, 2}->includesAll(Set{1,2}) ", true);
		evaluateBooleanResult(name, "Set {1, 2}->includesAll(Set{1}) ", true);
		evaluateBooleanResult(name, "Set {1, 2}->includesAll(Set{2}) ", true);
		evaluateBooleanResult(name, "Set {1, 2}->includesAll(Set{1,2,3}) ", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includesAll(Set{2, 3, 40, 70, 60, 50, 10, 9, 8, 1}) ", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includesAll(Set{2, 3, 40, 45, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "Set { }->includesAll(Set{1}) ", false);
		evaluateBooleanResult(name, "Set { }->includesAll(Set{}) ", true);
	}

	public void	testExcludesAll() {
		String	name = "testEncludesAll";
		
		evaluateBooleanResult(name, "Set {1}->excludesAll(Set{1}) ", false);
		evaluateBooleanResult(name, "Set {1}->excludesAll(Set{2}) ", true);
		evaluateBooleanResult(name, "Set {1, 2}->excludesAll(Set{1,2}) ", false);
		evaluateBooleanResult(name, "Set {1, 2}->excludesAll(Set{1}) ", false);
		evaluateBooleanResult(name, "Set {1, 2}->excludesAll(Set{2}) ", false);
		evaluateBooleanResult(name, "Set {1, 2}->excludesAll(Set{1,2,3}) ", false);
		evaluateBooleanResult(name, "Set {1, 2}->excludesAll(Set{3}) ", true);
		evaluateBooleanResult(name, "Set {1, 2}->excludesAll(Set{3, 4, 5}) ", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Set{2, 3, 40, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Set{2, 3, 40, 45, 70, 60, 50, 10, 9, 8, 1}) ", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Set{100, 200, 300, 400, 500}) ", true);
		evaluateBooleanResult(name, "Set { }->excludesAll(Set{1}) ", true);
		evaluateBooleanResult(name, "Set { }->excludesAll(Set{}) ", true);
		evaluateInvalidResult(name, "Set {1, 2.0, 10/0, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Set{100, 200, 300, 400, 500}) ");

	}

	public void	testIsEmpty() {
		String	name = "testIsEmpty";
		
		evaluateBooleanResult(name, "Set {1}->isEmpty() ", false);
		evaluateBooleanResult(name, "Set {1, 2}->isEmpty() ", false);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->isEmpty() ", false);
		evaluateBooleanResult(name, "Set { }->isEmpty() ", true);
		evaluateBooleanResult(name, "Set { null }->isEmpty() ", false);
		evaluateInvalidResult(name, "Set { 10/0 }->isEmpty() ");
	}

	public void	testNotEmpty() {
		String	name = "testNotEmpty";
		
		evaluateBooleanResult(name, "Set {1}->notEmpty() ", true);
		evaluateBooleanResult(name, "Set {1, 2}->notEmpty() ", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->notEmpty() ", true);
		evaluateBooleanResult(name, "Set { }->notEmpty() ", false);
		evaluateBooleanResult(name, "Set { null }->notEmpty() ", true);
		evaluateInvalidResult(name, "Set { 10/0 }->notEmpty() ");
	}

	public void	testSum() {
		String	name = "testSum";
		
		evaluateIntegerResult(name, "Set {0}->sum() ", 0);
		evaluateIntegerResult(name, "Set {1}->sum() ", 1);
		evaluateIntegerResult(name, "Set {1, 2}->sum() ", 3);
		evaluateIntegerResult(name, "Set {-1, -2, 0, 1, 2}->sum() ", 0);
		evaluateIntegerResult(name, "Set {0..10}->sum() ", 55);
		evaluateIntegerResult(name, "Set {-1, -2, 0, null, 1, 2}->sum() ", 0);
		evaluateIntegerResult(name, "Set {-1, -2, 0, null, 1, 2}->size() ", 6);
		evaluateInvalidResult(name, "Set {10/0, 1.0, 2.0, 3.0}->sum()");
	}

	
	public void	testAvg() {
		String	name = "testAvg";
		
		evaluateRealResult(name, "Set {0}->avg() ", 0);
		evaluateRealResult(name, "Set {1}->avg() ", 1);
		evaluateRealResult(name, "Set {1, 2}->avg() ", 1.5);
		evaluateRealResult(name, "Set {-1, -2, 0, 1, 2}->avg() ", 0);
		evaluateRealResult(name, "Set {0..10}->avg() ", 5.0);
		evaluateRealResult(name, "Set {-1, -2, 0, null, 1, 2}->avg() ", 0);
		evaluateInvalidResult(name, "Set {10/0, 1.0, 2.0, 3.0}->avg()");
	}

	public void	testMax() {
		String	name = "testMax";
		
		evaluateIntegerResult(name, "Set {0}->max() ", 0);
		evaluateIntegerResult(name, "Set {1}->max() ", 1);
		evaluateIntegerResult(name, "Set {1, 2}->max() ", 2);
		evaluateIntegerResult(name, "Set {-1, -2, 0, 1, 2}->max() ", 2);
		evaluateIntegerResult(name, "Set {0..10}->max() ", 10);
		evaluateIntegerResult(name, "Set {-1, -2, 0, null, 1, 2}->max() ", 2);
		evaluateInvalidResult(name, "Set {10/0, 1.0, 2.0, 3.0}->max()");
	}

	public void	testMin() {
		String	name = "testMin";
		
		evaluateIntegerResult(name, "Set {0}->min() ", 0);
		evaluateIntegerResult(name, "Set {1}->min() ", 1);
		evaluateIntegerResult(name, "Set {1, 2}->min() ", 1);
		evaluateIntegerResult(name, "Set {-1, -2, 0, 1, 2}->min() ", -2);
		evaluateIntegerResult(name, "Set {0..10}->min() ", 0);
		evaluateIntegerResult(name, "Set {-1, -2, 0, null, 1, 2}->min() ", -2);
		evaluateInvalidResult(name, "Set {10/0, 1.0, 2.0, 3.0}->min()");
	}
	
	public	void	testSetUnion() {
		String	name = "testSetUnion";
		
		evaluateBooleanResult(name, "Set {}->union(Set{})->isEmpty() ", true);
		evaluateBooleanResult(name, "Set {}->union(Set{1})->includesAll(Set{1}) ", true);
		evaluateBooleanResult(name, "Set {}->union(Set{1, 2})->includesAll(Set{1, 2}) ", true);
		evaluateBooleanResult(name, "Set {1}->union(Set{2})->includesAll(Set{1, 2}) ", true);
		evaluateBooleanResult(name, "Set {1, 3}->union(Set{2})->includesAll(Set{1, 2, 3}) ", true);
		evaluateBooleanResult(name, "Set {1, 3}->union(Set{2, 4, 5})->includesAll(Set{1, 2, 3, 4, 5}) ", true);
		evaluateBooleanResult(name, "Set {1, 3}->union(Set{1, 3})->includesAll(Set{1, 3}) ", true);
		evaluateBooleanResult(name, "Set {1, 3}->union(Set{1, 2, 3, 4})->includesAll(Set{1, 2, 3, 4}) ", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4}->union(Set{})->includesAll(Set{1, 2, 3, 4}) ", true);
	}
	
	public	void	testBagUnion() {
		String	name = "testBagUnion";
		
		evaluateIntegerResult(name, "Set {}->union(Bag{})->size()", 0);
		evaluateIntegerResult(name, "Set {}->union(Bag{1})->size()", 1);
		evaluateIntegerResult(name, "Set {}->union(Bag{1, 2})->size()", 2);
		evaluateIntegerResult(name, "Set {1}->union(Bag{2})->size()", 2);
		evaluateIntegerResult(name, "Set {1, 3}->union(Bag{2})->size()", 3);
		evaluateIntegerResult(name, "Set {1, 3}->union(Bag{2, 4, 5})->size()", 5);
		evaluateIntegerResult(name, "Set {1, 3}->union(Bag{1, 3})->size()", 4);
		evaluateIntegerResult(name, "Set {1, 3}->union(Bag{1, 2, 3, 4})->size()", 6);
		evaluateIntegerResult(name, "Set {1, 3}->union(Bag{1, 2, 2, 3, 3, 4, 5})->size()", 9);
		
	}

	public	void	testSetEquality() {
		String	name = "testSetEquality";
		
		evaluateBooleanResult(name, "Set {} = Set {}", true);
		evaluateBooleanResult(name, "Set {} = Set {1}", false);
		evaluateBooleanResult(name, "Set {1} = Set {}", false);
		evaluateBooleanResult(name, "Set {1} = Set {1}", true);
		evaluateBooleanResult(name, "Set {2} = Set {1}", false);
		evaluateBooleanResult(name, "Set {1} = Set {2}", false);
		evaluateBooleanResult(name, "Set {1, 2} = Set {1}", false);
		evaluateBooleanResult(name, "Set {1, 2} = Set {1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2} = Set {2, 1}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6} = Set {2, 1, 5, 4, 3, 6}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6} = Set {2, 1, 5, 4, 3}", false);
		evaluateBooleanResult(name, "Set {}->union(Set{1, 2}) = Set{1, 2}", true);
		
//		evaluateBooleanResult(name, "Set {1, 2, 3, 4}->union(Set{true}) = Set{1, 2, 3, 4, true} ", true);
	}

	public	void	testSetIntersection() {
		String	name = "testBagUnion";
		
		evaluateBooleanResult(name, "Set {}->intersection(Set {})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {}->intersection(Set {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1}->intersection(Set {})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1}->intersection(Set {1})->includesAll(Set{1})", true);
		evaluateBooleanResult(name, "Set {2}->intersection(Set {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1}->intersection(Set {2})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1, 2}->intersection(Set {1}) = Set{1}", true);
		evaluateBooleanResult(name, "Set {1, 2}->intersection(Set {1, 2}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2}->intersection(Set {2, 1}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->intersection(Set {2, 1, 5, 4, 3, 6}) = Set { 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->intersection(Set {2, 1, 5, 4, 3}) = Set { 1, 2, 3, 4, 5}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->intersection(Set {2, 7, 8, 1, 9, 4, 3}) = Set { 1, 2, 3, 4}", true);
	}

	public	void	testBagIntersection() {
		String	name = "testBagIntersection";
		
		evaluateBooleanResult(name, "Set {}->intersection(Bag {})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {}->intersection(Bag {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1}->intersection(Bag {1})->includesAll(Set{1})", true);
		evaluateBooleanResult(name, "Set {2}->intersection(Bag {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1}->intersection(Bag {2})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1, 2}->intersection(Bag {1}) = Set{1}", true);
		evaluateBooleanResult(name, "Set {1, 2}->intersection(Bag {1, 2}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2}->intersection(Bag {1, 1, 2, 2, 2, 2}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2}->intersection(Bag {2, 1}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->intersection(Bag {2, 1, 2, 1, 5, 4, 3, 6}) = Set { 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->intersection(Bag {2, 1, 5, 4, 4, 3, 3}) = Set { 1, 2, 3, 4, 5}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->intersection(Bag {2, 7, 8, 1, 9, 1, 9, 4, 3}) = Set { 1, 2, 3, 4}", true);
	}


	public	void	testSetDifference() {
		String	name = "testSetDifference";
		
		evaluateBooleanResult(name, "(Set {} - Set {})->isEmpty()", true);
		evaluateBooleanResult(name, "(Set {} - Set {1})->isEmpty()", true);
		evaluateBooleanResult(name, "(Set {1} - Set {1})->isEmpty()", true);
		evaluateBooleanResult(name, "(Set {2} - Set {1}) = Set{2}", true);
		evaluateBooleanResult(name, "(Set {1} - Set {2}) = Set{1}", true);
		evaluateBooleanResult(name, "Set {1, 2} - Set {1} = Set{2}", true);
		evaluateBooleanResult(name, "Set {1, 2} - Set {2} = Set{1}", true);
		evaluateBooleanResult(name, "(Set {1, 2} - Set {1, 2})->isEmpty()", true);
		evaluateBooleanResult(name, "(Set {1, 2} - Set {1, 2})->isEmpty()", true);
		evaluateBooleanResult(name, "(Set {1, 2} - Set {2, 1})->isEmpty()", true);
		evaluateBooleanResult(name, "(Set {1, 2, 3, 4, 5, 6} - Set {2, 1, 5, 4, 3, 6})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6} - Set {2, 1, 5, 4, 3} = Set { 6 }", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6} - Set {5, 3} = Set { 1, 2, 4, 6 }", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6} - Set {2, 7, 8, 1, 9, 4, 3} = Set { 5, 6}", true);
		
		evaluateBooleanResult(name, "Set {1, 2} - Set {2, 1} = Set{}", true);
	}
	

	public	void	testSetIncluding() {
		String	name = "testSetIncluding";
		
		evaluateBooleanResult(name, "Set {}->including(1) = Set{1}", true);
		evaluateBooleanResult(name, "Set {1}->including(1) = Set{1}", true);
		evaluateBooleanResult(name, "Set {2}->including(1) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1}->including(2) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2}->including(1) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2}->including(2) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->including(3) = Set { 6, 5, 4, 3, 2, 1}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->including(7) = Set { 1, 2, 3, 4, 5, 6, 7 }", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->including(null) = Set { 1, 2, 3, 4, 5, 6, null }", true);
		evaluateInvalidResult(name, "Set {1, 2, 3, 4, 5, 6}->including(10.div(0))");
	}

	public	void	testSetExcluding() {
		String	name = "testSetExcluding";
		
		evaluateBooleanResult(name, "(Set {}->excluding(1))->isEmpty()", true);
		evaluateBooleanResult(name, "(Set {1}->excluding(1))->isEmpty()", true);
		evaluateBooleanResult(name, "Set {2}->excluding(1) = Set{2}", true);
		evaluateBooleanResult(name, "Set {1}->excluding(2) = Set{1}", true);
		evaluateBooleanResult(name, "Set {1, 2}->excluding(1) = Set{2}", true);
		evaluateBooleanResult(name, "Set {1, 2}->excluding(2) = Set{1}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->excluding(3) = Set { 6, 5, 4, 2, 1}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->excluding(7) = Set { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6}->excluding(null) = Set { 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6, null}->excluding(null) = Set { 1, 2, 3, 4, 5, 6}", true);
		evaluateInvalidResult(name, "Set {1, 2, 3, 4, 5, 6}->excluding(10.div(0))");

	}

	public	void	testSymmetricDifference() {
		String	name = "testSymmetricDifference";

		evaluateBooleanResult(name, "Set {} ->symmetricDifference(Set {})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {} ->symmetricDifference(Set {1}) = Set {1}", true);
		evaluateBooleanResult(name, "Set {1} ->symmetricDifference(Set {1})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {2} ->symmetricDifference(Set {1}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1} ->symmetricDifference(Set {2}) = Set{1, 2}", true);
		evaluateBooleanResult(name, "Set {1} ->symmetricDifference(Set {1, 2}) = Set{2}", true);
		evaluateBooleanResult(name, "Set {1, 2} ->symmetricDifference(Set {1}) = Set{2}", true);
		evaluateBooleanResult(name, "Set {1, 2} ->symmetricDifference(Set {2}) = Set{1}", true);
		evaluateBooleanResult(name, "Set {1, 2} ->symmetricDifference(Set {1, 2})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1, 2} ->symmetricDifference(Set {1, 2})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1, 2} ->symmetricDifference(Set {2, 1})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6} ->symmetricDifference(Set {2, 1, 5, 4, 3, 6})->isEmpty()", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6} ->symmetricDifference(Set {2, 1, 5, 4, 3}) = Set { 6 }", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6} ->symmetricDifference(Set {5, 3}) = Set { 1, 2, 4, 6 }", true);
		evaluateBooleanResult(name, "Set {5, 3} ->symmetricDifference(Set {1, 2, 3, 4, 5, 6}) = Set { 1, 2, 4, 6 }", true);
		evaluateBooleanResult(name, "Set {5, 3, 7, 9} ->symmetricDifference(Set {1, 2, 3, 4, 5, 6}) = Set { 7, 9, 1, 2, 4, 6 }", true);
		evaluateBooleanResult(name, "Set {1, 2, 3, 4, 5, 6, 10} ->symmetricDifference(Set {2, 7, 8, 1, 9, 4, 3}) = Set { 5, 6, 7, 8, 9, 10}", true);
	}

	public	void	testFlatten() {
		String	name = "testFlatten";

		evaluateBooleanResult(name, "Set { Set{1}, Set{2}, Set{3} }->flatten() = Set{ 1, 2, 3 }", true);
		evaluateBooleanResult(name, "Set { Set{1}, Set{1}, Set{1} }->flatten() = Set{ 1 }", true);
		evaluateBooleanResult(name, "Set { Set{1}, Set{1, 2, 3, 4 }, Set{2, 1, 3} }->flatten() = Set{ 1, 2, 3, 4 }", true);
		evaluateBooleanResult(name, "Set { Set{ Set{1}, Set{2}, Set{9} }, Set{1, 2, 3, 4 }, Set{2, 1, 3}, Set { Set{ Set{1, 2, 5 }, Set{ 2, 5, 6 } }, Set{Set {1, 2, 6}, Set{ 4, 5, 6, 7}}}  }->flatten() = Set{ 1, 2, 3, 4, 5, 6, 7, 9 }", true);
		evaluateBooleanResult(name, "Set { Set{1}, Set{}, Set{2}, Set{3} }->flatten() = Set{ 1, 2, 3 }", true);
		evaluateBooleanResult(name, "Set { Set{1}, Set{2}, Set{3}, Set{ Set{7, 4}, Set{ 8, 2}}, 4, 5 }->flatten() = Set{ 1, 2, 3, 4, 5, 7, 8 }", true);
		evaluateBooleanResult(name, "Set { Set{1}, null, Set{2}, Set{3} }->flatten() = Set{ 1, 2, 3, null }", true);
		evaluateBooleanResult(name, "Set { Set{1}, Set{null}, Set{2}, Set{3} }->flatten() = Set{ 1, 2, 3, null }", true);
	}
	
	public	void	testConversions() {
		String	name = "testConversions";

		evaluateBooleanResult(name, "Set{1,2,3}->asBag() = Bag{1, 2, 3}", true);
		evaluateBooleanResult(name, "Set { Set{1}, Set{1, 2, 3, 4 }, Set{2, 1, 3} }->flatten()->asBag() = Bag{ 1, 2, 3, 4 }", true);
	}

	public	void	testProduct() {
		String	name = "testProduct";

		evaluateIntegerResult(name, "Set{1,2,3}->product(Set{1})->size()", 3);
		evaluateIntegerResult(name, "Set{1,2,3}->product(Set{1,2})->size()", 6);
		evaluateIntegerResult(name, "Set{1,2,3}->product(Set{4,5,6})->size()", 9);
		evaluateIntegerResult(name, "Set{1,2,3}->product(Set{})->size()", 0);
		evaluateIntegerResult(name, "Set{}->product(Set{1, 2, 3})->size()", 0);
		evaluateIntegerResult(name, "Set{1}->product(Set{1, 2, 3})->size()", 3);
		evaluateIntegerResult(name, "Set{}->product(Set{})->size()", 0);
		
		evaluateIntegerResult(name, "Set{1,2,3}->product(Bag{1,1, 2, 2})->size()", 12);
		evaluateIntegerResult(name, "Set{1,2,3}->product(Sequence{1,1, 2, 2})->size()", 12);
		evaluateIntegerResult(name, "Set{1,2,3}->product(OrderedSet{1,2, 3, 4})->size()", 12);
		
		evaluateIntegerResult(name, "Set{1,2,3}->product(Set{1,2})->collect(first)->sum()", 12);
		evaluateIntegerResult(name, "Set{1,2,3}->product(Set{1,2})->collect(second)->sum()", 9);
		evaluateIntegerResult(name, "Set{1,2,3}->product(Set{4,5,6})->collect(first)->sum()", 18);
		evaluateIntegerResult(name, "Set{1,2,3}->product(Set{4,5,6})->collect(second)->sum()", 45);
	}
}
