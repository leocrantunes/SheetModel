/*
 * Created on 20/04/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.compiler;

import java.io.PrintWriter;
import java.util.List;

import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;

import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.Action;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.TestActions;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.oclScript.CSTCreateObjectStatementCS;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.oclScript.CSTStatementCS;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
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
public class TestPrimitiveStatements extends TestActions {

	protected	PSWOclScriptCompiler oclScriptCompiler;
	
	public TestPrimitiveStatements() {
		super();
		// TODO Auto-generated constructor stub
	}
	
	public void		setUp() throws Exception {
		super.setUp();
		oclScriptCompiler = new PSWOclScriptCompiler(environment, new ConstraintSourceTrackerImpl());
	}

	public	void	testCreateFilm() throws Exception {
		String	statement = "create Film;";
		
		CSTCreateObjectStatementCS statementNode = (CSTCreateObjectStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testCreateFilm", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action createAction = statementNode.getAction();
		assertNotNull(createAction);
		
		oclScriptVM.executeAction(createAction, objectSpace, evalEnv);
		
		CoreClassifier c = getClassifier("Film");
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
	}
	
	public	void	testModifyAttribute() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : Film; \r\n" +
			"var i : Integer = 100; \r\n" + 
			"const j : Integer = 200; \r\n" +
			"var k : Integer = i + j; \r\n"+
			"aFilm := create Film; \r\n" +
			"aFilm.name := 'Title of the film'; \r\n" +
			"aFilm.rentalFee := k + 10 \r\n" +
		    "end \r\n";
		
		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testModifyAttribute", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action block = statementNode.getAction();
		assertNotNull(block);
		
		oclScriptVM.executeAction(block, objectSpace, evalEnv);
		
		CoreClassifier c = getClassifier("Film");
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		List films = objectSpace.getObjectsOfClass(c);
		OclObjectValue aFilm = (OclObjectValue) films.get(0);
		assertEquals("Title of the film", ((OclStringValue) aFilm.getValueOf("name")).stringValue());
		assertEquals(310, ((OclIntegerValue) aFilm.getValueOf("rentalFee")).intValue());
	}
	

	public	void	testModifyClassAttribute() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm1 : Film; \r\n" +
			"var aFilm2 : Film; \r\n" +
			"aFilm1 := create Film; \r\n" +
			"aFilm2 := create Film; \r\n" +
			"aFilm1.days := 30; \r\n" +
			"aFilm1.name := 'Title of the film'; \r\n" +
			"aFilm2.days := 50; \r\n" +
			"aFilm2.name := 'Title of the film'; \r\n" +
			"aFilm2.rentalFee := Film::days + 10; \r\n" +
			"aFilm1.rentalFee := aFilm1.days + 10 \r\n" +
		    "end \r\n";
		
		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testModifyClassAttribute", new PrintWriter(System.out));
		assertNotNull(statementNode);
		Action block = statementNode.getAction();
		assertNotNull(block);
		
		oclScriptVM.executeAction(block, objectSpace, evalEnv);
		
		CoreClassifier c = getClassifier("Film");
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(2, objectSpace.getObjectsOfClass(c).size());
		List films = objectSpace.getObjectsOfClass(c);
		OclObjectValue aFilm1 = (OclObjectValue) films.get(0);
		OclObjectValue aFilm2 = (OclObjectValue) films.get(1);
		assertEquals(60, ((OclIntegerValue) aFilm1.getValueOf("rentalFee")).intValue());
		assertEquals(60, ((OclIntegerValue) aFilm2.getValueOf("rentalFee")).intValue());
	}

	public	void	testLinks_01() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var distributors : Set(Distributor) = Set{}; \r\n" +
			"var aDistributor : Distributor; \r\n" +
			"var i : Integer = 100; \r\n" + 
			"const j : Integer = 200; \r\n" +
			"var k : Integer = i + j; \r\n"+
			"aFilm := create SpecialFilm; \r\n" +
			"aFilm.name := 'Title of the film'; \r\n" +
			"aFilm.rentalFee := k + 10; \r\n" +
			"aFilm.lateReturnFee := 150.25; \r\n" +
			"aDistributor := create Distributor; \r\n" +
			"distributors := Set{}->including(aDistributor); \r\n" +
			"aDistributor := create Distributor; \r\n" +
			"distributors := distributors->including(aDistributor); \r\n" +
			"aFilm.dist := distributors \r\n" +
		    "end \r\n";
		
		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testLinks", new PrintWriter(System.out));
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
		assertEquals(310, ((OclIntegerValue) aFilm.getValueOf("rentalFee")).intValue());
		
		CoreAssociationEnd assocEnd = c.lookupAssociationEnd("dist");
		List dist = aFilm.getValueOf(assocEnd);
		assertEquals(2, dist.size());
	}

	public	void	testLinks_02() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var distributors : Set(Distributor) = Set{}; \r\n" +
			"var aDistributor : Distributor; \r\n" +
			"var aTape : Tape; \r\n" +
			"var anotherTape : Tape; \r\n" +
			"var reserv01 : Reservation; \r\n" +
			"var reserv02 : Reservation; \r\n" +
			"var i : Integer = 100; \r\n" + 
			"const j : Integer = 200; \r\n" +
			"var k : Integer = i + j; \r\n"+
			"aFilm := create SpecialFilm; \r\n" +
			"aFilm.name := 'Title of the film'; \r\n" +
			"aFilm.rentalFee := k + 10; \r\n" +
			"aFilm.lateReturnFee := 150.25; \r\n" +
			"aDistributor := create Distributor; \r\n" +
			"distributors := Set{}->including(aDistributor); \r\n" +
			"aDistributor := create Distributor; \r\n" +
			"distributors := distributors->including(aDistributor); \r\n" +
			"aFilm.dist := distributors; \r\n" +
			"aDistributor := create Distributor; \r\n" +
			"aFilm.dist := Set{}->including(aDistributor); \r\n " +
			"aTape := create Tape; \r\n" +
			"aTape.number := 10; \r\n" +
			"aTape.theFilm := aFilm; \r\n" +
			"anotherTape := create Tape; \r\n" +
			"anotherTape.number := 30; \r\n" +
			"anotherTape.theFilm := aFilm; \r\n" +
			"aFilm.rentalFee := aTape.number + anotherTape.number; \r\n" +
			"reserv01 := create Reservation; \r\n" +
			"reserv02 := create Reservation; \r\n" +
			"reserv01.Film := aFilm; \r\n" +
			"reserv02.Film := aFilm; \r\n" +
			"aFilm.rentalFee := aFilm.rentalFee + aFilm.Reservation->size(); \r\n" +
			"delete aDistributor; \r\n " +
		    "end \r\n";
		
		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testLinks", new PrintWriter(System.out));
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
		assertEquals(42, ((OclIntegerValue) aFilm.getValueOf("rentalFee")).intValue());

		CoreClassifier distributor = getClassifier("Distributor");
		assertNotNull(objectSpace.getObjectsOfClass(distributor));
		assertEquals(2, objectSpace.getObjectsOfClass(distributor).size());

		CoreAssociationEnd assocEnd = c.lookupAssociationEnd("dist");
		List dist = aFilm.getValueOf(assocEnd);
		assertEquals(0, dist.size());
		
		CoreAssociationEnd assocEndReservation = c.lookupAssociationEnd("Reservation");
		List reserv = aFilm.getValueOf(assocEndReservation);
		assertEquals(2, reserv.size());
	}
	public	void	testNewObjectWithInitialization() throws Exception {
		String	statement =
			"begin \r\n" +
			"var aFilm : SpecialFilm; \r\n" +
			"var distributors : Set(Distributor) = Set{}; \r\n" +
			"var aDistributor : Distributor; \r\n" +
			"var i : Integer = 100; \r\n" + 
			"const j : Integer = 200; \r\n" +
			"var k : Integer = i + j; \r\n"+
			"aDistributor := create Distributor; \r\n" +
			"distributors := Set{}->including(aDistributor); \r\n" +
			"aDistributor := create Distributor; \r\n" +
			"distributors := distributors->including(aDistributor); \r\n" +
			"aFilm := create SpecialFilm(name := 'Title of the film', rentalFee := k + 10, lateReturnFee := 150.25, dist:= distributors); \r\n" +
		    "end \r\n";
		
		CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testLinks", new PrintWriter(System.out));
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
		assertEquals(310, ((OclIntegerValue) aFilm.getValueOf("rentalFee")).intValue());
		assertEquals(150.25, ((OclRealValue) aFilm.getValueOf("lateReturnFee")).doubleValue().doubleValue(), 0.00001);
		
		CoreAssociationEnd assocEnd = c.lookupAssociationEnd("dist");
		List dist = aFilm.getValueOf(assocEnd);
		assertEquals(2, dist.size());
	}
}
