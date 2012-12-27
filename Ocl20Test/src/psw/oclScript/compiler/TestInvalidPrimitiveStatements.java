/*
 * Created on 24/04/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.compiler;

import java.io.PrintWriter;

import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.TestActions;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.oclScript.CSTCreateObjectStatementCS;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTrackerImpl;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestInvalidPrimitiveStatements extends TestActions {

	protected	PSWOclScriptCompiler oclScriptCompiler;
	
	public TestInvalidPrimitiveStatements() {
		super();
		// TODO Auto-generated constructor stub
	}
	
	public void		setUp() throws Exception {
		super.setUp();
		oclScriptCompiler = new PSWOclScriptCompiler(environment, new ConstraintSourceTrackerImpl());
	}

	public	void	testInvalidCreateObject() throws Exception {
		String	statement[]  = { "create Integer;", 
											"create Set(Film);",
											"create Situation;",
											"create IProduct;",
											"create ;"
		};
		
		for (int i = 0; i < statement.length; i++) {
			CSTCreateObjectStatementCS statementNode = (CSTCreateObjectStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidCreateObject", new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

	public	void	testInvalidCreateObjectWithInitialization() throws Exception {
		String	statement[] = { 
				"create Film (name := , rentalFee := true);",
				"create Film (name := 10, rentalFee := true);",
				"create Film (name := 'alex', rentalFee := true);",
				"create Film (nameX := 'alex', rentalFeeY := true);",
				"create Film ( := 'alex', rentalFeeY := true);",
				"create Film ((nameX := 'alex', rentalFeeY := true);",
				"create Film (name := 'alex'; rentalFee := 10);"
		};

		for (int i = 0; i < statement.length; i++) {
			CSTCreateObjectStatementCS statementNode = (CSTCreateObjectStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidCreateObjectWithInitialization-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

	public	void	testInvalidVariableAssignement() throws Exception {
		String	statement[] = {
				"begin  \r\n" +
				"var x : Integer;  \r\n" +
				"  x := y; " +
				"end",
				
				"begin  \r\n" +
				"var x : Integer;  \r\n" +
				"  x := 'alexandre'; " +
				"end",
				
				"begin  \r\n" +
				"const x : Integer;  \r\n" +
				"  x := 20; " +
				"end",

				"begin  \r\n" +
				"const x : Integer = 40;  \r\n" +
				"  x := 50; " +
				"end",

				"begin  \r\n" +
				"var x : Set(Integer) = Set{1,2,3};  \r\n" +
				"  x := 10; " +
				"end",

				"begin  \r\n" +
				"var x : Set(Integer) = Set{1,2,3};  \r\n" +
				"  x := Sequence{2,3,4}; " +
				"end",

		};

		for (int i = 0; i < statement.length; i++) {
			CSTCreateObjectStatementCS statementNode = (CSTCreateObjectStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidVariableAssignment-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

	public	void	testInvalidAttributeAssignement() throws Exception {
		String	statement[] = {
				"begin  \r\n" +
				"var x : Film;  \r\n" +
				"  x.rentalFee := y; " +
				"end",
				
				"begin  \r\n" +
				"var x : Film;  \r\n" +
				"  x.abc := 'alexandre'; " +
				"end",
				
				"begin  \r\n" +
				"var x : Set(Film);  \r\n" +
				"  x.rentalFee := 20; " +
				"end",
				
				"begin  \r\n" +
				"var x : Film;  \r\n" +
				"  Tape::allInstances().theFilm.rentalFee := 10; \r\n" +
				"end",
		};

		for (int i = 0; i < statement.length; i++) {
			CSTCreateObjectStatementCS statementNode = (CSTCreateObjectStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidAttributeAssignment-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

	public	void	testInvalidLinkAssignement() throws Exception {
		String	statement[] = {
				"begin  \r\n" +
				"var x : Tape;  \r\n" +
				"  x.theFilm := 10; " +
				"end",
				
				"begin  \r\n" +
				"var x : Tape;  \r\n" +
				"  x.theFilm := Film::allInstances(); " +
				"end",
				
				"begin  \r\n" +
				"var x : SpecialFilm;  \r\n" +
				"  x.dist := Sequence{}; " +
				"end",

				"begin  \r\n" +
				"var x : SpecialFilm;  \r\n" +
				"  x.dist := Set{1,2,3}; " +
				"end"
		};

		for (int i = 0; i < statement.length; i++) {
			CSTCreateObjectStatementCS statementNode = (CSTCreateObjectStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidLinkAssignment-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

}
