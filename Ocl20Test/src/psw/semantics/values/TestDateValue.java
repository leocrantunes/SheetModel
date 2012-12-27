package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;


import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestPSWOclEvaluator;

public class TestDateValue extends TestPSWOclEvaluator {
	
	public void testGetData() throws Exception {
		executeOperation(getDate("03/10/2005"), "getDay", getInt(3));
		executeOperation(getDate("03/10/2005"), "getMonth", getInt(10));
		executeOperation(getDate("03/10/2005"), "getYear", getInt(2005));
		
		executeOperation(getDate("01/10/2005"), "getDow", getInt(7));
		executeOperation(getDate("02/10/2005"), "getDow", getInt(1));
		executeOperation(getDate("03/10/2005"), "getDow", getInt(2));
		executeOperation(getDate("04/10/2005"), "getDow", getInt(3));
		
		executeOperation(getDate("31/01/2005"), "getDay", getInt(31));
		executeOperation(getDate("31/01/2005"), "getMonth", getInt(1));
		executeOperation(getDate("31/01/2005"), "getYear", getInt(2005));
	}

	public void testIsBefore_After() throws Exception {
		executeOperation(getDate("03/10/2005"), "isBefore", getDate("04/10/2005"), getBoolean(true));
		executeOperation(getDate("03/10/2005"), "isBefore", getDate("01/10/2006"), getBoolean(true));
		executeOperation(getDate("03/10/2005"), "isBefore", getDate("04/10/2004"), getBoolean(false));
		executeOperation(getDate("03/10/2005"), "isBefore", getDate("01/10/2005"), getBoolean(false));
		executeOperation(getDate("03/10/2005"), "isBefore", getDate("03/10/2005"), getBoolean(false));

		executeOperation(getDate("03/10/2005"), "isAfter", getDate("04/10/2005"), getBoolean(false));
		executeOperation(getDate("03/10/2005"), "isAfter", getDate("01/10/2006"), getBoolean(false));
		executeOperation(getDate("03/10/2005"), "isAfter", getDate("04/10/2004"), getBoolean(true));
		executeOperation(getDate("03/10/2005"), "isAfter", getDate("01/10/2005"), getBoolean(true));
		executeOperation(getDate("03/10/2005"), "isAfter", getDate("03/10/2005"), getBoolean(false));
	}
	
	
	public void testIsLeapYear() throws Exception {
		executeOperation(getDate("03/10/2005"), "isLeapYear", getBoolean(false));
		executeOperation(getDate("03/10/2006"), "isLeapYear", getBoolean(false));
		executeOperation(getDate("03/10/2007"), "isLeapYear", getBoolean(false));
		executeOperation(getDate("03/10/2008"), "isLeapYear", getBoolean(true));
		executeOperation(getDate("03/10/2009"), "isLeapYear", getBoolean(false));
	}

	public void testAddDay() throws Exception {
		executeOperation(getDate("03/10/2005"), "addDay", getInt(1), getDate("04/10/2005"));
		executeOperation(getDate("03/10/2005"), "addDay", getInt(2), getDate("05/10/2005"));
		executeOperation(getDate("03/10/2005"), "addDay", getInt(10), getDate("13/10/2005"));
		executeOperation(getDate("03/10/2005"), "addDay", getInt(20), getDate("23/10/2005"));
		executeOperation(getDate("03/10/2005"), "addDay", getInt(28), getDate("31/10/2005"));
		executeOperation(getDate("03/10/2005"), "addDay", getInt(29), getDate("01/11/2005"));
		
		executeOperation(getDate("30/12/2005"), "addDay", getInt(2), getDate("01/01/2006"));
	}
	
	public void testAddMonth() throws Exception {
		executeOperation(getDate("03/10/2005"), "addMonth", getInt(1), getDate("03/11/2005"));
		executeOperation(getDate("03/10/2005"), "addMonth", getInt(2), getDate("03/12/2005"));
		executeOperation(getDate("03/10/2005"), "addMonth", getInt(3), getDate("03/01/2006"));
		executeOperation(getDate("03/10/2005"), "addMonth", getInt(15), getDate("03/01/2007"));
		
		executeOperation(getDate("31/10/2005"), "addMonth", getInt(1), getDate("30/11/2005"));
		executeOperation(getDate("30/11/2005"), "addMonth", getInt(1), getDate("30/12/2005"));
	}	

	public void testAddYear() throws Exception {
		executeOperation(getDate("03/10/2005"), "addYear", getInt(1), getDate("03/10/2006"));
		executeOperation(getDate("03/10/2005"), "addYear", getInt(2), getDate("03/10/2007"));
	}	
	
	public void testEquality() throws Exception {
		executeOperation(getDate("03/10/2005"), "=", getDate("03/10/2005"), getBoolean(true));
		executeOperation(getDate("03/10/2005"), "=", getDate("04/10/2005"), getBoolean(false));
	}
	
	public void testDifference() throws Exception {
		executeOperation(getDate("03/10/2005"), "<>", getDate("03/10/2005"), getBoolean(false));
		executeOperation(getDate("03/10/2005"), "<>", getDate("04/10/2005"), getBoolean(true));
	}

}
