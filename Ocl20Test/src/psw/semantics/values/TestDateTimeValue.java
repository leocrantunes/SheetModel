package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestPSWOclEvaluator;

public class TestDateTimeValue extends TestPSWOclEvaluator {
	
	public void testGetData() throws Exception {
		executeOperation(getDateTime("03/10/2005 10:04:50" ), "getDay", getInt(3));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "getMonth", getInt(10));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "getYear", getInt(2005));
		executeOperation(getDateTime("03/10/2005 10:04:50" ), "getHour", getInt(10));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "getMinute", getInt(4));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "getSecond", getInt(50));
		
		executeOperation(getDateTime("04/10/2005 23:04:50"), "getMinute", getInt(4));
		executeOperation(getDateTime("04/10/2005 23:04:50"), "getSecond", getInt(50));
		executeOperation(getDateTime("04/10/2005 23:04:50"), "getHour", getInt(23));
		
		executeOperation(getDateTime("01/10/2005 23:04:50"), "getDow", getInt(7));
		executeOperation(getDateTime("02/10/2005 23:04:50"), "getDow", getInt(1));
		executeOperation(getDateTime("03/10/2005 23:04:50"), "getDow", getInt(2));
		executeOperation(getDateTime("04/10/2005 23:04:50"), "getDow", getInt(3));
		
		executeOperation(getDateTime("31/01/2005  23:04:50"), "getDay", getInt(31));
		executeOperation(getDateTime("31/01/2005  23:04:50"), "getMonth", getInt(1));
		executeOperation(getDateTime("31/01/2005  23:04:50"), "getYear", getInt(2005));
	}

	public void testIsBefore_After() throws Exception {
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("04/10/2005 10:04:50"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("01/10/2006 10:04:50"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("04/10/2004 10:04:50"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("01/10/2005 10:04:50"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("01/10/2005 23:04:50"), getBoolean(false));
		
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("03/10/2005 09:04:50"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("03/10/2005 10:04:49"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("03/10/2005 10:04:50"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("03/10/2005 10:04:51"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isBefore", getDateTime("03/10/2005 11:04:50"), getBoolean(true));

		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("04/10/2005 10:04:50"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("01/10/2006 10:04:50"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("04/10/2004 10:04:50"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("01/10/2005 10:04:50"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("01/10/2005 23:04:50"), getBoolean(true));
		
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("03/10/2005 09:04:50"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("03/10/2005 10:04:49"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("03/10/2005 10:04:50"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("03/10/2005 10:04:51"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 10:04:50"), "isAfter", getDateTime("03/10/2005 11:04:50"), getBoolean(false));
	}
	
	
	public void testIsLeapYear() throws Exception {
		executeOperation(getDateTime("03/10/2005 10:00:00"), "isLeapYear", getBoolean(false));
		executeOperation(getDateTime("03/10/2006 10:00:00"), "isLeapYear", getBoolean(false));
		executeOperation(getDateTime("03/10/2007 10:00:00"), "isLeapYear", getBoolean(false));
		executeOperation(getDateTime("03/10/2008 10:00:00"), "isLeapYear", getBoolean(true));
		executeOperation(getDateTime("03/10/2009 10:00:00"), "isLeapYear", getBoolean(false));
	}

	public void testAddDay() throws Exception {
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addDay", getInt(1), getDateTime("04/10/2005 10:00:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addDay", getInt(2), getDateTime("05/10/2005 10:00:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addDay", getInt(10), getDateTime("13/10/2005 10:00:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addDay", getInt(20), getDateTime("23/10/2005 10:00:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addDay", getInt(28), getDateTime("31/10/2005 10:00:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addDay", getInt(29), getDateTime("01/11/2005 10:00:00"));
		
		executeOperation(getDateTime("30/12/2005 10:00:00"), "addDay", getInt(2), getDateTime("01/01/2006 10:00:00"));
	}
	
	public void testAddMonth() throws Exception {
		executeOperation(getDateTime("03/10/2005 12:34:56"), "addMonth", getInt(1), getDateTime("03/11/2005 12:34:56"));
		executeOperation(getDateTime("03/10/2005 12:34:56"), "addMonth", getInt(2), getDateTime("03/12/2005 12:34:56"));
		executeOperation(getDateTime("03/10/2005 12:34:56"), "addMonth", getInt(3), getDateTime("03/01/2006 12:34:56"));
		executeOperation(getDateTime("03/10/2005 12:34:56"), "addMonth", getInt(15), getDateTime("03/01/2007 12:34:56"));
		
		executeOperation(getDateTime("31/10/2005 12:34:56"), "addMonth", getInt(1), getDateTime("30/11/2005 12:34:56"));
		executeOperation(getDateTime("30/11/2005 12:34:56"), "addMonth", getInt(1), getDateTime("30/12/2005 12:34:56"));
	}	

	public void testAddYear() throws Exception {
		executeOperation(getDateTime("03/10/2005 12:34:56"), "addYear", getInt(1), getDateTime("03/10/2006 12:34:56"));
		executeOperation(getDateTime("03/10/2005 12:34:56"), "addYear", getInt(2), getDateTime("03/10/2007 12:34:56"));
	}	
	
	public void testAddHour() throws Exception {
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addHour", getInt(1), getDateTime("03/10/2005 11:00:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addHour", getInt(14), getDateTime("04/10/2005 00:00:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addHour", getInt(15), getDateTime("04/10/2005 01:00:00"));
		
		executeOperation(getDateTime("31/12/2005 10:00:00"), "addHour", getInt(15), getDateTime("01/01/2006 01:00:00"));
	}
	
	public void testAddMinute() throws Exception {
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addMinute", getInt(1), getDateTime("03/10/2005 10:01:00"));
		executeOperation(getDateTime("03/10/2005 10:59:00"), "addMinute", getInt(1), getDateTime("03/10/2005 11:00:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addMinute", getInt(61), getDateTime("03/10/2005 11:01:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addMinute", getInt(60 * 25 + 1), getDateTime("04/10/2005 11:01:00"));
	}	

	public void testAddSecond() throws Exception {
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addSecond", getInt(1), getDateTime("03/10/2005 10:00:01"));
		executeOperation(getDateTime("03/10/2005 10:00:59"), "addSecond", getInt(1), getDateTime("03/10/2005 10:01:00"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addSecond", getInt(61), getDateTime("03/10/2005 10:01:01"));
		executeOperation(getDateTime("03/10/2005 10:00:00"), "addSecond", getInt(60 * 60 + 1), getDateTime("03/10/2005 11:00:01"));
	}	
	
	public void testEquality() throws Exception {
		executeOperation(getDateTime("03/10/2005 12:34:56"), "=", getDateTime("03/10/2005 12:34:56"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 12:34:56"), "=", getDateTime("03/10/2005 12:34:57"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 12:34:56"), "=", getDateTime("04/10/2005 12:34:56"), getBoolean(false));
	}
	
	public void testDifference() throws Exception {
		executeOperation(getDateTime("03/10/2005 12:34:56"), "<>", getDateTime("03/10/2005 12:34:56"), getBoolean(false));
		executeOperation(getDateTime("03/10/2005 12:34:56"), "<>", getDateTime("03/10/2005 12:34:57"), getBoolean(true));
		executeOperation(getDateTime("03/10/2005 12:34:56"), "<>", getDateTime("04/10/2005 12:34:56"), getBoolean(true));
	}


	
}
