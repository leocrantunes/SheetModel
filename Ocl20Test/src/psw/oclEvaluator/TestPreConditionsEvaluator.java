/*
 * Created on 29/04/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.constraints.OclPreConstraint;
import ocl20.constraints.OclPrePostConstraint;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PreconditionException;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.StrongerPreconditionException;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestPreConditionsEvaluator extends TestObjectEval {

	/**
	 * 
	 */
	public TestPreConditionsEvaluator() {
		super();
		// TODO Auto-generated constructor stub
	}

	public void		setUp() throws Exception {
		modelRepository = null;
		super.setUp();
	}

	
	public	void	testPreCondition_02() {
		try {
			String	sourceStream = "testPreCondition02";
			
			String	body = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	body : dayOfWeek * self.rentalFee * 2.0";
			
			oclCompiler.compileOclStream(body, sourceStream, new PrintWriter(System.out));
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(10) + 50 + self.getRentalFee(2)", 530);
			} catch (PreconditionException e) {
				fail();
			}
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
			} catch (PreconditionException e) {
				fail();
			}	
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
	
	public	void	testPreCondition_01() {
		try {
			String	sourceStream = "testPreCondition01";
			
			String	preCondition01 = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	pre pre01: dayOfWeek >= 1 and dayOfWeek <= 10 pre pre02: dayOfWeek >= 1 and dayOfWeek <= 8" ;
			String	body = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	body : dayOfWeek * self.rentalFee * 2.0";
			
			oclCompiler.compileOclStream(preCondition01, sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream(body, sourceStream + "-c", new PrintWriter(System.out));

			CoreClassifier filmClass = (CoreClassifier) environment.lookup("Film");
			assertNotNull(filmClass);
			List params = new ArrayList();
			params.add((CoreClassifier) environment.lookup("Integer"));
			CoreOperation oper = filmClass.lookupOperation("getRentalFee", params);
			assertNotNull(oper);
			Collection specifications = oper.getSpecifications();
			assertEquals(1, specifications.size());
			OclPrePostConstraint prePost = (OclPrePostConstraint) specifications.iterator().next();
			Collection preConditions = prePost.getPreConditions();
			assertEquals(2, preConditions.size());
			Iterator iterPre = preConditions.iterator();
			OclPreConstraint pre01 = (OclPreConstraint) iterPre.next();
			OclPreConstraint pre02 = (OclPreConstraint) iterPre.next();
			assertEquals("pre01", pre01.getName());
			assertEquals("pre02", pre02.getName());
			
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2) + 50 + self.getRentalFee(10)", 530);
				fail();
			} catch (PreconditionException e) {
				assertEquals(1, e.getPreconditionsFailed().size());
				OclPreConstraint constraint = (OclPreConstraint) e.getPreconditionsFailed().iterator().next();
				assertEquals("pre02", constraint.getName());
				System.out.println(e.getMessage());
			}
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
				fail();
			} catch (PreconditionException e) {
				assertEquals(2, e.getPreconditionsEvaluated().size());
				assertEquals(2, e.getPreconditionsFailed().size());
				Iterator iter = e.getPreconditionsFailed().iterator();
				OclPreConstraint constraint = (OclPreConstraint) iter.next();
				assertEquals("pre01", constraint.getName());
				constraint = (OclPreConstraint) iter.next();
				assertEquals("pre02", constraint.getName());
				System.out.println(e.getMessage());
			}	
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	
	public	void	testPreCondition_03() {
		try {
			String	sourceStream = "testPreCondition03";
			
			String	preCondition01 = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	pre pre01: dayOfWeek >= 1 and dayOfWeek <= 10" ;
			String	preCondition02 = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	pre pre02: dayOfWeek >= 1 and dayOfWeek <= 8";
			String	body = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	body : dayOfWeek * self.rentalFee * 2.0";
			
			oclCompiler.compileOclStream(preCondition01, sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream(preCondition02, sourceStream + "-a", new PrintWriter(System.out));
			oclCompiler.compileOclStream(body, sourceStream + "-c", new PrintWriter(System.out));

			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2) + 50 + self.getRentalFee(10)", 530);
				fail();
			} catch (PreconditionException e) {
				assertEquals(1, e.getPreconditionsFailed().size());
				OclPreConstraint constraint = (OclPreConstraint) e.getPreconditionsFailed().iterator().next();
				assertEquals("pre02", constraint.getName());
				System.out.println(e.getMessage());
			}
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
				fail();
			} catch (PreconditionException e) {
				assertEquals(2, e.getPreconditionsEvaluated().size());
				assertEquals(2, e.getPreconditionsFailed().size());
				Iterator iter = e.getPreconditionsFailed().iterator();
				OclPreConstraint constraint = (OclPreConstraint) iter.next();
				assertEquals("pre01", constraint.getName());
				constraint = (OclPreConstraint) iter.next();
				assertEquals("pre02", constraint.getName());
				System.out.println(e.getMessage());
			}	
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testPreCondition_04() {
		try {
			String	sourceStream = "testPreCondition04";
			
			String	preCondition01 = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	pre pre01: dayOfWeek >= 1 and dayOfWeek <= 10" ;
			String	preCondition02 = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	pre pre02: dayOfWeek >= 1 and dayOfWeek <= 8  pre pre03: dayOfWeek >= 1 and dayOfWeek <= 30";
			String	body = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	body : dayOfWeek * self.rentalFee * 2.0";
			
			oclCompiler.compileOclStream(preCondition01, sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream(preCondition02, sourceStream + "-a", new PrintWriter(System.out));
			oclCompiler.compileOclStream(body, sourceStream + "-c", new PrintWriter(System.out));

			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2) + 50 + self.getRentalFee(10)", 530);
				fail();
			} catch (PreconditionException e) {
				assertEquals(1, e.getPreconditionsFailed().size());
				OclPreConstraint constraint = (OclPreConstraint) e.getPreconditionsFailed().iterator().next();
				assertEquals("pre02", constraint.getName());
				System.out.println(e.getMessage());
			}
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
				fail();
			} catch (PreconditionException e) {
				assertEquals(3, e.getPreconditionsEvaluated().size());
				assertEquals(2, e.getPreconditionsFailed().size());
				Iterator iter = e.getPreconditionsFailed().iterator();
				OclPreConstraint constraint = (OclPreConstraint) iter.next();
				assertEquals("pre01", constraint.getName());
				constraint = (OclPreConstraint) iter.next();
				assertEquals("pre02", constraint.getName());
				System.out.println(e.getMessage());
			}	
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	
	public	void	testPreCondition_05() {
		try {
			String	sourceStream = "testPreCondition05";
			
			String	preCondition01 = "context Tape::tapesQty() : Integer pre pre01: Tape::allInstances()->size() > 1" ;
			String	preCondition02 = "context Tape::tapesQty() : Integer pre pre02: Tape::allInstances()->size() < 5"; 
			String	body = "context Tape::tapesQty() : Integer body : Tape::allInstances()->size()";
			
			oclCompiler.compileOclStream(preCondition01, sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream(preCondition02, sourceStream + "-a", new PrintWriter(System.out));
			oclCompiler.compileOclStream(body, sourceStream + "-c", new PrintWriter(System.out));

			createInstanceOf("Tape");
			createInstanceOf("Tape");
			
			rawEvaluateIntegerExpression(sourceStream, "Tape::tapesQty()", 2);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testPreCondition_06() {
		try {
			String	sourceStream = "testPreCondition06";
			
			String	preCondition01 = "context Tape::tapesQty() : Integer pre pre01: Tape::allInstances()->size() > 1" ;
			String	preCondition02 = "context Tape::tapesQty() : Integer pre pre02: Tape::allInstances()->size() < 5"; 
			String	body = "context Tape::tapesQty() : Integer body : Tape::allInstances()->size()";
			
			oclCompiler.compileOclStream(preCondition01, sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream(preCondition02, sourceStream + "-a", new PrintWriter(System.out));
			oclCompiler.compileOclStream(body, sourceStream + "-c", new PrintWriter(System.out));

			createInstanceOf("Tape");
			
			try {
				rawEvaluateIntegerExpression(sourceStream, "Tape::tapesQty()", 1);
				fail();
			} catch (PreconditionException e) {
				assertEquals(2, e.getPreconditionsEvaluated().size());
				assertEquals(1, e.getPreconditionsFailed().size());
				Iterator iter = e.getPreconditionsFailed().iterator();
				OclPreConstraint constraint = (OclPreConstraint) iter.next();
				assertEquals("pre01", constraint.getName());
				System.out.println(e.getMessage());
			}	
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	
	public	void	testPreCondition_Subcontracts() {
		try {
			String	sourceStream = "testPreCondition_Subcontracts" + "-b";
			
			String	body1 = "context Film::getSpecialFee(day : Integer) : Real 	pre: day > 10 body : self.rentalFee * 3.0 + day";
			String	body2 = "context SpecialFilm::getSpecialFee(day : Integer) : Real 	pre: day > 5 body : if (day > 10) then self.rentalFee * 1.0 else self.rentalFee + (day * 2)  * 1.0 endif";
			
			oclCompiler.compileOclStream(body1, sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream(body2, sourceStream + "-a", new PrintWriter(System.out));
			
			OclObjectValue	obj = createInstanceOf("Film");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));

			OclObjectValue	special = createInstanceOf("SpecialFilm");
			special.setValueOf("name", new OclStringValue("AI"));
			special.setValueOf("days", new OclIntegerValue(10));
			special.setValueOf("code", new OclStringValue("1234"));
			special.setValueOf("rentalFee", new OclIntegerValue(30));

			
			evalEnv.add("film", obj);
			insertObjectInEnvironment("film", "Film");		
			evalEnv.add("special", special);
			insertObjectInEnvironment("special", "SpecialFilm");		
			evalEnv.add("other", special);
			insertObjectInEnvironment("other", "Film");		

			try {
				rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(1)", 61);
				fail();
			} catch (PreconditionException e) {
			}
			
			try {
				rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(6)", 66);
				fail();
			} catch (PreconditionException e) {
			}
			
			rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(11)", 71);
			
			try {
				rawEvaluateRealExpression(sourceStream, "special.getSpecialFee(1)", 32);
				fail();
			} catch (PreconditionException e) {
			}
			rawEvaluateRealExpression(sourceStream, "special.getSpecialFee(6)", 42);
			rawEvaluateRealExpression(sourceStream, "other.getSpecialFee(11)", 30);

			try {
				rawEvaluateRealExpression(sourceStream, "other.getSpecialFee(1)", 32);
				fail();
			} catch (PreconditionException e) {
			}
			
			rawEvaluateRealExpression(sourceStream, "other.getSpecialFee(6)", 42);
			rawEvaluateRealExpression(sourceStream, "other.getSpecialFee(11)", 30);

		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	public	void	testPreCondition_Subcontracts_Inconsistency() {
		try {
			String	sourceStream = "testPreCondition_Subcontracts" + "-c";
			
			String	body2 = "context Film::getSpecialFee(day : Integer) : Real 	pre: day > 5 body : if (day > 10) then self.rentalFee * 1.0 else self.rentalFee + (day * 2)  * 1.0 endif";
			String	body1 = "context SpecialFilm::getSpecialFee(day : Integer) : Real 	pre: day > 10 body : self.rentalFee * 3.0 + day";
			
			oclCompiler.compileOclStream(body1, sourceStream, new PrintWriter(System.out));
			oclCompiler.compileOclStream(body2, sourceStream + "-a", new PrintWriter(System.out));
			
			OclObjectValue	obj = createInstanceOf("Film");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));

			OclObjectValue	special = createInstanceOf("SpecialFilm");
			special.setValueOf("name", new OclStringValue("AI"));
			special.setValueOf("days", new OclIntegerValue(10));
			special.setValueOf("code", new OclStringValue("1234"));
			special.setValueOf("rentalFee", new OclIntegerValue(30));

			
			evalEnv.add("film", obj);
			insertObjectInEnvironment("film", "Film");		
			evalEnv.add("special", special);
			insertObjectInEnvironment("special", "SpecialFilm");		
			evalEnv.add("other", special);
			insertObjectInEnvironment("other", "Film");		

			try {
				rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(1)", 22);
				fail();
			} catch (PreconditionException e) {
			}
			rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(6)", 32);
			rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(11)", 20);
			
			try {
				rawEvaluateRealExpression(sourceStream, "special.getSpecialFee(1)", 91);
				fail();
			} catch (PreconditionException e) {
			}
			try {
				rawEvaluateRealExpression(sourceStream, "special.getSpecialFee(6)", 96);
				fail();
			} catch (PreconditionException e) {
			}
			rawEvaluateRealExpression(sourceStream, "special.getSpecialFee(11)", 101);
			
			try {
				rawEvaluateRealExpression(sourceStream, "other.getSpecialFee(1)", 91);
				fail();
			} catch (PreconditionException e) {
			}
			try {
				rawEvaluateRealExpression(sourceStream, "other.getSpecialFee(6)", 96);
				fail();
			} catch (StrongerPreconditionException e) {
			} catch (PreconditionException e) {
				fail();
			}
			rawEvaluateRealExpression(sourceStream, "other.getSpecialFee(11)", 101);

		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}
	
}
