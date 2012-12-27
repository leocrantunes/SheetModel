/*
 * Created on Dec 26, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import impl.ocl20.constraints.ExpressionInOclImpl;

import java.util.ArrayList;
import java.util.List;

import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.constraints.OclBodyConstraint;
import ocl20.constraints.OclDeriveConstraint;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class TestDefConstraints extends TestPropertyCallExp {

	public void testNewAttribute_01() {
		doTestManyContextOK("context Film def: newAttr : Integer = 10 context SpecialFilm inv: newAttr > 10",     
				getCurrentMethodName());
		CoreClassifier film = (CoreClassifier) environment.lookup("Film");
		OclDeriveConstraint constraint = (OclDeriveConstraint) film.getDeriveConstraint("newAttr");
		assertNotNull(constraint);
		assertEquals("Integer", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());		
	}

	public void testNewAttributeOK_02() {
		doTestContextNotOK("context Film def: newAttr : Integer = true",     
				getCurrentMethodName());
		CoreClassifier film = (CoreClassifier) environment.lookup("Film");
		CoreAttribute attr = (CoreAttribute) film.lookupAttribute("newAttr");
		assertNotNull(attr);
		assertEquals("Integer", attr.getFeatureType().getName());
		assertNull(film.getDeriveConstraint("newAttr"));
	}

	public void testNewAttributeOK_03() {
		doTestContextOK("context Film def: newAttr : Integer = 10  def: newAttr2 : Boolean = true",     
				getCurrentMethodName());
		CoreClassifier film = (CoreClassifier) environment.lookup("Film");

		assertNotNull((CoreAttribute) film.lookupAttribute("newAttr"));
		OclDeriveConstraint constraint = (OclDeriveConstraint) film.getDeriveConstraint("newAttr");
		assertNotNull(constraint);
		assertEquals("Integer", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
		
		assertNotNull((CoreAttribute) film.lookupAttribute("newAttr2"));
		constraint = (OclDeriveConstraint) film.getDeriveConstraint("newAttr2");
		assertNotNull(constraint);
		assertEquals("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());		
	}


	public void testNewAttribute_02() {
		doTestContextNotOK("context Film def: name : Integer = 10",     
				getCurrentMethodName());
	}

	public void testNewAttribute_03() {
		doTestContextNotOK("context Film def: newAttr = 10",     
				getCurrentMethodName());
	}

	public void testNewAttribute_04() {
		doTestContextNotOK("context Film def: newAttr : Integer",     
				getCurrentMethodName());
	}

	public void testNewAttribute_05() {
		doTestManyContextNotOK("context Film def: newAttr : Integer = 20 context SpecialFilm def: newAttr : Integer = 10",     
				getCurrentMethodName());
	}

	public void testNewAttribute_06() {
		doTestContextNotOK("context Film def: newAttr : Integer = 10 def: newAttr : Boolean = true",     
				getCurrentMethodName());
	}

	public void testNewAttribute_07() {
		doTestManyContextNotOK("context SpecialFilm def: abc : Integer = 20 context Film def: abc : Integer = 10",     
				getCurrentMethodName());
	}

	public void testNewAttribute_08() {
		doTestContextNotOK("context Film def: name : Integer = true",     
				getCurrentMethodName());
	}

	public void testNewAttribute_09() {
		doTestManyContextNotOK("context Film def: newAttr : Integer = 10  context Film def: newAttr : Integer = 20",     
				getCurrentMethodName());
	}

	public void testNewOperation_01() {
		doTestContextOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"",     
				getCurrentMethodName());
		CoreClassifier classifier = (CoreClassifier) environment.lookup("SpecialFilm");		
		List paramTypes = new ArrayList();
		CoreClassifier returnType = (CoreClassifier) environment.lookup("Boolean");
		
		CoreOperation definedOperation = classifier.lookupSameSignatureOperation("isSpecialFilm", paramTypes, returnType);
		assertNotNull(definedOperation);
		assertNotNull(definedOperation.getBodyDefinition());
		OclBodyConstraint	constraint = definedOperation.getBodyDefinition();
		assertNotNull(constraint);
		assertEquals("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
	}

	public void testNewOperation_02() {
		doTestContextNotOK("context SpecialFilm def: isSpecialFilm() : Boolean = 10 + 20",     
				getCurrentMethodName());
	}

	public void testNewOperation_03() {
		doTestContextOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\" " +
			"inv: tapes->size() > 0 implies isSpecialFilm()",     
				getCurrentMethodName());
	}

	public void testNewOperation_NotOK01() {
		doTestContextNotOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"  def: isSpecialFilm() : Boolean = name = \"Special\"",     
				getCurrentMethodName());
				
		CoreClassifier classifier = (CoreClassifier) environment.lookup("SpecialFilm");		
		List paramTypes = new ArrayList();
		CoreClassifier returnType = (CoreClassifier) environment.lookup("Boolean");
		
		CoreOperation definedOperation = classifier.lookupSameSignatureOperation("isSpecialFilm", paramTypes, returnType);
		assertNotNull(definedOperation);
		assertNotNull(definedOperation.getBodyDefinition());
		OclBodyConstraint	constraint = definedOperation.getBodyDefinition();
		assertNotNull(constraint);
		assertEquals("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
	}

	public void testNewOperation_NotOK02() {
		doTestManyContextNotOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"  context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"",     
				getCurrentMethodName());
				
		CoreClassifier classifier = (CoreClassifier) environment.lookup("SpecialFilm");		
		List paramTypes = new ArrayList();
		CoreClassifier returnType = (CoreClassifier) environment.lookup("Boolean");
		
		CoreOperation definedOperation = classifier.lookupSameSignatureOperation("isSpecialFilm", paramTypes, returnType);
		assertNotNull(definedOperation);
		assertNotNull(definedOperation.getBodyDefinition());
		OclBodyConstraint	constraint = definedOperation.getBodyDefinition();
		assertNotNull(constraint);
		assertEquals("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
	}

	public void testNewOperation_NotOK03() {
		doTestManyContextNotOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"  context SpecialFilm::isSpecialFilm() : Boolean body: true",     
				getCurrentMethodName());
				
	}

	public void testNewOperation_OK04() {
		doTestManyContextOK("context SpecialFilm def: isSpecialFilm() : Boolean = name = \"Special\"  context SpecialFilm::isSpecialFilm() : Boolean post: true",     
				getCurrentMethodName());
				
		CoreClassifier classifier = (CoreClassifier) environment.lookup("SpecialFilm");		
		List paramTypes = new ArrayList();
		CoreClassifier returnType = (CoreClassifier) environment.lookup("Boolean");
		
		CoreOperation definedOperation = classifier.lookupSameSignatureOperation("isSpecialFilm", paramTypes, returnType);
		assertNotNull(definedOperation);
		assertNotNull(definedOperation.getBodyDefinition());
		OclBodyConstraint	constraint = definedOperation.getBodyDefinition();
		assertNotNull(constraint);
		assertEquals("Boolean", ((ExpressionInOclImpl) constraint.getExpression()).getBodyExpression().getType().getName());
	}

	public void testNewOperation_OK05() {
		doTestManyContextOK("context SpecialFilm def: xpto1000() : Integer = 1000  \r\n  context SpecialFilm def: abc : Integer = xpto1000() \r\n",     
				getCurrentMethodName());
	}

	
}
