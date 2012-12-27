/*
 * Created on Nov 28, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;


import java.util.Collection;

import ocl20.common.CoreClassifier;
import ocl20.types.BagType;
import ocl20.types.CollectionType;
import ocl20.types.OrderedSetType;
import ocl20.types.SequenceType;
import ocl20.types.SetType;
import ocl20.types.TupleType;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.type.CSTTypeCS;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestTypeCS extends TestNodeCS {


	public void test_01() {
		String stream = "MyExample::Film ";
		doParseTestOK(stream, this.getCurrentMethodName(), "Film", CoreClassifier.class);
	}
	
	public void test_02() {
		String stream = "Set  ( Film ) ";
		doParseTestOK(stream, this.getCurrentMethodName(), "Set(Film)", SetType.class);
	}

	public void test_03() {
		String stream = "Bag  ( Film ) ";
		doParseTestOK(stream, this.getCurrentMethodName(), "Bag(Film)", BagType.class);
	}

	public void test_04() {
		String stream = "Sequence  ( Film ) ";
		doParseTestOK(stream, this.getCurrentMethodName(), "Sequence(Film)", SequenceType.class);
	}

	public void test_05() {
		String stream = "Collection  ( Film ) ";
		doParseTestOK(stream, this.getCurrentMethodName(), "Collection(Film)", CollectionType.class);
	}
	
	public void test_06() {
		String stream = "OrderedSet  ( Film ) ";
		doParseTestOK(stream, this.getCurrentMethodName(), "OrderedSet(Film)", OrderedSetType.class);
	}

	public void test_SetOclAny() {
		String stream = "Set(OclAny)";
		doParseTestOK(stream, this.getCurrentMethodName(), "Set(OclAny)", SetType.class);
	}


	public void test_07() {
		String stream = "Set(Bag(Sequence(Film)))";
		CoreClassifier c = doParseTestOK(stream, this.getCurrentMethodName(), stream, SetType.class);
	    SetType s = (SetType) c;
	  	assertTrue(s.getElementType() instanceof BagType);
	  	BagType b = (BagType) s.getElementType();
	  	assertTrue(b.getElementType() instanceof SequenceType);
	    SequenceType seq = (SequenceType) b.getElementType();
	    assertTrue(seq.getElementType() instanceof CoreClassifier);
	    assertEquals("Film", seq.getElementType().getName());
	}

	public void test_08() {
		String stream = "Tuple(a : Film)";
		doParseTestOK(stream, this.getCurrentMethodName(), "Tuple(a : Film)", TupleType.class);
	}

	public void test_09() {
		String stream = "Tuple(a : Film,  b : String, c : Integer)";
		CoreClassifier c = doParseTestOK(stream, this.getCurrentMethodName(), "Tuple(a : Film, b : String, c : Integer)", TupleType.class);
		TupleType tuple = (TupleType) c;
		Collection parts = tuple.getTupleParts();
		assertEquals(3, parts.size());
	}

	public void test_10() {
		String stream = "Set(Tuple(a : Film,  b : String, c : Integer))";
		doParseTestOK(stream, this.getCurrentMethodName(), "Set(Tuple(a : Film, b : String, c : Integer))", SetType.class);
	}

	public void test_12() {
		String stream = "Set(Tuple(a : Film,  b : String, c : Integer))";
		doParseTestOK(stream, this.getCurrentMethodName(), "Set(Tuple(a : Film, b : String, c : Integer))", SetType.class);
	}


	public void test_13() {
		String stream = "Tuple(s1:Set(Integer), s2:Bag(String))";
		doParseTestOK(stream, this.getCurrentMethodName(), "Tuple(s1 : Set(Integer), s2 : Bag(String))", TupleType.class);
	}

	public void test_14() {
		String stream = "Tuple(a : Film,  b : Film)";
		doParseTestOK(stream, this.getCurrentMethodName(), "Tuple(a : Film, b : Film)", TupleType.class);
	}


	public void test_15() {
		String stream = "Set(Sequence(Foo))";
		doParseTestNotOK(stream, this.getCurrentMethodName());
	}

	public void test_16() {
		String stream = "Tuple(a, b, c)";
		doParseTestNotOK(stream, this.getCurrentMethodName());
	}

	public void test_17() {
		String stream = "Tuple(a : Film, b : String, c)";
		doParseTestNotOK(stream, this.getCurrentMethodName());
	}

	public void test_18() {
		String stream = "Tuple( : Film,  : String, c : Integer)";
		doParseTestNotOK(stream, this.getCurrentMethodName());
	}

	public void test_19() {
		String stream = "Tuple(c : Film, a : String, c : Integer)";
		doParseTestNotOK(stream, this.getCurrentMethodName());
	}

	public void test_20() {
		String stream = "Tuple(c : Film, a : String, d : Integer = 10)";
		doParseTestNotOK(stream, this.getCurrentMethodName());
	}

	public void test_21() {
		String stream = "Tuple(c : Film, a : String, d : Foo)";
		doParseTestNotOK(stream, this.getCurrentMethodName());
	}
	
	public void test_22() {
		String stream = "Set()";
		doParseTestNotOK(stream, this.getCurrentMethodName());
	}


	public CoreClassifier doParseTestOK(String stream, String inputName, String resultName, Class className) {
		try {
			CSTNode node = parseOK(stream, inputName); 
			assertTrue(node instanceof CSTTypeCS);
			CSTTypeCS type = (CSTTypeCS) node;
			
			CoreClassifier ast = type.getAst();
			assertNotNull(ast);
			assertEquals(resultName, ast.getName());
			assertTrue(className.isAssignableFrom(ast.getClass()));
			return ast;
		} 
		catch (Exception e) {
			e.printStackTrace();
			fail();
		}
		
		return null;
	}

	public void doParseTestNotOK(String stream, String inputName) {
		try {
			parseWithError(stream, inputName);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic.TestNodeCS#getSpecificNodeClass()
	 */
	protected Class getSpecificNodeClass() throws Exception {
		// TODO Auto-generated method stub
		return CSTTypeCS.class;
	}

}
