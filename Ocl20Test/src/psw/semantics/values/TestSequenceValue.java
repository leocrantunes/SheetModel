/*
 * Created on Apr 29, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.ArrayList;
import java.util.List;

import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestPSWOclEvaluator;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestSequenceValue extends TestPSWOclEvaluator  {

	public void	testSequenceIntegerAdd() {
		OclSequenceValue	aSequence = new OclSequenceValue(OclTypesFactory.createSequenceType(OclTypesFactory.createOclIntegerType()));
		
		try {
			verifySize(aSequence, 0);
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aSequence.add(aNumber);
			verifySize(aSequence, 1);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(20);
			aSequence.add(aNumber);
			verifySize(aSequence, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aSequence.add(aNumber);
			verifySize(aSequence, 3);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclRealValue aNumber = new OclRealValue("10.0");
			aSequence.add(aNumber);
			fail();
		} catch (IllegalArgumentException e) {
		}
		
		try {
			OclStringValue aValue = new OclStringValue("alex");
			aSequence.add(aValue);
			fail();
		} catch (IllegalArgumentException e) {
		}
	}

	public void	testSequenceIntegerIncludes() {
		executeTestIncludes(createSequence(), new OclIntegerValue(10), true);
		executeTestIncludes(createSequence(), new OclIntegerValue(20), true);
		executeTestIncludes(createSequence(), new OclIntegerValue(30), false);
	}

	public void	testSequenceIntegerExcludes() {
		executeTestExcludes(createSequence(), new OclIntegerValue(10), false);
		executeTestExcludes(createSequence(), new OclIntegerValue(20), false);
		executeTestExcludes(createSequence(), new OclIntegerValue(30), true);
	}

	public void	testCount() {
		executeTestCount(createSequence(), new OclIntegerValue(10), 2);
		executeTestCount(createSequence(), new OclIntegerValue(20), 1);
		executeTestCount(createSequence(), new OclIntegerValue(30), 0);
		
		OclSequenceValue	setOne = createIntegerSequence(new int[] { 10, 20, 30, 20, 30, 40, 30, 30, 50 });
		executeTestCount(setOne, new OclIntegerValue(20), 2);
		executeTestCount(setOne, new OclIntegerValue(10), 1);
		executeTestCount(setOne, new OclIntegerValue(50), 1);
		executeTestCount(setOne, new OclIntegerValue(30), 4);
	}

	public void	testIncludesExcludesAll() {
		OclSequenceValue	setOne = createIntegerSequence(new int[] { 10, 20, 30, 20, 30, 40, 50 });
		OclSequenceValue	setTwo = createIntegerSequence(new int[] { 10, 20, 30, 40, 50 });
		OclSequenceValue	setThree = createIntegerSequence(new int[] { 10, 10, 20, 20, 30 });
		OclSequenceValue	setFour = createIntegerSequence(new int[] {  20 });
		OclSequenceValue	setFive = createIntegerSequence(new int[] { 90 });
		OclSequenceValue	setSix = createIntegerSequence(new int[] { 10, 90 });
		OclSequenceValue	setSeven = createIntegerSequence(new int[] { 25, 30 });
		OclSequenceValue	setEight = createIntegerSequence(new int[] { 90, 100, 110 });
		
		executeTestIncludesAll(setOne, setTwo, true);
		executeTestIncludesAll(setOne, setThree, true);
		executeTestIncludesAll(setOne, setFour,  true);
		executeTestIncludesAll(setOne, setFive,  false);
		executeTestIncludesAll(setOne, setSix, false);
		executeTestIncludesAll(setOne, setSeven, false);
		executeTestIncludesAll(setOne, setEight, false);
		
		executeTestExcludesAll(setOne, setTwo, false);
		executeTestExcludesAll(setOne, setThree, false);
		executeTestExcludesAll(setOne, setFour,  false);
		executeTestExcludesAll(setOne, setFive,  true);
		executeTestExcludesAll(setOne, setSix, false);
		executeTestExcludesAll(setOne, setSeven, false);
		executeTestExcludesAll(setOne, setEight, true);
	}

	public void testEmpty() {
		OclSequenceValue	setOne = createIntegerSequence(new int[] {  });
		OclSequenceValue	setTwo = createIntegerSequence(new int[] { 10 });
		OclSequenceValue	setThree = createIntegerSequence(new int[] { 10, 20, 30, 30, 40, 50 });

		assertTrue(executeBooleanOperation(setOne, "isEmpty"));
		assertFalse(executeBooleanOperation(setTwo, "isEmpty"));
		assertFalse(executeBooleanOperation(setThree, "isEmpty"));

		assertFalse(executeBooleanOperation(setOne, "notEmpty"));
		assertTrue(executeBooleanOperation(setTwo, "notEmpty"));
		assertTrue(executeBooleanOperation(setThree, "notEmpty"));
	}

	public void	testSum() {
		OclSequenceValue	setOne = createIntegerSequence(new int[] { 10, 20, 30, 40, 50 });
		OclSequenceValue	setTwo = createIntegerSequence(new int[] { 10 });
		OclSequenceValue	setThree = createIntegerSequence(new int[] {  });
		OclSequenceValue	setFour = createIntegerSequence(new int[] { 10, 10, 20, 30, 40, 40, 50, 50 });
		
		assertEquals(150, executeIntegerOperation(setOne, "sum", null));
		assertEquals(10, executeIntegerOperation(setTwo, "sum", null));
		assertEquals(250, executeIntegerOperation(setFour, "sum", null));
		assertEquals(0, executeIntegerOperation(setThree, "sum", null));
	}

	public void	testUnion() {
		executeTestSequenceOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 11, 390);
		executeTestSequenceOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 6, 165);
		executeTestSequenceOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 14, 470);
		executeTestSequenceOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 5, 150);
		executeTestSequenceOperation("union", new int[] { }, new int[] { 15, 25, 35, 45, 55, 65}, 6, 240);
		executeTestSequenceOperation("union", new int[] { }, new int[] { }, 0, 0);
	}

	public void 	testEquality() {
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, true);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 20, 30, 40, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50, 70 }, new int[] { 20, 30, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 50, 10, 40, 70 }, false);
		executeTestEquality(new int[] { 10 }, new int[] { 10 }, true);
		executeTestEquality(new int[] { 10 }, new int[] { 10, 10 }, false);
		executeTestEquality(new int[] { 10, 20, 30 }, new int[] { 10, 10, 10, 20, 20, 20, 30, 30, 30 }, false);
		executeTestEquality(new int[] { }, new int[] { }, true);
		executeTestEquality(new int[] { 10 }, new int[] { 20 }, false);
	}

	public void 	testIncluding() {
		executeTestModifySequenceOperation("including", new int[] { 10, 20, 30, 40, 50 }, 60, 6, 210);
		executeTestModifySequenceOperation("including", new int[] { 10, 20, 30, 40, 50 }, 0, 6, 150);
		executeTestModifySequenceOperation("including", new int[] { }, 50, 1, 50);
		executeTestModifySequenceOperation("including", new int[] { 10  }, 20, 2, 30);
		executeTestModifySequenceOperation("including", new int[] { 10, 20, 30, 40, 50 }, 10, 6, 160);
	}

	public void 	testExcluding() {
		executeTestModifySequenceOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 60, 5, 150);
		executeTestModifySequenceOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 10, 4, 140);
		executeTestModifySequenceOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 50, 4, 100);
		executeTestModifySequenceOperation("excluding", new int[] { }, 50, 0, 0);
		executeTestModifySequenceOperation("excluding", new int[] { 10  }, 20, 1, 10);
		executeTestModifySequenceOperation("excluding", new int[] { 10  }, 10, 0, 0);
		executeTestModifySequenceOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 10, 4, 140);
		executeTestModifySequenceOperation("excluding", new int[] { 10, 20, 30, 40, 50, 50, 50, 50 }, 50, 4, 100);
		executeTestModifySequenceOperation("excluding", new int[] { 10, 10, 10, 10, 20, 30, 40, 50, 50, 50, 50 }, 10 , 7, 290);
	}

	public void 	testFlatten() {
		OclSequenceValue	setOne = createIntegerSequence(new int[] { 10, 20, 30, 40, 50 });
		OclSequenceValue	setTwo = createIntegerSequence(new int[] { 40, 50 });
		OclSequenceValue	setThree = createIntegerSequence(new int[] { 40, 60 });
		OclSequenceValue	setFour = createIntegerSequence(new int[] { 100, 200, 300 });
		OclSequenceValue	setFive = createIntegerSequence(new int[] { 400, 500 });
		OclSequenceValue	setSix = createIntegerSequence(new int[] { 600, 500 });

		OclSequenceValue	temp1 = new OclSequenceValue(OclTypesFactory.createSequenceType(OclTypesFactory.createSequenceType(OclTypesFactory.createOclIntegerType())));
		OclSequenceValue	temp2 = new OclSequenceValue(OclTypesFactory.createSequenceType(OclTypesFactory.createSequenceType(OclTypesFactory.createOclIntegerType())));
		OclSequenceValue	temp3 = new OclSequenceValue(OclTypesFactory.createSequenceType(OclTypesFactory.createSequenceType(OclTypesFactory.createOclIntegerType())));

		temp1.add(setOne);
		temp1.add(setTwo);
		temp2.add(setThree);
		temp2.add(setFour);
		temp3.add(setFive);
		temp3.add(setSix);

		OclSequenceValue	compoundSequence1 = new OclSequenceValue(OclTypesFactory.createSequenceType(OclTypesFactory.createSequenceType(OclTypesFactory.createSequenceType(OclTypesFactory.createOclIntegerType()))));
		compoundSequence1.add(temp1);
		compoundSequence1.add(temp2);
		compoundSequence1.add(temp3);
		
		executeTestFlatten(temp1, 7, 240);
		executeTestFlatten(temp2, 5, 700);
		executeTestFlatten(temp3, 4, 2000);
		executeTestFlatten(compoundSequence1, 16, 2940);
		
//		executeTestModifySequenceOperation("flatten", new int[] { 10, 20, 30, 40, 50 }, 60, 5, 150);
	}		

	public void	testEquals() {
		OclSequenceValue	setOne = createIntegerSequence(new int[] { 10, 20, 30, 40, 50 });
		OclSequenceValue	setTwo = createIntegerSequence(new int[] { 10, 20, 30, 40, 50 });
		OclSequenceValue	setThree = createIntegerSequence(new int[] { 30, 10, 40, 50, 20 });
		OclSequenceValue	setFour = createIntegerSequence(new int[] { 40, 50, 20 });
		OclSequenceValue	setFive = createIntegerSequence(new int[] { 10, 20, 30, 40, 50, 10 });

		assertTrue(setOne.equals(setOne));
		assertTrue(setOne.equals(setTwo));
		assertFalse(setOne.equals(setThree));
		assertFalse(setOne.equals(setFour));
		assertFalse(setOne.equals(setFive));
	}

	public void 	testAsSet() {
		executeTestConversion("asSet", new int[] { 10, 10, 20, 20, 30, 40, 40, 40, 40, 50 }, 60, 6, 210);
		executeTestConversion("asSet", new int[] { 10, 10, 10, 20, 30, 40, 50 }, 10, 5, 150);
		executeTestConversion("asSet", new int[] { 10, 20, 30, 40, 50, 50, 50, 50 }, 50, 5, 150);
		executeTestConversion("asSet", new int[] { }, 50, 1, 50);
		executeTestConversion("asSet", new int[] { 10  }, 20, 2, 30);
		executeTestConversion("asSet", new int[] { 10  }, 10, 1, 10);
		executeTestConversion("asSet", new int[] { 10, 20, 30, 40, 50 }, 10, 5, 150);
	}

	public void 	testAsSequence() {
		executeTestConversion("asSequence", new int[] { 10, 20, 30, 40, 50, 50, 50 }, 60, 8, 310);
		executeTestConversion("asSequence", new int[] { 10, 20, 30, 40, 50, 50, 50 }, 10, 8, 260);
		executeTestConversion("asSequence", new int[] { 10, 20, 30, 40, 50 }, 50, 6, 200);
		executeTestConversion("asSequence", new int[] { }, 50, 1, 50);
		executeTestConversion("asSequence", new int[] { 10  }, 20, 2, 30);
		executeTestConversion("asSequence", new int[] { 10  }, 10, 2, 20);
		executeTestConversion("asSequence", new int[] { 10, 20, 30, 40, 50 }, 10, 6, 160);
	}

	
	public void	testAppend() {
		executeTestInsert("append", new int[] { 10, 20, 30, 40, 50 }, 60, new int[] { 10, 20, 30, 40, 50, 60 });
		executeTestInsert("append", new int[] { 10, 20, 30, 40, 50 }, 0, new int[] { 10, 20, 30, 40, 50, 0 } );
		executeTestInsert("append", new int[] { }, 50, new int[] { 50 } );
		executeTestInsert("append", new int[] { 10  }, 20, new int[] { 10, 20 });
	}

	public void	testPrepend() {
		executeTestInsert("prepend", new int[] { 10, 20, 30, 40, 50 }, 60, new int[] { 60, 10, 20, 30, 40, 50 });
		executeTestInsert("prepend", new int[] { 10, 20, 30, 40, 50 }, 0, new int[] { 0, 10, 20, 30, 40, 50 } );
		executeTestInsert("prepend", new int[] { }, 50, new int[] { 50 } );
		executeTestInsert("prepend", new int[] { 10  }, 20, new int[] { 20, 10 });
	}

	public void	testInsertAt() {
		executeTestInsertAt("insertAt", new int[] { 10, 20, 30, 40, 50 }, 60, 1, new int[] { 60, 10, 20, 30, 40, 50 });
		executeTestInsertAt("insertAt", new int[] { 10, 20, 30, 40, 50 }, 60, 2, new int[] { 10, 60, 20, 30, 40, 50 });
		executeTestInsertAt("insertAt", new int[] { 10, 20, 30, 40, 50 }, 60, 6, new int[] { 10, 20, 30, 40, 50, 60 });
		executeTestInsertAt("insertAt", new int[] { }, 50, 1, new int[] { 50 } );
		executeTestInsertAt("insertAt", new int[] { 10  }, 20, 2, new int[] { 10, 20 });
		executeTestInsertAt("insertAt", new int[] { 10  }, 20, 1, new int[] { 20, 10});
	}

	public void	testFirstAndLast() {
		executeTestFirstAndLast("first", new int[] { 10, 20, 30, 40, 50 }, 10);
		executeTestFirstAndLast("first", new int[] { 15 }, 15 );
		executeTestFirstAndLast("first", new int[] { 15, 20 }, 15 );
		executeTestFirstAndLast("first", createIntegerSequence(new int[] {  }), getInvalid() );
		
		executeTestFirstAndLast("last", new int[] { 10, 20, 30, 40, 50 }, 50);
		executeTestFirstAndLast("last", new int[] { 25 }, 25 );
		executeTestFirstAndLast("last", new int[] { 10, 35  }, 35);
		executeTestFirstAndLast("last", createIntegerSequence(new int[] {  }), getInvalid() );
	}

	public void	testGetElement() {
		executeTestGetElement("at", new int[] { 10, 20, 30, 40, 50 }, 1, 10);
		executeTestGetElement("at", new int[] { 15 }, 1, 15 );
		executeTestGetElement("at", new int[] { 15, 20 }, 1, 15  );
		
		executeTestGetElement("at", new int[] { 10, 20, 30, 40, 50 }, 5, 50);
		executeTestGetElement("at", new int[] { 25 }, 1, 25 );
		executeTestGetElement("at", new int[] { 10, 35  }, 2, 35);
		executeTestGetElement("at", createIntegerSequence(new int[] { 10, 35  }), 3, getInvalid());
		executeTestGetElement("at", createIntegerSequence(new int[] { 10, 35  }), 0, getInvalid());
		executeTestGetElement("at", createIntegerSequence(new int[] { 10, 35  }), 3, getInvalid());
		executeTestGetElement("at", createIntegerSequence(new int[] { 10, 35  }, 1), 3, getNull());
		executeTestGetElement("at", createIntegerSequence(new int[] { 10, 35  }, 1), 4, getInvalid());
	}

	public void	testIndexOf() {
		executeTestIndexOf("indexOf", new int[] { 10, 20, 30, 40, 50 }, 1, 10);
		executeTestIndexOf("indexOf", new int[] { 15 }, 1, 15 );
		executeTestIndexOf("indexOf", new int[] { 15, 20 }, 1, 15  );
		executeTestIndexOf("indexOf", new int[] { 15, 20 }, -1, 30  );
		
		executeTestIndexOf("indexOf", new int[] { 10, 20, 30, 40, 50 }, 5, 50);
		executeTestIndexOf("indexOf", new int[] { 25 }, 1, 25 );
		executeTestIndexOf("indexOf", new int[] { 10, 35  }, 2, 35);
	}


	private	void	executeTestFirstAndLast(String opName, int[] values1, int expectedValue) {
		OclSequenceValue		setOne = createIntegerSequence(values1);
		
		OclIntegerValue	result = (OclIntegerValue) setOne.executeOperation(opName, null);
		assertEquals(expectedValue, result.intValue());
	}

	private	void	executeTestFirstAndLast(String opName, OclSequenceValue setOne, OclValue expectedValue) {
		OclValue	result = (OclValue) setOne.executeOperation(opName, null);
		checkResult(result, expectedValue);
	}

	private	void	executeTestGetElement(String opName, int[] values1, int index, int expectedValue) {
		OclSequenceValue		setOne = createIntegerSequence(values1);
		executeTestGetElement(opName, setOne, index, getInt(expectedValue));
	}

	private	void	executeTestGetElement(String opName, OclSequenceValue setOne, int index, OclValue expectedValue) {
		OclIntegerValue indexValue = new OclIntegerValue(index);
		List arguments = new ArrayList();
		arguments.add(indexValue);
		
		OclValue	result = (OclValue) setOne.executeOperation(opName, arguments);
		checkResult(result, expectedValue);
	}

	private	void	executeTestIndexOf(String opName, int[] values1, int expectedIndex, int value) {
		OclSequenceValue		setOne = createIntegerSequence(values1);
		OclIntegerValue oclvalue = new OclIntegerValue(value);
		List arguments = new ArrayList();
		arguments.add(oclvalue);
		
		OclIntegerValue	result = (OclIntegerValue) setOne.executeOperation(opName, arguments);
		assertEquals(expectedIndex, result.intValue());
	}


	private	void	executeTestInsert(String opName, int[] values1, int value, int[] values2) {
		OclSequenceValue		setOne = createIntegerSequence(values1);
		OclSequenceValue		setTwo = createIntegerSequence(values2);
		OclIntegerValue	element = new OclIntegerValue(value);
		List arguments = new ArrayList();
		arguments.add(element);
		
		OclSequenceValue	resultSequence = (OclSequenceValue) setOne.executeOperation(opName, arguments);
		assertTrue(resultSequence.equals(setTwo));
	}


	private	void	executeTestInsertAt(String opName, int[] values1, int value, int position, int[] values2) {
		OclSequenceValue		setOne = createIntegerSequence(values1);
		OclSequenceValue		setTwo = createIntegerSequence(values2);
		OclIntegerValue	element = new OclIntegerValue(value);
		OclIntegerValue	positionValue = new OclIntegerValue(position);
		List arguments = new ArrayList();
		arguments.add(positionValue);
		arguments.add(element);
		
		OclSequenceValue	resultSequence = (OclSequenceValue) setOne.executeOperation(opName, arguments);
		assertTrue(resultSequence.equals(setTwo));
	}

	private void	executeTestIncludes(OclSequenceValue aSequence, OclValue value, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclBooleanValue) aSequence.executeOperation("includes", arguments)).booleanValue()));
	}

	private void	executeTestExcludes(OclSequenceValue aSequence, OclValue value, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclBooleanValue) aSequence.executeOperation("excludes", arguments)).booleanValue()));
	}

	private void	executeTestCount(OclSequenceValue aSequence, OclValue value, int expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclIntegerValue) aSequence.executeOperation("count", arguments)).intValue()));
	}

	private void	executeTestIncludesAll(OclSequenceValue setOne, OclSequenceValue setTwo, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(setTwo);
		assertEquals(expectedResult, (((OclBooleanValue) setOne.executeOperation("includesAll", arguments)).booleanValue()));
	}
	
	private void	executeTestExcludesAll(OclSequenceValue setOne, OclSequenceValue setTwo, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(setTwo);
		assertEquals(expectedResult, (((OclBooleanValue) setOne.executeOperation("excludesAll", arguments)).booleanValue()));
	}

	private	void executeTestSequenceOperation(String opName, int[] values1, int[] values2, int expectedSize, int expectedTotal) {
		OclSequenceValue	setOne = createIntegerSequence(values1);
		OclSequenceValue	setTwo = createIntegerSequence(values2);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclSequenceValue	resultSequence = (OclSequenceValue) setOne.executeOperation(opName, arguments);
		verifySize(resultSequence, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultSequence, "sum", null));
	}

	private	void executeTestModifySequenceOperation(String opName, int[] values1, int value, int expectedSize, int expectedTotal) {
		OclSequenceValue		setOne = createIntegerSequence(values1);
		OclIntegerValue	setTwo = new OclIntegerValue(value);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclSequenceValue	resultSequence = (OclSequenceValue) setOne.executeOperation(opName, arguments);
		verifySize(resultSequence, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultSequence, "sum", null));
	}

	private	void executeTestEquality(int[] values1, int[] values2, boolean expectedResult) {
		OclSequenceValue	setOne = createIntegerSequence(values1);
		OclSequenceValue	setTwo = createIntegerSequence(values2);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclBooleanValue result = (OclBooleanValue) setOne.executeOperation("=", arguments);
		assertEquals(expectedResult, result.booleanValue());
	}

	private	void executeTestFlatten(OclSequenceValue setOne, int expectedSize, int expectedTotal) {
		OclSequenceValue	resultSequence = (OclSequenceValue) setOne.executeOperation("flatten", null);
		verifySize(resultSequence, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultSequence, "sum", null));
	}


	private	boolean executeBooleanOperation(OclSequenceValue aSequence, String operation) {
		return (((OclBooleanValue) aSequence.executeOperation(operation, null)).booleanValue());
	}

	private	long executeIntegerOperation(OclCollectionValue aSequence, String operation, List arguments) {
		return ((OclIntegerValue) aSequence.executeOperation(operation, arguments)).intValue();
	}
	
	private	void executeTestConversion(String opName, int[] values1, int value, int expectedSize, int expectedTotal) {
		OclSequenceValue		setOne = createIntegerSequence(values1);
		OclIntegerValue	setTwo = new OclIntegerValue(value);
		
		OclCollectionValue	resultSet = (OclCollectionValue) setOne.executeOperation(opName, null);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		OclCollectionValue resultCollection = (OclCollectionValue) resultSet.executeOperation("including", arguments);
		verifySize(resultCollection, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultCollection, "sum", null));
	}


	private	OclSequenceValue	createSequence() {
		OclSequenceValue	aSequence = new OclSequenceValue(OclTypesFactory.createSequenceType(OclTypesFactory.createOclIntegerType()));
		
		try {
			verifySize(aSequence, 0);
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aSequence.add(aNumber);
			verifySize(aSequence, 1);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(20);
			aSequence.add(aNumber);
			verifySize(aSequence, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aSequence.add(aNumber);
			verifySize(aSequence, 3);
		} catch (IllegalArgumentException e) {
			fail();
		}
		
		return	aSequence;
	}

	private	OclSequenceValue	createIntegerSequence(int[] values) {
		OclSequenceValue	aSequence = new OclSequenceValue(OclTypesFactory.createSequenceType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aSequence.add(aNumber);
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aSequence;
	}

	private	OclSequenceValue	createIntegerSequence(int[] values, int nulls) {
		OclSequenceValue	aSequence = createIntegerSequence(values);
		
		try {
			for (int i = 0; i < nulls; i++) {
				aSequence.add(getNull());
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aSequence;
	}

	private	void verifySize(OclCollectionValue collection, int expectedValue) {
		assertEquals(expectedValue, ((OclIntegerValue) collection.executeOperation("size", null)).intValue() );
	}
}
