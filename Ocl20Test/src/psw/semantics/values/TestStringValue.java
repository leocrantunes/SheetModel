/*
 * Created on 27/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;


import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.TestPSWOclEvaluator;

/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestStringValue extends TestPSWOclEvaluator {
	public void	testSize() {
		String op = "size";
		
		executeOperation(getString(""), op, getInt(0));
		executeOperation(getString("a"), op, getInt(1));
		executeOperation(getString("abcdefghij"), op, getInt(10));
	}

	public void	testConcat() {
		String op = "concat";
		
		executeOperation(getString(""), op, getString(""), getString(""));
		executeOperation(getString(""), op, getString("a"), getString("a"));
		executeOperation(getString("a"), op, getString(""), getString("a"));
		executeOperation(getString("a"), op, getString("b"), getString("ab"));
		executeOperation(getString("abc"), op, getString("defg"), getString("abcdefg"));
	}

	public void testSubstring() {
		String op = "substring";
		
		executeOperation(getString("alex"), op, getInt(1), getInt(1), getString("a"));
		executeOperation(getString("alex"), op, getInt(1), getInt(4), getString("alex"));
		executeOperation(getString("alex"), op, getInt(2), getInt(3), getString("le"));
		executeOperation(getString("alex"), op, getInt(2), getInt(6), getInvalid());
		executeOperation(getString("alex"), op, getInt(0), getInt(2), getInvalid());
		executeOperation(getString("alex"), op, getInt(3), getInt(1), getInvalid());
	}

	public void	testToInteger() {
		String op = "toInteger";
		
		executeOperation(getString("0"), op, getInt(0));
		executeOperation(getString("1"), op, getInt(1));
		executeOperation(getString("100"), op, getInt(100));
		executeOperation(getString("-50"), op, getInt(-50));
	}

	public void	testToReal() {
		String op = "toReal";
		
		executeOperation(getString("0.0"), op, getReal("0.0"));
		executeOperation(getString("1.0"), op, getReal("1.0"));
		executeOperation(getString("100.2345"), op, getReal("100.2345"));
		executeOperation(getString("-50.9876"), op, getReal("-50.9876"));
	}
	
	public void	testToDate() throws Exception {
		String	op = "toDate";
		
		executeOperation(getString("10/05/2005"), op, getDate("10/5/2005"));
		executeOperation(getString("1/1/2005"), op, getDate("1/1/2005"));
		executeOperation(getString("01/01/2005"), op, getDate("01/01/2005"));
		executeOperation(getString("35/05/2005"), op, getInvalid());
		executeOperation(getString("5/35/2005"), op, getInvalid());
		executeOperation(getString("10/05"), op, getInvalid());
		executeOperation(getString("10-05-2005"), op, getInvalid());
	}
	
	public void	testToDateTime() throws Exception {
		String	op = "toDateTime";
		
		executeOperation(getString("10/05/2005 10:50:40"), op, getDateTime("10/5/2005 10:50:40"));
		executeOperation(getString("1/1/2005 23:40:33"), op, getDateTime("1/1/2005 23:40:33"));
		executeOperation(getString("01/01/2005 9:5:40"), op, getDateTime("01/01/2005 09:05:40"));
		executeOperation(getString("01/01/2005  9:5:40"), op, getDateTime("01/01/2005 09:05:40"));
		executeOperation(getString("01/01/2005 00:00:00"), op, getDate("01/01/2005"));
		executeOperation(getString("01/01/2005 10:44"), op, getInvalid());
		executeOperation(getString("01/01/2005"), op, getInvalid());
		executeOperation(getString("01/01/2005 25:00:40"), op, getInvalid());
		executeOperation(getString("01/01/2005 10:60:40"), op, getInvalid());
		executeOperation(getString("01/01/2005 10:10:60"), op, getInvalid());
	}

	

	public void	testEquality() {
		String op = "=";
		
		executeOperation(getString(""), op, getString(""), getBoolean(true));
		executeOperation(getString("a"), op, getString("a"), getBoolean(true));
		executeOperation(getString("a"), op, getString("A"), getBoolean(false));
		executeOperation(getString("alex"), op, getString("alex"), getBoolean(true));
		executeOperation(getString("alex"), op, getString("aleX"), getBoolean(false));
		executeOperation(getString("alex"), op, getString("john"), getBoolean(false));
	}

	public void	testDifferent() {
		String op = "<>";
		
		executeOperation(getString(""), op, getString(""), getBoolean(false));
		executeOperation(getString("a"), op, getString("a"), getBoolean(false));
		executeOperation(getString("a"), op, getString("A"), getBoolean(true));
		executeOperation(getString("alex"), op, getString("alex"), getBoolean(false));
		executeOperation(getString("alex"), op, getString("aleX"), getBoolean(true));
		executeOperation(getString("alex"), op, getString("john"), getBoolean(true));
	}

	public void	testStartsWith() {
		String op = "startsWith";
		
		executeOperation(getString(""), op, getString("a"), getBoolean(false));
		executeOperation(getString(""), op, getString(""), getBoolean(false));
		executeOperation(getString("a"), op, getString(""), getBoolean(false));
		executeOperation(getString("a"), op, getString("b"), getBoolean(false));
		executeOperation(getString("a"), op, getString("a"), getBoolean(true));
		executeOperation(getString("abcdef"), op, getString("a"), getBoolean(true));
		executeOperation(getString("abcdef"), op, getString("abcd"), getBoolean(true));
		executeOperation(getString("abcdef"), op, getString("abd"), getBoolean(false));
	}

	
	public void	testEndsWith() {
		String op = "endsWith";
		
		executeOperation(getString(""), op, getString(""), getBoolean(false));
		executeOperation(getString(""), op, getString("a"), getBoolean(false));
		executeOperation(getString("a"), op, getString(""), getBoolean(false));
		executeOperation(getString("a"), op, getString("b"), getBoolean(false));
		executeOperation(getString("a"), op, getString("a"), getBoolean(true));
		executeOperation(getString("abcdef"), op, getString("a"), getBoolean(false));
		executeOperation(getString("abcdef"), op, getString("f"), getBoolean(true));
		executeOperation(getString("abcdef"), op, getString("def"), getBoolean(true));
		executeOperation(getString("abcdef"), op, getString("cef"), getBoolean(false));
	}
	
	public void	testIndexOf() {
		String op = "indexOf";
		
		executeOperation(getString(""), op, getString(""), getInt(0));
		executeOperation(getString(""), op, getString("a"), getInt(0));
		executeOperation(getString("a"), op, getString(""), getInt(0));
		executeOperation(getString("a"), op, getString("b"), getInt(0));
		executeOperation(getString("a"), op, getString("a"), getInt(1));
		executeOperation(getString("abcdef"), op, getString("a"), getInt(1));
		executeOperation(getString("abcdef"), op, getString("b"), getInt(2));
		executeOperation(getString("abcdef"), op, getString("f"), getInt(6));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(1));
		executeOperation(getString("abcabc"), op, getString("f"), getInt(0));
		
		executeOperation(getString("abcabc"), op, getString(""), getInt(1), getInt(0));
		executeOperation(getString(""), op, getString(""), getInt(1), getInt(0));
		executeOperation(getString(""), op, getString("abc"), getInt(1), getInt(0));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(1), getInt(1));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(2), getInt(4));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(4), getInt(4));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(5), getInt(0));
		
		executeOperation(getString("abcabc"), op, getString("a"), getInt(0), getInt(1));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(10), getInt(0));
	}
	
	
	public void	testLastIndexOf() {
		String op = "lastIndexOf";
		
		executeOperation(getString(""), op, getString(""), getInt(0));
		executeOperation(getString(""), op, getString("a"), getInt(0));
		executeOperation(getString("a"), op, getString(""), getInt(0));
		executeOperation(getString("a"), op, getString("b"), getInt(0));
		executeOperation(getString("a"), op, getString("a"), getInt(1));
		executeOperation(getString("abcdef"), op, getString("a"), getInt(1));
		executeOperation(getString("abcdef"), op, getString("b"), getInt(2));
		executeOperation(getString("abcdef"), op, getString("f"), getInt(6));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(4));
		executeOperation(getString("abcabc"), op, getString("f"), getInt(0));
		
		executeOperation(getString("abcabc"), op, getString(""), getInt(1), getInt(0));
		executeOperation(getString(""), op, getString(""), getInt(1), getInt(0));
		executeOperation(getString(""), op, getString("abc"), getInt(1), getInt(0));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(1), getInt(1));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(2), getInt(1));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(4), getInt(4));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(5), getInt(4));
		
		executeOperation(getString("abcabc"), op, getString("a"), getInt(0), getInt(0));
		executeOperation(getString("abcabc"), op, getString("a"), getInt(10), getInt(4));
	}
	
	public void	testTrim() {
		String op = "trim";
		
		executeOperation(getString(""), op, getString(""));
		executeOperation(getString("       "), op, getString(""));
		executeOperation(getString("      abc"), op, getString("abc"));
		executeOperation(getString("       abc       "), op, getString("abc"));
		executeOperation(getString(" abc"), op, getString("abc"));
		executeOperation(getString("abc  "), op, getString("abc"));
		executeOperation(getString("abc def "), op, getString("abc def"));
		executeOperation(getString(" abc def "), op, getString("abc def"));
		executeOperation(getString("     abc def "), op, getString("abc def"));
	}

	public void	testLTrim() {
		String op = "ltrim";
		
		executeOperation(getString(""), op, getString(""));
		executeOperation(getString("       "), op, getString(""));
		executeOperation(getString("      abc"), op, getString("abc"));
		executeOperation(getString("       abc       "), op, getString("abc       "));
		executeOperation(getString(" abc"), op, getString("abc"));
		executeOperation(getString("abc  "), op, getString("abc  "));
		executeOperation(getString("abc def "), op, getString("abc def "));
		executeOperation(getString(" abc def "), op, getString("abc def "));
		executeOperation(getString("     abc def "), op, getString("abc def "));
	}

	public void	testRTrim() {
		String op = "rtrim";
		
		executeOperation(getString(""), op, getString(""));
		executeOperation(getString("       "), op, getString(""));
		executeOperation(getString("      abc"), op, getString("      abc"));
		executeOperation(getString("       abc       "), op, getString("       abc"));
		executeOperation(getString(" abc"), op, getString(" abc"));
		executeOperation(getString("abc  "), op, getString("abc"));
		executeOperation(getString("abc def "), op, getString("abc def"));
		executeOperation(getString(" abc def "), op, getString(" abc def"));
		executeOperation(getString("     abc def "), op, getString("     abc def"));
	}

	public void	testLike() {
		String op = "like";
		
		executeOperation(getString("fluzao"), op, getString("flu.*"), getBoolean(true));
		executeOperation(getString("fluzao"), op, getString("fla"), getBoolean(false));
		executeOperation(getString("fluzao"), op, getString("flu.a."), getBoolean(true));
		executeOperation(getString("fluzao"), op, getString("flu.mi"), getBoolean(false));
		executeOperation(getString("fluzao"), op, getString(".*ao"), getBoolean(true));
}

	
	public void	testToUpper() {
		String op = "toUpper";
		
		executeOperation(getString(""), op, getString(""));
		executeOperation(getString("       "), op, getString("       "));
		executeOperation(getString("abc"), op, getString("ABC"));
		executeOperation(getString("abC"), op, getString("ABC"));
		executeOperation(getString("Abc"), op, getString("ABC"));
		executeOperation(getString("ABC"), op, getString("ABC"));
	}

	public void	testToLower() {
		String op = "toLower";
		
		executeOperation(getString(""), op, getString(""));
		executeOperation(getString("       "), op, getString("       "));
		executeOperation(getString("ABC"), op, getString("abc"));
		executeOperation(getString("abC"), op, getString("abc"));
		executeOperation(getString("Abc"), op, getString("abc"));
		executeOperation(getString("abc"), op, getString("abc"));
	}

	
	public void	testEquals() {
		executeEqualsTest("alex", "alex",  true);
		executeEqualsTest("alex", "john", false);
		
		OclStringValue val = ValuesFactory.createStringValue("john");
		assertTrue(val.equals(val));
		OclBooleanValue valBoolean = ValuesFactory.createBooleanValue(true);
		assertFalse(val.equals(valBoolean));
	}


	private	void	executeEqualsTest(String val1, String val2, boolean expectedResult) {
		OclStringValue v1 = ValuesFactory.createStringValue(val1);
		OclStringValue v2 = ValuesFactory.createStringValue(val2);
		assertNotNull(v1);
		assertNotNull(v2);
		assertEquals(expectedResult, v1.equals(v2));			
	}

	
}
