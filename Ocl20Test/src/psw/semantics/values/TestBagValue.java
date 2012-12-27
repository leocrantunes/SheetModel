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
public class TestBagValue extends TestPSWOclEvaluator {

	public void	testBagIntegerAdd() {
		OclBagValue	aBag = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType()));
		
		try {
			verifySize(aBag, 0);
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aBag.add(aNumber);
			verifySize(aBag, 1);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(20);
			aBag.add(aNumber);
			verifySize(aBag, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aBag.add(aNumber);
			verifySize(aBag, 3);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclRealValue aNumber = new OclRealValue("10.0");
			aBag.add(aNumber);
			fail();
		} catch (IllegalArgumentException e) {
		}
		
		try {
			OclStringValue aValue = new OclStringValue("alex");
			aBag.add(aValue);
			fail();
		} catch (IllegalArgumentException e) {
		}
		
		createBagWithNull();
		createBagWithNulls(5);
	}

	public void	testBagIntegerIncludes() {
		executeTestIncludes(createBag(), new OclIntegerValue(10), true);
		executeTestIncludes(createBag(), new OclIntegerValue(20), true);
		executeTestIncludes(createBag(), new OclIntegerValue(30), false);
		
		executeTestIncludes(createBagWithNull(), new OclIntegerValue(10), true);
		executeTestIncludes(createBagWithNull(), new OclIntegerValue(20), true);
		executeTestIncludes(createBagWithNull(), new OclIntegerValue(30), false);
		executeTestIncludes(createBagWithNull(), getNull(), true);
		executeOperation(createBagWithNull(), "includes", getInvalid(), getInvalid());

		executeTestIncludes(createEmptyBag(), new OclIntegerValue(10), false);
		executeTestIncludes(createEmptyBag(), getNull(), false);

	}

	public void	testBagIntegerExcludes() {
		executeTestExcludes(createBag(), new OclIntegerValue(10), false);
		executeTestExcludes(createBag(), new OclIntegerValue(20), false);
		executeTestExcludes(createBag(), new OclIntegerValue(30), true);
	}

	public void	testCount() {
		executeTestCount(createBag(), new OclIntegerValue(10), 2);
		executeTestCount(createBag(), new OclIntegerValue(20), 1);
		executeTestCount(createBag(), new OclIntegerValue(30), 0);
		
		OclBagValue	setOne = createIntegerBag(new int[] { 10, 20, 30, 20, 30, 40, 30, 30, 50 });
		executeTestCount(setOne, new OclIntegerValue(20), 2);
		executeTestCount(setOne, new OclIntegerValue(10), 1);
		executeTestCount(setOne, new OclIntegerValue(50), 1);
		executeTestCount(setOne, new OclIntegerValue(30), 4);
		executeTestCount(setOne, getNull(),  0);
		
		OclBagValue	setOneWithNulls = createIntegerBag(new int[] { 10, 20, 30, 20, 30, 40, 30, 30, 50 }, 4);
		executeTestCount(setOneWithNulls, new OclIntegerValue(20), 2);
		executeTestCount(setOneWithNulls, getNull(),  4);
		
	}

	public void	testIncludesExcludesAll() {
		OclBagValue	setOne = createIntegerBag(new int[] { 10, 20, 30, 20, 30, 40, 50 });
		OclBagValue	setTwo = createIntegerBag(new int[] { 10, 20, 30, 40, 50 });
		OclBagValue	setThree = createIntegerBag(new int[] { 10, 10, 20, 20, 30 });
		OclBagValue	setFour = createIntegerBag(new int[] {  20 });
		OclBagValue	setFive = createIntegerBag(new int[] { 90 });
		OclBagValue	setSix = createIntegerBag(new int[] { 10, 90 });
		OclBagValue	setSeven = createIntegerBag(new int[] { 25, 30 });
		OclBagValue	setEight = createIntegerBag(new int[] { 90, 100, 110 });
		
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
		OclBagValue	setOne = createIntegerBag(new int[] {  });
		OclBagValue	setTwo = createIntegerBag(new int[] { 10 });
		OclBagValue	setThree = createIntegerBag(new int[] { 10, 20, 30, 30, 40, 50 });

		assertTrue(executeBooleanOperation(setOne, "isEmpty"));
		assertFalse(executeBooleanOperation(setTwo, "isEmpty"));
		assertFalse(executeBooleanOperation(setThree, "isEmpty"));

		assertFalse(executeBooleanOperation(setOne, "notEmpty"));
		assertTrue(executeBooleanOperation(setTwo, "notEmpty"));
		assertTrue(executeBooleanOperation(setThree, "notEmpty"));
	}

	public void	testSum() {
		OclBagValue	setOne = createIntegerBag(new int[] { 10, 20, 30, 40, 50 });
		OclBagValue	setTwo = createIntegerBag(new int[] { 10 });
		OclBagValue	setThree = createIntegerBag(new int[] {  });
		OclBagValue	setFour = createIntegerBag(new int[] { 10, 10, 20, 30, 40, 40, 50, 50 });
		
		assertEquals(150, executeIntegerOperation(setOne, "sum", null));
		assertEquals(10, executeIntegerOperation(setTwo, "sum", null));
		assertEquals(250, executeIntegerOperation(setFour, "sum", null));
		assertEquals(0, executeIntegerOperation(setThree, "sum", null));
	}

	public void	testUnion() {
		executeTestBagOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 11, 390);
		executeTestBagOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 6, 165);
		executeTestBagOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 14, 470);
		executeTestBagOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 5, 150);
		executeTestBagOperation("union", new int[] { }, new int[] { 15, 25, 35, 45, 55, 65}, 6, 240);
		executeTestBagOperation("union", new int[] { }, new int[] { }, 0, 0);
		
		executeTestBagOperation("union", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }), createIntegerBag(new int[] { 20, 30 }), 7, 200);
		executeTestBagOperation("union", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }, 2), createIntegerBag(new int[] { 20, 30 }), 9, 200);
		executeTestBagOperation("union", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }, 2), createIntegerBag(new int[] { 20, 30 }, 1), 10, 200);
		executeTestBagOperation("union", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }, 2), createIntegerBag(new int[] { 20, 30 }, 2), 11, 200);
		executeTestBagOperation("union", createIntegerBag(new int[] { }, 2), createIntegerBag(new int[] { 20, 30 }, 2), 6, 50);
		executeTestBagOperation("union", createIntegerBag(new int[] { }, 2), createIntegerBag(new int[] {  }), 2, 0);
	}

	public void 	testEquality() {
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 50, 10, 40 }, true);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 20, 30, 40, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50, 70 }, new int[] { 20, 30, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 50, 10, 40, 70 }, false);
		executeTestEquality(new int[] { 10 }, new int[] { 10 }, true);
		executeTestEquality(new int[] { 10 }, new int[] { 10, 10 }, false);
		executeTestEquality(new int[] { 10, 20, 30 }, new int[] { 10, 10, 10, 20, 20, 20, 30, 30, 30 }, false);
		executeTestEquality(new int[] { }, new int[] { }, true);
		executeTestEquality(new int[] { 10 }, new int[] { 20 }, false);
	}

	public void 	testIntersection() {
		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, 5, 150);
		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40 }, 4, 100);
		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 40, 50 }, 4, 140);
		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 40  }, 1, 40);
		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 0, 0);
		executeTestBagOperation("intersection", new int[] { }, new int[] { 10, 20, 30, 40, 50 }, 0, 0);
		executeTestBagOperation("intersection", new int[] { 40  }, new int[] { 10, 20, 30, 40, 50 }, 1, 40);
		executeTestBagOperation("intersection", new int[] { 10, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, 4, 130);

		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 0, 0);
		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 0, 0);
		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 3, 80);
		executeTestBagOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] {  } , 0, 0);
		executeTestBagOperation("intersection", new int[] { }, new int[] { }, 0, 0);
		
		executeTestBagOperation("intersection", new int[] { 10, 10, 20, 20, 20, 20, 30, 30, 40, 50 }, new int[] { 10, 10, 10, 20, 20, 30, 40, 50 }, 7, 180);

		executeTestBagOperation("intersection", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }), createIntegerBag(new int[] { 20, 30 }), 2, 50);
		executeTestBagOperation("intersection", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }, 2), createIntegerBag(new int[] { 20, 30 }), 2, 50);
		executeTestBagOperation("intersection", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }, 2), createIntegerBag(new int[] { 20, 30 }, 1), 3, 50);
		executeTestBagOperation("intersection", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }, 2), createIntegerBag(new int[] { 20, 30 }, 2), 4, 50);
		executeTestBagOperation("intersection", createIntegerBag(new int[] { }, 2), createIntegerBag(new int[] { 20, 30 }, 2), 2, 0);
		executeTestBagOperation("intersection", createIntegerBag(new int[] { }, 2), createIntegerBag(new int[] {  }, 1), 1, 0);
		executeTestBagOperation("intersection", createIntegerBag(new int[] { }, 2), createIntegerBag(new int[] {  }), 0, 0);

	}


	public void 	testIncluding() {
		executeTestModifyBagOperation("including", new int[] { 10, 20, 30, 40, 50 }, 60, 6, 210);
		executeTestModifyBagOperation("including", new int[] { 10, 20, 30, 40, 50 }, 0, 6, 150);
		executeTestModifyBagOperation("including", new int[] { }, 50, 1, 50);
		executeTestModifyBagOperation("including", new int[] { 10  }, 20, 2, 30);
		executeTestModifyBagOperation("including", new int[] { 10, 20, 30, 40, 50 }, 10, 6, 160);
	}

	public void 	testExcluding() {
		executeTestModifyBagOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 60, 5, 150);
		executeTestModifyBagOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 10, 4, 140);
		executeTestModifyBagOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 50, 4, 100);
		executeTestModifyBagOperation("excluding", new int[] { }, 50, 0, 0);
		executeTestModifyBagOperation("excluding", new int[] { 10  }, 20, 1, 10);
		executeTestModifyBagOperation("excluding", new int[] { 10  }, 10, 0, 0);
		executeTestModifyBagOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 10, 4, 140);
		executeTestModifyBagOperation("excluding", new int[] { 10, 20, 30, 40, 50, 50, 50, 50 }, 50, 4, 100);
		executeTestModifyBagOperation("excluding", new int[] { 10, 10, 10, 10, 20, 30, 40, 50, 50, 50, 50 }, 10 , 7, 290);
	}

	public void 	testFlatten() {
		OclBagValue	setOne = createIntegerBag(new int[] { 10, 20, 30, 40, 50 });
		OclBagValue	setTwo = createIntegerBag(new int[] { 40, 50 });
		OclBagValue	setThree = createIntegerBag(new int[] { 40, 60 });
		OclBagValue	setFour = createIntegerBag(new int[] { 100, 200, 300 });
		OclBagValue	setFive = createIntegerBag(new int[] { 400, 500 });
		OclBagValue	setSix = createIntegerBag(new int[] { 600, 500 });

		OclBagValue	temp1 = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType())));
		OclBagValue	temp2 = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType())));
		OclBagValue	temp3 = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType())));

		temp1.add(setOne);
		temp1.add(setTwo);
		temp2.add(setThree);
		temp2.add(setFour);
		temp3.add(setFive);
		temp3.add(setSix);

		OclBagValue	compoundBag1 = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createBagType(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType()))));
		compoundBag1.add(temp1);
		compoundBag1.add(temp2);
		compoundBag1.add(temp3);
		
		executeTestFlatten(temp1, 7, 240);
		executeTestFlatten(temp2, 5, 700);
		executeTestFlatten(temp3, 4, 2000);
		executeTestFlatten(compoundBag1, 16, 2940);
		
//		executeTestModifyBagOperation("flatten", new int[] { 10, 20, 30, 40, 50 }, 60, 5, 150);
	}		

	public void	testEquals() {
		OclBagValue	setOne = createIntegerBag(new int[] { 10, 20, 30, 40, 50 });
		OclBagValue	setTwo = createIntegerBag(new int[] { 10, 20, 30, 40, 50 });
		OclBagValue	setThree = createIntegerBag(new int[] { 30, 10, 40, 50, 20 });
		OclBagValue	setFour = createIntegerBag(new int[] { 40, 50, 20 });
		OclBagValue	setFive = createIntegerBag(new int[] { 10, 20, 30, 40, 50, 10 });
		
		OclBagValue	setSix= createIntegerBag(new int[] { 10, 20, 30, 40, 50, 10 }, 4);
		OclBagValue	setSeven= createIntegerBag(new int[] { 30, 50, 10, 40, 20, 10 }, 4);
		OclBagValue	setEight= createIntegerBag(new int[] { 30, 50, 10, 40, 20, 10 }, 3);
		
		assertTrue(setOne.equals(setOne));
		assertTrue(setOne.equals(setTwo));
		assertTrue(setOne.equals(setThree));
		assertFalse(setOne.equals(setFour));
		assertFalse(setOne.equals(setFive));
		assertTrue(setSix.equals(setSeven));
		assertFalse(setSix.equals(setEight));
		assertFalse(setSeven.equals(setEight));
	}

	public void 	testAsSet() {
		executeTestConversion("asSet", new int[] { 10, 10, 20, 20, 30, 40, 40, 40, 40, 50 }, 60, 6, 210);
		executeTestConversion("asSet", new int[] { 10, 10, 10, 20, 30, 40, 50 }, 10, 5, 150);
		executeTestConversion("asSet", new int[] { 10, 20, 30, 40, 50, 50, 50, 50 }, 50, 5, 150);
		executeTestConversion("asSet", new int[] { }, 50, 1, 50);
		executeTestConversion("asSet", new int[] { 10  }, 20, 2, 30);
		executeTestConversion("asSet", new int[] { 10  }, 10, 1, 10);
		executeTestConversion("asSet", new int[] { 10, 20, 30, 40, 50 }, 10, 5, 150);
		executeTestConversion("asSet", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }, 2), 10, 6, 150);
	}

	public void 	testAsBag() {
		executeTestConversion("asBag", new int[] { 10, 20, 30, 40, 50, 50, 50 }, 60, 8, 310);
		executeTestConversion("asBag", new int[] { 10, 20, 30, 40, 50, 50, 50 }, 10, 8, 260);
		executeTestConversion("asBag", new int[] { 10, 20, 30, 40, 50 }, 50, 6, 200);
		executeTestConversion("asBag", new int[] { }, 50, 1, 50);
		executeTestConversion("asBag", new int[] { 10  }, 20, 2, 30);
		executeTestConversion("asBag", new int[] { 10  }, 10, 2, 20);
		executeTestConversion("asBag", new int[] { 10, 20, 30, 40, 50 }, 10, 6, 160);
		executeTestConversion("asBag", createIntegerBag(new int[] { 10, 20, 30, 40, 50 }, 2), 10, 8, 160);
	}


	private void	executeTestIncludes(OclBagValue aBag, OclValue value, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclBooleanValue) aBag.executeOperation("includes", arguments)).booleanValue()));
	}

	private void	executeTestExcludes(OclBagValue aBag, OclValue value, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclBooleanValue) aBag.executeOperation("excludes", arguments)).booleanValue()));
	}

	private void	executeTestCount(OclBagValue aBag, OclValue value, int expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		assertEquals(expectedResult, (((OclIntegerValue) aBag.executeOperation("count", arguments)).intValue()));
	}

	private void	executeTestIncludesAll(OclBagValue setOne, OclBagValue setTwo, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(setTwo);
		assertEquals(expectedResult, (((OclBooleanValue) setOne.executeOperation("includesAll", arguments)).booleanValue()));
	}
	
	private void	executeTestExcludesAll(OclBagValue setOne, OclBagValue setTwo, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(setTwo);
		assertEquals(expectedResult, (((OclBooleanValue) setOne.executeOperation("excludesAll", arguments)).booleanValue()));
	}

	private	void executeTestBagOperation(String opName, int[] values1, int[] values2, int expectedSize, int expectedTotal) {
		OclBagValue	setOne = createIntegerBag(values1);
		OclBagValue	setTwo = createIntegerBag(values2);
		executeTestBagOperation(opName, setOne, setTwo, expectedSize, expectedTotal);
	}

	private	void executeTestBagOperation(String opName, OclBagValue setOne, OclBagValue setTwo, int expectedSize, int expectedTotal) {
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclBagValue	resultBag = (OclBagValue) setOne.executeOperation(opName, arguments);
		verifySize(resultBag, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultBag, "sum", null));
	}

	private	void executeTestModifyBagOperation(String opName, int[] values1, int value, int expectedSize, int expectedTotal) {
		OclBagValue		setOne = createIntegerBag(values1);
		OclIntegerValue	setTwo = new OclIntegerValue(value);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclBagValue	resultBag = (OclBagValue) setOne.executeOperation(opName, arguments);
		verifySize(resultBag, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultBag, "sum", null));
	}

	private	void executeTestEquality(int[] values1, int[] values2, boolean expectedResult) {
		OclBagValue	setOne = createIntegerBag(values1);
		OclBagValue	setTwo = createIntegerBag(values2);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclBooleanValue result = (OclBooleanValue) setOne.executeOperation("=", arguments);
		assertEquals(expectedResult, result.booleanValue());
	}

	private	void executeTestFlatten(OclBagValue setOne, int expectedSize, int expectedTotal) {
		OclBagValue	resultBag = (OclBagValue) setOne.executeOperation("flatten", null);
		verifySize(resultBag, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultBag, "sum", null));
	}


	private	boolean executeBooleanOperation(OclBagValue aBag, String operation) {
		return (((OclBooleanValue) aBag.executeOperation(operation, null)).booleanValue());
	}

	private	long executeIntegerOperation(OclCollectionValue aBag, String operation, List arguments) {
		return ((OclIntegerValue) aBag.executeOperation(operation, arguments)).intValue();
	}
	
	private	void executeTestConversion(String opName, int[] values1, int value, int expectedSize, int expectedTotal) {
		OclBagValue		setOne = createIntegerBag(values1);
		executeTestConversion(opName, setOne, value, expectedSize, expectedTotal);
	}

	private	void executeTestConversion(String opName, OclCollectionValue setOne, int value, int expectedSize, int expectedTotal) {
		OclIntegerValue	setTwo = new OclIntegerValue(value);
		
		OclCollectionValue	resultSet = (OclCollectionValue) setOne.executeOperation(opName, null);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		OclCollectionValue resultCollection = (OclCollectionValue) resultSet.executeOperation("including", arguments);
		verifySize(resultCollection, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultCollection, "sum", null));
	}


	private	OclBagValue	createBag() {
		OclBagValue	aBag = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType()));
		
		try {
			verifySize(aBag, 0);
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aBag.add(aNumber);
			verifySize(aBag, 1);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(20);
			aBag.add(aNumber);
			verifySize(aBag, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aBag.add(aNumber);
			verifySize(aBag, 3);
		} catch (IllegalArgumentException e) {
			fail();
		}
		
		return	aBag;
	}
	
	private	OclBagValue	createBagWithNull() {
		return	createBagWithNulls(1);
	}

	private	OclBagValue	createBagWithNulls(int qty) {
		OclBagValue aBag = createBag();
		
		for (int i = 0; i < qty; i++)
			aBag.add(getNull());
		
		verifySize(aBag, 3 + qty);

		try {
			aBag.add(getInvalid());
		} catch (Exception e) {
		}
		verifySize(aBag, 3 + qty);

		return	aBag;
	}

	private	OclBagValue	createEmptyBag() {
		return	new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType()));
	}

	private	OclBagValue	createIntegerBag(int[] values) {
		OclBagValue	aBag = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aBag.add(aNumber);
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aBag;
	}

	private	OclBagValue	createIntegerBag(int[] values, int nulls) {
		OclBagValue	aBag = new OclBagValue(OclTypesFactory.createBagType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aBag.add(aNumber);
			}
			for (int i = 0; i < nulls; i++) {
				aBag.add(getNull());
			} 

		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aBag;
	}



	private	void verifySize(OclCollectionValue collection, int expectedValue) {
		assertEquals(expectedValue, ((OclIntegerValue) collection.executeOperation("size", null)).intValue() );
	}


}
