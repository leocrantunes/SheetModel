/*
 * Created on 07/07/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import java.io.PrintWriter;

import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;

import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclNullValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestEvalAttributeCallExp extends TestObjectEval {

	protected String sourceStream;
	
	public void tearDown() {
		oclCompiler.deleteAllConstraintsForSource(sourceStream);
		oclCompiler.deleteAllConstraintsForSource(sourceStream + "-a");
		oclCompiler.deleteAllConstraintsForSource(sourceStream + "-b");
		oclCompiler.deleteAllConstraintsForSource(sourceStream + "-c");
		oclCompiler.deleteAllConstraintsForSource(sourceStream + "-d");
	}

	public	void	testAttribute() {
		sourceStream = "testAttribute";
		OclObjectValue	obj = createInstanceOf("SpecialFilm");
		
		try {
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("lateReturnFee", new OclRealValue("5.90"));
		}	
		catch (RuntimeException e) {
			fail();
		}
		evalEnv.add("self", obj);
		insertObjectInEnvironment("self", "SpecialFilm");		
		
		evaluateStringExpression(sourceStream, "self.name", "AI");
		evaluateIntegerExpression(sourceStream, "self.days1", 10);
		evaluateStringExpression(sourceStream, "self.code", "1234");
		evaluateRealExpression(sourceStream, "self.lateReturnFee", 5.90);
	}

	public	void	testExternalObjectAttributes() {
		sourceStream = "testExternalObjectAttributes";
		
		OclObjectValue	obj = createInstanceOf("SpecialFilm");
		obj.setValueOf("name", new OclStringValue("AI"));
		obj.setValueOf("days1", new OclIntegerValue(10));
		obj.setValueOf("code", new OclStringValue("1234"));
		obj.setValueOf("lateReturnFee", new OclRealValue("5.90"));
		
		evalEnv.add("obj1", obj);
		insertObjectInEnvironment("obj1", "SpecialFilm");		
		
		evaluateStringExpression(sourceStream, "obj1.name", "AI");
		evaluateIntegerExpression(sourceStream, "obj1.days1", 10);
		evaluateStringExpression(sourceStream, "obj1.code", "1234");
		evaluateRealExpression(sourceStream, "obj1.lateReturnFee", 5.90);
	}

	public	void	testTwoInstances() {
		sourceStream = "testTwoInstances";
		
		OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
		obj1.setValueOf("name", new OclStringValue("AI"));
		obj1.setValueOf("days1", new OclIntegerValue(10));
		obj1.setValueOf("code", new OclStringValue("1234"));
		obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));

		OclObjectValue	obj2 = createInstanceOf("SpecialFilm");
		obj2.setValueOf("name", new OclStringValue("AI"));
		obj2.setValueOf("days1", new OclIntegerValue(20));
		obj2.setValueOf("code", new OclStringValue("2345"));
		obj2.setValueOf("lateReturnFee", new OclRealValue("1.10"));
		
		evalEnv.add("obj1", obj1);
		evalEnv.add("obj2", obj2);
		insertObjectInEnvironment("obj1", "SpecialFilm");		
		insertObjectInEnvironment("obj2", "SpecialFilm");
		
		evaluateIntegerExpression(sourceStream, "obj1.days1 + obj2.days1", 30);
		evaluateStringExpression(sourceStream, "obj2.code", "2345");
		evaluateRealExpression(sourceStream, "obj1.lateReturnFee + obj2.lateReturnFee", 7.00);
	}

	public	void	testClassAttributes() {
		sourceStream = "testClassAttributes";

		evaluateNullExpression(sourceStream, "Film::days");
		
		CoreClassifier film = (CoreClassifier) environment.lookup("Film");
		CoreAttribute attrib = film.lookupAttribute("days");
		assertNotNull(attrib);
		objectSpace.setValueForClassifierAttribute(attrib, new OclIntegerValue(200));
		
		OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
		obj1.setValueOf("name", new OclStringValue("AI"));
		obj1.setValueOf("days1", new OclIntegerValue(10));
		obj1.setValueOf("code", new OclStringValue("1234"));
		obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));
		evalEnv.add("obj1", obj1);
		insertObjectInEnvironment("obj1", "SpecialFilm");		
		
		evaluateIntegerExpression(sourceStream, "Film::days", 200);
		evaluateIntegerExpression(sourceStream, "obj1.days", 200);
	}

	
	public	void	testInitAttributes() {
		try {
			String sourceStream1 = "testInitAttributes";
			String sourceStream2 = "testInitAttributes-a";
			
			oclCompiler.compileOclStream("context SpecialFilm::rentalFee : Integer init: 30", sourceStream1, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::days1 : Integer init: 50", sourceStream2, new PrintWriter(System.out));
			
			OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
			obj1.setValueOf("name", new OclStringValue("AI"));
			obj1.setValueOf("days1", new OclIntegerValue(10));
			obj1.setValueOf("code", new OclStringValue("1234"));
			obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));
			
			evalEnv.add("obj1", obj1);
			insertObjectInEnvironment("obj1", "SpecialFilm");		
			
			evaluateIntegerExpression(sourceStream1, "obj1.days1", 10);
			evaluateIntegerExpression(sourceStream1, "obj1.rentalFee", 30);
			
			oclCompiler.deleteAllConstraintsForSource(sourceStream1);
			oclCompiler.deleteAllConstraintsForSource(sourceStream2);
			
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testInitAttributes_02() {
		try {
			sourceStream = "testInitAttributes";
			
			oclCompiler.compileOclStream("context Film::rentalFee : Integer init: 30", sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::days1 : Integer init: 50", sourceStream + "-a", new PrintWriter(System.out));
			
			OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
			obj1.setValueOf("name", new OclStringValue("AI"));
			obj1.setValueOf("days1", new OclIntegerValue(10));
			obj1.setValueOf("code", new OclStringValue("1234"));
			obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));
			
			evalEnv.add("obj1", obj1);
			insertObjectInEnvironment("obj1", "SpecialFilm");		
			
			evaluateIntegerExpression(sourceStream, "obj1.days1", 10);
			evaluateIntegerExpression(sourceStream, "obj1.rentalFee", 30);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testInitAttributes_03() {
		try {
			sourceStream = "testInitAttributes";
			
			oclCompiler.compileOclStream("context Film::rentalFee : Integer init: 20", sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::rentalFee : Integer init: 30", sourceStream + "-a", new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::days1 : Integer init: 50", sourceStream + "-b", new PrintWriter(System.out));
			
			OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
			obj1.setValueOf("name", new OclStringValue("AI"));
			obj1.setValueOf("days1", new OclIntegerValue(10));
			obj1.setValueOf("code", new OclStringValue("1234"));
			obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));
			
			OclObjectValue	obj2 = createInstanceOf("Film");
			obj2.setValueOf("name", new OclStringValue("AI"));
			obj2.setValueOf("code", new OclStringValue("2345"));
			
			evalEnv.add("obj1", obj1);
			insertObjectInEnvironment("obj1", "SpecialFilm");		
			evalEnv.add("obj2", obj2);
			insertObjectInEnvironment("obj2", "Film");		
			
			evaluateIntegerExpression(sourceStream, "obj1.days1", 10);
			evaluateIntegerExpression(sourceStream, "obj1.rentalFee", 30);
			evaluateIntegerExpression(sourceStream, "obj2.rentalFee", 20);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testInitAttributes_04() {
		try {
			sourceStream = "testInitAttributes";
			
			oclCompiler.compileOclStream("context SpecialFilm::rentalFee : Integer init: self.code.toInteger()", sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::code : String init: \"4433\" ", sourceStream + "-a", new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::days1 : Integer init: 50", sourceStream + "-b", new PrintWriter(System.out));
			
			OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
			
			evalEnv.add("obj1", obj1);
			insertObjectInEnvironment("obj1", "SpecialFilm");		

			evaluateIntegerExpression(sourceStream, "obj1.rentalFee", 4433);

			obj1.setValueOf("name", new OclStringValue("AI"));
			obj1.setValueOf("code", new OclStringValue("1234"));
			obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));

			evaluateIntegerExpression(sourceStream, "obj1.days1", 50);
			evaluateStringExpression(sourceStream, "obj1.code", "1234");
			evaluateIntegerExpression(sourceStream, "obj1.rentalFee", 4433);

		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testDeriveAttributes_01() {
		OclObjectValue obj1 = null;
		try {
			sourceStream = "testDeriveAttributes_01";
			
			oclCompiler.compileOclStream("context SpecialFilm::rentalFee : Integer derive: self.code.toInteger() + 20", sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::code : String init: \"4433\" ", sourceStream + "-a", new PrintWriter(System.out));
			
			obj1 = createInstanceOf("SpecialFilm");
			obj1.setValueOf("name", new OclStringValue("AI"));
			obj1.setValueOf("days1", new OclIntegerValue(10));
			obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));
			
			evalEnv.add("obj1", obj1);
			insertObjectInEnvironment("obj1", "SpecialFilm");		
			
			evaluateIntegerExpression(sourceStream, "obj1.rentalFee", 4453);
			obj1.setValueOf("code", new OclStringValue("3322"));
			evaluateIntegerExpression(sourceStream, "obj1.rentalFee", 3342);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}

		try {
			obj1.setValueOf("rentalFee", new OclIntegerValue(4400));
			fail();
		} catch (Exception e) {
			System.out.print(e.getMessage());
		}
	}

	public	void	testDeriveAttributes_02() {
		try {
			sourceStream = "testDeriveAttributes_02";
			
			oclCompiler.compileOclStream("context Film::rentalFee : Integer derive: self.code.toInteger() + 100", sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context Film::code : String init: \"1000\" ", sourceStream + "-a", new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::rentalFee : Integer derive: self.code.toInteger() + 200", sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::code : String init: \"2000\" ", sourceStream + "-c", new PrintWriter(System.out));
			
			OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
			obj1.setValueOf("name", new OclStringValue("AI"));
			obj1.setValueOf("days1", new OclIntegerValue(10));
			obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));
			
			OclObjectValue	obj2 = createInstanceOf("Film");
			obj1.setValueOf("name", new OclStringValue("AI"));
			obj1.setValueOf("days", new OclIntegerValue(10));
			
			evalEnv.add("obj1", obj1);
			insertObjectInEnvironment("obj1", "SpecialFilm");		
			evalEnv.add("obj2", obj2);
			insertObjectInEnvironment("obj2", "Film");		
			
			evaluateIntegerExpression(sourceStream, "obj1.rentalFee", 2200);
			obj1.setValueOf("code", new OclStringValue("3000"));
			evaluateIntegerExpression(sourceStream, "obj1.rentalFee", 3200);
			
			evaluateIntegerExpression(sourceStream, "obj2.rentalFee", 1100);
			obj2.setValueOf("code", new OclStringValue("5000"));
			evaluateIntegerExpression(sourceStream, "obj2.rentalFee", 5100);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}



	public	void	testDefAttributes_01() {
		try {
			sourceStream = "testDefAttributes_01";
			
			oclCompiler.compileOclStream("context Film def :netPrice : Integer = self.code.toInteger() + 100", sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream("context Film::code : String init: \"1000\" ", sourceStream + "-a", new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm def : anotherPrice : Integer = self.code.toInteger() + 200", sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm::code : String init: \"2000\" ", sourceStream + "-c", new PrintWriter(System.out));
			oclCompiler.compileOclStream("context SpecialFilm def: mySet: Set(Integer) = Set{1, 10, 100} ", sourceStream + "-d", new PrintWriter(System.out));
			
			OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
			obj1.setValueOf("name", new OclStringValue("AI"));
			obj1.setValueOf("days1", new OclIntegerValue(10));
			obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));
			
			evalEnv.add("obj1", obj1);
			insertObjectInEnvironment("obj1", "SpecialFilm");		
			
			evaluateIntegerExpression(sourceStream, "obj1.netPrice", 2100);
			obj1.setValueOf("code", new OclStringValue("3000"));
			evaluateIntegerExpression(sourceStream, "obj1.netPrice", 3100);
			evaluateIntegerExpression(sourceStream, "obj1.anotherPrice", 3200);
			evaluateIntegerExpression(sourceStream, "obj1.mySet->sum()", 111);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		} catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testTupleAttribute() {
		sourceStream = "testTupleAttribute";
		
		evaluateStringExpression(sourceStream, "let x : Tuple(name : String, value : Integer) = Tuple{name : String = \"Alexandre\", value : Integer = 10} in x.name", "Alexandre");
		evaluateIntegerExpression(sourceStream, "let x : Tuple(name : String, value : Integer) = Tuple{name : String = \"Alexandre\", value : Integer = 10} in x.value", 10);
	}


	public	void	testCollect() {
		sourceStream = "testCollect";
		
		OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
		obj1.setValueOf("name", new OclStringValue("AI"));
		obj1.setValueOf("days1", new OclIntegerValue(10));
		obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));

		OclObjectValue	obj2 = createInstanceOf("SpecialFilm");
		obj2.setValueOf("name", new OclStringValue("AI"));
		obj2.setValueOf("days1", new OclIntegerValue(20));
		obj2.setValueOf("code", new OclStringValue("2345"));
		obj2.setValueOf("lateReturnFee", new OclRealValue("1.10"));

		OclObjectValue	obj3 = createInstanceOf("SpecialFilm");
		obj3.setValueOf("name", new OclStringValue("AI"));
		obj3.setValueOf("days1", new OclIntegerValue(20));
		obj3.setValueOf("code", new OclStringValue("1000"));
		obj3.setValueOf("lateReturnFee", new OclRealValue("1.10"));

		evalEnv.add("obj1", obj1);
		evalEnv.add("obj2", obj2);
		evalEnv.add("obj3", obj3);
		insertObjectInEnvironment("obj1", "SpecialFilm");		
		insertObjectInEnvironment("obj2", "SpecialFilm");
		insertObjectInEnvironment("obj3", "SpecialFilm");
		
		evaluateIntegerExpression(sourceStream, "SpecialFilm::allInstances()->collect(code.toInteger())->size()", 3);
		evaluateIntegerExpression(sourceStream, "SpecialFilm::allInstances()->collect(code.toInteger())->sum()", 3345);
	}
	public	void	testNullAttribute() {
		sourceStream = "testNullAttribute";

		
		OclObjectValue	obj1 = createInstanceOf("SpecialFilm");
		obj1.setValueOf("days1", new OclIntegerValue(10));
		obj1.setValueOf("lateReturnFee", new OclRealValue("5.90"));

		evalEnv.add("obj1", obj1);
		insertObjectInEnvironment("obj1", "SpecialFilm");		
		
		evaluateBooleanExpression(sourceStream, "obj1.name.oclIsNull()", true);
		evaluateBooleanExpression(sourceStream, "obj1.name->isEmpty()", true);
		evaluateBooleanExpression(sourceStream, "obj1.name.oclIsUndefined()", true);
		
		obj1.setValueOf("name", new OclStringValue("alex"));
		evaluateBooleanExpression(sourceStream, "obj1.name->notEmpty()", true);	
		evaluateBooleanExpression(sourceStream, "obj1.name.oclIsUndefined()", false);
		
		obj1.setValueOf("name", new OclNullValue());
		evaluateBooleanExpression(sourceStream, "obj1.name.oclIsNull()", true);
		evaluateBooleanExpression(sourceStream, "obj1.name->isEmpty()", true);
	}

		
}
