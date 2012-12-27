/*
 * Created on 27/04/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.compiler;

import java.io.PrintWriter;
import java.util.List;

import ocl20.common.CoreClassifier;

import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PostconditionException;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PreconditionException;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.Action;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.TestActions;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.oclScript.CSTStatementCS;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTrackerImpl;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestOperationCallActions extends TestActions {

	protected	PSWOclScriptCompiler oclScriptCompiler;
	
	public TestOperationCallActions() {
		super();
		// TODO Auto-generated constructor stub
	}
	
	public void		setUp() throws Exception {
		modelRepository = null;
		super.setUp();
		oclScriptCompiler = new PSWOclScriptCompiler(environment, new ConstraintSourceTrackerImpl());
	}
	
	protected	void	compileOclDefinitions() throws Exception {
		String definitions1 =
				"context	Film \r\n" +
				"def: getValueAbc(x : Integer) : Integer = 100 * x \r\n" +
				"context   Tape \r\n" +
				"def: getFilmXpto() : Film = self.theFilm \r\n" +
				"context	Film \r\n" +
				"def: y : Integer = getValueAbc(10) \r\n";
		
		String definitions2 =
				"context SpecialFilm::computeFee() : Integer \r\n " +
				" actionBody:  \r\n" +
				" begin \r\n" +
				" var x: Integer; \r\n" +
				"  x := getValueAbc(10); \r\n" +
				" return x + 500 + 10; \r\n" +
				" end"
		;
		
		String definitions3 = 
			"context Film::setRentalFee(value : Integer) \r\n " +
			" body:  \r\n" +
			"    self.rentalFee";

		String definitions4 = 
			"context Film::getRentalFee(dayOfWeek : Integer) : Real \r\n " +
			" actionBody:  \r\n" +
			"    self.rentalFee := 10";

		String definitions5 = 
			"context Film::getRentalFee(dayOfWeek : Integer) : Real \r\n " +
			" body:  \r\n" +
			"    10 * dayOfWeek";

		
//		PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
		List result = oclScriptCompiler.compileOclStream(definitions1, "ocldefinitions 1", new PrintWriter(System.out));
		assertNotNull(result);
		assertTrue(result.size() > 0);

		List result3 = oclScriptCompiler.compileOclStream(definitions3, "ocldefinitions 3", new PrintWriter(System.out));
		assertNull(result3);
		List result4 = oclScriptCompiler.compileOclStream(definitions4, "ocldefinitions 4", new PrintWriter(System.out));
		assertNull(result4);

		List result5 = oclScriptCompiler.compileOclStream(definitions5, "ocldefinitions 5", new PrintWriter(System.out));
		assertNotNull(result5);
		
		result = oclScriptCompiler.compileOclStream(definitions2, "ocldefinitions 2", new PrintWriter(System.out));
		assertNotNull(result);
		assertTrue(result.size() > 0);

	}
	
	public	void	testOpCall01() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var y : Real = 200; \r\n" +
			"var z : Real; \r\n" +
			"aFilm := create SpecialFilm; \r\n" +
			"aFilm.name := 'Title of the film'; \r\n" +
			"z := aFilm.computeFee().floor().max(100) + 30; \r\n" +
			"aFilm.lateReturnFee := y + z \r\n" +
		    "end \r\n";

		compileOclDefinitions();

		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testOpCall01", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action block = statementNode.getAction();
		assertNotNull(block);
		
		oclScriptVM.executeAction(block, objectSpace, evalEnv);
		
		CoreClassifier c = getClassifier("SpecialFilm");
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		List films = objectSpace.getObjectsOfClass(c);
		OclObjectValue aFilm = (OclObjectValue) films.get(0);
		assertEquals("Title of the film", ((OclStringValue) aFilm.getValueOf("name")).stringValue());
		assertEquals(1740, ((OclRealValue) aFilm.getValueOf("lateReturnFee")).doubleValue().doubleValue(), 0.0001);
	}

	public	void	compilePrePostConditions() throws Exception{
		String definitions1 =
			"context	Film \r\n" +
			"def: getValueAbc(x : Integer) : Integer = 100 * x \r\n" +
			"context  Film::getValueAbc(x : Integer) : Integer \r\n" +
			"pre: x >=5 and x <= 20\r\n" +
			"post: result <= 1000 \r\n" +
			"context   Tape \r\n" +
			"def: getFilmXpto() : Film = self.theFilm \r\n" +
			"context	Film \r\n" +
			"def: y : Integer = getValueAbc(10) \r\n";
	
	String definitions2 =
			"context SpecialFilm::computeFee() : Integer \r\n " +
			" actionBody:  \r\n" +
			" begin \r\n" +
			" var x: Integer; \r\n" +
			" var d: Distributor; \r\n" +
			"  x := getValueAbc(10); \r\n" +
			" d := create Distributor;  \r\n" +
			" self.dist := self.dist->including(d); \r\n" +
			" return x + 500 + 10; \r\n" +
			" end  \r\n" +
			"pre: rentalFee >= 10 \r\n" +
			"post: result = 1000 + 510 \r\n" +
			"post: self.dist->exists(d | d.oclIsNew()  and self.dist - self.dist@pre = Set{d}) \r\n";
	;
	
	
		List result = oclScriptCompiler.compileOclStream(definitions1, "ocldefinitions 1", new PrintWriter(System.out));
		assertNotNull(result);

		result = oclScriptCompiler.compileOclStream(definitions2, "ocldefinitions 2", new PrintWriter(System.out));
		assertNotNull(result);
	}	
	
	public void		testPrePostConditions_01() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var y : Real = 200; \r\n" +
			"var z : Integer; \r\n" +
			"aFilm := create SpecialFilm; \r\n" +
			"aFilm.rentalFee := aFilm.getValueAbc(20); \r\n" +
		    "end \r\n";

		compilePrePostConditions();

		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testPrePostConditions_01", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action block = statementNode.getAction();
		assertNotNull(block);
		
		try {
			oclScriptVM.executeAction(block, objectSpace, evalEnv);
			fail();
		} catch (PostconditionException e) {
			assertEquals(1, e.getPostconditionsFailed().size());
		}
		
	}

	public void		testPrePostConditions_02() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var y : Real = 200; \r\n" +
			"var z : Integer; \r\n" +
			"aFilm := create SpecialFilm; \r\n" +
			"aFilm.rentalFee := aFilm.getValueAbc(21); \r\n" +
		    "end \r\n";

		compilePrePostConditions();

		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testPrePostConditions_02", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action block = statementNode.getAction();
		assertNotNull(block);

		try {
			oclScriptVM.executeAction(block, objectSpace, evalEnv);
			fail();
		} catch (PreconditionException e) {
			assertEquals(1, e.getPreconditionsFailed().size());
		}
	}

	public void		testPrePostConditions_03() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var y : Real = 200; \r\n" +
			"var z : Real; \r\n" +
			"aFilm := create SpecialFilm; \r\n" +
			"aFilm.rentalFee := 5; \r\n" +
			"aFilm.rentalFee := aFilm.computeFee().floor().oclAsType(Integer); \r\n" +
		    "end \r\n";

		compilePrePostConditions();

		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testPrePostConditions_03", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action block = statementNode.getAction();
		assertNotNull(block);

		try {
			oclScriptVM.executeAction(block, objectSpace, evalEnv);
			fail();
		} catch (PreconditionException e) {
			assertEquals(1, e.getPreconditionsFailed().size());
		}
	}

	
	public void		testPrePostConditions_04() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var y : Real = 200; \r\n" +
			"var z : Real; \r\n" +
			"aFilm := create SpecialFilm; \r\n" +
			"aFilm.name := 'Title of the film'; \r\n" +
			"aFilm.rentalFee := 10; \r\n" +
			"z := aFilm.computeFee().floor().max(100) + 30; \r\n" +
			"aFilm.lateReturnFee := y + z \r\n" +
		    "end \r\n";

		compilePrePostConditions();

		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testPrePostConditions_04", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action block = statementNode.getAction();
		assertNotNull(block);
		
		oclScriptVM.executeAction(block, objectSpace, evalEnv);
		
		CoreClassifier c = getClassifier("SpecialFilm");
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		
		CoreClassifier d = getClassifier("Distributor");
		assertNotNull(objectSpace.getObjectsOfClass(d));
		assertEquals(1, objectSpace.getObjectsOfClass(d).size());

		
		List films = objectSpace.getObjectsOfClass(c);
		OclObjectValue aFilm = (OclObjectValue) films.get(0);
		assertEquals("Title of the film", ((OclStringValue) aFilm.getValueOf("name")).stringValue());
		assertEquals(1740, ((OclRealValue) aFilm.getValueOf("lateReturnFee")).doubleValue().doubleValue(), 0.0001);
	}

	public	void	compilePrePostConditions2() throws Exception{
		String definitions1 =
			"context	Film \r\n" +
			"def: getValueAbc(x : Integer) : Integer = 100 * x \r\n" +
			"context  Film::getValueAbc(x : Integer) : Integer \r\n" +
			"pre: x >=5 and x <= 20\r\n" +
			"post: result <= 1000 \r\n" +
			
			"context	Film \r\n" +
			"def: getValueBde(x : Integer) : Integer = 100 + rentalFee * x \r\n";

		
		String definitions2 =
			"context SpecialFilm::computeFee() : Integer \r\n " +
			" actionBody:  \r\n" +
			" begin \r\n" +
			" var x: Integer = 1000; \r\n" +
			" self.rentalFee := 40; \r\n" + 
			" return x + 500 + 10; \r\n" +
			" end  \r\n" +
			"pre: rentalFee >= 10 \r\n" +
			"post: result = 1000 + 510 \r\n" +
			"post:  self.getValueBde(1) - self.getValueBde@pre(1) = 30 \r\n";
		;
	
		List result = oclScriptCompiler.compileOclStream(definitions1, "ocldefinitions 1", new PrintWriter(System.out));
		assertNotNull(result);

		result = oclScriptCompiler.compileOclStream(definitions2, "ocldefinitions 2", new PrintWriter(System.out));
		assertNotNull(result);
	}	

	public void		testPrePostConditions_05() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var y : Real = 200; \r\n" +
			"var z : Real; \r\n" +
			"aFilm := create SpecialFilm; \r\n" +
			"aFilm.name := 'Title of the film'; \r\n" +
			"aFilm.rentalFee := 10; \r\n" +
			"z := aFilm.computeFee().floor().max(100) + 30; \r\n" +
			"aFilm.lateReturnFee := y + z \r\n" +
		    "end \r\n";

		compilePrePostConditions2();

		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testPrePostConditions_04", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action block = statementNode.getAction();
		assertNotNull(block);
		
		oclScriptVM.executeAction(block, objectSpace, evalEnv);
		
		CoreClassifier c = getClassifier("SpecialFilm");
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		
		List films = objectSpace.getObjectsOfClass(c);
		OclObjectValue aFilm = (OclObjectValue) films.get(0);
		assertEquals("Title of the film", ((OclStringValue) aFilm.getValueOf("name")).stringValue());
		assertEquals(1740, ((OclRealValue) aFilm.getValueOf("lateReturnFee")).doubleValue().doubleValue(), 0.0001);
	}


}
