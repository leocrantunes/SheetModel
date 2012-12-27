/*
 * Created on Jun 28, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.List;

import ocl20.common.CoreEnumLiteral;
import ocl20.common.CoreEnumeration;
import ocl20.evaluation.OclValue;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class OclEnumValue extends OclPrimitiveValue {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	/**
	 * Comment for <code>serialVersionUID</code>
	 */
	private	CoreEnumLiteral	value;
	private	String	enumLiteralString;

	public Object clone() {
		OclEnumValue theClone = (OclEnumValue) super.clone();
		theClone.value = value;
		if (theClone.enumLiteralString != null)
			theClone.enumLiteralString = new String(enumLiteralString);
		return	theClone;
	}

	public OclEnumValue() {
	}

	public OclEnumValue(CoreEnumLiteral	literal) {
		setCoreType(literal.getTheEnumeration());
		this.value = literal;
		this.enumLiteralString = literal.getName();
	}
	
    /**
     * @return Returns the enumLiteralString.
     */
    public String getEnumLiteralString() {
        return enumLiteralString;
    }
    /**
     * @param enumLiteralString The enumLiteralString to set.
     */
    public void setEnumLiteralString(String enumLiteralString) {
        this.enumLiteralString = enumLiteralString;
        CoreEnumeration enumType = (CoreEnumeration) this.getType();
        if (enumType != null) {
            this.value = enumType.lookupEnumLiteral(this.enumLiteralString);
        }
    }
    
	public CoreEnumLiteral	getValue() {
	    if (value == null) {
	        this.setEnumLiteralString(this.enumLiteralString);
	    }
	    
		return	value;
	}
	
	public int hashCode() {
		return this.getValue().getName().hashCode();
	}

	public OclValue executeOperation(
		String opName,
		List arguments) {
		try {
			if (opName.equals("=")) {
			   return	this.equal((OclEnumValue) arguments.get(0));
			} else if (opName.equals("<>")) {
			   return	this.notEqual((OclEnumValue) arguments.get(0));
			} else
				return	super.executeOperation(opName, arguments);
		} catch (RuntimeException e) {
			return	getOclValuesFactory().createInvalidValue();
		}
	}
	
	protected	OclValue	equal(OclValue arg) {
		if (this == arg) {
			return	getOclValuesFactory().createBooleanValue(true);
		}
		if (arg.getType() == this.getType()) {
			return getOclValuesFactory().createBooleanValue(this.getValue().getName().equals(((OclEnumValue) arg).getValue().getName()));
		} else {
			return	super.equal(arg);
		}
	}

	public String toString() {
		return this.getEnumLiteralString();
	}
}
