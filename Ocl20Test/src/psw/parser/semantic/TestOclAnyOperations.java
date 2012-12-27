/*
 * Created on Jul 29, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.parser.semantic;

import java.util.List;

import ocl20.expressions.OclExpression;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class TestOclAnyOperations extends TestPropertyCallExp {

	public void testEqual() {
		List constraints	= doTestContextOK("context Tape inv: self = self",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallExp(oclExpression, "=", "Boolean");
	}

	public void testNotEqual() {
		List constraints	= doTestContextOK("context Tape inv: self <> self",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallExp(oclExpression, "<>", "Boolean");
	}

	public void testOclIsNew() {
		List constraints	= doTestContextOK("context Tape inv: self.oclIsNew()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallExp(oclExpression, "oclIsNew", "Boolean");
	}

	public void testOclIsUndefined() {
		List constraints	= doTestContextOK("context Tape inv: Set{1, 2}->asSequence()->first().oclIsUndefined()",     
				getCurrentMethodName());
	
		OclExpression oclExpression = getConstraintExpression(constraints);
		checkOperationCallExp(oclExpression, "oclIsUndefined", "Boolean");
	}

	public void testOclAsType() {
		List constraints	= doTestContextOK("context Tape inv: self.theFilm.oclAsType(SpecialFilm).lateReturnFee > 40.0",     
				getCurrentMethodName());
	}
	
	public void testOclIsTypeOf() {
		List constraints	= doTestContextOK("context Tape inv: self.theFilm.oclIsTypeOf(SpecialFilm)",     
				getCurrentMethodName());
	}

	public void testOclIsKindOf() {
		List constraints	= doTestContextOK("context Tape inv: self.theFilm.oclIsKindOf(SpecialFilm)",     
				getCurrentMethodName());
	}

	public void testAllInstances() {
		List constraints	= doTestContextOK("context Tape def: allTapes : Set(Tape) = Tape::allInstances()",     
				getCurrentMethodName());
	}

	public void testAllInstances_02() {
		List constraints	= doTestContextOK("context Tape inv : 20 = Tape::allInstances()->size()",     
				getCurrentMethodName());
	}
	
	public void testOclAnyType() {
		List constraints	= doTestContextOK("context Tape inv: let x : OclAny = self in x.oclIsUndefined()",     
				getCurrentMethodName());
	}
	
	public void testOclVoid() {
		List constraints	= doTestContextOK("context Tape inv: let a : Set(OclVoid) = Set{} in a->forAll(x : OclVoid | x.oclIsUndefined())",     
				getCurrentMethodName());
	}
}
