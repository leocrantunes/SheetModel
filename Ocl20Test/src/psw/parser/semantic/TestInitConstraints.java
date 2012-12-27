/*
 * Created on Dec 27, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import java.util.List;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestInitConstraints extends TestPropertyCallExp {
	public void testInit_01() {
		doTestContextOK("context Film::name init : \"film name\"",     
				getCurrentMethodName());
	}

	public void testInit_15() {
		doTestContextOK("context Film::name : String init : \"film name\"",     
				getCurrentMethodName());
	}

	public void testInit_02() {
		doTestContextOK("context Tape::number derive : 10 ",     
				getCurrentMethodName());
	}

	public void testInit_03() {
		doTestManyContextOK("context Film::name init : \"film name\"  " +
								       			"context SpecialFilm::name init : \"special film name\"",     
				getCurrentMethodName());
	}

	public void testInit_04() {
		doTestContextOK(  "context SpecialFilm::name init : \"special film name\"",     
				getCurrentMethodName());
	}

	public void testInvalidInit() {
		doTestContextNotOK(  "context SpecialFilm::days init : rentalFee + 20",     
				getCurrentMethodName());
	}


	public void testInitRedefinition() {
		doTestManyContextNotOK(
										"context Film::name init : \"film name\" \r\n" +
									  	"context Tape::number derive : 10 \r\n" + 
									   	"context Film::name init : \"special film name\"",     
				getCurrentMethodName());
	}


	public void testInitWrongDeclaration() {
		doTestContextNotOK("context Tape::number : String init : \"joao\"",     
				getCurrentMethodName());
	}

	public void testInitWrongExpressionType() {
		doTestContextNotOK("context Tape::number init : \"joao\"",     
				getCurrentMethodName());
	}

	public void testDeriveRedefinition() {
		doTestManyContextNotOK(
									"context Tape::number derive : 10 " +
									"context Tape::number derive : 20",     
				getCurrentMethodName());
	}

	public void testderiveWrongExpressionType() {
		doTestContextNotOK("context Tape::number derive : true ",     
				getCurrentMethodName());
	}

	protected List getConstraints(CSTNode rootNode) {
		return	null;
	}
//	protected List doTestContextOK(String expression, String testName) {
//		try {
//			rootNode = getNode(expression, testName);
//			oclSemanticAnalyzer.analyze(environment, rootNode);
//			CSTAttrOrAssocContextCS context = (CSTAttrOrAssocContextCS) rootNode;
//			CSTNode constraints = context.getValueExpressionNodeCS();
//			List result = new ArrayList();
//			result.add(constraints);
//			return result;
//		}
//		catch (SemanticException ex) {
//			OCLWorkbenchToken token = ex.getNode().getToken();
//			System.out.println(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.getMessage());
//			fail();
//		}
//		catch (Exception e) {
//			System.out.println(e.getMessage());
//			fail();
//		}
//		return null;
//	}

}
