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
public class OclBooleanValue extends OclPrimitiveValue  {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	boolean	value;
	
	public OclBooleanValue() {
		this(false);
	}
	
	public Object clone() {
		OclBooleanValue theClone = (OclBooleanValue) super.clone();
		theClone.value = value;
		return	theClone;
	}

	public OclBooleanValue(boolean value) {
		setCoreType(OclTypesFactory.createOclBooleanType());
		this.value = value;
	}

	public OclBooleanValue(OclBooleanValue value) {
		setCoreType(value.getType());
		this.value = value.booleanValue();
	}

	public	boolean	getValue() {
	    return	value;
	}

	public	void setValue(boolean value) {
	    this.value = value;
	}

	public boolean	booleanValue() {
		return	getValue();
	}

	public int hashCode() {
		return new Boolean(value).hashCode();
	}

	// modify to include three-value boolean logic

	public OclValue executeOperation(
		String opName,
		List arguments) {
		try {
			if (opName.equals("or")) {
				return	this.or((OclValue) arguments.get(0));
			} else if (opName.equals("not")) {
				return	this.not();
			} else if (opName.equals("xor")) {
				return	this.xor((OclValue) arguments.get(0));
			} else if (opName.equals("and")) {
				return	this.and((OclValue) arguments.get(0));
			} else if (opName.equals("implies")) {
				return	this.implies((OclValue) arguments.get(0));
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


	protected	OclValue	or(OclValue arg) {
		if (arg instanceof OclBooleanValue) {
			return	getOclValuesFactory().createBooleanValue(this.booleanValue() || ((OclBooleanValue) arg).booleanValue());
		} else if (arg instanceof OclUndefinedValue) {
			if (this.isTrue()) {
				return	getOclValuesFactory().createBooleanValue(true);  // true or undefined = true 
			} else {
				return	arg;			// false or undefined = undefined
			}
		} else
			return	getOclValuesFactory().createInvalidValue();
	}
	
	protected	OclValue	and(OclValue arg) {
		if (arg instanceof OclBooleanValue) {
			return	getOclValuesFactory().createBooleanValue(this.booleanValue() && ((OclBooleanValue) arg).booleanValue());
		} else if (arg instanceof OclUndefinedValue) {
			if (this.isTrue()) {
				return	arg;						// true and undefined = undefined
			} else {
				return	getOclValuesFactory().createBooleanValue(false);  // false and undefined = false
			}
		} else
			return	getOclValuesFactory().createInvalidValue();
	}

	protected	OclValue	xor(OclValue arg) {
		if (arg instanceof OclBooleanValue) {
			return	getOclValuesFactory().createBooleanValue(this.booleanValue() ^ ((OclBooleanValue) arg).booleanValue());
		} else if (arg instanceof OclUndefinedValue) {
			return	arg;
		} else {
			return	getOclValuesFactory().createInvalidValue();
		}
	}

	protected	OclValue	implies(OclValue arg) {
		if (arg instanceof OclBooleanValue) {
			boolean isPremiseFalse = ! this.booleanValue();
			boolean arePremiseAndConsequentTrue = this.booleanValue() && ((OclBooleanValue) arg).booleanValue();
			return	getOclValuesFactory().createBooleanValue(isPremiseFalse || arePremiseAndConsequentTrue);
		} else if (arg instanceof OclUndefinedValue) {
			if (this.isTrue()) {
				return	arg;									// true implies undefined = undefined	
			} else {
				return	getOclValuesFactory().createBooleanValue(true);  // false implies * = true
			}
		} else
			return	getOclValuesFactory().createInvalidValue();
	}

	protected	OclBooleanValue	not() {
		return	getOclValuesFactory().createBooleanValue(! this.booleanValue());
	}

	protected	OclValue	equal(OclValue arg) {
		if (arg instanceof OclBooleanValue) {
			return getOclValuesFactory().createBooleanValue(this.booleanValue() == ((OclBooleanValue) arg).booleanValue());
		} else {
			return	super.equal(arg);
		}
	}

	protected	OclStringValue	toStringValue() {
		return	getOclValuesFactory().createStringValue(this.toString()); 
	}
	
	public String toString() {
		return	Boolean.toString(this.booleanValue());
	}
	
}
