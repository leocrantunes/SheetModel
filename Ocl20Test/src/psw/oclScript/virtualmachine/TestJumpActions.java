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
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.Action;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.InputPin;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.GroupAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.IterateAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.LoopAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.jump.JumpBreakAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.jump.JumpContinueAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.jump.JumpGotoAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.AddVariableValueAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.OclExpressionEvalAction;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestJumpActions extends TestActions {

	public void testLoopAction_ForWithBreak() throws Exception {
		VariableDeclaration	rentalFee = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("rentalFee", getClassifier("Integer"), null);
		VariableDeclaration	i = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("i", getClassifier("Integer"), null);
		
		OclExpression  oclExpression = compileExpression("0");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		InputPin inputPin1 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin1);
		AddVariableValueAction		variableAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin1);

		OclExpression  iterExpression = compileExpression("1");
		OclExpressionEvalAction zeroEvalAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(iterExpression);
		InputPin inputPin4 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(zeroEvalAction.getResult(), inputPin4);
		AddVariableValueAction		iterInitAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(i, inputPin4);

		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement(rentalFee.getName(), rentalFee, false);
		compilerEnv.addElement(i.getName(), i, false);
		OclExpression  clauseExpression = compileExpression("i <= 10", compilerEnv);
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(clauseExpression);
		
		OclExpression  incrementExp = compileExpression("rentalFee + 10", compilerEnv);
		OclExpressionEvalAction incrementAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementExp);
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incrementAction.getResult(), inputPin2);
		AddVariableValueAction		incrementVarAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin2);
		
		OclExpression  conditionExp = compileExpression("rentalFee > 50", compilerEnv);
		OclExpressionEvalAction conditionAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(conditionExp);
		JumpBreakAction breakAction = AstOclScriptElementFactory.getInstance().createBreakAction();
		Action breakIfGreater50 = AstOclScriptElementFactory.getInstance().createConditionalAction(conditionAction, breakAction, null);
		
		OclExpression  incrementIterExp = compileExpression("i + 1", compilerEnv);
		OclExpressionEvalAction incAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementIterExp);
		InputPin inputPin5 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incAction.getResult(), inputPin5);
		AddVariableValueAction		incrementIterAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(i, inputPin5);
		
		OclExpression  resultExpression = compileExpression("rentalFee", compilerEnv);
		OclExpressionEvalAction resultAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(resultExpression);

		GroupAction bodyAction = new GroupAction();
		bodyAction.addAction(breakIfGreater50);
		bodyAction.addAction(incrementVarAction);
		
		LoopAction	loopAction = AstOclScriptElementFactory.getInstance().createLoopAction(clauseAction, bodyAction, incrementIterAction);
		
		GroupAction  groupAction = new GroupAction();
		groupAction.addVariable(rentalFee);
		groupAction.addAction(variableAction);
		
		GroupAction loopGroupAction = new GroupAction();
		loopGroupAction.addVariable(i);
		loopGroupAction.addAction(iterInitAction);
		loopGroupAction.addAction(loopAction);
		
		groupAction.addAction(loopGroupAction);
		groupAction.addAction(resultAction);
		
		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);
		
		assertTrue(resultAction.getResult().getValue() instanceof OclIntegerValue);
		assertEquals(60, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}

	public void testLoopAction_ForWithContinue() throws Exception {
		VariableDeclaration	rentalFee = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("rentalFee", getClassifier("Integer"), null);
		VariableDeclaration	i = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("i", getClassifier("Integer"), null);
		
		OclExpression  oclExpression = compileExpression("0");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		InputPin inputPin1 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin1);
		AddVariableValueAction		variableAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin1);

		OclExpression  iterExpression = compileExpression("1");
		OclExpressionEvalAction zeroEvalAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(iterExpression);
		InputPin inputPin4 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(zeroEvalAction.getResult(), inputPin4);
		AddVariableValueAction		iterInitAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(i, inputPin4);

		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement(rentalFee.getName(), rentalFee, false);
		compilerEnv.addElement(i.getName(), i, false);
		OclExpression  clauseExpression = compileExpression("i <= 10", compilerEnv);
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(clauseExpression);
		
		OclExpression  incrementExp = compileExpression("rentalFee + 10", compilerEnv);
		OclExpressionEvalAction incrementAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementExp);
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incrementAction.getResult(), inputPin2);
		AddVariableValueAction		incrementVarAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin2);
		
		OclExpression  conditionExp = compileExpression("(i >= 3 and i <= 7) or i = 9", compilerEnv);
		OclExpressionEvalAction conditionAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(conditionExp);
		JumpContinueAction continueAction = AstOclScriptElementFactory.getInstance().createContinueAction();
		Action continueCase = AstOclScriptElementFactory.getInstance().createConditionalAction(conditionAction, continueAction, null);

		OclExpression  incrementIterExp = compileExpression("i + 1", compilerEnv);
		OclExpressionEvalAction incAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementIterExp);
		InputPin inputPin5 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incAction.getResult(), inputPin5);
		AddVariableValueAction		incrementIterAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(i, inputPin5);

		OclExpression  resultExpression = compileExpression("rentalFee", compilerEnv);
		OclExpressionEvalAction resultAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(resultExpression);

		GroupAction bodyAction = new GroupAction();
		bodyAction.addAction(continueCase);
		bodyAction.addAction(incrementVarAction);
		
		LoopAction	loopAction = AstOclScriptElementFactory.getInstance().createLoopAction(clauseAction, bodyAction, incrementIterAction);
		
		GroupAction  groupAction = new GroupAction();
		groupAction.addVariable(rentalFee);
		groupAction.addAction(variableAction);
		
		GroupAction loopGroupAction = new GroupAction();
		loopGroupAction.addVariable(i);
		loopGroupAction.addAction(iterInitAction);
		loopGroupAction.addAction(loopAction);
		
		groupAction.addAction(loopGroupAction);
		groupAction.addAction(resultAction);
		
		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);
		
		assertTrue(resultAction.getResult().getValue() instanceof OclIntegerValue);
		assertEquals(40, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}
	
	public	void	testSimpleIterationWithBreak() throws Exception {
		CoreClassifier	sequenceIntegerType = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createSequenceType(getClassifier("Integer"));
		
		VariableDeclaration	setValues = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("mySet", sequenceIntegerType, null);
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(compileExpression("Sequence{1, 2, -3, 4, -5, 8, 7, -10}"));
		InputPin inputPin1 = AstOclScriptElementFactory.getInstance().createInputPin(sequenceIntegerType);
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin1);
		AddVariableValueAction		variableAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(setValues, inputPin1);

		VariableDeclaration	otherSetValues = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("otherSet", sequenceIntegerType, null);
		OclExpressionEvalAction oclAction2 = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(compileExpression("Sequence{}"));
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(sequenceIntegerType);
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction2.getResult(), inputPin2);
		AddVariableValueAction		variableAction2 = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(otherSetValues, inputPin2);

		VariableDeclaration number = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("number", getClassifier("Integer"), null);

		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement("mySet", setValues, false);
		compilerEnv.addElement("otherSet", otherSetValues, false);
		compilerEnv.addElement(number.getName(), number, false);
		
		OclExpression  conditionExp = compileExpression("number < 0", compilerEnv);
		OclExpressionEvalAction conditionAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(conditionExp);
		JumpBreakAction breakAction = AstOclScriptElementFactory.getInstance().createBreakAction();
		Action breakIf = AstOclScriptElementFactory.getInstance().createConditionalAction(conditionAction, breakAction, null);
		
		OclExpression  bodyExpression = compileExpression("otherSet->including(number * 10)", compilerEnv);
		OclExpressionEvalAction bodyExpressionAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(bodyExpression);
		InputPin inputPin5 = AstOclScriptElementFactory.getInstance().createInputPin(sequenceIntegerType);
		AstOclScriptElementFactory.getInstance().createDataFlow(bodyExpressionAction.getResult(), inputPin5);
		AddVariableValueAction		updateAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(otherSetValues, inputPin5);

		GroupAction updateGroupAction = new GroupAction();
		updateGroupAction.addAction(breakIf);
		updateGroupAction.addAction(updateAction);
		
		IterateAction collectionAction = new IterateAction(number, inputPin1, updateGroupAction);
		
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
		assertEquals(30, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}

	public	void	testSimpleIterationWithContinue() throws Exception {
		
		CoreClassifier	setIntegerType = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createSetType(getClassifier("Integer"));
		
		VariableDeclaration	setValues = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("mySet", setIntegerType, null);
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(compileExpression("Set{1, 2, -3, 4, -5, 8, 7, -10}"));
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
		
		
		OclExpression  conditionExp = compileExpression("number < 0", compilerEnv);
		OclExpressionEvalAction conditionAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(conditionExp);
		JumpContinueAction continueAction = AstOclScriptElementFactory.getInstance().createContinueAction();
		Action continueIf = AstOclScriptElementFactory.getInstance().createConditionalAction(conditionAction, continueAction, null);
		
		OclExpression  bodyExpression = compileExpression("otherSet->including(number * 10)", compilerEnv);
		OclExpressionEvalAction bodyExpressionAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(bodyExpression);
		InputPin inputPin5 = AstOclScriptElementFactory.getInstance().createInputPin(setIntegerType);
		AstOclScriptElementFactory.getInstance().createDataFlow(bodyExpressionAction.getResult(), inputPin5);
		AddVariableValueAction		updateAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(otherSetValues, inputPin5);

		GroupAction updateGroupAction = new GroupAction();
		updateGroupAction.addAction(continueIf);
		updateGroupAction.addAction(updateAction);
		
		IterateAction collectionAction = new IterateAction(number, inputPin1, updateGroupAction);
		
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
		assertEquals(220, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}


	public	void	testJumpGoto() throws Exception {
		VariableDeclaration	rentalFee = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("rentalFee", getClassifier("Integer"), null);
		VariableDeclaration	i = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("i", getClassifier("Integer"), null);
		
		OclExpression  oclExpression = compileExpression("0");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		InputPin inputPin1 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin1);
		AddVariableValueAction		rentalFeeInitAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin1);

		OclExpression  iterExpression = compileExpression("1");
		OclExpressionEvalAction zeroEvalAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(iterExpression);
		InputPin inputPin4 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(zeroEvalAction.getResult(), inputPin4);
		AddVariableValueAction		iterInitAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(i, inputPin4);

		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement(rentalFee.getName(), rentalFee, false);
		compilerEnv.addElement(i.getName(), i, false);

		OclExpression  incrementIterExp = compileExpression("i + 1", compilerEnv);
		OclExpressionEvalAction incAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementIterExp);
		InputPin inputPin5 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incAction.getResult(), inputPin5);
		AddVariableValueAction		incrementIterAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(i, inputPin5);

		OclExpression  clauseExpression = compileExpression("i > 10", compilerEnv);
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(clauseExpression);
		JumpGotoAction gotoEndAction = AstOclScriptElementFactory.getInstance().createGotoAction();
		Action testEndAction = AstOclScriptElementFactory.getInstance().createConditionalAction(clauseAction, gotoEndAction, null);

		OclExpression  incrementExp = compileExpression("rentalFee + 10", compilerEnv);
		OclExpressionEvalAction incrementAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementExp);
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incrementAction.getResult(), inputPin2);
		AddVariableValueAction		incrementVarAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin2);

		JumpGotoAction gotoIncAction = AstOclScriptElementFactory.getInstance().createGotoAction();

		OclExpression  incrementExp2 = compileExpression("rentalFee + 50", compilerEnv);
		OclExpressionEvalAction incrementAction2 = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementExp2);
		InputPin inputPin6 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incrementAction2.getResult(), inputPin6);
		AddVariableValueAction		endAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin6);

		
		OclExpression  resultExpression = compileExpression("rentalFee", compilerEnv);
		OclExpressionEvalAction resultAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(resultExpression);

		GroupAction bodyAction = new GroupAction();
		bodyAction.addAction(incrementVarAction);
		
		GroupAction  groupAction = new GroupAction();
		groupAction.addVariable(rentalFee);
		groupAction.addVariable(i);
		
		groupAction.addAction(rentalFeeInitAction);
		groupAction.addAction(iterInitAction);
		
		groupAction.addAction(incrementIterAction);
		groupAction.addAction(testEndAction);
		groupAction.addAction(incrementVarAction);
		groupAction.addAction(gotoIncAction);
		groupAction.addAction(endAction);
		
		groupAction.addAction(resultAction);
		
		groupAction.addJumpPoint(AstOclScriptElementFactory.getInstance().createJumpPoint("inc:"), incrementIterAction);
		groupAction.addJumpPoint(AstOclScriptElementFactory.getInstance().createJumpPoint("end:"), endAction);
		
		gotoIncAction.setJumpPoint(groupAction.getJumpPoint("inc:"));
		gotoEndAction.setJumpPoint(groupAction.getJumpPoint("end:"));
		
		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);
		
		assertTrue(resultAction.getResult().getValue() instanceof OclIntegerValue);
		assertEquals(140, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}
}
