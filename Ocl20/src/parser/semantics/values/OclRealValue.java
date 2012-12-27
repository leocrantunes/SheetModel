/*
 * Created on 27/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.math.BigDecimal;
import java.util.List;

import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclRealValue extends OclPrimitiveValue {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
//	private	double	value;
	private BigDecimal value;
	private	String	valueAsString;
	
	public Object clone() {
		OclRealValue theClone = (OclRealValue) super.clone();
		theClone.value = new BigDecimal(value.toString());
		theClone.valueAsString = valueAsString;
		return	theClone;
	}

	public OclRealValue() {
	    this.setCoreType(OclTypesFactory.createOclRealType());
	    value = new BigDecimal(0);
	    valueAsString = value.toString();
	}
	
//	public OclRealValue(double value) {
//		this.setCoreType(OclTypesFactory.createOclRealType());
//		this.value = new BigDecimal(value);
//	}

	public OclRealValue(String value) {
		this.setCoreType(OclTypesFactory.createOclRealType());
		this.value = new BigDecimal(value);
		this.valueAsString = value;
	}

	public OclRealValue(BigDecimal value) {
		this.setCoreType(OclTypesFactory.createOclRealType());
		this.value = value;
		this.valueAsString = value.toString();
	}
	
	
	public String getValueAsString() {
	    return	valueAsString;
	}
	
	public void setValueAsString(String value) {
		this.valueAsString = value;
	    this.value = new BigDecimal(value);
	}
	
	public BigDecimal doubleValue() {
		return	value;
	}
	
	public int hashCode() {
		return value.hashCode();
	}

	public	OclValue	executeOperation(String opName, List arguments) {
		try {
			 if (isArgumentUndefined(arguments)) {
			 	return	(OclValue) arguments.get(0);
			 }
			 if (opName.equals("+")) {
				return	this.add((OclRealValue) arguments.get(0));
			 } else if (opName.equals("-") && hasArguments(arguments)) {
				return	this.subtract((OclRealValue) arguments.get(0));
			 } else if (opName.equals("*")) {
				return	this.plus((OclRealValue) arguments.get(0));
			 } else if (opName.equals("/")) {
				return	this.div((OclRealValue) arguments.get(0));
			 } else if (opName.equals("abs")) {
				return	this.realAbs();
			 } else if (opName.equals("-") && !hasArguments(arguments)) {
				return	this.realMinus();
			 } else if (opName.equals("floor")) {
				return	this.floor();
			 } else if (opName.equals("round")) {
				return	this.round();
			 } else if (opName.equals("max")) {
				return	this.max((OclRealValue) arguments.get(0));
			 } else if (opName.equals("min")) {
				return	this.min((OclRealValue) arguments.get(0));
			 } else if (opName.equals("<")) {
				return	this.less((OclRealValue) arguments.get(0));
			 } else if (opName.equals("<=")) {
				return	this.lessEqual((OclRealValue) arguments.get(0));
			 } else if (opName.equals(">")) {
				return	this.greater((OclRealValue) arguments.get(0));
			 } else if (opName.equals(">=")) {
				return	this.greaterEqual((OclRealValue) arguments.get(0));
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

	protected	OclRealValue	add(OclRealValue arg) {
		return	getOclValuesFactory().createRealValue(this.doubleValue().add(arg.doubleValue()));
	}
	
	protected	OclRealValue	subtract(OclRealValue arg) {
		return	getOclValuesFactory().createRealValue(this.doubleValue().subtract(arg.doubleValue()));
	}

	protected	OclRealValue	plus(OclRealValue arg) {
		return	getOclValuesFactory().createRealValue(this.doubleValue().multiply(arg.doubleValue()));
	}

	protected	OclRealValue	div(OclRealValue arg) {
		return	getOclValuesFactory().createRealValue(String.valueOf(this.doubleValue().doubleValue() / arg.doubleValue().doubleValue()));
	}

	protected	OclRealValue	realAbs() {
		return	getOclValuesFactory().createRealValue(this.doubleValue().abs());
	}
	
	protected	OclRealValue	realMinus() {
		return	getOclValuesFactory().createRealValue(this.doubleValue().negate());
	}

	protected	OclIntegerValue	round() {
		return	getOclValuesFactory().createIntegerValue(Math.round(this.doubleValue().doubleValue()));
	}

	protected	OclIntegerValue	floor() {
		return	getOclValuesFactory().createIntegerValue(this.doubleValue().longValue());
	}

	protected	OclRealValue	max(OclRealValue arg) {
		return	getOclValuesFactory().createRealValue(this.doubleValue().max(arg.doubleValue()));
	}

	protected	OclRealValue	min(OclRealValue arg) {
		return	getOclValuesFactory().createRealValue(this.doubleValue().min(arg.doubleValue()));
	}

	protected	OclBooleanValue	less(OclRealValue arg) {
		return	getOclValuesFactory().createBooleanValue(this.doubleValue().compareTo(arg.doubleValue()) < 0);
	}

	protected	OclBooleanValue	lessEqual(OclRealValue arg) {
		return	getOclValuesFactory().createBooleanValue(this.doubleValue().compareTo(arg.doubleValue()) <= 0);
	}

	protected	OclBooleanValue	greater(OclRealValue arg) {
		return	getOclValuesFactory().createBooleanValue(this.doubleValue().compareTo(arg.doubleValue()) > 0);
	}

	protected	OclBooleanValue	greaterEqual(OclRealValue arg) {
		return	getOclValuesFactory().createBooleanValue(this.doubleValue().compareTo(arg.doubleValue()) >= 0);
	}
	
	protected	OclValue	equal(OclValue arg) {
		if (arg instanceof OclRealValue || arg instanceof OclIntegerValue) {
			return getOclValuesFactory().createBooleanValue(this.doubleValue().compareTo(((OclRealValue)arg).doubleValue()) == 0);
		} else {
			return	super.equal(arg);
		}
	}

	protected	OclStringValue	toStringValue() {
		return	getOclValuesFactory().createStringValue(this.toString()); 
	}

	public String toString() {
		return this.doubleValue().toString();
	}
}
