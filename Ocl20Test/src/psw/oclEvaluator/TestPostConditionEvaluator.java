/*
 * Created on 30/04/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator;

import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.constraints.OclPostConstraint;
import ocl20.constraints.OclPrePostConstraint;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.IObjectSpace;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PSWObjectSpace;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PSWOclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PostconditionException;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.WeakerPostconditionException;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.OCLSemanticException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.OCLSemanticAnalyzerVisitor;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestPostConditionEvaluator extends TestObjectEval {

	/**
	 * 
	 */
	public TestPostConditionEvaluator() {
		super();
		// TODO Auto-generated constructor stub
	}

	public void		setUp() throws Exception {
		modelRepository = null;
		super.setUp();
	}

	
	public	void	testCloneObjectSpace() {
		IObjectSpace beforeSnapshot = new PSWObjectSpace();
		
			OclObjectValue	obj = createInstanceOf(beforeSnapshot, "SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));

			OclObjectValue	film3 = createInstanceOf(beforeSnapshot, "SpecialFilm");
			film3.setValueOf("name", new OclStringValue("Lord of the Rings"));
			film3.setValueOf("days1", new OclIntegerValue(50));
			film3.setValueOf("code", new OclStringValue("4567"));
			film3.setValueOf("rentalFee", new OclIntegerValue(40));

			OclObjectValue	dist01 = createInstanceOf(beforeSnapshot, "Distributor");
			OclObjectValue	dist02 = createInstanceOf(beforeSnapshot, "Distributor");

			IObjectSpace  afterSnapshot = beforeSnapshot.clone();

			CoreClassifier specialFilmClass = (CoreClassifier) environment.lookup("SpecialFilm");
			CoreAssociationEnd distRole = specialFilmClass.lookupAssociationEnd("dist");
			assertNotNull(distRole);
			
			CoreAssociation association = distRole.getTheAssociation();
			
			CoreAssociationEnd	filmRole = association.getAssociationEnd("films");
			assertNotNull(filmRole);

			beforeSnapshot.createLink(association, 
					new ArrayList(Arrays.asList(new Object[] { obj, dist01} )), 
					new ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));
			
			beforeSnapshot.createLink(association, 
					new ArrayList(Arrays.asList(new Object[] { obj, dist02} )), 
					new ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

			beforeSnapshot.createLink(association, 
					new ArrayList(Arrays.asList(new Object[] { film3, dist02} )), 
					new ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

			afterSnapshot = beforeSnapshot.clone();

			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		

			objectSpace = beforeSnapshot;
//			evaluateIntegerExpression("testClone", "self.dist->size()", 2);
			evaluateIntegerExpression("testClone", "self.days1", 10);
			
			OclObjectValue	newObject = afterSnapshot.getObjectForId(obj.getGUID());
			OclObjectValue	newFilm3 = afterSnapshot.getObjectForId(film3.getGUID());
			
			assertSame(afterSnapshot, ((PSWOclObjectValue) newObject).getObjSpace()); 
			assertNotSame(beforeSnapshot, ((PSWOclObjectValue) newObject).getObjSpace());
			assertSame(beforeSnapshot, ((PSWOclObjectValue) obj).getObjSpace());
			
			afterSnapshot.deleteObject(newFilm3);
			
			evalEnv.add("self", newObject);
			objectSpace = afterSnapshot;
			evaluateIntegerExpression("testClone", "self.dist->size()", 2);
			evaluateIntegerExpression("testClone", "self.days1", 10);

			OclObjectValue	spFilm2 = createInstanceOf(afterSnapshot, "SpecialFilm");
			spFilm2.setValueOf("name", new OclStringValue("Other"));
			spFilm2.setValueOf("days1", new OclIntegerValue(700));
			spFilm2.setValueOf("code", new OclStringValue("6789"));
			spFilm2.setValueOf("rentalFee", new OclIntegerValue(40));

			OclObjectValue	dist01after = afterSnapshot.getObjectForId(dist01.getGUID());
			OclObjectValue	dist02after = afterSnapshot.getObjectForId(dist02.getGUID());
			
			afterSnapshot.createLink(association, 
					new ArrayList(Arrays.asList(new Object[] { spFilm2, dist01after} )), 
					new ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

			afterSnapshot.deleteLink(association, 
					new ArrayList(Arrays.asList(new Object[] { newObject, dist01after} ))); 
			
			newObject.setValueOf("days1", new OclIntegerValue(90));
			OclObjectValue	dist03 = createInstanceOf(beforeSnapshot, "Distributor");

			afterSnapshot.createLink(association, 
					new ArrayList(Arrays.asList(new Object[] { newObject, dist03} )), 
					new ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

			evalEnv.add("self", obj);
			objectSpace = beforeSnapshot;
			evaluateIntegerExpression("testClone", "self.dist->size()", 2);
			evaluateIntegerExpression("testClone", "self.days1", 10);

			insertObjectInEnvironment("other", "SpecialFilm");
			insertObjectInEnvironment("dist01", "Distributor");
			insertObjectInEnvironment("dist02", "Distributor");
			evalEnv.add("self", newObject);
			evalEnv.add("other", spFilm2);
			evalEnv.add("dist01", dist01after);
			evalEnv.add("dist02", dist02after);
			objectSpace = afterSnapshot;
			evaluateIntegerExpression("testClone", "self.dist->size()", 2);
			evaluateIntegerExpression("testClone", "self.days1", 90);
			evaluateIntegerExpression("testClone", "other.dist->size()", 1);
			
			OCLSemanticAnalyzerVisitor.setCompilingForTwoSnapshotsEvaluation(true);
			
//			OclValue result1 = evaluateExpression("testClone", "self.dist@pre", beforeSnapshot, afterSnapshot);
//			OclValue result2 = evaluateExpression("testClone", "self.dist@pre.films", beforeSnapshot, afterSnapshot);
//			OclValue result3 = evaluateExpression("testClone", "self.dist@pre.films.days@pre", beforeSnapshot, afterSnapshot);
			
			assertTrue(getInvalid().oclIsUndefined());
			
			evaluateIntegerExpression("testClone", "self.days1@pre", 10, beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "self.days1@pre + self.days1", 100, beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "self.dist@pre->size()", 2, beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "self.dist@pre->size() + self.dist->size()", 4, beforeSnapshot, afterSnapshot);
			evaluateBooleanExpression("testClone", "other.oclIsNew()", true, beforeSnapshot, afterSnapshot);
			evaluateBooleanExpression("testClone", "self.oclIsNew()", false, beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "self.dist@pre.films.days1->sum() ", 790, beforeSnapshot, afterSnapshot);
			evaluateExpression("testClone", "self.dist@pre.films.days1@pre->sum() ", getInvalid(), beforeSnapshot, afterSnapshot);
			evaluateExpression("testClone", "self.dist@pre.films@pre.days1->sum() ", getInvalid(), beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "self.dist@pre.films@pre.days1@pre->sum() ", 70, beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "self.dist@pre->iterate(x  ; result : Integer = 0 | result + x.films@pre.days1@pre->sum()) ", 70, beforeSnapshot, afterSnapshot);
			evaluateExpression("testClone", "self.dist@pre->iterate(x  ; result : Integer = 0 | result + x.films.days1@pre->sum()) ", getInvalid(), beforeSnapshot, afterSnapshot);
			
			
			evaluateExpression("testClone", "dist01.films.days1@pre->size()", getInvalid(), beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "dist02.films@pre.days1@pre->sum()", 60, beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "dist02.films.days1@pre->sum()", 10, beforeSnapshot, afterSnapshot);
			evaluateIntegerExpression("testClone", "dist02.films.days1->sum()", 90, beforeSnapshot, afterSnapshot);
	}		
			
	
	public	void	testBody_01() {
		try {
			String	sourceStream = "testBody_01";
			
			String	body = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	body : dayOfWeek * self.rentalFee * 2.0";
			
			oclCompiler.compileOclStream(body, sourceStream, new PrintWriter(System.out));
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(10) + 50 + self.getRentalFee(2)", 530);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	
	public	void	testBody_02() {
		try {
			String	sourceStream = "testBody_02";
			
			String	body = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	\r\n" +
			"def : a : Real = dayOfWeek * 2.0 \r\n" +
			"def : b : Real = self.rentalFee \r\n" +
			"body : b * a";
			
			oclCompiler.compileOclStream(body, sourceStream, new PrintWriter(System.out));
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(10) + 50 + self.getRentalFee(2)", 530);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}

	
	
	public	void	testPostCondition_01() {
		try {
			String	sourceStream = "testPostCondition01";
			
			String	preCondition01 = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	" +
					"post post01: dayOfWeek >= 1 and dayOfWeek <= 10 " +
					"post post02: dayOfWeek >= 1 and dayOfWeek <= 8" ;
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
			Collection postConditions = prePost.getPostConditions();
			assertEquals(2, postConditions.size());
			Iterator iterPre = postConditions.iterator();
			OclPostConstraint post01 = (OclPostConstraint) iterPre.next();
			OclPostConstraint post02 = (OclPostConstraint) iterPre.next();
			assertEquals("post01", post01.getName());
			assertEquals("post02", post02.getName());
			
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			try {
				rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			} catch (PostconditionException e) {
				System.out.println("failed: " + e.getPostconditionsFailed().size());
				System.out.println("evaluated: " + e.getPostconditionsEvaluated().size());
				fail();
			}
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2) + 50 + self.getRentalFee(10)", 530);
				fail();
			} catch (PostconditionException e) {
				assertEquals(1, e.getPostconditionsFailed().size());
				OclPostConstraint constraint = (OclPostConstraint) e.getPostconditionsFailed().iterator().next();
				assertEquals("post02", constraint.getName());
				System.out.println(e.getMessage());
			}
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
				fail();
			} catch (PostconditionException e) {
				assertEquals(2, e.getPostconditionsEvaluated().size());
				assertEquals(2, e.getPostconditionsFailed().size());
				Iterator iter = e.getPostconditionsFailed().iterator();
				OclPostConstraint constraint = (OclPostConstraint) iter.next();
				assertEquals("post01", constraint.getName());
				constraint = (OclPostConstraint) iter.next();
				assertEquals("post02", constraint.getName());
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

	
	public	void	testPostCondition_03() {
		try {
			String	sourceStream = "testPostCondition03";
			
			String	preCondition01 = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	" +
					"post post01: dayOfWeek >= 1 and dayOfWeek <= 10 " +
					"post post02: result >= 1 and result <= 100" ;
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
			Collection postConditions = prePost.getPostConditions();
			assertEquals(2, postConditions.size());
			Iterator iterPre = postConditions.iterator();
			OclPostConstraint post01 = (OclPostConstraint) iterPre.next();
			OclPostConstraint post02 = (OclPostConstraint) iterPre.next();
			assertEquals("post01", post01.getName());
			assertEquals("post02", post02.getName());
			
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2) + self.getRentalFee(1)", 120);
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(10)", 400);
				fail();
			} catch (PostconditionException e) {
				assertEquals(1, e.getPostconditionsFailed().size());
				OclPostConstraint constraint = (OclPostConstraint) e.getPostconditionsFailed().iterator().next();
				assertEquals("post02", constraint.getName());
				System.out.println(e.getMessage());
			}
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
				fail();
			} catch (PostconditionException e) {
				assertEquals(2, e.getPostconditionsEvaluated().size());
				assertEquals(2, e.getPostconditionsFailed().size());
				Iterator iter = e.getPostconditionsFailed().iterator();
				OclPostConstraint constraint = (OclPostConstraint) iter.next();
				assertEquals("post01", constraint.getName());
				constraint = (OclPostConstraint) iter.next();
				assertEquals("post02", constraint.getName());
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

	
	public	void	testPostCondition_04() {
		try {
			String	sourceStream = "testPostCondition04";
			
			String	filmbody = "context Film \r\n" +
			"def: getXpto(dayOfWeek : Integer) : Real 	= dayOfWeek * self.rentalFee * 2.0";

			String	specialbody = "context SpecialFilm \r\n" +
			"def: getXpto(dayOfWeek : Integer) : Real 	= dayOfWeek * self.rentalFee * 5.0 \r\n" +
			"def: getA(dayOfWeek : Integer) : Real = getXpto(dayOfWeek) + 10 \r\n "+
			"def: getB(dayOfWeek : Integer) : Real = Film::getXpto(dayOfWeek) + 5";

			
			oclCompiler.compileOclStream(filmbody, sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream(specialbody, sourceStream + "-c", new PrintWriter(System.out));

			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		

			evalEnv.add("film2", obj);
			insertObjectInEnvironment("film2", "Film");		

			OclObjectValue	film = createInstanceOf("Film");
			film.setValueOf("name", new OclStringValue("AI"));
			film.setValueOf("code", new OclStringValue("1234"));
			film.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("film", film);
			insertObjectInEnvironment("film", "Film");		

			rawEvaluateRealExpression(sourceStream, "film.getXpto(1)", 40);
			rawEvaluateRealExpression(sourceStream, "self.oclAsType(Film).getXpto(1)", 100);
			rawEvaluateRealExpression(sourceStream,  "self.getXpto(1)", 100);
			rawEvaluateRealExpression(sourceStream,  "self.getA(2)", 210);
			rawEvaluateRealExpression(sourceStream,  "self.getB(2)", 85);
			rawEvaluateRealExpression(sourceStream,  "film2.getXpto(2)", 200);
			rawEvaluateRealExpression(sourceStream,  "film2.oclAsType(SpecialFilm).getXpto(2)", 200);
			rawEvaluateRealExpression(sourceStream,  "film2.oclAsType(SpecialFilm).getA(2)", 210);
			rawEvaluateRealExpression(sourceStream,  "film2.oclAsType(SpecialFilm).getB(2)", 85);
		} catch (OCLSemanticException e) {
			System.out.println(e);
			fail();
		}  catch (Exception e) {
			e.printStackTrace();
			fail();
		}
	}


	public	void	testPostCondition_05() {
		try {
			String	sourceStream = "testPostCondition05";
			
			String	preCondition01 = "context Film::getSpecialFee(day : Integer) : Real 	" +
					"post post01: day >= 1 and day <= 10 " +
					"post post02: result >= 1 and result <= 100" ;
			String	body = "context Film::getSpecialFee(day : Integer) : Real 	body : day * self.rentalFee * 2.0";
			String	preCondition02 = "context SpecialFilm::getSpecialFee(day : Integer) : Real 	" +
			"post postspecial01: day >= 1 and day <= 10 " +
			"post postspecial02: result >= 1 and result <= 500" ;
			String	body1 = "context SpecialFilm::getSpecialFee(day : Integer) : Real 	body : day * self.rentalFee * 3.0";
			oclCompiler.compileOclStream(preCondition01, sourceStream + "-b", new PrintWriter(System.out));
			oclCompiler.compileOclStream(body, sourceStream + "-c", new PrintWriter(System.out));
			oclCompiler.compileOclStream(preCondition02, sourceStream + "-d", new PrintWriter(System.out));
			oclCompiler.compileOclStream(body1, sourceStream + "-e", new PrintWriter(System.out));

			CoreClassifier filmClass = (CoreClassifier) environment.lookup("Film");
			assertNotNull(filmClass);
			List params = new ArrayList();
			params.add((CoreClassifier) environment.lookup("Integer"));
			CoreOperation oper = filmClass.lookupOperation("getSpecialFee", params);
			assertNotNull(oper);
			Collection specifications = oper.getSpecifications();
			assertEquals(1, specifications.size());
			OclPrePostConstraint prePost = (OclPrePostConstraint) specifications.iterator().next();
			Collection postConditions = prePost.getPostConditions();
			assertEquals(2, postConditions.size());
			Iterator iterPre = postConditions.iterator();
			OclPostConstraint post01 = (OclPostConstraint) iterPre.next();
			OclPostConstraint post02 = (OclPostConstraint) iterPre.next();
			assertEquals("post01", post01.getName());
			assertEquals("post02", post02.getName());
			
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		

			evalEnv.add("film", obj);
			insertObjectInEnvironment("film", "Film");		

			rawEvaluateRealExpression(sourceStream, "self.getSpecialFee(1)", 60);
			rawEvaluateRealExpression(sourceStream, "self.getSpecialFee(2)", 120);
			rawEvaluateRealExpression(sourceStream, "self.getSpecialFee(2) + self.getSpecialFee(1)", 180);
			rawEvaluateRealExpression(sourceStream, "self.getSpecialFee(5)", 300);
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getSpecialFee(10)", 600);
				fail();
			} catch (PostconditionException e) {
				assertEquals(1, e.getPostconditionsFailed().size());
				OclPostConstraint constraint = (OclPostConstraint) e.getPostconditionsFailed().iterator().next();
				assertEquals("postspecial02", constraint.getName());
				System.out.println(e.getMessage());
			}
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getSpecialFee(20) + 50 + self.getSpecialFee(2)", 930);
				fail();
			} catch (PostconditionException e) {
				assertEquals(2, e.getPostconditionsEvaluated().size());
				assertEquals(2, e.getPostconditionsFailed().size());
				Iterator iter = e.getPostconditionsFailed().iterator();
				OclPostConstraint constraint = (OclPostConstraint) iter.next();
				assertEquals("postspecial01", constraint.getName());
				constraint = (OclPostConstraint) iter.next();
				assertEquals("postspecial02", constraint.getName());
				System.out.println(e.getMessage());
			}
			
			
			rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(1)", 60);
			try {
				rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(5)", 300);
				fail();
			} catch (WeakerPostconditionException e) {
				assertEquals(1, e.getPostconditionsFailed().size());
				OclPostConstraint constraint = (OclPostConstraint) e.getPostconditionsFailed().iterator().next();
				assertEquals("post02", constraint.getName());
				System.out.println(e.getMessage());
			}
			try {
				rawEvaluateRealExpression(sourceStream, "film.getSpecialFee(40) + 50 + film.getSpecialFee(2)", 930);
				fail();
			} catch (PostconditionException e) {
				assertEquals(2, e.getPostconditionsEvaluated().size());
				assertEquals(2, e.getPostconditionsFailed().size());
				Iterator iter = e.getPostconditionsFailed().iterator();
				OclPostConstraint constraint = (OclPostConstraint) iter.next();
				assertEquals("postspecial01", constraint.getName());
				constraint = (OclPostConstraint) iter.next();
				assertEquals("postspecial02", constraint.getName());
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

	
	public	void	testPostCondition_06() {
		try {
			String	sourceStream = "testPostCondition06";
			
			String	preCondition01 = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	\r\n" +
					"def: a : Real = self.rentalFee \r\n" +
					"def: b : Real = dayOfWeek * 2.0 \r\n" +
					"def: c : Integer = dayOfWeek \r\n" +
					"def: d : Integer = Film::allInstances()->size() \r\n" +
					"pre pre01 : c >= 1 and c <= 30 \r\n" +
					"post post01: c >= 1 and c <= 10 \r\n" +
					"post post02: result >= 1 and result <= 100 \r\n" +
					"body : b * a \r\n";
//			String	body = "context Film::getRentalFee(dayOfWeek : Integer) : Real 	body : b * a \r\n";

			oclCompiler.compileOclStream(preCondition01, sourceStream + "-b", new PrintWriter(System.out));
//			oclCompiler.compileOclStream(body, sourceStream + "-c", new PrintWriter(System.out));

			CoreClassifier filmClass = (CoreClassifier) environment.lookup("Film");
			assertNotNull(filmClass);
			List params = new ArrayList();
			params.add((CoreClassifier) environment.lookup("Integer"));
			CoreOperation oper = filmClass.lookupOperation("getRentalFee", params);
			assertNotNull(oper);
			Collection specifications = oper.getSpecifications();
			assertEquals(1, specifications.size());
			OclPrePostConstraint prePost = (OclPrePostConstraint) specifications.iterator().next();
			Collection postConditions = prePost.getPostConditions();
			assertEquals(2, postConditions.size());
			Iterator iterPre = postConditions.iterator();
			OclPostConstraint post01 = (OclPostConstraint) iterPre.next();
			OclPostConstraint post02 = (OclPostConstraint) iterPre.next();
			assertEquals("post01", post01.getName());
			assertEquals("post02", post02.getName());
			
			
			OclObjectValue	obj = createInstanceOf("SpecialFilm");
			obj.setValueOf("name", new OclStringValue("AI"));
			obj.setValueOf("days1", new OclIntegerValue(10));
			obj.setValueOf("code", new OclStringValue("1234"));
			obj.setValueOf("rentalFee", new OclIntegerValue(20));
			
			evalEnv.add("self", obj);
			insertObjectInEnvironment("self", "SpecialFilm");		
			
			rawEvaluateRealExpression(sourceStream, "self.getRentalFee(1)", 40);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2)", 80);
			rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(2) + self.getRentalFee(1)", 120);
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(10)", 400);
				fail();
			} catch (PostconditionException e) {
				assertEquals(1, e.getPostconditionsFailed().size());
				OclPostConstraint constraint = (OclPostConstraint) e.getPostconditionsFailed().iterator().next();
				assertEquals("post02", constraint.getName());
				System.out.println(e.getMessage());
			}
			try {
				rawEvaluateRealExpression(sourceStream + "-a", "self.getRentalFee(20) + 50 + self.getRentalFee(2)", 930);
				fail();
			} catch (PostconditionException e) {
				assertEquals(2, e.getPostconditionsEvaluated().size());
				assertEquals(2, e.getPostconditionsFailed().size());
				Iterator iter = e.getPostconditionsFailed().iterator();
				OclPostConstraint constraint = (OclPostConstraint) iter.next();
				assertEquals("post01", constraint.getName());
				constraint = (OclPostConstraint) iter.next();
				assertEquals("post02", constraint.getName());
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

}
