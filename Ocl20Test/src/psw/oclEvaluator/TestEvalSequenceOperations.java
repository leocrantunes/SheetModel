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
public class TestEvalSequenceOperations extends TestEval {
	
		public void	testSize() {
			String	name = "testSize";
		
			evaluateIntegerResult(name, "Sequence {1}->size() ", 1);
			evaluateIntegerResult(name, "Sequence {1, 1}->size() ", 2);
			evaluateIntegerResult(name, "Sequence {1, 2}->size() ", 2);
			evaluateIntegerResult(name, "Sequence {1, 2, 2, 1}->size() ", 4);
			evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}->size() ", 10);
			evaluateIntegerResult(name, "Sequence { }->size() ", 0);
			evaluateIntegerResult(name, "Sequence {null}->size() ", 1);
			evaluateIntegerResult(name, "Sequence {null, null}->size() ", 2);
			evaluateIntegerResult(name, "Sequence {null, 1}->size() ", 2);
			evaluateIntegerResult(name, "Sequence {1, null}->size() ", 2);
			evaluateInvalidResult(name, "Sequence {1, 1.div(0)}->size() ");
		}
	
		public void	testIncludes() {
			String	name = "testIncludes";
		
			evaluateBooleanResult(name, "Sequence {1}->includes(1) ", true);
			evaluateBooleanResult(name, "Sequence {1, 1}->includes(1) ", true);
			evaluateBooleanResult(name, "Sequence {1}->includes(2) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->includes(1) ", true);
			evaluateBooleanResult(name, "Sequence {2, 1, 1, 2}->includes(1) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->includes(2) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->includes(3) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(1) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(10) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(40) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includes(45) ", false);
			evaluateBooleanResult(name, "Sequence { }->includes(0) ", false);
			evaluateBooleanResult(name, "Sequence {null}->includes(null) ", true);
			evaluateBooleanResult(name, "Sequence {null, 10}->includes(null) ", true);
			evaluateBooleanResult(name, "Sequence {10, null}->includes(null) ", true);
			evaluateBooleanResult(name, "Sequence {1,2,3}->includes(null) ", false);
			evaluateInvalidResult(name, "Sequence {1.div(0)}->includes(1) ");
		}

		public void	testExcludes() {
			String	name = "testExcludes";
		
			evaluateBooleanResult(name, "Sequence {1}->excludes(1) ", false);
			evaluateBooleanResult(name, "Sequence {1}->excludes(2) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludes(1) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludes(2) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludes(3) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(1) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(10) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(40) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludes(45) ", true);
			evaluateBooleanResult(name, "Sequence { }->excludes(0) ", true);
			evaluateBooleanResult(name, "Sequence {null}->excludes(null) ", false);
			evaluateBooleanResult(name, "Sequence {null, 10}->excludes(null) ", false);
			evaluateBooleanResult(name, "Sequence {10, null}->excludes(null) ", false);
			evaluateBooleanResult(name, "Sequence {1,2,3}->excludes(null) ", true);
			evaluateInvalidResult(name, "Sequence {1.div(0)}->excludes(1) ");
		}

		public void	testCount() {
			String	name = "testCount";
		
			evaluateIntegerResult(name, "Sequence {1}->count(1) ", 1);
			evaluateIntegerResult(name, "Sequence {1, 1}->count(1) ", 2);
			evaluateIntegerResult(name, "Sequence {1}->count(2) ", 0);
			evaluateIntegerResult(name, "Sequence {1, 2}->count(1) ", 1);
			evaluateIntegerResult(name, "Sequence {1, 2, 1, 2, 1}->count(1) ", 3);
			evaluateIntegerResult(name, "Sequence {1, 2}->count(2) ", 1);
			evaluateIntegerResult(name, "Sequence {1, 2}->count(3) ", 0);
			evaluateIntegerResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(1) ", 1);
			evaluateIntegerResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(10) ", 1);
			evaluateIntegerResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(40) ", 1);
			evaluateIntegerResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->count(45) ", 0);
			evaluateIntegerResult(name, "Sequence { }->count(1) ", 0);
			
			evaluateIntegerResult(name, "Sequence {null}->count(null) ", 1);
			evaluateIntegerResult(name, "Sequence {null, 1, 2}->count(null) ", 1);
			evaluateIntegerResult(name, "Sequence {1, 2, null}->count(null) ", 1);
			evaluateIntegerResult(name, "Sequence {null, 1, null}->count(null) ", 2);
			evaluateIntegerResult(name, "Sequence {1, 2, 3}->count(null) ", 0);
			evaluateInvalidResult(name, "Sequence {1.div(0)}->count(1) ");

		}

		public void	testIncludesAll() {
			String	name = "testIncludesAll";
		
			evaluateBooleanResult(name, "Sequence {1}->includesAll(Sequence{1}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 1}->includesAll(Sequence{1, 1}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 1}->includesAll(Sequence{1}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 1}->includesAll(Sequence{1, 1, 1}) ", true);
		
			evaluateBooleanResult(name, "Sequence {1}->includesAll(Sequence{2}) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->includesAll(Sequence{1,2}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 4}->includesAll(Set{1,2}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 4}->includesAll(Bag{1,1, 2, 2, 3}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->includesAll(Sequence{1}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->includesAll(Sequence{2}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->includesAll(Sequence{1,2,3}) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includesAll(Sequence{2, 3, 40, 70, 60, 50, 10, 9, 8, 1}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->includesAll(Sequence{2, 3, 40, 45, 70, 60, 50, 10, 9, 8, 1}) ", false);
			evaluateBooleanResult(name, "Sequence { }->includesAll(Sequence{1}) ", false);
			evaluateBooleanResult(name, "Sequence { }->includesAll(Sequence{}) ", true);
		}

		public void	testExcludesAll() {
			String	name = "testEncludesAll";
		
			evaluateBooleanResult(name, "Sequence {1}->excludesAll(Sequence{1}) ", false);
			evaluateBooleanResult(name, "Sequence {1}->excludesAll(Sequence{2}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludesAll(Sequence{1,2}) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludesAll(Sequence{1}) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludesAll(Sequence{2}) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludesAll(Sequence{1,2,3}) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludesAll(Sequence{3}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->excludesAll(Sequence{3, 4, 5}) ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Sequence{2, 3, 40, 70, 60, 50, 10, 9, 8, 1}) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Sequence{2, 3, 40, 45, 70, 60, 50, 10, 9, 8, 1}) ", false);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->excludesAll(Sequence{100, 200, 300, 400, 500}) ", true);
			evaluateBooleanResult(name, "Sequence { }->excludesAll(Sequence{1}) ", true);
			evaluateBooleanResult(name, "Sequence { }->excludesAll(Sequence{}) ", true);
		}

		public void	testIsEmpty() {
			String	name = "testIsEmpty";
		
			evaluateBooleanResult(name, "Sequence {1}->isEmpty() ", false);
			evaluateBooleanResult(name, "Sequence {1, 2}->isEmpty() ", false);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->isEmpty() ", false);
			evaluateBooleanResult(name, "Sequence { }->isEmpty() ", true);
			evaluateBooleanResult(name, "Sequence {null}->isEmpty() ", false);
		}

		public void	testNotEmpty() {
			String	name = "testNotEmpty";
		
			evaluateBooleanResult(name, "Sequence {1}->notEmpty() ", true);
			evaluateBooleanResult(name, "Sequence {1, 2}->notEmpty() ", true);
			evaluateBooleanResult(name, "Sequence {1, 2, 3, 40, 50, 60, 70, 8, 9, 10 }->notEmpty() ", true);
			evaluateBooleanResult(name, "Sequence { }->notEmpty() ", false);
			evaluateBooleanResult(name, "Sequence {null}->notEmpty() ", true);
		}

		public void	testSum() {
			String	name = "testSum";
		
			evaluateIntegerResult(name, "Sequence {0}->sum() ", 0);
			evaluateIntegerResult(name, "Sequence {1}->sum() ", 1);
			evaluateIntegerResult(name, "Sequence {1, 1}->sum() ", 2);
			evaluateIntegerResult(name, "Sequence {1, 2}->sum() ", 3);
			evaluateIntegerResult(name, "Sequence {-1, -2, 0, 1, 2, -1, -2, 1, 2, 5, 10}->sum() ", 15);
			evaluateIntegerResult(name, "Sequence {0..10}->sum() ", 55);
			evaluateIntegerResult(name, "Sequence {1, 1, null, null, 2, 2, null}->sum() ", 6);
		}

	public	void	testSequenceCount() {
		String	name = "testSequenceCount";
		
		evaluateIntegerResult(name, "Sequence {}->count(1)", 0);
		evaluateIntegerResult(name, "Sequence {1}->count(1)", 1);
		evaluateIntegerResult(name, "Sequence {2}->count(1)", 0);
		evaluateIntegerResult(name, "Sequence {1}->count(2)", 0);
		evaluateIntegerResult(name, "Sequence {1, 2}->count(1)", 1);
		evaluateIntegerResult(name, "Sequence {1, 2}->count(2)", 1);
		evaluateIntegerResult(name, "Sequence {1, 1, 1, 2, 2, 2}->count(2)", 3);
		evaluateIntegerResult(name, "Sequence {1, 1, 1, 2, 2, 2}->count(1)", 3);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->count(3)", 1);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->count(7)", 0);
	}

	public	void	testSequenceEquality() {
		String	name = "testSequenceEquality";
		
		evaluateBooleanResult(name, "Sequence {} = Sequence {}", true);
		evaluateBooleanResult(name, "Sequence {} = Sequence {1}", false);
		evaluateBooleanResult(name, "Sequence {1} = Sequence {}", false);
		evaluateBooleanResult(name, "Sequence {1} = Sequence {1}", true);
		evaluateBooleanResult(name, "Sequence {2} = Sequence {1}", false);
		evaluateBooleanResult(name, "Sequence {1} = Sequence {2}", false);
		evaluateBooleanResult(name, "Sequence {1, 2} = Sequence {1}", false);
		evaluateBooleanResult(name, "Sequence {1, 2} = Sequence {1, 2}", true);
		evaluateBooleanResult(name, "Sequence {1, 2} = Sequence {2, 1}", false);
		evaluateBooleanResult(name, "Sequence {1, 2, 1, 2, 1, 1, 3, 3} = Sequence {3, 3, 2, 2, 1, 1, 1, 1}", false);
		evaluateBooleanResult(name, "Sequence {1, 2, 1, 2, 1, 1, 3, 3} = Sequence {3, 3, 2, 2, 1, 1, 1}", false);
		evaluateBooleanResult(name, "Sequence {1, 2, 1, 2, 1, 1 } = Sequence {3, 3, 2, 2, 1, 1, 1, 1}", false);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6} = Sequence {2, 1, 5, 4, 3, 6}", false);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6} = Sequence {1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6} = Sequence {2, 1, 5, 4, 3}", false);
		
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4}->union(Sequence{})->includesAll(Sequence{1, 2, 3, 4}) ", true);
		
		evaluateBooleanResult(name, "Sequence {null} = Sequence {null}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, null} = Sequence {1, 2, null}", true);
	}


	public	void	testSequenceUnion() {
		String	name = "testSequenceUnion";
		
		evaluateBooleanResult(name, "Sequence {}->union(Sequence{})->isEmpty() ", true);
		evaluateBooleanResult(name, "Sequence {}->union(Sequence{1}) = Sequence{1} ", true);
		evaluateBooleanResult(name, "Sequence {}->union(Sequence{1, 2}) = Sequence{1, 2} ", true);
		evaluateBooleanResult(name, "Sequence {1}->union(Sequence{2}) = Sequence{1, 2} ", true);
		evaluateBooleanResult(name, "Sequence {1}->union(Sequence{1}) = Sequence{1, 1} ", true);
		evaluateBooleanResult(name, "Sequence {1}->union(Sequence{1, 2}) = Sequence{1, 1, 2} ", true);
		evaluateBooleanResult(name, "Sequence {1, 3}->union(Sequence{2}) = Sequence{1, 3, 2} ", true);
		evaluateBooleanResult(name, "Sequence {1, 2,  3}->union(Sequence{2}) = Sequence{1, 2, 3, 2} ", true);
		evaluateBooleanResult(name, "Sequence {1, 3}->union(Sequence{2, 4, 5}) = Sequence{1, 3, 2, 4, 5} ", true);
		evaluateBooleanResult(name, "Sequence {1, 3}->union(Sequence{1, 3}) = Sequence{1, 3,  1,  3} ", true);
		evaluateBooleanResult(name, "Sequence {1, 3}->union(Sequence{1, 1, 3, 3}) = Sequence{1, 3, 1, 1,  3,  3} ", true);
		evaluateBooleanResult(name, "Sequence {1, 3}->union(Sequence{1, 2, 3, 4}) = Sequence{1, 3, 1, 2, 3, 4} ", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4}->union(Sequence{})->includesAll(Sequence{1, 2, 3, 4}) ", true);
	}

	public	void	testSequenceAppend() {
		String	name = "testSequenceAppend";
		
		evaluateBooleanResult(name, "Sequence {}->append(1) = Sequence{1}", true);
		evaluateBooleanResult(name, "Sequence {1}->append(1) = Sequence{1, 1}", true);
		evaluateBooleanResult(name, "Sequence {2}->append(1) = Sequence{2, 1}", true);
		evaluateBooleanResult(name, "Sequence {1}->append(2) = Sequence{1, 2}", true);
		evaluateBooleanResult(name, "Sequence {1, 2}->append(1) = Sequence{1, 2, 1}", true);
		evaluateBooleanResult(name, "Sequence {1, 2}->append(2) = Sequence{1, 2, 2 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->append(3) = Sequence { 1, 2, 3, 4, 5, 6, 3}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->append(7) = Sequence { 1, 2, 3, 4, 5, 6, 7 }", true);
	}

	public	void	testSequencePrepend() {
		String	name = "testSequencePrepend";
		
		evaluateBooleanResult(name, "Sequence {}->prepend(1) = Sequence{1}", true);
		evaluateBooleanResult(name, "Sequence {1}->prepend(1) = Sequence{1, 1}", true);
		evaluateBooleanResult(name, "Sequence {2}->prepend(1) = Sequence{1, 2}", true);
		evaluateBooleanResult(name, "Sequence {1}->prepend(2) = Sequence{2, 1}", true);
		evaluateBooleanResult(name, "Sequence {1, 2}->prepend(1) = Sequence{1, 1, 2}", true);
		evaluateBooleanResult(name, "Sequence {1, 2}->prepend(2) = Sequence{2, 1, 2 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->prepend(3) = Sequence { 3, 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->prepend(7) = Sequence { 7, 1, 2, 3, 4, 5, 6}", true);
	}

	public	void	testSequenceInsertAt() {
		String	name = "testSequenceInsertAt";
		
		evaluateBooleanResult(name, "Sequence {}->insertAt(1, 9) = Sequence{9}", true);
		evaluateBooleanResult(name, "Sequence {1}->insertAt(1, 9) = Sequence{9, 1}", true);
		evaluateBooleanResult(name, "Sequence {2}->insertAt(1, 9) = Sequence{9, 2 }", true);
		evaluateBooleanResult(name, "Sequence {8}->insertAt(2, 9) = Sequence{8, 9}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->insertAt(1, 9) = Sequence { 9, 1, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->insertAt(7, 9) = Sequence { 1, 2, 3, 4, 5, 6, 9}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->insertAt(2, 9) = Sequence { 1, 9, 2, 3, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->insertAt(6, 9) = Sequence { 1, 2, 3, 4, 5, 9, 6}", true);
		evaluateInvalidResult(name, "Sequence {}->insertAt(2, 0) = Sequence{0}");
	}

	public	void	testSubSequence() {
		String	name = "testSubSequence";
		
		evaluateInvalidResult(name, "Sequence {}->subSequence(1, 1) = Sequence{}");
		evaluateBooleanResult(name, "Sequence {1}->subSequence(1, 1) = Sequence{1}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->subSequence(1, 1) = Sequence { 1 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->subSequence(1, 2) = Sequence { 1, 2 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->subSequence(1, 6) = Sequence { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->subSequence(6, 6) = Sequence { 6 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->subSequence(4, 6) = Sequence { 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->subSequence(2, 4) = Sequence { 2, 3, 4 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->subSequence(4, 2) = Sequence { }", true);

		evaluateBooleanResult(name, "Sequence {null}->subSequence(1, 1) = Sequence {null}", true);
		evaluateBooleanResult(name, "Sequence {null, 1, 2, null}->subSequence(3, 4) = Sequence {2, null}", true);

		evaluateBooleanResult(name, "Sequence {}->subSequence(2, 0) = Sequence{0}", false);
		evaluateInvalidResult(name, "Sequence {8}->subSequence(1, 2)");
		evaluateInvalidResult(name, "Sequence {1, 2, 3, 4, 5, 6}->subSequence(8, 10)");

	}

	public	void	testSequenceAt() {
		String	name = "testSequenceAt";
		
		evaluateInvalidResult(name, "Sequence {}->at(1) = Sequence{1}");
		evaluateIntegerResult(name, "Sequence {1}->at(1)", 1);
		evaluateIntegerResult(name, "Sequence {2}->at(1)", 2);
		evaluateIntegerResult(name, "Sequence {1, 2}->at(1)", 1);
		evaluateIntegerResult(name, "Sequence {1, 2}->at(2)", 2);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->at(3)", 3);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->at(6)", 6);
		evaluateInvalidResult(name, "Sequence {1, 2, 3, 4, 5, 6}->at(7)");
		evaluateInvalidResult(name, "Sequence {1, 2, 3, 4, 5, 6}->at(0)");
	}

	public	void	testSequenceFirst() {
		String	name = "testSequenceFirst";
		
		evaluateInvalidResult(name, "Sequence {}->first()");
		evaluateIntegerResult(name, "Sequence {1}->first()", 1);
		evaluateIntegerResult(name, "Sequence {2}->first()", 2);
		evaluateIntegerResult(name, "Sequence {1, 2}->first()", 1);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->first()", 1);
	}

	public	void	testSequenceLast() {
		String	name = "testSequenceLast";
		
		evaluateInvalidResult(name, "Sequence {}->last()");
		evaluateIntegerResult(name, "Sequence {1}->last()", 1);
		evaluateIntegerResult(name, "Sequence {2}->last()", 2);
		evaluateIntegerResult(name, "Sequence {1, 2}->last()", 2);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->last()", 6);
	}

	public	void	testSequenceIndexOf() {
		String	name = "testSequenceIndexOf";
		 
		evaluateIntegerResult(name, "Sequence {8}->indexOf(8)", 1);
		evaluateIntegerResult(name, "Sequence {9}->indexOf(9)", 1);
		evaluateIntegerResult(name, "Sequence {8, 9}->indexOf(8)", 1);
		evaluateIntegerResult(name, "Sequence {8, 9}->indexOf(9)", 2);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->indexOf(1)", 1);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->indexOf(3)", 3);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->indexOf(6)", 6);
		evaluateIntegerResult(name, "Sequence {1, 2, 3, 4, 5, 6}->indexOf(9)", -1);
	}

	public	void	testSequenceIncluding() {
		String	name = "testSequenceIncluding";
		
		evaluateBooleanResult(name, "Sequence {}->including(1) = Sequence{1}", true);
		evaluateBooleanResult(name, "Sequence {1}->including(1) = Sequence{1, 1}", true);
		evaluateBooleanResult(name, "Sequence {2}->including(1) = Sequence{2, 1}", true);
		evaluateBooleanResult(name, "Sequence {1}->including(2) = Sequence{1, 2}", true);
		evaluateBooleanResult(name, "Sequence {1, 2}->including(1) = Sequence{1, 2, 1}", true);
		evaluateBooleanResult(name, "Sequence {1, 2}->including(2) = Sequence{1, 2, 2 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->including(3) = Sequence { 1, 2, 3, 4, 5, 6, 3}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->including(7) = Sequence { 1, 2, 3, 4, 5, 6, 7 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->including(null) = Sequence { 1, 2, 3, 4, 5, 6, null }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6, null}->including(null) = Sequence { 1, 2, 3, 4, 5, 6, null, null }", true);

	}


	public	void	testSequenceExcluding() {
		String	name = "testSequenceExcluding";
		
		evaluateBooleanResult(name, "(Sequence {}->excluding(1))->isEmpty()", true);
		evaluateBooleanResult(name, "(Sequence {1}->excluding(1))->isEmpty()", true);
		evaluateBooleanResult(name, "Sequence {2}->excluding(1) = Sequence{2}", true);
		evaluateBooleanResult(name, "Sequence {1}->excluding(2) = Sequence{1}", true);
		evaluateBooleanResult(name, "Sequence {1, 2}->excluding(1) = Sequence{2}", true);
		evaluateBooleanResult(name, "Sequence {1, 1, 1, 1, 1, 2}->excluding(1) = Sequence{2}", true);
		evaluateBooleanResult(name, "Sequence {1, 2 , 2, 2, 2, 1}->excluding(1) = Sequence{2, 2, 2, 2}", true);
		evaluateBooleanResult(name, "Sequence {2, 2 , 2, 2, 2, 2}->excluding(2)->isEmpty()", true);
		evaluateBooleanResult(name, "Sequence {1, 2}->excluding(2) = Sequence{1}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->excluding(3) = Sequence { 1, 2, 4, 5, 6}", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->excluding(7) = Sequence { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6}->excluding(null) = Sequence { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6, null}->excluding(null) = Sequence { 1, 2, 3, 4, 5, 6 }", true);
		evaluateBooleanResult(name, "Sequence {1, 2, 3, 4, 5, 6, null, null}->excluding(null) = Sequence { 1, 2, 3, 4, 5, 6 }", true);
	}

	public	void	testConversions() {
		String	name = "testConversions";
		
		evaluateBooleanResult(name, "Sequence {1, 1, 1, 1, 1, 2, 2, 2}->asSet() = Set{1, 2}", true);
		evaluateBooleanResult(name, "Sequence {1, 1, 1, 1, 1, 2, 2, 2}->asBag() = Bag{1, 1, 1, 1, 1, 2, 2, 2}", true);
	}

}
