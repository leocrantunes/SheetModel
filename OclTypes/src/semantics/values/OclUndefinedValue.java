/*
 * Created on Apr 28, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.List;

import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclUndefinedValue extends OclValueImpl {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	public OclUndefinedValue() {
		setCoreType(OclTypesFactory.getType("OclVoid"));
	}
	
	public OclValue executeOperation(
		String opName,
		List arguments) {
		try {
			if (opName.equals("oclIsUndefined")) {
				return	getOclValuesFactory().createBooleanValue(true);
			} else if (opName.equals("or")) {
				return	this.or((OclValue) arguments.get(0));
			} else if (opName.equals("not")) {
				return	this.not();
			} else if (opName.equals("xor")) {
				return	this.xor((OclValue) arguments.get(0));
			} else if (opName.equals("and")) {
				return	this.and((OclValue) arguments.get(0));
			} else if (opName.equals("implies")) {
				return	this.implies((OclValue) arguments.get(0));
			} else
				return	super.executeOperation(opName, arguments);
		} catch (RuntimeException e) {
			return	this;
		}
	}

	protected	OclValue	or(OclValue arg) {
		if (arg.isTrue()) {
			return 	arg;	// undefined or true = true 
		} else {
			return	getReturnValue(arg);
		}
	}
	
	protected	OclValue	and(OclValue arg) {
		if (arg.isFalse()) {
			return 	arg;		// undefined and false = false
		} else {
			return	getReturnValue(arg);
		}
	}

	protected	OclValue	xor(OclValue arg) {
		return	getReturnValue(arg);
	}

	protected	OclValue	implies(OclValue arg) {
		if (arg.isTrue()) {
			return	arg;	// undefined implies true = true	
		} else {
			return	getReturnValue(arg);
		}
	}

	protected	OclValue	not() {
		return	this;
	}
	
	protected	OclValue	getReturnValue(OclValue arg) {
		if (arg.oclIsInvalid())	{
			return	arg;
		} else {
			return	this;
		}
	}
}
