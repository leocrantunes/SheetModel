/*
 * Created on Jun 28, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclBooleanValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclTupleValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestEvalTupleLiterals extends TestEval {

	private	OclTupleValue	compileTupleLiteral(String sourceStream, String expression) {
		
		try {
			exp = compileExpression(sourceStream, expression);
			
			assertNotNull(exp);
			
			OclTupleValue	value = (OclTupleValue) oclEvaluator.evaluate(exp, null, null);
			
			return	value;
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
		
		return	null;
	}

	public	void testSimpleTuple() {
		String	name = "testSimpleTuple";
		
		OclTupleValue	value = compileTupleLiteral(name, "Tuple{x:Integer = 10}");
		assertNotNull(value);
		assertEquals(((OclIntegerValue) value.getValueOf("x")).intValue(), 10);
	}

	public	void testTuple2() {
		String	name = "testTuple2";
		
		OclTupleValue	value = compileTupleLiteral(name, "Tuple{x:Integer = 10, y:String = \"alex\", z : Boolean = true}");
		assertNotNull(value);
		assertEquals(((OclIntegerValue) value.getValueOf("x")).intValue(), 10);
		assertEquals(((OclStringValue) value.getValueOf("y")).stringValue(), "alex");
		assertTrue(((OclBooleanValue) value.getValueOf("z")).booleanValue());
		
		try {
			assertTrue(((OclBooleanValue) value.getValueOf("abc")).booleanValue());
			fail();
		} catch (RuntimeException e) {
		}
	}


	public	void testTuple3() {
		String	name = "testTuple3";
		
		evaluateIntegerResult(name, "Tuple{x:Integer = 10, y:String = \"alex\", z : Boolean = true}.x", 10);
		evaluateStringResult(name, "Tuple{x:Integer = 10, y:String = \"alex\", z : Boolean = true}.y", "alex");
		evaluateBooleanResult(name, "Tuple{x:Integer = 10, y:String = \"alex\", z : Boolean = true}.z", true);
	}
	
	public	void testSetOfTuples() {
		String	name = "testSetOfTuples";
		
		evaluateIntegerResult(name, "Set { " +				"Tuple{x:Integer = 10, y:String = \"alex\", z : Boolean = true}, " +				"Tuple{x:Integer = 10, y:String = \"alex\", z : Boolean = true}, " +				"Tuple{x:Integer = 20, y:String = \"alex\", z : Boolean = false } " +			"}->size()", 3);
		
		evaluateIntegerResult(name, "Set { " +
				"Tuple{x:Integer = 10, y:String = \"alex\", z : Boolean = true}, " +
				"Tuple{x:Integer = 10, y:String = \"alex\", z : Boolean = true}, " +
				"Tuple{x:Integer = 20, y:String = \"alex\", z : Boolean = false } " +
			"}.x->sum()", 40);

	}
	
	public	void testTupleOfCollections() {
		String	name = "testSetOfCollections";
		
		evaluateIntegerResult(name, 
				"Tuple{x:Set(Integer) = Set{10, 20, 40}, y:Set(String) = Set{\"alex\"}, z : Set(Boolean) = Set {true, false} " +
				"}.x->sum()", 70);
		
		evaluateIntegerResult(name, 
				"Tuple{x = Set{10, 20, 40}, y = Set{\"alex\"}, z = Set {true, false} " +
				"}.x->sum()", 70);
		
		evaluateBooleanResult(name, 
				"Tuple{x = Set{10.0, 20 / 0, 40}, y = Set{\"alex\"}, z = Set {true, false} " +
				"}.x->oclIsInvalid()", true);
	}

	public	void testEquality() {
		String	name = "testEquality";

		evaluateBooleanResult(name, 
				"Tuple{x = 10, y = true, z = Tuple{a = 10, b = 20}}" +
				" = " +
				"Tuple{y = true, z = Tuple{b = 20, a = 10}, x = 10}",
				true);

		evaluateBooleanResult(name, 
				"Tuple{x = 10, y = true, z = Tuple{a = 10, b = 20}}" +
				" = " +
				"Tuple{y = true, z = Tuple{b = 20, a = 10}, x = 20}",
				false);

		evaluateBooleanResult(name, 
				"Tuple{x = 10, y = true, z = Tuple{a = 10, b = 20}}" +
				" = " +
				"Tuple{y = true, z = 20, x = 10}",
				false);

		evaluateBooleanResult(name, 
				"Tuple{x:Set(Integer) = Set{10, 20, 40}, y:Set(String) = Set{\"alex\"}, z = Sequence {true, false}}  " +
				" = " +
				"Tuple{y:Set(String) = Set{\"alex\"}, z = Sequence {true, false}, x:Set(Integer) = Set{40, 20, 10}}  ",
				true);

		evaluateBooleanResult(name, 
				"Tuple{x:Set(Integer) = Set{10, 20, 40}, y:Set(String) = Set{\"alex\"}, z = Sequence {true, false}}  " +
				" = " +
				"Tuple{y:Set(String) = Set{\"alexandre\"}, z = Sequence {true, false}, x:Set(Integer) = Set{40, 20, 10}}  ",
				false);

		evaluateBooleanResult(name, 
				"Tuple{x:Set(Integer) = Set{10, 20, 40}, y:Set(String) = Set{\"alex\"}, z = Sequence {true, false}}  " +
				" <> " +
				"Tuple{y:Set(String) = Set{\"alex\"}, z = Sequence {true, false}, x:Set(Integer) = Set{40, 20, 10}}  ",
				false);
		
		evaluateBooleanResult(name, 
				"Tuple{x:Set(Integer) = Set{10, 20, 40}, y:Set(String) = Set{\"alex\"}, z = Sequence {true, false}}  " +
				" <> " +
				"Tuple{y:Set(String) = Set{\"alexandre\"}, z = Sequence {true, false}, x:Set(Integer) = Set{40, 20, 10}}  ",
				true);
	}

}
