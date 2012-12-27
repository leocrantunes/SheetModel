/*
 * Created on Apr 28, 2004
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
public class TestSetValue extends TestPSWOclEvaluator {

	public void	testSetIntegerAdd() {
		OclSetValue	aSet = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType()));
		
		try {
			verifySize(aSet, 0);
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aSet.add(aNumber);
			verifySize(aSet, 1);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(20);
			aSet.add(aNumber);
			verifySize(aSet, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aSet.add(aNumber);
			verifySize(aSet, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclRealValue aNumber = new OclRealValue("10.0");
			aSet.add(aNumber);
			fail();
		} catch (IllegalArgumentException e) {
		}
		
		try {
			OclStringValue aValue = new OclStringValue("alex");
			aSet.add(aValue);
			fail();
		} catch (IllegalArgumentException e) {
		}
	}

	public void	testSetIntegerIncludes() {
		executeTestIncludes(createSet(), new OclIntegerValue(10), true);
		executeTestIncludes(createSet(), new OclIntegerValue(20), true);
		executeTestIncludes(createSet(), new OclIntegerValue(30), false);
		executeTestIncludes(createSet(), getNull(), false);
		executeOperation(createSet(), "includes", getInvalid(), getInvalid());
		
		executeTestIncludes(createSetWithNull(), new OclIntegerValue(10), true);
		executeTestIncludes(createSetWithNull(), new OclIntegerValue(20), true);
		executeTestIncludes(createSetWithNull(), new OclIntegerValue(30), false);
		executeTestIncludes(createSetWithNull(), getNull(), true);
		executeOperation(createSetWithNull(), "includes", getInvalid(), getInvalid());

		executeTestIncludes(createEmptySet(), new OclIntegerValue(10), false);
		executeTestIncludes(createEmptySet(), getNull(), false);
	}

	public void	testSetIntegerExcludes() {
		executeTestExcludes(createSet(), new OclIntegerValue(10), false);
		executeTestExcludes(createSet(), new OclIntegerValue(20), false);
		executeTestExcludes(createSet(), new OclIntegerValue(30), true);
		executeTestExcludes(createSet(), getNull(), true);
		executeOperation(createSet(), "excludes", getInvalid(), getInvalid());
		
		executeTestExcludes(createSetWithNull(), new OclIntegerValue(10), false);
		executeTestExcludes(createSetWithNull(), new OclIntegerValue(20), false);
		executeTestExcludes(createSetWithNull(), new OclIntegerValue(30), true);
		executeTestExcludes(createSetWithNull(), getNull(), false);
		executeOperation(createSetWithNull(), "excludes", getInvalid(), getInvalid());

		executeTestExcludes(createEmptySet(), new OclIntegerValue(10), true);
		executeTestExcludes(createEmptySet(), getNull(), true);
	}

	public void	testCount() {
		executeTestCount(createSet(), new OclIntegerValue(10), 1);
		executeTestCount(createSet(), new OclIntegerValue(20), 1);
		executeTestCount(createSet(), new OclIntegerValue(30), 0);
		executeTestCount(createSet(), getNull(), 0);
		executeOperation(createSet(), "count", getInvalid(), getInvalid());

		executeTestCount(createSetWithNull(), new OclIntegerValue(10), 1);
		executeTestCount(createSetWithNull(), new OclIntegerValue(20), 1);
		executeTestCount(createSetWithNull(), new OclIntegerValue(30), 0);
		executeTestCount(createSetWithNull(), getNull(), 1);
		executeOperation(createSetWithNull(), "count", getInvalid(), getInvalid());

		executeTestCount(createEmptySet(), new OclIntegerValue(10), 0);
		executeTestCount(createEmptySet(), getNull(), 0);
	}

	public void	testIncludesExcludesAll() {
		OclSetValue	setOne = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setTwo = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setThree = createIntegerSet(new int[] { 10, 20, 30 });
		OclSetValue	setFour = createIntegerSet(new int[] {  20 });
		OclSetValue	setFive = createIntegerSet(new int[] { 90 });
		OclSetValue	setSix = createIntegerSet(new int[] { 10, 90 });
		OclSetValue	setSeven = createIntegerSet(new int[] { 25, 30 });
		OclSetValue	setEight = createIntegerSet(new int[] { 90, 100, 110 });
		OclSetValue emptySet = createIntegerSet(new int[] { } );

		OclSetValue	setWithNull = createIntegerSet(new int[] { 10, 20, 30 });
		setWithNull.add(getNull());

		OclSetValue compoundSet = 	new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType())));
		compoundSet.add(setOne);
		compoundSet.add(setFive);
		
		OclSetValue compoundElement1 = 	new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType())));
		compoundSet.add(setOne);

		OclSetValue compoundElement2 = 	new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType())));
		compoundSet.add(setOne);
		compoundSet.add(setEight);

		executeTestIncludesAll(setOne, setTwo, true);
		executeTestIncludesAll(setOne, setThree, true);
		executeTestIncludesAll(setOne, setFour,  true);
		executeTestIncludesAll(setOne, setFive,  false);
		executeTestIncludesAll(setOne, setSix, false);
		executeTestIncludesAll(setOne, setSeven, false);
		executeTestIncludesAll(setOne, setEight, false);
		executeTestIncludesAll(setOne, emptySet, true);
		executeTestIncludesAll(setFour, setOne, false);
		
		
		executeTestIncludesAll(compoundSet, compoundElement1, true);
		executeTestIncludesAll(compoundSet, compoundElement2, true);

		executeTestIncludesAll(setWithNull, setOne, false);
		executeTestIncludesAll(setWithNull, setThree, true);
		executeTestIncludesAll(setWithNull, setFour, true);
		executeTestIncludesAll(setWithNull, setWithNull, true);
		
		executeTestExcludesAll(setOne, setTwo, false);
		executeTestExcludesAll(setOne, setThree, false);
		executeTestExcludesAll(setOne, setFour,  false);
		executeTestExcludesAll(setOne, setFive,  true);
		executeTestExcludesAll(setOne, setSix, false);
		executeTestExcludesAll(setOne, setSeven, false);
		executeTestExcludesAll(setOne, setEight, true);
		executeTestExcludesAll(setOne, emptySet, true);
		
		executeTestExcludesAll(setFour, setOne, false);
		executeTestExcludesAll(setFour, setFive, true);
		executeTestExcludesAll(setFour, setEight, true);
	}

	public void testEmpty() {
		OclSetValue	setOne = createIntegerSet(new int[] {  });
		OclSetValue	setTwo = createIntegerSet(new int[] { 10 });
		OclSetValue	setThree = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });

		assertTrue(executeBooleanOperation(setOne, "isEmpty"));
		assertFalse(executeBooleanOperation(setTwo, "isEmpty"));
		assertFalse(executeBooleanOperation(setThree, "isEmpty"));
		assertFalse(executeBooleanOperation(createSetWithNull(), "isEmpty"));
		
		assertFalse(executeBooleanOperation(setOne, "notEmpty"));
		assertTrue(executeBooleanOperation(setTwo, "notEmpty"));
		assertTrue(executeBooleanOperation(setThree, "notEmpty"));
		assertTrue(executeBooleanOperation(createSetWithNull(), "notEmpty"));
	}

	public void	testSum() {
		OclSetValue	setOne = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setTwo = createIntegerSet(new int[] { 10 });
		OclSetValue	setThree = createIntegerSet(new int[] {  });

		OclSetValue	setWithNull = createIntegerSet(new int[] { 10, 20, 30 });
		setWithNull.add(getNull());

		assertEquals(150, executeIntegerOperation(setOne, "sum", null));
		assertEquals(10, executeIntegerOperation(setTwo, "sum", null));
		assertEquals(0, executeIntegerOperation(setThree, "sum", null));
		assertEquals(60, executeIntegerOperation(setWithNull, "sum", null));
	}

	public void	testAvg() {
		OclSetValue	setOne = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setTwo = createIntegerSet(new int[] { 10 });
		OclSetValue	setThree = createIntegerSet(new int[] {  });

		OclSetValue	setWithNull = createIntegerSet(new int[] { 10, 20, 30 });
		setWithNull.add(getNull());

		OclSetValue nullSet = createEmptySet();
		nullSet.add(getNull());
		
		assertEquals(getReal("30.0"), executeRealOperation(setOne, "avg", null));
		assertEquals(getReal("10.0"), executeRealOperation(setTwo, "avg", null));
		assertSame(getInvalid(), executeRealOperation(setThree, "avg", null));
		assertEquals(getReal("20.0"), executeRealOperation(setWithNull, "avg", null));
		assertSame(getNull(), executeRealOperation(nullSet, "avg", null));
	}

	public void	testMax() {
		OclSetValue	setOne = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setTwo = createIntegerSet(new int[] { 10 });
		OclSetValue	setThree = createIntegerSet(new int[] {  });

		OclSetValue	setWithNull = createIntegerSet(new int[] { 10, 20, 30 });
		setWithNull.add(getNull());

		OclSetValue nullSet = createEmptySet();
		nullSet.add(getNull());
		
		assertEquals(getReal("50.0"), executeRealOperation(setOne, "max", null));
		assertEquals(getReal("10.0"), executeRealOperation(setTwo, "max", null));
		assertSame(getInvalid(), executeRealOperation(setThree, "max", null));
		assertEquals(getReal("30.0"), executeRealOperation(setWithNull, "max", null));
		assertSame(getNull(), executeRealOperation(nullSet, "max", null));
	}

	public void	testMin() {
		OclSetValue	setOne = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setTwo = createIntegerSet(new int[] { 10 });
		OclSetValue	setThree = createIntegerSet(new int[] {  });

		OclSetValue	setWithNull = createIntegerSet(new int[] { 10, 20, 30 });
		setWithNull.add(getNull());

		OclSetValue nullSet = createEmptySet();
		nullSet.add(getNull());
		
		assertEquals(getReal("10.0"), executeRealOperation(setOne, "min", null));
		assertEquals(getReal("10.0"), executeRealOperation(setTwo, "min", null));
		assertSame(getInvalid(), executeRealOperation(setThree, "min", null));
		assertEquals(getReal("10.0"), executeRealOperation(setWithNull, "min", null));
		assertSame(getNull(), executeRealOperation(nullSet, "min", null));
	}
	
	public void	testUnion() {
		executeTestSetOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 11, 390);
		executeTestSetOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 6, 165);
		executeTestSetOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 11, 390);
		executeTestSetOperation("union", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 5, 150);
		executeTestSetOperation("union", new int[] { }, new int[] { 15, 25, 35, 45, 55, 65}, 6, 240);
		executeTestSetOperation("union", new int[] { }, new int[] { }, 0, 0);
		
		executeTestSetOperation("union", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 10, 20, 30 }), 4, 60);
		executeTestSetOperation("union", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 40, 50, 60 }), 7, 210);
		executeTestSetOperation("union", createIntegerSetWithNull(new int[] {  }), createIntegerSetWithNull(new int[] { 40, 50, 60 }), 4, 150);
	}

	public void 	testEquality() {
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 50, 10, 40 }, true);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50, 70 }, new int[] { 20, 30, 50, 10, 40 }, false);
		executeTestEquality(new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 50, 10, 40, 70 }, false);
		executeTestEquality(new int[] { 10 }, new int[] { 10 }, true);
		executeTestEquality(new int[] { }, new int[] { }, true);
		executeTestEquality(new int[] { 10 }, new int[] { 20 }, false);
	}

	public void 	testIntersectionSet() {
		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 20, 30, 30, 20, 30, 40, 50 }, 5, 150);
		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 40, 40 }, 4, 100);
		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 20, 20, 30, 30, 40, 50 }, 4, 140);
		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 40 , 40, 40 }, 1, 40);
		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 0, 0);
		executeTestSetOperation("intersection", new int[] { }, new int[] { 10, 20, 30, 40, 50, 50, 50 }, 0, 0);
		executeTestSetOperation("intersection", new int[] { 40  }, new int[] { 10, 20, 30, 40, 50, 40, 40 }, 1, 40);
		executeTestSetOperation("intersection", new int[] { 10, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50, 30, 10, 50 }, 4, 130);

		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 0, 0);
		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 0, 0);
		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 3, 80);
		executeTestSetOperation("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] {  } , 0, 0);
		executeTestSetOperation("intersection", new int[] { }, new int[] { }, 0, 0);
		
		executeTestSetOperation("intersection", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 10, 20, 30 }), 4, 60);
		executeTestSetOperation("intersection", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 40, 50, 60 }), 1, 0);
		executeTestSetOperation("intersection", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSet(new int[] { 10, 20, 30 }), 3, 60);
		executeTestSetOperation("intersection", createIntegerSet(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 10, 20, 30 }), 3, 60);
		executeTestSetOperation("intersection", createIntegerSetWithNull(new int[] {  }), createIntegerSet(new int[] { }), 0, 0);
	}

	public void 	testIntersectionBag() {
		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, 5, 150);
		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40 }, 4, 100);
		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 40, 50 }, 4, 140);
		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 40  }, 1, 40);
		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 0, 0);
		executeTestBagIntersection("intersection", new int[] { }, new int[] { 10, 20, 30, 40, 50 }, 0, 0);
		executeTestBagIntersection("intersection", new int[] { 40  }, new int[] { 10, 20, 30, 40, 50 }, 1, 40);
		executeTestBagIntersection("intersection", new int[] { 10, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, 4, 130);

		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 0, 0);
		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 0, 0);
		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 3, 80);
		executeTestBagIntersection("intersection", new int[] { 10, 20, 30, 40, 50 }, new int[] {  } , 0, 0);
		executeTestBagIntersection("intersection", new int[] { }, new int[] { }, 0, 0);
	}



	public void 	testDifference() {
		executeTestSetOperation("-", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, 0, 0);
		executeTestSetOperation("-", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40 }, 1, 50);
		executeTestSetOperation("-", new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 40, 50 }, 1, 10);
		executeTestSetOperation("-", new int[] { 10, 20, 30, 40, 50 }, new int[] { 40  }, 4, 110);
		executeTestSetOperation("-", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 5, 150);
		executeTestSetOperation("-", new int[] { }, new int[] { 10, 20, 30, 40, 50 }, 0, 0);
		executeTestSetOperation("-", new int[] { 40  }, new int[] { 10, 20, 30, 40, 50 }, 0, 0);
		executeTestSetOperation("-", new int[] { 10, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, 0, 0);

		executeTestSetOperation("-", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 5, 150);
		executeTestSetOperation("-", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 5, 150);
		executeTestSetOperation("-", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 2, 70);
		executeTestSetOperation("-", new int[] { }, new int[] { }, 0, 0);
		
		executeTestSetOperation("-", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 10, 20, 30 }), 0, 0);
		executeTestSetOperation("-", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 40, 50, 60 }), 3, 60);
		executeTestSetOperation("-", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSet(new int[] { 10, 20, 30 }), 1, 0);
		executeTestSetOperation("-", createIntegerSet(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 10, 20, 30 }), 0, 0);
		executeTestSetOperation("-", createIntegerSetWithNull(new int[] {  }), createIntegerSet(new int[] { }), 1, 0);

	}

	public void 	testSymmetricDifference() {
		executeTestSetOperation("symmetricDifference", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, 0, 0);
		executeTestSetOperation("symmetricDifference", new int[] { 10, 20, 30, 40, 50 }, new int[] { 10, 20, 30, 40 }, 1, 50);
		executeTestSetOperation("symmetricDifference", new int[] { 10, 20, 30, 40, 50 }, new int[] { 20, 30, 40, 50 }, 1, 10);
		executeTestSetOperation("symmetricDifference", new int[] { 10, 20, 30, 40, 50 }, new int[] { 40  }, 4, 110);
		executeTestSetOperation("symmetricDifference", new int[] { 10, 20, 30, 40, 50 }, new int[] { }, 5, 150);
		executeTestSetOperation("symmetricDifference", new int[] { }, new int[] { 10, 20, 30, 40, 50 }, 5, 150);
		executeTestSetOperation("symmetricDifference", new int[] { 40  }, new int[] { 10, 20, 30, 40, 50 }, 4, 110);
		executeTestSetOperation("symmetricDifference", new int[] { 10, 30, 40, 50 }, new int[] { 10, 20, 30, 40, 50 }, 1, 20);

		executeTestSetOperation("symmetricDifference", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15, 25, 35, 45, 55, 65}, 11, 390);
		executeTestSetOperation("symmetricDifference", new int[] { 10, 20, 30, 40, 50 }, new int[] { 15 } , 6, 165);
		executeTestSetOperation("symmetricDifference", new int[] { 10, 15, 20, 30, 40, 50 }, new int[] { 10, 15, 25, 30, 35, 40, 45, 55, 65}, 7, 295);
		executeTestSetOperation("symmetricDifference", new int[] { }, new int[] { }, 0, 0);
		
		executeTestSetOperation("symmetricDifference", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 10, 20, 30 }), 0, 0);
		executeTestSetOperation("symmetricDifference", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 40, 50, 60 }), 6, 210);
		executeTestSetOperation("symmetricDifference", createIntegerSetWithNull(new int[] { 10, 20, 30 }), createIntegerSet(new int[] { 10, 20, 30 }), 1, 0);
		executeTestSetOperation("symmetricDifference", createIntegerSet(new int[] { 10, 20, 30 }), createIntegerSetWithNull(new int[] { 10, 20, 30 }), 1, 0);
		executeTestSetOperation("symmetricDifference", createIntegerSetWithNull(new int[] {  }), createIntegerSet(new int[] { }), 1, 0);
		executeTestSetOperation("symmetricDifference", createIntegerSetWithNull(new int[] { 10, 20, 30, 60 }), createIntegerSetWithNull(new int[] { 10, 20, 30, 40, 50 }), 3, 150);
		executeTestSetOperation("symmetricDifference", createIntegerSetWithNull(new int[] { 10, 20, 30, 60 }), createIntegerSet(new int[] { 10, 20, 30, 40, 50 }), 4, 150);
	}

	public void 	testIncluding() {
		executeTestModifySetOperation("including", new int[] { 10, 20, 30, 40, 50 }, 60, 6, 210);
		executeTestModifySetOperation("including", new int[] { 10, 20, 30, 40, 50 }, 0, 6, 150);
		executeTestModifySetOperation("including", new int[] { }, 50, 1, 50);
		executeTestModifySetOperation("including", new int[] { 10  }, 20, 2, 30);
		executeTestModifySetOperation("including", new int[] { 10, 20, 30, 40, 50 }, 10, 5, 150);
		executeTestModifySetOperation("including", createIntegerSet(new int[] { 10, 20, 30, 40, 50 }), getNull(), 6, 150);
		executeTestModifySetOperation("including", createIntegerSetWithNull(new int[] { 10, 20, 30, 40, 50 }), getNull(), 6, 150);
		executeTestModifySetOperation("including", createIntegerSetWithNull(new int[] { }), getNull(), 1, 0);
	}

	public void 	testExcluding() {
		executeTestModifySetOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 60, 5, 150);
		executeTestModifySetOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 10, 4, 140);
		executeTestModifySetOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 50, 4, 100);
		executeTestModifySetOperation("excluding", new int[] { }, 50, 0, 0);
		executeTestModifySetOperation("excluding", new int[] { 10  }, 20, 1, 10);
		executeTestModifySetOperation("excluding", new int[] { 10  }, 10, 0, 0);
		executeTestModifySetOperation("excluding", new int[] { 10, 20, 30, 40, 50 }, 10, 4, 140);
		executeTestModifySetOperation("excluding", createIntegerSet(new int[] { 10, 20, 30, 40, 50 }), getNull(), 5, 150);
		executeTestModifySetOperation("excluding", createIntegerSetWithNull(new int[] { 10, 20, 30, 40, 50 }), getNull(), 5, 150);
		executeTestModifySetOperation("excluding", createIntegerSet(new int[] { }), getNull(), 0, 0);
		executeTestModifySetOperation("excluding", createIntegerSetWithNull(new int[] { }), getNull(), 0, 0);

	}

	public void 	testAsSet() {
		executeTestConversion("asSet", new int[] { 10, 20, 30, 40, 50 }, 60, 6, 210);
		executeTestConversion("asSet", new int[] { 10, 20, 30, 40, 50 }, 10, 5, 150);
		executeTestConversion("asSet", new int[] { 10, 20, 30, 40, 50 }, 50, 5, 150);
		executeTestConversion("asSet", new int[] { }, 50, 1, 50);
		executeTestConversion("asSet", new int[] { 10  }, 20, 2, 30);
		executeTestConversion("asSet", new int[] { 10  }, 10, 1, 10);
		executeTestConversion("asSet", new int[] { 10, 20, 30, 40, 50 }, 10, 5, 150);
	}

	public void 	testAsBag() {
		executeTestConversion("asBag", new int[] { 10, 20, 30, 40, 50 }, 60, 6, 210);
		executeTestConversion("asBag", new int[] { 10, 20, 30, 40, 50 }, 10, 6, 160);
		executeTestConversion("asBag", new int[] { 10, 20, 30, 40, 50 }, 50, 6, 200);
		executeTestConversion("asBag", new int[] { }, 50, 1, 50);
		executeTestConversion("asBag", new int[] { 10  }, 20, 2, 30);
		executeTestConversion("asBag", new int[] { 10  }, 10, 2, 20);
		executeTestConversion("asBag", new int[] { 10, 20, 30, 40, 50 }, 10, 6, 160);
	}


	public void 	testFlatten() {
		OclSetValue	setOne = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setTwo = createIntegerSet(new int[] { 40, 50 });
		OclSetValue	setThree = createIntegerSet(new int[] { 40, 60 });
		OclSetValue	setFour = createIntegerSet(new int[] { 100, 200, 300 });
		OclSetValue	setFive = createIntegerSet(new int[] { 400, 500 });
		OclSetValue	setSix = createIntegerSet(new int[] { 600, 500 });
		OclSetValue	setFiveWithNull = createIntegerSetWithNull(new int[] { 400, 500 });
		OclSetValue	setSixWithNull = createIntegerSetWithNull(new int[] { 600, 500 });

		OclSetValue	temp1 = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType())));
		OclSetValue	temp2 = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType())));
		OclSetValue	temp3 = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType())));
		OclSetValue	temp4 = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType())));

		temp1.add(setOne);
		temp1.add(setTwo);
		
		temp2.add(setThree);
		temp2.add(setFour);
		
		temp3.add(setFive);
		temp3.add(setSix);

		temp4.add(setFiveWithNull);
		temp4.add(setSixWithNull);

		OclSetValue	compoundSet1 = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType()))));
		compoundSet1.add(temp1);
		compoundSet1.add(temp2);
		compoundSet1.add(temp3);
		
		executeTestFlatten(temp1, 5, 150);
		executeTestFlatten(temp2, 5, 700);
		executeTestFlatten(temp3, 3, 1500);
		executeTestFlatten(temp4, 4, 1500);
		executeTestFlatten(compoundSet1, 12, 2310);
		
//		executeTestModifySetOperation("flatten", new int[] { 10, 20, 30, 40, 50 }, 60, 5, 150);
	}		

	public void	testEquals() {
		OclSetValue	setOne = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setTwo = createIntegerSet(new int[] { 10, 20, 30, 40, 50 });
		OclSetValue	setThree = createIntegerSet(new int[] { 30, 10, 40, 50, 20 });
		OclSetValue	setFour = createIntegerSet(new int[] { 40, 50, 20 });

		assertTrue(setOne.equals(setOne));
		assertTrue(setOne.equals(setTwo));
		assertTrue(setOne.equals(setThree));
		assertFalse(setOne.equals(setFour));
	}


	private void	executeTestIncludes(OclSetValue aSet, OclValue value, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		checkResult(getBoolean(expectedResult), aSet.executeOperation("includes", arguments));
	}

	private void	executeTestExcludes(OclSetValue aSet, OclValue value, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		checkResult(getBoolean(expectedResult), aSet.executeOperation("excludes", arguments));
	}

	private void	executeTestCount(OclSetValue aSet, OclValue value, int expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(value);
		
		checkResult(getInt(expectedResult), aSet.executeOperation("count", arguments));
	}

	private void	executeTestIncludesAll(OclSetValue setOne, OclSetValue setTwo, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(setTwo);
		checkResult(getBoolean(expectedResult), setOne.executeOperation("includesAll", arguments));
	}
	
	private void	executeTestExcludesAll(OclSetValue setOne, OclSetValue setTwo, boolean expectedResult) {
		List	arguments = new ArrayList();
		arguments.add(setTwo);
		checkResult(getBoolean(expectedResult), setOne.executeOperation("excludesAll", arguments));
	}

	private	void executeTestSetOperation(String opName, int[] values1, int[] values2, int expectedSize, int expectedTotal) {
		OclSetValue	setOne = createIntegerSet(values1);
		OclSetValue	setTwo = createIntegerSet(values2);
		executeTestSetOperation(opName, setOne, setTwo, expectedSize, expectedTotal);
	}

	private	void executeTestSetOperation(String opName, OclSetValue setOne, OclSetValue setTwo, int expectedSize, int expectedTotal) {
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclSetValue	resultSet = (OclSetValue) setOne.executeOperation(opName, arguments);
		verifySize(resultSet, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultSet, "sum", null));
	}

	private	void executeTestBagIntersection(String opName, int[] values1, int[] values2, int expectedSize, int expectedTotal) {
		OclSetValue	setOne = createIntegerSet(values1);
		OclBagValue	bagTwo = createIntegerBag(values2);
		List arguments = new ArrayList();
		arguments.add(bagTwo);
		
		OclSetValue	resultSet = (OclSetValue) setOne.executeOperation(opName, arguments);
		verifySize(resultSet, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultSet, "sum", null));
	}

	private	void executeTestModifySetOperation(String opName, int[] values1, int value, int expectedSize, int expectedTotal) {
		OclSetValue		setOne = createIntegerSet(values1);
		executeTestModifySetOperation(opName, setOne, getInt(value), expectedSize, expectedTotal);
	}

	private	void executeTestModifySetOperation(String opName, OclSetValue setOne, OclValue value, int expectedSize, int expectedTotal) {
		List arguments = new ArrayList();
		arguments.add(value);
		
		OclSetValue	resultSet = (OclSetValue) setOne.executeOperation(opName, arguments);
		verifySize(resultSet, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultSet, "sum", null));
	}

	private	void executeTestConversion(String opName, int[] values1, int value, int expectedSize, int expectedTotal) {
		OclSetValue		setOne = createIntegerSet(values1);
		OclIntegerValue	setTwo = new OclIntegerValue(value);
		
		OclCollectionValue	resultSet = (OclCollectionValue) setOne.executeOperation(opName, null);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		OclCollectionValue resultCollection = (OclCollectionValue) resultSet.executeOperation("including", arguments);
		verifySize(resultCollection, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultCollection, "sum", null));
	}


	private	void executeTestEquality(int[] values1, int[] values2, boolean expectedResult) {
		OclSetValue	setOne = createIntegerSet(values1);
		OclSetValue	setTwo = createIntegerSet(values2);
		List arguments = new ArrayList();
		arguments.add(setTwo);
		
		OclBooleanValue result = (OclBooleanValue) setOne.executeOperation("=", arguments);
		assertEquals(expectedResult, result.booleanValue());
	}

	private	void executeTestFlatten(OclSetValue setOne, int expectedSize, int expectedTotal) {
		OclSetValue	resultSet = (OclSetValue) setOne.executeOperation("flatten", null);
		verifySize(resultSet, expectedSize);
		if (expectedSize > 0)
			assertEquals(expectedTotal, executeIntegerOperation(resultSet, "sum", null));
	}


	private	boolean executeBooleanOperation(OclSetValue aSet, String operation) {
		return (((OclBooleanValue) aSet.executeOperation(operation, null)).booleanValue());
	}

	private	long executeIntegerOperation(OclCollectionValue aSet, String operation, List arguments) {
		return ((OclIntegerValue) aSet.executeOperation(operation, arguments)).intValue();
	}

	private	OclValue executeRealOperation(OclCollectionValue aSet, String operation, List arguments) {
		return aSet.executeOperation(operation, arguments);
	}


	private	OclSetValue	createEmptySet() {
		return	new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType()));
	}

	private	OclSetValue	createSet() {
		OclSetValue	aSet = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType()));
		
		try {
			verifySize(aSet, 0);
			OclIntegerValue aNumber = new OclIntegerValue(10);
			aSet.add(aNumber);
			verifySize(aSet, 1);
		} catch (IllegalArgumentException e) {
			fail();
		}

		try {
			OclIntegerValue aNumber = new OclIntegerValue(20);
			aSet.add(aNumber);
			verifySize(aSet, 2);
		} catch (IllegalArgumentException e) {
			fail();
		}
		
		return	aSet;
	}
	
	private	OclSetValue	createSetWithNull() {
		OclSetValue aSet = createSet();
		
		aSet.add(getNull());
		verifySize(aSet, 3);
	
		try {
			aSet.add(getInvalid());
		} catch (Exception e) {
		}
		verifySize(aSet, 3);
		
		aSet.add(getNull());
		verifySize(aSet, 3);
		
		return	aSet;
	}
	
	

	private	OclSetValue	createIntegerSet(int[] values) {
		OclSetValue	aSet = new OclSetValue(OclTypesFactory.createSetType(OclTypesFactory.createOclIntegerType()));
		
		try {
			for (int i = 0; i < values.length; i++) {
				OclIntegerValue aNumber = new OclIntegerValue(values[i]);
				aSet.add(aNumber);
			} 
		} catch (IllegalArgumentException e) {
			fail();
		}

		return	aSet;
	}

	private	OclSetValue	createIntegerSetWithNull(int[] values) {
		OclSetValue	aSet = createIntegerSet(values);
		aSet.add(getNull());
		return	aSet;
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


	private	void verifySize(OclCollectionValue collection, int expectedValue) {
		assertEquals(expectedValue, ((OclIntegerValue) collection.executeOperation("size", null)).intValue() );
	}

}
