/*
 * Created on Apr 15, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine;

import impl.ocl20.util.AstOclModelElementFactoryManager;
import ocl20.common.CoreClassifier;
import ocl20.environment.Environment;
import ocl20.expressions.OclExpression;
import ocl20.expressions.VariableDeclaration;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.factory.AstOclScriptElementFactory;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.InputPin;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.GroupAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.IterateAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.AddVariableValueAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.OclExpressionEvalAction;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCollectionAction extends TestActions {

	public	void	testSimpleIteration() throws Exception {
		
		CoreClassifier	setIntegerType = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createSetType(getClassifier("Integer"));
		
		VariableDeclaration	setValues = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("mySet", setIntegerType, null);
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(compileExpression("Set{1, 2, 3, 4, 5}"));
		InputPin inputPin1 = AstOclScriptElementFactory.getInstance().createInputPin(setIntegerType);
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin1);
		AddVariableValueAction		variableAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(setValues, inputPin1);

		VariableDeclaration	otherSetValues = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("otherSet", setIntegerType, null);
		OclExpressionEvalAction oclAction2 = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(compileExpression("Set{}"));
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(setIntegerType);
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction2.getResult(), inputPin2);
		AddVariableValueAction		variableAction2 = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(otherSetValues, inputPin2);

		VariableDeclaration number = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("number", getClassifier("Integer"), null);

		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement("mySet", setValues, false);
		compilerEnv.addElement("otherSet", otherSetValues, false);
		compilerEnv.addElement(number.getName(), number, false);
		
		OclExpression  bodyExpression = compileExpression("otherSet->including(number * 10)", compilerEnv);
		OclExpressionEvalAction bodyExpressionAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(bodyExpression);
		InputPin inputPin5 = AstOclScriptElementFactory.getInstance().createInputPin(setIntegerType);
		AstOclScriptElementFactory.getInstance().createDataFlow(bodyExpressionAction.getResult(), inputPin5);
		AddVariableValueAction		updateAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(otherSetValues, inputPin5);

		IterateAction collectionAction = new IterateAction(number, inputPin1, updateAction);
		
		OclExpression  resultExpression = compileExpression("otherSet->sum()", compilerEnv);
		OclExpressionEvalAction resultAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(resultExpression);
		
		GroupAction groupAction = new GroupAction();
		groupAction.addVariable(setValues);
		groupAction.addVariable(otherSetValues);
		
		groupAction.addAction(variableAction);
		groupAction.addAction(variableAction2);
		groupAction.addAction(collectionAction);
		groupAction.addAction(resultAction);
		
		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);
		
		assertTrue(resultAction.getResult().getValue() instanceof OclIntegerValue);
		assertEquals(150, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}
	
}
