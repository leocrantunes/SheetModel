/*
 * Created on Apr 15, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine;

import java.util.ArrayList;

import impl.ocl20.util.AstOclModelElementFactoryManager;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.environment.Environment;
import ocl20.expressions.OclExpression;
import ocl20.expressions.VariableDeclaration;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.factory.AstOclScriptElementFactory;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.InputPin;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.ConditionalAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.GroupAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.LoopAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.AddAttributeValueAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.AddVariableValueAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.CreateObjectAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.OclExpressionEvalAction;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCompositeActions extends TestActions {

	public void testGroupOfOneAction() throws Exception {
		CoreClassifier c = getClassifier("Film");
		CreateObjectAction createObjectAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);

		GroupAction	groupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		groupAction.addAction(createObjectAction);

		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);

		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
	}	

	public void testSequenceOfInstantiations() throws Exception {
		CoreClassifier c = getClassifier("Film");

		GroupAction	groupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		for (int i = 0; i < 5; i++) {
			CreateObjectAction createObjectAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);
			groupAction.addAction(createObjectAction);
		}

		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);

		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(5, objectSpace.getObjectsOfClass(c).size());
	}	


	public void testCreationAndModification() throws Exception {
		CoreClassifier c = getClassifier("Film");
		CreateObjectAction createObjectAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);

		OclExpression  oclExpression = compileExpression("\"alexandre o grande\"");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);

		InputPin valuePin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("String"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), valuePin);
		
		InputPin objectPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		OclObjectValue aFilm = objectSpace.createObject(getClassifier("Film"));
		objectPin.setValue(aFilm);
		
		CoreAttribute attribute = getClassifier("Film").lookupAttribute("name");
		assertNotNull(attribute);
		AddAttributeValueAction	attributeAction = AstOclScriptElementFactory.getInstance().createAddAttributeValueAction(attribute, objectPin, valuePin);

		GroupAction	groupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		groupAction.addAction(createObjectAction);
		groupAction.addAction(attributeAction);

		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);

		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(2, objectSpace.getObjectsOfClass(c).size());
		assertEquals("alexandre o grande", ((OclStringValue) aFilm.getValueOf("name")).stringValue()); 
	}	

	
	public void testInnerGroups() throws Exception {
		CoreClassifier c = getClassifier("Film");

		GroupAction	groupAction1 = AstOclScriptElementFactory.getInstance().createGroupAction();
		for (int i = 0; i < 5; i++) {
			groupAction1.addAction(AstOclScriptElementFactory.getInstance().createCreateObjectAction(c));
		}

		GroupAction	groupAction2 = AstOclScriptElementFactory.getInstance().createGroupAction();
		for (int i = 0; i < 5; i++) {
			groupAction2.addAction(AstOclScriptElementFactory.getInstance().createCreateObjectAction(c));
		}

		GroupAction	groupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		groupAction.addAction(groupAction1);
		groupAction.addAction(groupAction2);
		groupAction.addAction(AstOclScriptElementFactory.getInstance().createCreateObjectAction(c));

		
		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);

		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(11, objectSpace.getObjectsOfClass(c).size());
	}	

	
	public void testGroupVariables() throws Exception {
		VariableDeclaration	myFilm = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("myFilm", getClassifier("Film"), null); 
		VariableDeclaration	rentalFee = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("rentalFee", getClassifier("Integer"), null);

		CoreClassifier c = getClassifier("Film");
		
		InputPin inputPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		CreateObjectAction createObjectAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);
		AstOclScriptElementFactory.getInstance().createDataFlow(createObjectAction.getResult(), inputPin);
		AddVariableValueAction		variableAction1 = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(myFilm, inputPin);

		OclExpression  oclExpression = compileExpression("50 + 10");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin2);
		AddVariableValueAction		variableAction2 = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin2);

		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement(myFilm.getName(), myFilm, false);
		compilerEnv.addElement(rentalFee.getName(), rentalFee, false);
		
		OclExpression myFilmExpression = compileExpression("myFilm", compilerEnv);
		OclExpression rentalFeeExpression = compileExpression("rentalFee", compilerEnv);
		
		OclExpressionEvalAction myFilmExp = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(myFilmExpression);
		InputPin sourcePin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		AstOclScriptElementFactory.getInstance().createDataFlow(myFilmExp.getResult(), sourcePin);

		OclExpressionEvalAction rentalFeeExp = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(rentalFeeExpression);
		InputPin valuePin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(rentalFeeExp.getResult(), valuePin);

		CoreAttribute attribute = getClassifier("Film").lookupAttribute("rentalFee");
		assertNotNull(attribute);
		AddAttributeValueAction	attributeAction = AstOclScriptElementFactory.getInstance().createAddAttributeValueAction(attribute, sourcePin, valuePin);

		GroupAction	groupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		
		groupAction.addVariable(myFilm);
		groupAction.addVariable(rentalFee);
		
		groupAction.addAction(variableAction1);
		groupAction.addAction(variableAction2);
		groupAction.addAction(attributeAction);

		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);

		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		OclObjectValue aFilm = (OclObjectValue) objectSpace.getObjectsOfClass(c).iterator().next();
		assertEquals(60, ((OclIntegerValue) aFilm.getValueOf("rentalFee")).intValue()); 
	}	
	

	public void testNestedGroupVariables() throws Exception {
		VariableDeclaration	myFilm = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("myFilm", getClassifier("Film"), null); 
		VariableDeclaration	rentalFee = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("rentalFee", getClassifier("Integer"), null);

		CoreClassifier c = getClassifier("Film");
		
		InputPin inputPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		CreateObjectAction createObjectAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);
		AstOclScriptElementFactory.getInstance().createDataFlow(createObjectAction.getResult(), inputPin);
		AddVariableValueAction		variableAction1 = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(myFilm, inputPin);

		OclExpression  oclExpression = compileExpression("50 + 10");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin2);
		AddVariableValueAction		variableAction2 = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin2);
		
		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement(myFilm.getName(), myFilm, false);
		compilerEnv.addElement(rentalFee.getName(), rentalFee, false);
		
		OclExpression myFilmExpression = compileExpression("myFilm", compilerEnv);
		OclExpression rentalFeeExpression = compileExpression("rentalFee", compilerEnv);
		
		OclExpressionEvalAction myFilmExp = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(myFilmExpression);
		InputPin sourcePin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		AstOclScriptElementFactory.getInstance().createDataFlow(myFilmExp.getResult(), sourcePin);

		OclExpressionEvalAction rentalFeeExp = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(rentalFeeExpression);
		InputPin valuePin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(rentalFeeExp.getResult(), valuePin);

		CoreAttribute attribute = getClassifier("Film").lookupAttribute("rentalFee");
		assertNotNull(attribute);
		AddAttributeValueAction	attributeAction = AstOclScriptElementFactory.getInstance().createAddAttributeValueAction(attribute, sourcePin, valuePin);

		
		
		GroupAction innerGroupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		
		VariableDeclaration	innerRentalFee = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("rentalFee", getClassifier("Integer"), null);

		OclExpression  innerOclExpression = compileExpression("50 + 50");
		OclExpressionEvalAction innerOclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(innerOclExpression);
		InputPin inputPin3 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(innerOclAction.getResult(), inputPin3);
		AddVariableValueAction		variableAction3 = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(innerRentalFee, inputPin3);

		Environment nestedCompilerEnv = compilerEnv.nestedEnvironment();
		nestedCompilerEnv.addElement(innerRentalFee.getName(), innerRentalFee, false);
		
		OclExpression innerMyFilmExpression = compileExpression("myFilm", nestedCompilerEnv);
		OclExpression innerRentalFeeExpression = compileExpression("myFilm.rentalFee + rentalFee", nestedCompilerEnv);
		
		OclExpressionEvalAction myFilmExp2 = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(innerMyFilmExpression);
		InputPin sourcePin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		AstOclScriptElementFactory.getInstance().createDataFlow(myFilmExp2.getResult(), sourcePin2);

		OclExpressionEvalAction rentalFeeExp2 = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(innerRentalFeeExpression);
		InputPin valuePin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(rentalFeeExp2.getResult(), valuePin2);

		attribute = getClassifier("Film").lookupAttribute("rentalFee");
		assertNotNull(attribute);
		AddAttributeValueAction	attributeAction2 = AstOclScriptElementFactory.getInstance().createAddAttributeValueAction(attribute, sourcePin2, valuePin2);

		innerGroupAction.addVariable(innerRentalFee);
		innerGroupAction.addAction(variableAction3);
		innerGroupAction.addAction(attributeAction2);
		
		GroupAction	groupAction = AstOclScriptElementFactory.getInstance().createGroupAction();

		groupAction.addVariable(myFilm);
		groupAction.addVariable(rentalFee);
		
		groupAction.addAction(variableAction1);
		groupAction.addAction(variableAction2);
		groupAction.addAction(attributeAction);
		groupAction.addAction(innerGroupAction);

		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);
		
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		OclObjectValue aFilm = (OclObjectValue) objectSpace.getObjectsOfClass(c).iterator().next();
		assertEquals(160, ((OclIntegerValue) aFilm.getValueOf("rentalFee")).intValue()); 
	}	

	
	public void	testConditionalAction_01() throws Exception {
		
		OclExpression  oclExpression = compileExpression("50 > 49");
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		
		CoreClassifier c = getClassifier("Film");
		CreateObjectAction createFilmAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);
		GroupAction	createFilmGroupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		createFilmGroupAction.addAction(createFilmAction);

		CoreClassifier d = getClassifier("Distributor");
		CreateObjectAction createDistributorAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(d);
		GroupAction	createDistributorGroupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		createDistributorGroupAction.addAction(createDistributorAction);

		ConditionalAction conditionalAction = AstOclScriptElementFactory.getInstance().createConditionalAction(clauseAction, createFilmGroupAction, createDistributorGroupAction);
		
		oclScriptVM.executeAction(conditionalAction, objectSpace, evalEnv);

		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		assertNull(objectSpace.getObjectsOfClass(d));
	}
	

	public void	testConditionalAction_02() throws Exception {
		
		OclExpression  oclExpression = compileExpression("50 < 50");
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		
		CoreClassifier c = getClassifier("Film");
		CreateObjectAction createFilmAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);
		GroupAction	createFilmGroupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		createFilmGroupAction.addAction(createFilmAction);

		CoreClassifier d = getClassifier("Distributor");
		CreateObjectAction createDistributorAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(d);
		GroupAction	createDistributorGroupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		createDistributorGroupAction.addAction(createDistributorAction);

		ConditionalAction conditionalAction = AstOclScriptElementFactory.getInstance().createConditionalAction(clauseAction, createFilmGroupAction, createDistributorGroupAction);
		
		oclScriptVM.executeAction(conditionalAction, objectSpace, evalEnv);

		assertNotNull(objectSpace.getObjectsOfClass(d));
		assertEquals(1, objectSpace.getObjectsOfClass(d).size());
		assertNull(objectSpace.getObjectsOfClass(c));
	}

	public void	testConditionalAction_03() throws Exception {
		
		OclExpression  oclExpression = compileExpression("50 < 50");
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		
		CoreClassifier c = getClassifier("Film");
		CreateObjectAction createFilmAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);
		GroupAction	createFilmGroupAction = AstOclScriptElementFactory.getInstance().createGroupAction();
		createFilmGroupAction.addAction(createFilmAction);

		CoreClassifier d = getClassifier("Distributor");

		ConditionalAction conditionalAction = AstOclScriptElementFactory.getInstance().createConditionalAction(clauseAction, createFilmGroupAction, null);
		
		oclScriptVM.executeAction(conditionalAction, objectSpace, evalEnv);

		assertNull(objectSpace.getObjectsOfClass(c));
		assertNull(objectSpace.getObjectsOfClass(d));
	}

	public void testLoopAction_While() throws Exception {
		VariableDeclaration	rentalFee = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("rentalFee", getClassifier("Integer"), null);
		
		OclExpression  oclExpression = compileExpression("0");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		InputPin inputPin1 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin1);
		AddVariableValueAction		variableAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin1);

		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement(rentalFee.getName(), rentalFee, false);
		OclExpression  clauseExpression = compileExpression("rentalFee < 10", compilerEnv);
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(clauseExpression);
		
		OclExpression  incrementExp = compileExpression("rentalFee + 1", compilerEnv);
		OclExpressionEvalAction incrementAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementExp);
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incrementAction.getResult(), inputPin2);
		AddVariableValueAction		incrementVarAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin2);

		OclExpression  resultExpression = compileExpression("rentalFee", compilerEnv);
		OclExpressionEvalAction resultAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(resultExpression);

		LoopAction	loopAction = AstOclScriptElementFactory.getInstance().createLoopAction(clauseAction, incrementVarAction);
		
		GroupAction  groupAction = new GroupAction();
		groupAction.addVariable(rentalFee);
		groupAction.addAction(variableAction);
		groupAction.addAction(loopAction);
		groupAction.addAction(resultAction);
		
		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);
		
		assertTrue(resultAction.getResult().getValue() instanceof OclIntegerValue);
		assertEquals(10, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}

	
	public void testLoopAction_Repeat() throws Exception {
		VariableDeclaration	rentalFee = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("rentalFee", getClassifier("Integer"), null);
		
		OclExpression  oclExpression = compileExpression("0");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		InputPin inputPin1 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin1);
		AddVariableValueAction		variableAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin1);

		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement(rentalFee.getName(), rentalFee, false);
		OclExpression  clauseExpression = compileExpression("rentalFee = 10", compilerEnv);
		CoreOperation notOperation = getClassifier("Boolean").lookupOperation("not", new ArrayList());
		OclExpression	untilExpression  = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createOperationCallExp(clauseExpression, notOperation, new ArrayList(), getClassifier("Boolean"), false);
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(untilExpression);
		
		OclExpression  incrementExp = compileExpression("rentalFee + 1", compilerEnv);
		OclExpressionEvalAction incrementAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementExp);
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incrementAction.getResult(), inputPin2);
		AddVariableValueAction		incrementVarAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(rentalFee, inputPin2);

		OclExpression  resultExpression = compileExpression("rentalFee", compilerEnv);
		OclExpressionEvalAction resultAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(resultExpression);

		LoopAction	loopAction = AstOclScriptElementFactory.getInstance().createLoopAction(clauseAction, incrementVarAction);
		
		GroupAction  groupAction = new GroupAction();
		groupAction.addVariable(rentalFee);
		groupAction.addAction(variableAction);
		groupAction.addAction(incrementVarAction);
		groupAction.addAction(loopAction);
		groupAction.addAction(resultAction);
		
		oclScriptVM.executeAction(groupAction, objectSpace, evalEnv);
		
		assertTrue(resultAction.getResult().getValue() instanceof OclIntegerValue);
		assertEquals(10, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}

	
	public void testLoopAction_For() throws Exception {
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

		OclExpression  incrementIterExp = compileExpression("i + 1", compilerEnv);
		OclExpressionEvalAction incAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(incrementIterExp);
		InputPin inputPin5 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(incAction.getResult(), inputPin5);
		AddVariableValueAction		incrementIterAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(i, inputPin5);
		
		OclExpression  resultExpression = compileExpression("rentalFee", compilerEnv);
		OclExpressionEvalAction resultAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(resultExpression);

		GroupAction bodyAction = new GroupAction();
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
		assertEquals(100, ((OclIntegerValue) (resultAction.getResult().getValue())).intValue());
	}


}
