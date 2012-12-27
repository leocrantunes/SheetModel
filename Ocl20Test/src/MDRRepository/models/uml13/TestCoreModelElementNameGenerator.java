/*
 * Created on 05/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.MDRRepository.models.uml13;

import impl.ocl20.common.CoreModelElementNameGeneratorImpl;
import impl.ocl20.common.ModelElementNameGenerator;

import java.util.Iterator;

import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreModelElement;
import ocl20.common.CoreOperation;
import ocl20.common.CorePackage;




/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class TestCoreModelElementNameGenerator extends TestUml13CoreModelElement  {

	private	ModelElementNameGenerator nameGenerator = CoreModelElementNameGeneratorImpl.getInstance();
	
	public TestCoreModelElementNameGenerator(String arg0) throws Exception {
		super(arg0);
	}
	
	
	protected CorePackage getPackage(CorePackage pkg, String name) {
		for (Iterator it = pkg.getElemOwnedElements().iterator();
			it.hasNext();
			) {
			CoreModelElement element = (CoreModelElement) it.next();

			if (element instanceof CorePackage && element.getName().equals(name))
				return (CorePackage) element;
			}
		return null;								
	}

	protected CoreClassifier getClassifier(CorePackage pkg, String name) {
		for (Iterator it = pkg.getElemOwnedElements().iterator();
			it.hasNext();
			) {
			CoreModelElement element = (CoreModelElement) it.next();

			if (element instanceof CoreClassifier && element.getName().equals(name))
				return (CoreClassifier) element;
			}
							
		return null;							
	}

	protected CoreAttribute getAttribute(CoreClassifier cls, String name) {
		for (Iterator it = cls.getClassifierFeatures().iterator();
			it.hasNext();
			) {
			CoreModelElement element = (CoreModelElement) it.next();

			if (element instanceof CoreAttribute && element.getName().equals(name))
				return (CoreAttribute) element;
			}
							
		return null;							
	}

	protected CoreOperation getOperation(CoreClassifier cls, String name) {
		for (Iterator it = cls.getClassifierFeatures().iterator();
			it.hasNext();
			) {
			CoreModelElement element = (CoreModelElement) it.next();

			if (element instanceof CoreOperation && element.getName().equals(name))
				return (CoreOperation) element;
			}
							
		return null;							
	}


	public void testPackageNameGenerator() {
		CorePackage	pkg = getPackage(model, "MyExample");
		
		assertNotNull(pkg);
		assertEquals("MyExample", nameGenerator.generateName(pkg));
		
		pkg = getPackage(pkg, "package_1");
		assertNotNull(pkg);
		assertEquals("package_1", nameGenerator.generateName(pkg));
	}
	
	public void testClassifierNameGenerator() {
		CorePackage	pkg = getPackage(model, "MyExample");
		
		assertNotNull(pkg);
		
		CoreClassifier cls = getClassifier(pkg, "Film");
		assertNotNull(cls);
		assertEquals("Film", nameGenerator.generateName(cls));
		
		cls = getClassifier(pkg, "Tape");
		assertNotNull(cls);
		assertEquals("Tape", nameGenerator.generateName(cls));
	}
	
	public void testAttributeNameGenerator() {
		CorePackage	pkg = getPackage(model, "MyExample");
		
		assertNotNull(pkg);
		
		CoreClassifier cls = getClassifier(pkg, "Film");
		assertNotNull(cls);

		CoreAttribute att = getAttribute(cls, "name");
		
		assertNotNull(att);
		assertEquals("name", nameGenerator.generateName(att));
	}

	public void testOperationNameGenerator() {
		CorePackage	pkg = getPackage(model, "MyExample");
		
		assertNotNull(pkg);
		
		CoreClassifier cls = getClassifier(pkg, "Film");
		assertNotNull(cls);

		CoreOperation op = getOperation(cls, "getRentalFee");
		
		assertNotNull(op);
		assertEquals("getRentalFee$$Integer", nameGenerator.generateName(op));
		
		op = getOperation(cls, "getTapes");
		assertNotNull(op);
		assertEquals("getTapes", nameGenerator.generateName(op));
	}

	public void testOperationNameMatches() {
		assertTrue(nameGenerator.operationNameMatches("op", "op"));
		assertTrue(nameGenerator.operationNameMatches("op$$int", "op"));
		assertTrue(nameGenerator.operationNameMatches("op$$int$$double", "op"));
		assertFalse(nameGenerator.operationNameMatches("oper", "op"));
	}

}
