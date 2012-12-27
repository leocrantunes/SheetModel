/*
 * Created on 27/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;


import java.util.List;

import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclIntegerValue extends OclRealValue {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	long	intValue;

	public OclIntegerValue() {
		setCoreType(OclTypesFactory.createOclIntegerType());
	}

	public OclIntegerValue(long value) {
		setCoreType(OclTypesFactory.createOclIntegerType());
		this.setIntValue(value);
	}
	
	public Object clone() {
		OclIntegerValue theClone = (OclIntegerValue) super.clone();
		theClone.intValue = intValue;
		return	theClone;
	}

	public long getIntValue() {
	    return	 intValue;
	}
	
	public void setIntValue(long value) {
	    this.intValue = value;
	    super.setValueAsString((String.valueOf(value) + ".0"));
	}
	
	public long	intValue() {
		return	getIntValue();
	}
	
	private	boolean isArgumentReal(List arguments) {
		return	hasArguments(arguments) && (OclRealValue.class).equals(arguments.get(0).getClass());
	}
	
	public	OclValue	executeOperation(String opName, List arguments) {
		try {
			if (isArgumentUndefined(arguments)) {
				return	(OclValue) arguments.get(0);
			}
			if (isArgumentReal(arguments)) {
				// if argument is Real, result should be a Real
				return	super.executeOperation(opName, arguments); 
			}
			else if (opName.equals("+")) {
				return	this.add((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("-") && hasArguments(arguments)) {
				return	this.subtract((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("*")) {
				return	this.plus((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("div")) {
				return	this.div((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("/")) {
				return	this.divide((OclRealValue) arguments.get(0));
			} else if (opName.equals("abs") && !hasArguments(arguments)) {
				return	this.intAbs();
			} else if (opName.equals("-") && !hasArguments(arguments)) {
				return	this.intMinus();
			} else if (opName.equals("mod")) {
				return	this.mod((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("max")) {
				return	this.max((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("min")) {
				return	this.min((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("=")) {
				return	this.equal((OclValue) arguments.get(0));
			} else if (opName.equals("<>")) {
				return	this.notEqual((OclValue) arguments.get(0));
			} else if (opName.equals("toString")) {
				return	this.toStringValue();
			} else
				return	super.executeOperation(opName, arguments);
		} catch (RuntimeException e) {
			return	getOclValuesFactory().createInvalidValue();
		}
	}

	protected	OclIntegerValue	add(OclIntegerValue arg) {
		return	getOclValuesFactory().createIntegerValue(this.intValue() + arg.intValue());
	}
	
	protected	OclIntegerValue	subtract(OclIntegerValue arg) {
		return	getOclValuesFactory().createIntegerValue(this.intValue() - arg.intValue());
	}

	protected	OclIntegerValue	plus(OclIntegerValue arg) {
		return	getOclValuesFactory().createIntegerValue(this.intValue() * arg.intValue());
	}

	protected	OclIntegerValue	div(OclIntegerValue arg) {
		return	getOclValuesFactory().createIntegerValue(this.intValue() / arg.intValue());
	}

	protected	OclRealValue	divide(OclRealValue arg) {
		return	getOclValuesFactory().createRealValue(String.valueOf(this.intValue / arg.doubleValue().doubleValue()));
	}

	protected	OclIntegerValue	intAbs() {
		return	getOclValuesFactory().createIntegerValue(Math.abs(this.intValue()));
	}

	protected	OclIntegerValue	intMinus() {
		return	getOclValuesFactory().createIntegerValue(- this.intValue());
	}

	protected	OclIntegerValue	mod(OclIntegerValue arg) {
		return	getOclValuesFactory().createIntegerValue(this.intValue() % arg.intValue());
	}

	protected	OclIntegerValue	max(OclIntegerValue arg) {
		return	getOclValuesFactory().createIntegerValue(Math.max(this.intValue(), arg.intValue()));
	}

	protected	OclIntegerValue	min(OclIntegerValue arg) {
		return	getOclValuesFactory().createIntegerValue(Math.min(intValue(), arg.intValue()));
	}

	protected	OclValue	equal(OclValue arg) {
		if (arg instanceof OclIntegerValue) {
			return getOclValuesFactory().createBooleanValue(this.intValue() == ((OclIntegerValue) arg).intValue());
		} else {
			return super.equal(arg);
		}
	}

	protected	OclStringValue	toStringValue() {
		return	getOclValuesFactory().createStringValue(this.toString()); 
	}

	public String toString() {
		return	Long.toString(this.intValue());
	}
}
