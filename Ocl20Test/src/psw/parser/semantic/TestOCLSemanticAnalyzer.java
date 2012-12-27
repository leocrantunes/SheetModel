/* * Created on Nov 21, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import java.io.PrintWriter;
import java.io.Reader;
import java.io.StringReader;
import java.util.Set;
import java.util.TreeSet;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreModel;
import ocl20.environment.Environment;

import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetaModelRepositoryException;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFMetamodelRepositoryFactory;
import br.ufrj.cos.lens.odyssey.MDRRepository.MOFRepositoryReader;
import br.ufrj.cos.lens.odyssey.MDRRepository.models.Uml14ModelsRepository;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.controller.OCLWorkbenchLexer;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.controller.OCLWorkbenchParser;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.controller.OCLWorkbenchToken;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.OCLSemanticAnalyzer;
import junit.framework.TestCase;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestOCLSemanticAnalyzer extends TestCase {

	OCLSemanticAnalyzer   oclSemanticAnalyzer = new OCLSemanticAnalyzer();
	Environment			  environment;
	CSTNode				  rootNode;

	/* (non-Javadoc)
 	* @see junit.framework.TestCase#setUp()
 	*/
	protected void setUp() throws Exception {
		super.setUp();
		assertNotNull(environment = getEnvironment());
	}

	public void testPackageDeclaration_01() {
		doTestPackageOK("package MyExample endpackage", getCurrentMethodName());
	}

	public void testPackageDeclaration_02() {
		doTestPackageOK("package MyExample::package_1 endpackage", getCurrentMethodName());
	}


	public void testPackageDeclaration_03() {
		doTestPackageNotOK("package MyTestPackage endpackage", getCurrentMethodName());
	}

	public void testPackageDeclaration_04() {
		doTestPackageNotOK("package Film endpackage", getCurrentMethodName());
	}


	public void testClassifier_01() {
		doTestContextOK("context Film inv: expression",     
				getCurrentMethodName());

	}

	public void testClassifier_02() {
		doTestContextOK("context MyExample::package_1::package_1_1::MyBoolean inv: expression",   
				getCurrentMethodName());
	}

	public void testClassifier_03() {
		doTestContextNotOK("context Product inv: expression",   
				getCurrentMethodName());
	}

	public void testClassifier_04() {
		doTestContextNotOK("context MyExample::package_1::Product inv: expression",  
				getCurrentMethodName());
	}

	public void testClassifier_05() {
		try {
			rootNode = getRootNode("package MyExample::package_1::package_1_1 context  Product inv: expression context Rental inv: exp context Film inv: exp context  Product inv: expression endpackage" +
				" package java context Integer inv: expression endpackage", "testClassifier_05");
			oclSemanticAnalyzer.analyze(environment, rootNode);
		}
		catch (OCLSemanticException ex) {
			OCLWorkbenchToken token = ex.getNode().getToken();
			System.out.println(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.getMessage());
			fail();
		}
		catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}

	public void testClassifier_06() {
		doTestPackageOK("package MyExample context Film inv first: expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_07() {
		doTestPackageOK("package MyExample context Film inv first: expr inv second: expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_08() {
		doTestPackageNotOK("package MyExample context Film inv first: expr inv second: expr inv first: expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_09() {
		doTestPackageOK("package MyExample context Film inv first: expr inv second: expr " +
			"  context Tape inv first: expr inv second : expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_10() {
		doTestPackageNotOK("package MyExample context Film inv first: expr inv second: expr " +
			"  context SpecialFilm inv first: expr inv second : expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_11() {
		doTestPackageOK("package MyExample context Film inv first: expr inv second: expr " +
			"  context SpecialFilm inv third: expr inv fourth: expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_12() {
		assertNotNull(environment.lookupLocal("OclString"));
		assertNotNull(environment.lookup("OclString"));
		assertTrue(environment.lookup("OclString") instanceof CoreClassifier);
		doTestPackageOK("package MyExample context OclString inv: expr endpackage" , getCurrentMethodName());
	}


	public void testInitExpression_AttributeOK() {
		doTestContextOK("context Film::rentalFee : Integer  init: value", 
				getCurrentMethodName());
	}

	public void testInitExpression_WrongType() {
		doTestContextNotOK("context Film::rentalFee : Real init: value", 
				getCurrentMethodName());
	}

	public void testInitExpression_WrongName() {
		doTestContextNotOK("context Film::rentalFee1 : Real init: value",
				getCurrentMethodName());
	}

	public void testInitExpression_WrongClassifier() {
		doTestContextNotOK("context AFilm::rentalFee1 : Real init: value", 
				getCurrentMethodName());
	}

	public void testInitExpression_RoleNameOK() {
		doTestContextOK("context Tape::theFilm : Film init: value", 
				getCurrentMethodName());
	}

	public void testInitExpression_WrongRoleType() {
		doTestContextNotOK("context Tape::theFilm : Tape init: value", 
				getCurrentMethodName());
	}

	public void testInitExpression_RoleNameWithoutTypeOK() {
		doTestContextOK("context Film::rentalFee init: value", 
				getCurrentMethodName());
	}

	public void testInitExpression_SubClassOK() {
		doTestContextOK("context SpecialFilm::rentalFee : Integer init: value", 
				getCurrentMethodName());
	}

	public void testInitExpression_SubClassRedefinitionOK() {
		doTestPackageOK("package MyExample context SpecialFilm::rentalFee : Integer init: value " +
			"  context Film::rentalFee : Integer init: value " +
			"  endpackage", getCurrentMethodName());
	}
	public void testInitExpression_WrongSubClass_01() {
		doTestPackageNotOK("package MyExample context SpecialFilm::rentalFee : Integer init: value " +
			" context Film::rentalFee : Integer init: value " +
			"  context SpecialFilm::rentalFee : Integer init: value "+
			" endpackage", getCurrentMethodName());
	}

	public void testInitExpression_WrongSubClass_02() {
		doTestPackageNotOK("package MyExample context SpecialFilm::rentalFee : Integer init: value " +
			" context Film::rentalFee : Integer init: value " +
			"  context SpecialFilm::rentalFee : Integer derive: value "+
			" endpackage", getCurrentMethodName());
	}

	public void testDeriveExpression_01() {
		doTestContextOK("context Film::rentalFee : Integer  derive: value", 
				getCurrentMethodName());
	}

	public void testDeriveExpression_02() {
		doTestContextNotOK("context Film::rentalFee : String derive: value", 
				getCurrentMethodName());
	}

	public void testClassifier_DefExpression_01() {
		doTestPackageOK("package MyExample context Film def:  var : double = expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_DefExpression_02() {
		doTestPackageNotOK("package MyExample context Film def:  var : double = expr  " +
			" context Film def: var2 : int = expr  def: var2 : double = expr1 endpackage", getCurrentMethodName());
	}

	public void testClassifier_DefExpression_03() {
		doTestPackageNotOK("package MyExample context Film def:  var : double = expr  " +
			" context Film def: var2 : int = expr  def: var : double = expr1 endpackage", getCurrentMethodName());
	}


	public void testClassifier_DefExpression_04() {
		doTestPackageNotOK("package MyExample context Film def:  code : type = expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_DefExpression_05() {
		doTestPackageNotOK("package MyExample context Film def:  code : type = endpackage", getCurrentMethodName());
	}

	public void testClassifier_DefExpression_06() {
		doTestPackageNotOK("package MyExample context Film def:  code : type endpackage", getCurrentMethodName());
	}

	public void testClassifier_DefExpression_07() {
		doTestPackageOK("package MyExample context Film def:  xpto = expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_DefExpression_08() {
		doTestPackageNotOK("package MyExample context Film def:  xpto = expr " +
			" context SpecialFilm def: xpto = expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_OperationDefExpression_01() {
		doTestPackageOK("package MyExample context Film def: operation(a : int, b : double) : int = expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_OperationDefExpression_02() {
		doTestPackageNotOK("package MyExample context Film def: operation(a : int, b : double) : int = expr " +
			" context Film def: operation(x : int, y : double) : int = expr endpackage", getCurrentMethodName());
	}

	public void testClassifier_OperationDefExpression_03() {
		doTestPackageNotOK("package MyExample context Film def: getRentalFee(dayOfWeek : int) : double = expr " +
			" endpackage", getCurrentMethodName());
	}

	public void testClassifier_OperationDefExpression_04() {
		doTestPackageOK("package MyExample context Film def: getRentalFee(dayOfWeek : double) : double = expr " +
			" endpackage", getCurrentMethodName());
	}

	public void testClassifier_OperationDefExpression_05() {
		doTestPackageNotOK("package MyExample context Film def: Film::getRentalFee() : double = expr " +
			" endpackage", getCurrentMethodName());
	}


	public void testOperationContext_01() {
		doTestContextOK("context Film::getRentalFee(dayOfWeek : int) : double body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_02() {
		doTestContextNotOK("context Film::getRentalFee(dayOfWeek : double) : double body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_03() {
		doTestContextNotOK("context Film::getRentalFee(dayOfWeek : int, another : int) : double body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_04() {
		doTestContextNotOK("context Film::getRentalFee() body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_05() {
		doTestContextNotOK("context Film::getRental()  body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_06() {
		doTestContextNotOK("context Film::getRentalFee(dayOfWeek : int) : int body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_07() {
		doTestContextNotOK("context Foo::getRentalFee(dayOfWeek : int) : double body : expression",
			getCurrentMethodName());			
	}


	public void testOperationContext_08() {
		doTestContextOK("context Film::setDaysForReturn(days : int) body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_09() {
		doTestContextNotOK("context getRentalFee(dayOfWeek : int) : double body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_10() {
		doTestContextOK("context SpecialFilm::doSomething() post : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_11() {
		doTestContextOK("context SpecialFilm::doSomething(a : int, b : int, c :double) : Integer pre : expression",
			getCurrentMethodName());			
	}


	public void testOperationContext_12() {
		doTestContextNotOK("context SpecialFilm::setDaysForReturn(days : int) body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_13() {
		doTestContextNotOK("context SpecialFilm::doSomething(a : int, b : int, c :double) : int body : expression",
			getCurrentMethodName());			
	}

	public void testOperationContext_14() {
		doTestContextNotOK("context SpecialFilm::doSomething(a : int, b : double, c :int) : int body : expression",
			getCurrentMethodName());			
	}



	private void doTestPackageOK(String expression, String testName) {
		try {
			rootNode = getRootNode(expression, testName);
			oclSemanticAnalyzer.analyze(environment, rootNode);
		}
		catch (OCLSemanticException ex) {
			OCLWorkbenchToken token = ex.getNode().getToken();
			System.out.println(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.getMessage());
			fail();
		}
		catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}

	private void doTestPackageNotOK(String expression, String testName) {
		try {	
			rootNode = getRootNode(expression, testName);
			oclSemanticAnalyzer.analyze(environment, rootNode);
			fail();
		}
		catch (OCLSemanticException ex) {
			OCLWorkbenchToken token = ex.getNode().getToken();
			System.out.println(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.getMessage());
			fail();
		}

		catch (Exception e) {
			System.out.println(e.getMessage());
		}
	}		


	private void doTestContextOK(String expression, String testName) {
		try {
			rootNode = getClassifierRootNode(expression, testName);
			oclSemanticAnalyzer.analyze(environment, rootNode);
		}
		catch (OCLSemanticException ex) {
			OCLWorkbenchToken token = ex.getNode().getToken();
			System.out.println(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.getMessage());
			fail();
		}

		catch (Exception e) {
			System.out.println(e.getMessage());
			fail();
		}
	}

	private void doTestContextNotOK(String expression, String testName) {
		try {
			rootNode = getClassifierRootNode(expression, testName);
			oclSemanticAnalyzer.analyze(environment, rootNode);
			fail();
		}
		catch (OCLSemanticException ex) {
			OCLWorkbenchToken token = ex.getNode().getToken();
			System.out.println(token.getFilename() + ":" + token.getLine() + "[" + token.getColumn() + "]" + ex.getMessage());
			fail();
		}

		catch (Exception e) {
			System.out.println(e.getMessage());
		}
	}

	private String getCurrentMethodName() {
		return new Exception().getStackTrace()[1].getMethodName();
	}

	private Environment getEnvironment() throws MOFMetaModelRepositoryException {
		CoreModel model;
		Uml14ModelsRepository  modelRepository = new Uml14ModelsRepository(MOFMetamodelRepositoryFactory.getRepository());
		String extentName = "PoseidonExample";
		modelRepository.importModel(extentName, "tests/resource/examples/myExamplePoseidon.xml");
		MOFRepositoryReader repository = modelRepository;
		assertNotNull(model = repository.getModel(extentName));
		return	model.getEnvironmentWithoutParents();	}

	private CSTNode getRootNode(String source, String inputName) throws Exception {
		OCLWorkbenchParser parser;
		PrintWriter err; 
	 
		Reader in = new StringReader(source);
		String inName = inputName;
		err = new PrintWriter(System.out);
		err.flush();
			
		Set errorsList = new TreeSet();
		OCLWorkbenchLexer lexer = new OCLWorkbenchLexer(in, inName, err, errorsList);
		parser = new OCLWorkbenchParser(inName, lexer, err, errorsList);
		
		CSTNode result = null;
		
		try {				
			result = parser.packageDeclarationCS();	
		} catch(antlr.RecognitionException e) {
			err.println(parser.getFilename() +":" + 
			"[" + e.getLine() + ", " + e.getColumn() + "]: " + 
			e.getMessage());
			throw e;
		} catch(antlr.TokenStreamRecognitionException e) {
			err.println(parser.getFilename() +":" + 
			"[" + e.recog.getLine() + ", " + e.recog.getColumn() + "]: " + 
			e.recog.getMessage());
			throw e;
		} catch(antlr.TokenStreamException ex) {
			err.println(parser.getFilename() +":" + ex.getMessage());
			throw ex;
		} finally {
			err.flush(); 
		}

		if (parser.errorCount() != 0)
			throw new Exception("syntatic errors in compilation");
		
		return result;
	}
	
	private CSTNode getClassifierRootNode(String source, String inputName) throws Exception {
			OCLWorkbenchParser parser;
			PrintWriter err; 
	 
			Reader in = new StringReader(source);
			String inName = inputName;
			err = new PrintWriter(System.out);
			err.flush();
			
		Set errorsList = new TreeSet();
		OCLWorkbenchLexer lexer = new OCLWorkbenchLexer(in, inName, err, errorsList);
		parser = new OCLWorkbenchParser(inName, lexer, err, errorsList);
		
			CSTNode result = null;
		
			try {				
				result = parser.contextDeclarationCS();	
			} catch(antlr.RecognitionException e) {
				err.println(parser.getFilename() +":" + 
				"[" + e.getLine() + ", " + e.getColumn() + "]: " + 
				e.getMessage());
				throw e;
			} catch(antlr.TokenStreamRecognitionException e) {
				err.println(parser.getFilename() +":" + 
				"[" + e.recog.getLine() + ", " + e.recog.getColumn() + "]: " + 
				e.recog.getMessage());
				throw e;
			} catch(antlr.TokenStreamException ex) {
				err.println(parser.getFilename() +":" + ex.getMessage());
				throw ex;
			}	finally {
				err.flush(); 
			}
			
			if (parser.errorCount() != 0)
				throw new Exception("syntatic errors in compilation");
			
			return result;
		}
		
}
