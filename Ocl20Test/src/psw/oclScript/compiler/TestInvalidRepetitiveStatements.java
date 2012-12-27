/*
 * Created on 25/04/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.compiler;

import java.io.PrintWriter;

import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.TestActions;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.oclScript.CSTStatementCS;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTrackerImpl;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestInvalidRepetitiveStatements extends TestActions {

	protected	PSWOclScriptCompiler oclScriptCompiler;
	
	public TestInvalidRepetitiveStatements() {
		super();
	}
	
	public void		setUp() throws Exception {
		super.setUp();
		oclScriptCompiler = new PSWOclScriptCompiler(environment, new ConstraintSourceTrackerImpl());
	}

	public	void	testInvalidWhile() throws Exception {
		String	statement[]  = { 
				"while true \r\n " +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",
				
				"while true do do \r\n " +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",

				"while do \r\n " +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",

				"while true do \r\n "  
				   	,

				"while 1 + 10 do \r\n " +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end"
				
		};
		
		for (int i = 0; i < statement.length; i++) {
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidWhile-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}
	
	public	void	testInvalidRepeat() throws Exception {
		String	statement[]  = { 
				"repeat  \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end" +
				"until; ",
				
				"repeat  \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",

				"repeat \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end \r\n" +
				"until 10 + 10; \r\n",

				"repeat  \r\n" +  
				"until true; "
				
		};
		
		for (int i = 0; i < statement.length; i++) {
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidRepeat-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

	public	void	testInvalidFor() throws Exception {
		String	statement[]  = { 
				"for i : Integer = 1 to 10 do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",

				"for \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",
				
				"for i := 1 to true do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",

				"for i := true to 10 do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",

				"for i = 1 to 10 do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",

				"for i := 1 to 10 step 'Alex' do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",

				"for i := 1 to 10 step 2' \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + 1; \r\n" +
				"end",
				
		};
		
		for (int i = 0; i < statement.length; i++) {
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidFor-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

	public	void	testInvalidForEach() throws Exception {
		String	statement[]  = { 
				"foreach  i : Integer in Set {'alex', 'john'} do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + i; \r\n" +
				"end",

				"foreach  i : Integer in 10 do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + i; \r\n" +
				"end",

				"begin \r\n " +
				"var y : Film; \r\n" +
				"foreach  i : Integer in y do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + i; \r\n" +
				"end \r\n" +
				"end ",

				"foreach  i in Set {10, 20} do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + i; \r\n" +
				"end",

				"foreach  i : Integer Set {10, 20} do \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + i; \r\n" +
				"end",

				"foreach  i : Integer in Set {10, 20} \r\n" +
				"begin \r\n" +
				"   var x : Integer; \r\n" +
				"   x := x + i; \r\n" +
				"end",
		};
		
		for (int i = 0; i < statement.length; i++) {
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidForEach-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

	
	public	void	testInvalidIf() throws Exception {
		String	statement[]  = { 
				"begin \r\n" +
				"var x : Integer; \r\n" +
				"doif 10 then x := 20; \r\n" +
				"end",

				"begin \r\n" +
				"var x : Integer; \r\n" +
				"doif then x := 20; else x := 30;" +
				"end",

				"begin \r\n" +
				"var x : Integer; \r\n" +
				"doif x > 20 x := 20; \r\n" +
				"end",
				
				"begin \r\n" +
				"var x : Integer; \r\n" +
				"doif Set{1,2,3} then x := 20; \r\n" +
				"end",

		};
		
		for (int i = 0; i < statement.length; i++) {
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement[i], "testInvalidIf-" + i, new PrintWriter(System.out));
			assertNull(statementNode);
		}
	}

}
