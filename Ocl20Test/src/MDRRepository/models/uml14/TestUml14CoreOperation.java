/*
 * Created on Feb 28, 2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml14;

import java.util.ArrayList;
import java.util.List;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;




/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestUml14CoreOperation extends TestUml14CoreModelElement {
	public TestUml14CoreOperation(String arg0) throws Exception {
		super(arg0);
	}

	private List getOperation(String packageName, String classifierName, String operationName) {
		CoreClassifier aClassifier = getClassifier(packageName, classifierName);
		List operations = getModelElements(aClassifier.getClassifierFeatures(), operationName, CoreOperation.class);
		assertTrue(operations.size() > 0);
		return operations;
	}

	private List getOperation(String classifierName, String operationName) {
		CoreClassifier aClassifier = (CoreClassifier) getModelElement(model.getElemOwnedElements(), classifierName, CoreClassifier.class);
		assertNotNull(aClassifier);
		List operations = getModelElements(aClassifier.getClassifierFeatures(), operationName, CoreOperation.class);
		assertTrue(operations.size() > 0);
		return operations;
	}


	public void testSingleInstanceOperation() {
		List operations = getOperation("MyExample", "Film", "getRentalFee");
		assertEquals(1, operations.size());
		CoreOperation theOperation = (CoreOperation) operations.get(0);
		assertEquals("Real", theOperation.getReturnType().getName());
		List paramTypes = new ArrayList(theOperation.getParametersTypesExceptReturn());
		assertEquals(1, paramTypes.size());
		CoreClassifier param1 = (CoreClassifier) paramTypes.get(0);
		assertEquals("Integer", param1.getName());
		assertTrue(theOperation.isInstanceScope());
		assertEquals("getRentalFee(dayOfWeek : Integer) : Real", theOperation.getFullSignatureAsString());
	}

	public void testMultipleInstanceOperation() {
		CoreClassifier specialFilm = (CoreClassifier) getModelElement(model.getElemOwnedElements(), "SpecialFilm", CoreClassifier.class);
		
		List operations = getOperation("SpecialFilm", "doSomething");
		assertEquals(2, operations.size());
		
		CoreOperation theOperation = (CoreOperation) operations.get(0);
		
		assertNull(theOperation.getReturnType());
		List paramTypes = new ArrayList(theOperation.getParametersTypesExceptReturn());
		assertEquals(0, paramTypes.size());
		assertEquals("doSomething()", theOperation.getFullSignatureAsString());
		assertTrue(theOperation.isInstanceScope());
		
		
		theOperation = (CoreOperation) operations.get(1);
		assertEquals("Integer", theOperation.getReturnType().getName());
		
		paramTypes = new ArrayList(theOperation.getParametersTypesExceptReturn());
		assertEquals(3, paramTypes.size());
		assertEquals("Integer", ((CoreClassifier) paramTypes.get(0)).getName());
		assertEquals("Integer", ((CoreClassifier) paramTypes.get(1)).getName());
		assertEquals("Real", ((CoreClassifier) paramTypes.get(2)).getName());
		
		assertEquals("doSomething(param1 : Integer, param2 : Integer, param3 : Real) : Integer", theOperation.getFullSignatureAsString());
		
		assertTrue(theOperation.isInstanceScope());
		assertEquals(specialFilm, theOperation.getFeatureOwner());
		assertEquals(specialFilm, theOperation.getElemOwner());
		
		List  argTypes = new ArrayList();
		CoreClassifier intClassifier = ((CoreClassifier) paramTypes.get(0));
		CoreClassifier realClassifier = ((CoreClassifier) paramTypes.get(2));
		assertNotNull(intClassifier);
		assertNotNull(realClassifier);
		argTypes.add(intClassifier);
		argTypes.add(intClassifier);
		argTypes.add(realClassifier);
		
		assertTrue(theOperation.hasSameSignature(argTypes, intClassifier));

		argTypes.clear();
		argTypes.add(intClassifier);
		argTypes.add(intClassifier);
		argTypes.add(intClassifier);
		
		assertFalse(theOperation.hasSameSignature(argTypes, intClassifier));

//		argTypes.clear();
//		for (int i = 0; i < 3; i++)
//			argTypes.add(intClassifier);
//		assertTrue(theOperation.hasMatchingSignature(argTypes));

		argTypes.clear();
		argTypes.add(intClassifier);
		assertFalse(theOperation.hasMatchingSignature(argTypes));

	}

	public void testStaticOperation() {
		List operations = getOperation("MyExample", "Tape", "tapesQty");
		assertEquals(1, operations.size());
		
		CoreOperation theOperation = (CoreOperation) operations.get(0);
		assertEquals("Integer", theOperation.getReturnType().getName());
		List paramTypes = new ArrayList(theOperation.getParametersTypesExceptReturn());
		assertEquals(0, paramTypes.size());
		assertFalse(theOperation.isInstanceScope());
	}

	public void testStereotype() {
		List operations = getOperation("MyExample", "Film", "getRentalFee");
		CoreOperation theOperation = (CoreOperation) operations.get(0);
		assertTrue(theOperation.hasStereotype("query"));
	}
}
