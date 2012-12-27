/*
 * Created on 24/04/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.compiler;

import java.io.PrintWriter;

import ocl20.common.CoreClassifier;
import ocl20.evaluation.OclValue;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.Action;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.TestActions;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.oclScript.CSTStatementCS;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTrackerImpl;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestRepetitiveStatements 	extends TestActions {

		protected	PSWOclScriptCompiler oclScriptCompiler;
		
		public TestRepetitiveStatements() {
			super();
			// TODO Auto-generated constructor stub
		}
		
		public void		setUp() throws Exception {
			super.setUp();
			oclScriptCompiler = new PSWOclScriptCompiler(environment, new ConstraintSourceTrackerImpl());
		}

		public	void	testWhile() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var i : Integer = 0; \r\n" +
				"var aFilm : Film; \r\n" +
				"while i < 10 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := i; \r\n " +
				"   i := i + 1; \r\n" +
				"end \r\n" +
			    "end \r\n";
			
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testWhile", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(45, ((OclIntegerValue) sum).intValue());
		}

		public	void	testRepeat() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var i : Integer = 0; \r\n" +
				"var aFilm : Film; \r\n" +
				"repeat \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := i; \r\n " +
				"   i := i + 1; \r\n" +
				"end \r\n" +
				"until i = 10; \r\n" +
			    "end \r\n";
			
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testRepeat", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(45, ((OclIntegerValue) sum).intValue());
		}
		

		public	void	testFor_01() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var aFilm : Film; \r\n" +
				"for i := 0 to 9 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := i; \r\n " +
				"end \r\n" +
			    "end \r\n";
			
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testFor_01", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(45, ((OclIntegerValue) sum).intValue());
		}

		public	void	testFor_02() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var aFilm : Film; \r\n" +
				"for i := 9 downto 0 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := i; \r\n " +
				"end \r\n" +
			    "end \r\n";
			
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testFor_02", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(45, ((OclIntegerValue) sum).intValue());
		}

		public	void	testFor_03() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var aFilm : Film; \r\n" +
				"var j : Integer = 0; \r\n" +
				"for i := 0 to 99 step 10 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := i; \r\n " +
				"end \r\n" +
			    "end \r\n";
			
			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testFor_03", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(450, ((OclIntegerValue) sum).intValue());
		}

		public	void	testForEach() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var aFilm : Film; \r\n" +
				"for i := 0 to 9 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := i; \r\n" +
				"end; \r\n" +
				"foreach f : Film in Film::allInstances() do \r\n" +
				"begin \r\n" +
				"    f.rentalFee := f.rentalFee * 10; \r\n" +
				"end \r\n" +
			    "end \r\n";


			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testForEach", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(450, ((OclIntegerValue) sum).intValue());
		}

		
		public	void	testIf() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var aFilm : Film; \r\n" +
				"for i := 0 to 9 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   doif i <= 4 then \r\n" +
				"   	aFilm.rentalFee := i * 10 \r\n" +
				"   else \r\n" +
				"       aFilm.rentalFee := i * 20;;; \r\n" +
				"   aFilm.name := 'test'; \r\n" +
				"end \r\n" +
			    "end \r\n";


			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testIf", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(800, ((OclIntegerValue) sum).intValue());
		}

		public	void	testIf_02() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var aFilm : Film; \r\n" +
				"for i := 0 to 9 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := 0; \r\n" +
				"   doif i <= 4 then \r\n" +
				"   begin \r\n" +
				"   	aFilm.rentalFee := i * 10 \r\n" +
				"   end; \r\n" +
				"   aFilm.name := 'test'; \r\n" +
				"end \r\n" +
			    "end \r\n";


			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testIf_02", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(100, ((OclIntegerValue) sum).intValue());
		}

		public	void	testBreak() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var aFilm : Film; \r\n" +
				"for i := 0 to 9 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := 0; \r\n" +
				"   doif i >= 5 then \r\n" +
				"   	break; \r\n" +
				"   aFilm.name := 'test'; \r\n" +
				"end \r\n" +
			    "end \r\n";


			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testBreak", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(6, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(0, ((OclIntegerValue) sum).intValue());
		}

		public	void	testContinue() throws Exception {
			String	statement = 
				"begin \r\n" +
				"var aFilm : Film; \r\n" +
				"for i := 0 to 9 do \r\n" +
				"begin \r\n" +
				"   aFilm := create Film;  \r\n" +
				"   aFilm.rentalFee := 0; \r\n" +
				"   doif i >= 5 then \r\n" +
				"   	continue; \r\n" +
				" 	aFilm.rentalFee := i * 10; \r\n" +
				"   aFilm.name := 'test'; \r\n" +
				"end \r\n" +
			    "end \r\n";


			CSTStatementCS statementNode = (CSTStatementCS) oclScriptCompiler.compileOclScriptStatement(statement, "testContinue", new PrintWriter(System.out));
			assertNotNull(statementNode);
			Action action = statementNode.getAction();
			assertNotNull(action);
			
			oclScriptVM.executeAction(action, objectSpace, evalEnv);
			
			CoreClassifier c = getClassifier("Film");
			assertNotNull(objectSpace.getObjectsOfClass(c));
			assertEquals(10, objectSpace.getObjectsOfClass(c).size());
			
			OclValue sum = evaluateExpression("Film::allInstances().rentalFee->sum()");
			assertEquals(100, ((OclIntegerValue) sum).intValue());
		}

}
