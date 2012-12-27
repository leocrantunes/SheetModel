/*
 * Created on Apr 13, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine;

import impl.ocl20.constraints.OclActionBodyConstraintImpl;
import impl.ocl20.util.AstOclModelElementFactoryManager;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreAssociationClass;
import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.environment.Environment;
import ocl20.evaluation.OclValue;
import ocl20.expressions.OclExpression;
import ocl20.expressions.VariableDeclaration;
import ocl20.types.CollectionType;
import br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator.EvalEnvironment;
import br.ufrj.cos.lens.odyssey.tools.psw.oclEvaluator.IEvalEnvironment;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.factory.AstOclScriptElementFactory;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.Action;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.InputPin;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.base.OutputPin;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.ConditionalAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.composite.GroupAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.jump.JumpReturnAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.AddAttributeValueAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.AddVariableValueAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.CallOperationAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.CreateLinkAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.CreateLinkObjectAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.CreateObjectAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.DeleteObjectAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.metamodel.primitive.OclExpressionEvalAction;
import br.ufrj.cos.lens.odyssey.tools.psw.oclScript.virtualmachine.OclScriptVMException;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclIntegerValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclRealValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclSetValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclStringValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValuesFactory;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestPrimitiveActions extends TestActions {


	public void testCreateObject() throws OclScriptVMException {
		CoreClassifier c = getClassifier("Film");

		CreateObjectAction createObjectAction = AstOclScriptElementFactory.getInstance().createCreateObjectAction(c);
		
		oclScriptVM.executeAction(createObjectAction, objectSpace, evalEnv);
		
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		assertEquals(createObjectAction.getResult().getValue(), objectSpace.getObjectsOfClass(c).iterator().next());
		
		oclScriptVM.executeAction(createObjectAction, objectSpace, evalEnv);
		
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(2, objectSpace.getObjectsOfClass(c).size());
	}

	public void testDeleteObject() throws Exception {
		CoreClassifier c = getClassifier("Film");

		OclObjectValue obj = objectSpace.createObject(c);
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		
		InputPin	inputPin = AstOclScriptElementFactory.getInstance().createInputPin(c);
		inputPin.setValue(obj);
		DeleteObjectAction deleteObjectAction = AstOclScriptElementFactory.getInstance().createDeleteObjectAction(inputPin);
		
		oclScriptVM.executeAction(deleteObjectAction, objectSpace, evalEnv);
		
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(0, objectSpace.getObjectsOfClass(c).size());
	}

	public void testDeleteInvalidObject() throws Exception {
		CoreClassifier c = getClassifier("Film");

		OclValue	value = evaluateExpression("5"); 
		
		InputPin	inputPin = AstOclScriptElementFactory.getInstance().createInputPin(c);
		inputPin.setValue(value);
		try {
			DeleteObjectAction deleteObjectAction = AstOclScriptElementFactory.getInstance().createDeleteObjectAction(inputPin);
			oclScriptVM.executeAction(deleteObjectAction, objectSpace, evalEnv);
			fail();
		} catch(OclScriptVMException e) {
			System.out.println(e.getMessage());
		}
	}

	public void testOclExpressionAction() throws Exception {
		OclExpression  oclExpression = compileExpression("5 + 10");
		
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);
		oclScriptVM.executeAction(oclAction, objectSpace, evalEnv);
		assertTrue(oclAction.getResult().getValue() instanceof OclIntegerValue);
		assertEquals(15, ((OclIntegerValue) (oclAction.getResult().getValue())).intValue());
	}


	public void testAddVariableValueAction() throws Exception {
		
		VariableDeclaration	variable = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("myVar", getClassifier("Integer"), null);
		
		OclExpression  oclExpression = compileExpression("5 + 10");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);

		InputPin inputPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Integer"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), inputPin);

		AddVariableValueAction		variableAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(variable, inputPin);
		
		IEvalEnvironment nestedEvalEnv = new EvalEnvironment(this.evalEnv);
		nestedEvalEnv.add(variable.getName(), evaluateExpression("2") );

		OclValue	value = nestedEvalEnv.getValueOf(variable.getName());
		assertNotNull(value);
		assertTrue(value instanceof OclIntegerValue);
		assertEquals(2, ((OclIntegerValue) value).intValue()); 

		oclScriptVM.executeAction(variableAction, objectSpace, nestedEvalEnv);
		
		value = nestedEvalEnv.getValueOf(variable.getName());
		assertNotNull(value);
		assertTrue(value instanceof OclIntegerValue);
		assertEquals(15, ((OclIntegerValue) value).intValue()); 
		
	}

	public void testAddAttributeValueAction() throws Exception {
		
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
		
		oclScriptVM.executeAction(attributeAction, objectSpace, evalEnv);
		
		OclValue value = aFilm.getValueOf("name");
		assertNotNull(value);
		assertTrue(value instanceof OclStringValue);
		assertEquals("alexandre o grande", ((OclStringValue) value).stringValue()); 
	}
	

	public void testInvalidAddAttributeValueAction() throws Exception {
		
		OclExpression  oclExpression = compileExpression("\"alexandre o grande\"");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);

		InputPin valuePin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("String"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), valuePin);
		
		InputPin objectPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		OclValue aFilm = OclValuesFactory.getInstance().createNullValue();
		objectPin.setValue(aFilm);
		
		CoreAttribute attribute = getClassifier("Film").lookupAttribute("name");
		assertNotNull(attribute);
		
		AddAttributeValueAction	attributeAction = AstOclScriptElementFactory.getInstance().createAddAttributeValueAction(attribute, objectPin, valuePin);
		
		try {
			oclScriptVM.executeAction(attributeAction, objectSpace, evalEnv);
			fail();
		} catch (OclScriptVMException e) {
			System.out.println(e.getMessage());
		}
	}	
	
	public void testInvalidTypeAddAttributeValueAction() throws Exception {
		OclExpression  oclExpression = compileExpression("\"alexandre o grande\"");
		OclExpressionEvalAction oclAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(oclExpression);

		InputPin valuePin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("String"));
		AstOclScriptElementFactory.getInstance().createDataFlow(oclAction.getResult(), valuePin);
		
		InputPin objectPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		OclValue aFilm = objectSpace.createObject(getClassifier("Film"));
		objectPin.setValue(aFilm);
		
		CoreAttribute attribute = getClassifier("Film").lookupAttribute("days");
		assertNotNull(attribute);
		
		AddAttributeValueAction	attributeAction = AstOclScriptElementFactory.getInstance().createAddAttributeValueAction(attribute, objectPin, valuePin);
		
		try {
			oclScriptVM.executeAction(attributeAction, objectSpace, evalEnv);
			fail();
		} catch (OclScriptVMException e) {
			System.out.println(e.getMessage());
		}
	}	
	
	public void testCreateLinkAction() throws Exception {
		
		CollectionType	type = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createSetType(getClassifier("Reservation"));
		InputPin valuePin = AstOclScriptElementFactory.getInstance().createInputPin(type);
		OclValue reservation01= objectSpace.createObject(getClassifier("Reservation"));
		OclSetValue setReservation = (OclSetValue) OclValuesFactory.getInstance().createCollectionValue(type); 
		setReservation.add(reservation01);
		valuePin.setValue(setReservation);
		
		InputPin objectPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Film"));
		OclObjectValue aFilm = objectSpace.createObject(getClassifier("Film"));
		objectPin.setValue(aFilm);

		CoreAssociationEnd assocEnd = getClassifier("Film").lookupAssociationEnd("Reservation");
		assertNotNull(assocEnd);
		
		CreateLinkAction	createLinkAction = AstOclScriptElementFactory.getInstance().createCreateLinkAction(assocEnd, objectPin, valuePin);
		
		try {
			oclScriptVM.executeAction(createLinkAction, objectSpace, evalEnv);
			
			List result = objectSpace.getLinkedObjects(aFilm, assocEnd);
			assertEquals(1, result.size());
			assertEquals(reservation01, result.iterator().next());
			
			
		} catch (OclScriptVMException e) {
			e.printStackTrace();
			fail();
		}
		
		
		OclValue reservation02= objectSpace.createObject(getClassifier("Reservation"));
		setReservation.add(reservation02);
		OclValue reservation03= objectSpace.createObject(getClassifier("Reservation"));
		setReservation.add(reservation03);
		valuePin.setValue(setReservation);
		
		try {
			oclScriptVM.executeAction(createLinkAction, objectSpace, evalEnv);
			
			List result = objectSpace.getLinkedObjects(aFilm, assocEnd);
			assertEquals(3, result.size());
			assertTrue(result.contains(reservation01));
			assertTrue(result.contains(reservation02));
			assertTrue(result.contains(reservation03));
		} catch (OclScriptVMException e) {
			e.printStackTrace();
			fail();
		}
		
		setReservation = (OclSetValue) OclValuesFactory.getInstance().createCollectionValue(type);
		OclValue reservation04= objectSpace.createObject(getClassifier("Reservation"));
		setReservation.add(reservation01);
		setReservation.add(reservation04);
		valuePin.setValue(setReservation);
		
		try {
			oclScriptVM.executeAction(createLinkAction, objectSpace, evalEnv);
			
			List result = objectSpace.getLinkedObjects(aFilm, assocEnd);
			assertEquals(2, result.size());
			assertTrue(result.contains(reservation01));
			assertTrue(result.contains(reservation04));
		} catch (OclScriptVMException e) {
			e.printStackTrace();
			fail();
		}

		
		valuePin.setValue(OclValuesFactory.getInstance().createNullValue());
		try {
			oclScriptVM.executeAction(createLinkAction, objectSpace, evalEnv);
			
			List result = objectSpace.getLinkedObjects(aFilm, assocEnd);
			assertEquals(0, result.size());
		} catch (OclScriptVMException e) {
			e.printStackTrace();
			fail();
		}

		
		OclSetValue emptySetReservation = (OclSetValue) OclValuesFactory.getInstance().createCollectionValue(type);
		valuePin.setValue(emptySetReservation);
		
		try {
			oclScriptVM.executeAction(createLinkAction, objectSpace, evalEnv);
			
			List result = objectSpace.getLinkedObjects(aFilm, assocEnd);
			assertEquals(0, result.size());
		} catch (OclScriptVMException e) {
			e.printStackTrace();
			fail();
		}
		
		
		InputPin singleValuePin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Reservation"));
		singleValuePin.setValue(reservation01); 
		createLinkAction = AstOclScriptElementFactory.getInstance().createCreateLinkAction(assocEnd, objectPin, singleValuePin);
		
		try {
			oclScriptVM.executeAction(createLinkAction, objectSpace, evalEnv);
			
			List result = objectSpace.getLinkedObjects(aFilm, assocEnd);
			assertEquals(1, result.size());
			assertTrue(result.contains(reservation01));
		} catch (OclScriptVMException e) {
			e.printStackTrace();
			fail();
		}
	}	
	

	public void testCreateLinkObjectAction() throws Exception {
		OclObjectValue[] distributors = createInstances("Distributor", 5);
		OclObjectValue[] specialFilms = createInstances("SpecialFilm", 3);

		createLinks(distributors, specialFilms);

		List	linkedObjectsPins = new ArrayList();
		
		InputPin specialFilmPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("SpecialFilm"));
		specialFilmPin.setValue(specialFilms[0]); 
		linkedObjectsPins.add(specialFilmPin);
		
		InputPin distributorPin = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Distributor"));
		distributorPin.setValue(distributors[0]); 
		linkedObjectsPins.add(distributorPin);
		
		CoreClassifier c = getClassifier("Allocation");

		CreateLinkObjectAction createLinkObjectAction = AstOclScriptElementFactory.getInstance().createCreateLinkObjectAction((CoreAssociationClass) c, linkedObjectsPins);
		
		oclScriptVM.executeAction(createLinkObjectAction, objectSpace, evalEnv);
		
		assertNotNull(objectSpace.getObjectsOfClass(c));
		assertEquals(1, objectSpace.getObjectsOfClass(c).size());
		assertEquals(createLinkObjectAction.getResult().getValue(), objectSpace.getObjectsOfClass(c).iterator().next());
	}	
		
	protected	void	createLinks(OclObjectValue[] distributors, 	OclObjectValue[] specialFilms) {
		CoreClassifier	specialFilmsClass = getClassifier("SpecialFilm");

		CoreAssociation	distributionAssoc = specialFilmsClass.lookupAssociationEnd("dist").getTheAssociation();
		assertNotNull(distributionAssoc);
		CoreAssociationEnd	filmRole = distributionAssoc.getAssociationEnd("films");
		assertNotNull(filmRole);
		CoreAssociationEnd	distRole = distributionAssoc.getAssociationEnd("dist");
		assertNotNull(distRole);
		CoreAssociationClass	allocationRole = specialFilmsClass.lookupAssociationClass("Allocation");
		assertNotNull(allocationRole);

		objectSpace.createLink(distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], distributors[0] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

		objectSpace.createLink(distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[0], distributors[1] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));

		objectSpace.createLink(distributionAssoc,  new ArrayList(Arrays.asList(new Object[] { specialFilms[1], distributors[1] } )), 
																						   new  ArrayList(Arrays.asList(new Object[] { filmRole, distRole } )));
	}
	
	protected	OclObjectValue[] createInstances(String className, int numberOfInstances) {
		CoreClassifier	classifier;
		if (className.indexOf("::") > 0)
			classifier = (CoreClassifier) environment.lookupPathName(className);
		else	
			classifier = (CoreClassifier) environment.lookup(className);
		OclObjectValue[] instances = new OclObjectValue[numberOfInstances];
		for (int i = 0; i < instances.length; i++) {
			instances[i] = objectSpace.createObject(classifier);			
		}
		return	instances;
	}

	
	public	void	testCallOperationAction() throws Exception {
		CoreClassifier	filmClass = getClassifier("Film");
		
		List params = new ArrayList();
		params.add(getClassifier("Integer"));
		CoreOperation	operation = filmClass.lookupOperation("getRentalFee", params);
		
		assertNotNull(operation);
		
		Action	actionBody = createActionBody();
		
		OclActionBodyConstraintImpl constraint = new OclActionBodyConstraintImpl();
		constraint.setAction(actionBody);
		constraint.setContextualOperation(operation);
		constraint.setSource("testCallOperationAction");
		operation.setActionBody(constraint);
		
		OclObjectValue targetObject = objectSpace.createObject(filmClass);
		targetObject.setValueOf("rentalFee", OclValuesFactory.getInstance().createIntegerValue(10));
		
		InputPin	targetObjectPin = new InputPin(getClassifier("Film"));
		targetObjectPin.setValue(targetObject);
		
		List	arguments = new ArrayList();
		InputPin	dayOfWeek = new InputPin(getClassifier("Integer"));
		dayOfWeek.setValue(OclValuesFactory.getInstance().createIntegerValue(6));
		arguments.add(dayOfWeek);
		
		OutputPin	resultPin = new OutputPin(getClassifier("Real"));
		
		CallOperationAction	operationAction = AstOclScriptElementFactory.getInstance().createCallOperationAction(targetObjectPin, operation, arguments, resultPin);
		
		oclScriptVM.executeAction(operationAction, objectSpace, evalEnv);
		
		assertEquals(60.47, ((OclRealValue) resultPin.getValue()).doubleValue().doubleValue(), 0.00001); 
	}
	
	
	protected	Action	createActionBody() throws Exception {
		GroupAction	groupAction = new GroupAction();
		
		VariableDeclaration	dayOfWeek = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("dayOfWeek", getClassifier("Integer"), null);
		VariableDeclaration	result = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("result", getClassifier("Real"), null);
		VariableDeclaration	self = AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createVariableDeclaration("self", getClassifier("Film"), null);
		
		Environment compilerEnv = environment.nestedEnvironment();
		compilerEnv.addElement(self.getName(), self, false);
		compilerEnv.addElement(dayOfWeek.getName(), dayOfWeek, false);
		compilerEnv.addElement(result.getName(), result, false);
		
		OclExpression  clauseExpression = compileExpression("dayOfWeek > 5", compilerEnv);
		OclExpressionEvalAction clauseAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(clauseExpression);

		OclExpression  option1Expression = compileExpression("self.rentalFee + 50.47", compilerEnv);
		OclExpressionEvalAction option1Action = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(option1Expression);
		InputPin inputPin1 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Real"));
		AstOclScriptElementFactory.getInstance().createDataFlow(option1Action.getResult(), inputPin1);
		AddVariableValueAction		thenAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(result, inputPin1);

		OclExpression  option2Expression = compileExpression("self.rentalFee + 100.89", compilerEnv);
		OclExpressionEvalAction option2Action = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(option1Expression);
		InputPin inputPin2 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Real"));
		AstOclScriptElementFactory.getInstance().createDataFlow(option2Action.getResult(), inputPin2);
		AddVariableValueAction		elseAction = AstOclScriptElementFactory.getInstance().createAddVariableValueAction(result, inputPin2);

		ConditionalAction conditionalAction = AstOclScriptElementFactory.getInstance().createConditionalAction(clauseAction, thenAction, elseAction);

		OclExpression  resultExpression = compileExpression("result", compilerEnv);
		OclExpressionEvalAction resultAction = AstOclScriptElementFactory.getInstance().createOclExpressionEvalAction(resultExpression);
		InputPin inputPin5 = AstOclScriptElementFactory.getInstance().createInputPin(getClassifier("Real"));
		AstOclScriptElementFactory.getInstance().createDataFlow(resultAction.getResult(), inputPin5);

		JumpReturnAction returnAction = AstOclScriptElementFactory.getInstance().createReturnAction(inputPin5);
		
		groupAction.addVariable(result);
		groupAction.addAction(conditionalAction);
		groupAction.addAction(returnAction);
		
//		OutputPin groupPin = new OutputPin(inputPin5.getType());
//		groupAction.setResultPin(groupPin);
		
		return	groupAction;
	}
}
