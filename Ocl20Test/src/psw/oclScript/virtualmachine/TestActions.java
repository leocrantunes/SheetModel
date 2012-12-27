/*
 * Created on Apr 15, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine;

import java.io.PrintWriter;

import ocl20.common.CoreClassifier;
import ocl20.environment.Environment;
import ocl20.evaluation.OclValue;
import ocl20.expressions.OclExpression;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.IObjectSpace;
import br.ufrj.cos.lens.odyssey.tools.psw.objectSpace.PSWObjectSpace;
import br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator.EvalEnvironment;
import br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator.IEvalEnvironment;
import br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator.TestEval;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.OclScriptVirtualMachine;
import br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.context.CSTExpressionInOclCS;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.ConstraintSourceTrackerImpl;
import br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.PSWOclCompiler;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestActions extends TestEval {

	protected	IObjectSpace  objectSpace;
	protected	IEvalEnvironment		evalEnv;
	protected	OclScriptVirtualMachine		oclScriptVM; 

	protected	CoreClassifier  getClassifier(String name) {
		CoreClassifier c = (CoreClassifier) environment.lookup(name);
		assertNotNull(c);
		return	c;
	}
	
	public void setUp() throws Exception {
		super.setUp();
		objectSpace =  createObjectSpace();
		evalEnv = createEvalEnvironment();
		oclScriptVM = new OclScriptVirtualMachine();
	}

	protected	IObjectSpace	createObjectSpace() {
		return	new PSWObjectSpace();
	}
	
	protected	IEvalEnvironment		createEvalEnvironment() {
		return	new	EvalEnvironment();
	}
	
	protected	OclValue	evaluateExpression(String expression) throws Exception {
		CSTExpressionInOclCS  exp = oclCompiler.compileOclExpression(expression, this.getName(), new PrintWriter(System.out));
		return	oclEvaluator.evaluate(exp.getAst().getBodyExpression(), evalEnv, objectSpace);
	}
	
	protected	OclExpression compileExpression(String expression) throws Exception {
		CSTExpressionInOclCS  exp = oclCompiler.compileOclExpression(expression, this.getName(), new PrintWriter(System.out));
		return	exp.getAst().getBodyExpression();
	}

	protected	OclExpression compileExpression(String expression,  Environment env) throws Exception {
		PSWOclCompiler oclCompiler = new PSWOclCompiler(env, new ConstraintSourceTrackerImpl());
		CSTExpressionInOclCS  exp = oclCompiler.compileOclExpression(expression, this.getName(), new PrintWriter(System.out));
		return	exp.getAst().getBodyExpression();
	}

	protected	OclValue	evaluateExpression(String expression, IEvalEnvironment evalEnv) throws Exception {
		CSTExpressionInOclCS  exp = oclCompiler.compileOclExpression(expression, this.getName(), new PrintWriter(System.out));
		return	oclEvaluator.evaluate(exp.getAst().getBodyExpression(), evalEnv, objectSpace);
	}

}
