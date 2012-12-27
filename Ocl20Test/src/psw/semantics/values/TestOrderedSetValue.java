/*
 * Created on 29/04/2004
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
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestOrderedSetValue extends TestPSWOclEvaluator  {


	public void	testOrderedSetIntegerAdd() {
		OclOrderedSetValue	aOrderedSet = new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOclIntegerType()));
		
		try {
			verifySize(aOrderedSet, 0);
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aOrderedSet.add(aNumber);
			verifySize(aOrderedSet, 1);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(20);
			aOrderedSet.add(aNumber);
			verifySize(aOrderedSet, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aOrderedSet.add(aNumber);
			verifySize(aOrderedSet, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclRealValue aNumber = new OclRealValue("10.0");
			aOrderedSet.add(aNumber);
			fail();
		} catch (IllegalArgumentException e) {
		}
		
		try {
			OclStringValue aValue = new OclStringValue("alex");
			aOrderedSet.add(aValue);
			fail();
		} catch (IllegalArgumentException e) {
		}
	}

	public void	testOrderedSetIntegerIncludes() {
		executeTestIncludes(createOrderedSet(), new OclIntegerValue(10), true);
		executeTestIncludes(createOrderedSet(), new OclIntegerValue(20), true);
		executeTestIncludes(createOrderedSet(), new OclIntegerValue(30), false);
	}

	public void	testOrderedSetIntegerExcludes() {
		executeTestExcludes(createOrderedSet(), new OclIntegerValue(10), false);
		executeTestExcludes(createOrderedSet(), new OclIntegerValue(20), false);
		executeTestExcludes(createOrderedSet(), new OclIntegerValue(30), true);
	}

	public void	testCount() {
		executeTestCount(createOrderedSet(), new OclIntegerValue(10), 1);
		executeTestCount(createOrderedSet(), new OclIntegerValue(20), 1);
		executeTestCount(createOrderedSet(), new OclIntegerValue(30), 0);
		
		OclOrderedSetValue	setOne = createIntegerOrderedSet(new int[] { 10, 20, 30, 20, 30, 40, 30, 30, 50 });
		executeTestCount(setOne, new OclIntegerValue(20), 1);
		executeTestCount(setOne, new OclIntegerValue(10), 1);
		executeTestCount(setOne, new OclIntegerValue(50), 1);
		executeTestCount(setOne, new OclIntegerValue(30), 1);
	}

	public void	testIncludesExcludesAll() {
		OclOrderedSetValue	setOne = createIntegerOrderedSet(new int[] { 10, 20, 30, 20, 30, 40, 50 });
		OclOrderedSetValue	setTwo = createIntegerOrderedSet(new int[] { 10, 20, 30, 40, 50 });
		OclOrderedSetValue	setThree = createIntegerOrderedSet(new int[] { 10, 10, 20, 20, 30 });
		OclOrderedSetValue	setFour = createIntegerOrderedSet(new int[] {  20 });
		OclOrderedSetValue	setFive = createIntegerOrderedSet(new int[] { 90 });
		OclOrderedSetValue	setSix = createIntegerOrderedSet(new int[] { 10, 90 });
		OclOrderedSetValue	setSeven = createIntegerOrderedSet(new int[] { 25, 30 });
		OclOrderedSetValue	setEight = createIntegerOrderedSet(new int[] { 90, 100, 110 });
		
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
		OclOrderedSetValue	setOne = createIntegerOrderedSet(new int[] {  });
		OclOrderedSetValue	setTwo = createIntegerOrderedSet(new int[] { 10 });
		OclOrderedSetValue	setThree = createIntegerOrderedSet(new int[] { 10, 20, 30, 30, 40, 50 });

		assertTrue(executeBooleanOperation(setOne, "isEmpty"));
		assertFalse(executeBooleanOperation(setTwo, "isEmpty"));
		assertFalse(executeBooleanOperation(setThree, "isEmpty"));

		assertFalse(executeBooleanOperation(setOne, "notEmpty"));
		assertTrue(executeBooleanOperation(setTwo, "notEmpty"));
		assertTrue(executeBooleanOperation(setThree, "notEmpty"));
	}

	public void	testSum() {
		OclOrderedSetValue	setOne = createIntegerOrderedSet(new int[] { 10, 20, 30, 40, 50 });
		OclOrderedSetValue	setTwo = createIntegerOrderedSet(new int[] { 10 });
		OclOrderedSetValue	setThree = createIntegerOrderedSet(new int[] {  });
		OclOrderedSetValue	setFour = createIntegerOrderedSet(new int[] { 10, 10, 20, 30, 40, 40, 50, 50 });
		
		assertEquals(150, executeIntegerOperation(setOne, "sum", null));
		assertEquals(10, executeIntegerOperation(setTwo, "sum", null));
		assertEquals(150, executeIntegerOperation(setFour, "sum", null));
		assertEquals(0, executeIntegerOperation(setThree, "sum", null));
	}

	public void	testUnion() {
		executeTestOrderedSetOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 11, 390);
		executeTestOrderedSetOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 6, 165);
		executeTestOrderedSetOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 11, 390);
		executeTestOrderedSetOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 5, 150);
		executeTestOrderedSetOperation("union", new int[] { }, new int[] { 15, 25, 35, 45, 55, 65}, 6, 240);
		executeTestOrderedSetOperation("union", new int[] { }, new int[] { }, 0, 0);
	}

	public void 	testEquality() {
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, true);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 20, 30, 40, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50, 70 }, new int[] { 20, 30, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 50, 10, 40, 70 }, false);
		executeTestEquality(new int[] { 10 }, new int[] { 10 }, true);
		executeTestEquality(new int[] { 10 }, new int[] { 10, 10 }, true);
		executeTestEquality(new int[] { 10, 20, 30 }, new int[] { 10, 10, 10, 20, 20, 20, 30, 30, 30 }, true);
		executeTestEquality(new int[] { }, new int[] { }, true);
		executeTestEquality(new int[] { 10 }, new int[] { 20 }, false);
	}

	public void 	testIncluding() {
		executeTestModifyOrderedSetOperation("including", new int[] { 10, 20, 30, 40, 50 }, 60, 6, 210);
		executeTestModifyOrderedSetOperation("including", new int[] { 10, 20, 30, 40, 50 }, 0, 6, 150);
		executeTestModifyOrderedSetOperation("including", new int[] { }, 50, 1, 50);
		executeTestModifyOrderedSetOperation("including", new int[] { 10  }, 20, 2, 30);
		executeTestModifyOrderedSetOperation("including", new int[] { 10, 20, 30, 40, 50 }, 10, 5, 150);
	}

	public void 	testExcluding() {
		executeTestModifyOrderedSetOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 60, 5, 150);
		executeTestModifyOrderedSetOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 10, 4, 140);
		executeTestModifyOrderedSetOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 50, 4, 100);
		executeTestModifyOrderedSetOperation("excluding", new int[] { }, 50, 0, 0);
		executeTestModifyOrderedSetOperation("excluding", new int[] { 10  }, 20, 1, 10);
		executeTestModifyOrderedSetOperation("excluding", new int[] { 10  }, 10, 0, 0);
		executeTestModifyOrderedSetOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 10, 4, 140);
		executeTestModifyOrderedSetOperation("excluding", new int[] { 10, 20, 30, 40, 50, 50, 50, 50 }, 50, 4, 100);
		executeTestModifyOrderedSetOperation("excluding", new int[] { 10, 10, 10, 10, 20, 30, 40, 50, 50, 50, 50 }, 10 , 4, 140);
	}

	public void 	testFlatten() {
		OclOrderedSetValue	setOne = createIntegerOrderedSet(new int[] { 10, 20, 30, 40, 50 });
		OclOrderedSetValue	setTwo = createIntegerOrderedSet(new int[] { 40, 50 });
		OclOrderedSetValue	setThree = createIntegerOrderedSet(new int[] { 40, 60 });
		OclOrderedSetValue	setFour = createIntegerOrderedSet(new int[] { 100, 200, 300 });
		OclOrderedSetValue	setFive = createIntegerOrderedSet(new int[] { 400, 500 });
		OclOrderedSetValue	setSix = createIntegerOrderedSet(new int[] { 600, 500 });

		OclOrderedSetValue	temp1 = new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOclIntegerType())));
		OclOrderedSetValue	temp2 = new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOclIntegerType())));
		OclOrderedSetValue	temp3 = new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOclIntegerType())));

		temp1.add(setOne);
		temp1.add(setTwo);
		temp2.add(setThree);
		temp2.add(setFour);
		temp3.add(setFive);
		temp3.add(setSix);

		OclOrderedSetValue	compoundOrderedSet1 = new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOclIntegerType()))));
		compoundOrderedSet1.add(temp1);
		compoundOrderedSet1.add(temp2);
		compoundOrderedSet1.add(temp3);
		
		executeTestFlatten(temp1, 5, 150);
		executeTestFlatten(temp2, 5, 700);
		executeTestFlatten(temp3, 3, 1500);
		executeTestFlatten(compoundOrderedSet1, 12, 2310);
		
//		executeTestModifyOrderedSetOperation("flatten", new int[] { 10, 20, 30, 40, 50 }, 60, 5, 150);
	}		

	public void	testEquals() {
		OclOrderedSetValue	setOne = createIntegerOrderedSet(new int[] { 10, 20, 30, 40, 50 });
		OclOrderedSetValue	setTwo = createIntegerOrderedSet(new int[] { 10, 20, 30, 40, 50 });
		OclOrderedSetValue	setThree = createIntegerOrderedSet(new int[] { 30, 10, 40, 50, 20 });
		OclOrderedSetValue	setFour = createIntegerOrderedSet(new int[] { 40, 50, 20 });
		OclOrderedSetValue	setFive = createIntegerOrderedSet(new int[] { 10, 20, 30, 40, 50, 10 });

		assertTrue(setOne.equals(setOne));
		assertTrue(setOne.equals(setTwo));
		assertFalse(setOne.equals(setThree));
		assertFalse(setOne.equals(setFour));
		assertTrue(setOne.equals(setFive));
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

	public void 	testAsOrderedSet() {
		executeTestConversion("asOrderedSet", new int[] { 10, 20, 30, 40, 50, 50, 50 }, 60, 6, 210);
		executeTestConversion("asOrderedSet", new int[] { 10, 20, 30, 40, 50, 50, 50 }, 10, 5, 150);
		executeTestConversion("asOrderedSet", new int[] { 10, 20, 30, 40, 50 }, 50, 5, 150);
		executeTestConversion("asOrderedSet", new int[] { }, 50, 1, 50);
		executeTestConversion("asOrderedSet", new int[] { 10  }, 20, 2, 30);
		executeTestConversion("asOrderedSet", new int[] { 10  }, 10, 1, 10);
		executeTestConversion("asOrderedSet", new int[] { 10, 20, 30, 40, 50 }, 10, 5, 150);
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
		
		executeTestFirstAndLast("last", new int[] { 10, 20, 30, 40, 50 }, 50);
		executeTestFirstAndLast("last", new int[] { 25 }, 25 );
		executeTestFirstAndLast("last", new int[] { 10, 35  }, 35);
	}

	public void	testGetElement() {
		executeTestGetElement("at", new int[] { 10, 20, 30, 40, 50 }, 1, 10);
		executeTestGetElement("at", new int[] { 15 }, 1, 15 );
		executeTestGetElement("at", new int[] { 15, 20 }, 1, 15  );
		
		executeTestGetElement("at", new int[] { 10, 20, 30, 40, 50 }, 5, 50);
		executeTestGetElement("at", new int[] { 25 }, 1, 25 );
		executeTestGetElement("at", new int[] { 10, 35  }, 2, 35);
	}

	public void	testIndexOf() {
		executeTestIndexOf("indexOf", new int[] { 10, 20, 30, 40, 50 }, 1, 10);
		executeTestIndexOf("indexOf", new int[] { 15 }, 1, 15 );
		executeTestIndexOf("indexOf", new int[] { 15, 20 }, 1, 15  );
		
		executeTestIndexOf("indexOf", new int[] { 10, 20, 30, 40, 50 }, 5, 50);
		executeTestIndexOf("indexOf", new int[] { 25 }, 1, 25 );
		executeTestIndexOf("indexOf", new int[] { 10, 35  }, 2, 35);
	}


	private	void	executeTestFirstAndLast(String opName, int[] values1, int expectedValue) {
		OclOrderedSetValue		setOne = createIntegerOrderedSet(values1);
		
		OclIntegerValue	result = (OclIntegerValue) setOne.executeOperation(opName, null);
		assertEquals(expectedValue, result.intValue());
	}

	private	void	executeTestGetElement(String opName, int[] values1, int index, int expectedValue) {
		OclOrderedSetValue		setOne = createIntegerOrderedSet(values1);
		OclIntegerValue indexValue = new OclIntegerValue(index);
		List arguments = new ArrayList();
		arguments.add(indexValue);
		
		OclIntegerValue	result = (OclIntegerValue) setOne.executeOperation(opName, arguments);
		assertEquals(expectedValue, result.intValue());
	}

	private	void	executeTestIndexOf(String opName, int[] values1, int expectedIndex, int value) {
		OclOrderedSetValue		setOne = createIntegerOrderedSet(values1);
		OclIntegerValue oclvalue = new OclIntegerValue(value);
		List arguments = new ArrayList();
		arguments.add(oclvalue);
		
		OclIntegerValue	result = (OclIntegerValue) setOne.executeOperation(opName, arguments);
		assertEquals(expectedIndex, result.intValue());
	}


	private	void	executeTestInsert(String opName, int[] values1, int value, int[] values2) {
		OclOrderedSetValue		setOne = createIntegerOrderedSet(values1);
		OclOrderedSetValue		setTwo = createIntegerOrderedSet(values2);
		OclIntegerValue	element = new OclIntegerValue(value);
		List arguments = new ArrayList();
		arguments.add(element);
		
		OclOrderedSetValue	resultOrderedSet = (OclOrderedSetValue) setOne.executeOperation(opName, arguments);
		assertTrue(resultOrderedSet.equals(setTwo));
	}


	private	void	executeTestInsertAt(String opName, int[] values1, int value, int position, int[] values2) {
		OclOrderedSetValue		setOne = createIntegerOrderedSet(values1);
		OclOrderedSetValue		setTwo = createIntegerOrderedSet(values2);
		OclIntegerValue	element = new OclIntegerValue(value);
		OclIntegerValue	positionValue = new OclIntegerValue(position);
		List arguments = new ArrayList();
		arguments.add(positionValue);
		arguments.add(element);
		
		OclOrderedSetValue	resultOrderedSet = (OclOrderedSetValue) setOne.executeOperation(opName, arguments);
		assertTrue(resultOrderedSet.equals(setTwo));
	}

	private void	executeTestIncludes(OclOrderedSetValue aOrderedSet, OclValue value, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclBooleanValue) aOrderedSet.executeOperation("includes", arguments)).booleanValue()));
	}

	private void	executeTestExcludes(OclOrderedSetValue aOrderedSet, OclValue value, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclBooleanValue) aOrderedSet.executeOperation("excludes", arguments)).booleanValue()));
	}

	private void	executeTestCount(OclOrderedSetValue aOrderedSet, OclValue value, int expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclIntegerValue) aOrderedSet.executeOperation("count", arguments)).intValue()));
	}

	private void	executeTestIncludesAll(OclOrderedSetValue setOne, OclOrderedSetValue setTwo, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(setTwo);
		assertEquals(expectedResult, (((OclBooleanValue) setOne.executeOperation("includesAll", arguments)).booleanValue()));
	}
	
	private void	executeTestExcludesAll(OclOrderedSetValue setOne, OclOrderedSetValue setTwo, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(setTwo);
		assertEquals(expectedResult, (((OclBooleanValue) setOne.executeOperation("excludesAll", arguments)).booleanValue()));
	}

	private	void executeTestOrderedSetOperation(String opName, int[] values1, int[] values2, int expectedSize, int expectedTotal) {
		OclOrderedSetValue	setOne = createIntegerOrderedSet(values1);
		OclOrderedSetValue	setTwo = createIntegerOrderedSet(values2);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclOrderedSetValue	resultOrderedSet = (OclOrderedSetValue) setOne.executeOperation(opName, arguments);
		verifySize(resultOrderedSet, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultOrderedSet, "sum", null));
	}

	private	void executeTestModifyOrderedSetOperation(String opName, int[] values1, int value, int expectedSize, int expectedTotal) {
		OclOrderedSetValue		setOne = createIntegerOrderedSet(values1);
		OclIntegerValue	setTwo = new OclIntegerValue(value);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclOrderedSetValue	resultOrderedSet = (OclOrderedSetValue) setOne.executeOperation(opName, arguments);
		verifySize(resultOrderedSet, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultOrderedSet, "sum", null));
	}

	private	void executeTestEquality(int[] values1, int[] values2, boolean expectedResult) {
		OclOrderedSetValue	setOne = createIntegerOrderedSet(values1);
		OclOrderedSetValue	setTwo = createIntegerOrderedSet(values2);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclBooleanValue result = (OclBooleanValue) setOne.executeOperation("=", arguments);
		assertEquals(expectedResult, result.booleanValue());
	}

	private	void executeTestFlatten(OclOrderedSetValue setOne, int expectedSize, int expectedTotal) {
		OclOrderedSetValue	resultOrderedSet = (OclOrderedSetValue) setOne.executeOperation("flatten", null);
		verifySize(resultOrderedSet, expectedSize);
		
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultOrderedSet, "sum", null));
	}


	private	boolean executeBooleanOperation(OclOrderedSetValue aOrderedSet, String operation) {
		return (((OclBooleanValue) aOrderedSet.executeOperation(operation, null)).booleanValue());
	}

	private	long executeIntegerOperation(OclCollectionValue aOrderedSet, String operation, List arguments) {
		return ((OclIntegerValue) aOrderedSet.executeOperation(operation, arguments)).intValue();
	}
	
	private	void executeTestConversion(String opName, int[] values1, int value, int expectedSize, int expectedTotal) {
		OclOrderedSetValue		setOne = createIntegerOrderedSet(values1);
		OclIntegerValue	setTwo = new OclIntegerValue(value);
		
		OclCollectionValue	resultSet = (OclCollectionValue) setOne.executeOperation(opName, null);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		OclCollectionValue resultCollection = (OclCollectionValue) resultSet.executeOperation("including", arguments);
		verifySize(resultCollection, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultCollection, "sum", null));
	}


	private	OclOrderedSetValue	createOrderedSet() {
		OclOrderedSetValue	aOrderedSet = new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOclIntegerType()));
		
		try {
			verifySize(aOrderedSet, 0);
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aOrderedSet.add(aNumber);
			verifySize(aOrderedSet, 1);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(20);
			aOrderedSet.add(aNumber);
			verifySize(aOrderedSet, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aOrderedSet.add(aNumber);
			verifySize(aOrderedSet, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}
		
		return	aOrderedSet;
	}

	private	OclOrderedSetValue	createIntegerOrderedSet(int[] values) {
		OclOrderedSetValue	aOrderedSet = new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aOrderedSet.add(aNumber);
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aOrderedSet;
	}


	private	void verifySize(OclCollectionValue collection, int expectedValue) {
		assertEquals(expectedValue, ((OclIntegerValue) collection.executeOperation("size", null)).intValue() );
	}
}
