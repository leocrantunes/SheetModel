/*
 * Created on 27/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.text.DateFormat;
import java.util.Date;
import java.util.List;
import java.util.regex.Pattern;

import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclStringValue extends OclPrimitiveValue {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	String	value;
	
	public Object clone() {
		OclStringValue theClone = (OclStringValue) super.clone();
		if (value != null)
			theClone.value = new String(value);
		return	theClone;
	}


	public OclStringValue() {
		setCoreType(OclTypesFactory.createOclStringType());
		value = new String("");
	}

	public OclStringValue(String value) {
		setCoreType(OclTypesFactory.createOclStringType());
		if (value != null)
			this.value = value;
		else
			this.value = new String("");
	}
	
	public String	getValue() {
	    return	this.value;
	}
	
	public void setValue(String value) {
	    this.value = value;
	}
	
	public String	stringValue() {
		return	getValue();
	}
	
	public int hashCode() {
		return value.hashCode();
	}

	public OclValue executeOperation(
		String opName,
		List arguments) {
		try {
			if (opName.equals("size")) {
			   return	this.size();
			} else if (opName.equals("concat") || opName.equals("+")) {
			   return	this.concat((OclStringValue) arguments.get(0));
			} else if (opName.equals("substring")) {
			   return	this.substring((OclIntegerValue) arguments.get(0), (OclIntegerValue) arguments.get(1));
			} else if (opName.equals("toInteger")) {
			   return	this.toInteger();
			} else if (opName.equals("toReal")) {
			   return	this.toReal();
			} else if (opName.equals("toDate")) {
				   return	this.toDate();
			} else if (opName.equals("toDateTime")) {
				   return	this.toDateTime();
			} else if (opName.equals("=")) {
			   return	this.equal((OclValue) arguments.get(0));
			} else if (opName.equals("<>")) {
			   return	this.notEqual((OclValue) arguments.get(0));
			} else if (opName.equals("<")) {
			   return	this.lessThan((OclValue) arguments.get(0));
			} else if (opName.equals("startsWith")) {
			   return	this.startsWith((OclStringValue) arguments.get(0));
			} else if (opName.equals("endsWith")) {
			   return	this.endsWith((OclStringValue) arguments.get(0));
			} else if (opName.equals("indexOf") && arguments.size() == 1) {
			   return	this.indexOf((OclStringValue) arguments.get(0));
			} else if (opName.equals("indexOf") && arguments.size() == 2) {
			   return	this.indexOf((OclStringValue) arguments.get(0), (OclIntegerValue) arguments.get(1));
			} else if (opName.equals("lastIndexOf") && arguments.size() == 1) {
			   return	this.lastIndexOf((OclStringValue) arguments.get(0));
			} else if (opName.equals("lastIndexOf") && arguments.size() == 2) {
			   return	this.lastIndexOf((OclStringValue) arguments.get(0), (OclIntegerValue) arguments.get(1));
			} else if (opName.equals("trim")) {
			   return	this.trim();
			} else if (opName.equals("ltrim")) {
			   return	this.lTrim();
			} else if (opName.equals("rtrim")) {
			   return	this.rTrim();
			} else if (opName.equals("like")) {
			   return	this.like((OclStringValue) arguments.get(0));
			} else if (opName.equals("toUpper")) {
			   return	this.toUpper();
			} else if (opName.equals("toLower")) {
			   return	this.toLower();
			} else
				return	super.executeOperation(opName, arguments);
		} catch (RuntimeException e) {
			return	getOclValuesFactory().createInvalidValue();
		}
	}
	
	protected	OclIntegerValue	size() {
		return	getOclValuesFactory().createIntegerValue(this.stringValue().length());
	}

	protected	OclStringValue	concat(OclStringValue arg) {
		return	getOclValuesFactory().createStringValue(this.stringValue() + arg.stringValue());
	}

	protected	OclStringValue	substring(OclIntegerValue lower, OclIntegerValue upper) {
		return	getOclValuesFactory().createStringValue(this.stringValue().substring((int) lower.intValue() - 1, (int) upper.intValue()));
	}

	protected	OclIntegerValue	toInteger() {
		return	getOclValuesFactory().createIntegerValue(Integer.parseInt(this.stringValue()));
	}

	protected	OclRealValue	toReal() {
		return	getOclValuesFactory().createRealValue(this.stringValue());
	}

	protected	OclValue	toDate() {
		OclValue result;
		try {
			if ("now".equals(this.stringValue())) {
				result = getOclValuesFactory().createDateValue(new Date());
			} else {
				DateFormat dateFormat = DateFormat.getDateInstance();
				dateFormat.setLenient(false);
				result = getOclValuesFactory().createDateValue(dateFormat.parse(this.stringValue()));	
			}
		} catch (Exception e) {
			result = getOclValuesFactory().createInvalidValue();
		}
//		System.out.println("to date - from: " + this.stringValue() + "  to: " + result);
		return	result;
	}
	
	protected	OclValue toDateTime() {
		OclValue result;
		DateFormat dateFormat = DateFormat.getDateTimeInstance();
		dateFormat.setLenient(false);
		try {
			if ("now".equals(this.stringValue())) {
				result = getOclValuesFactory().createDateTimeValue(new Date());
			} else {
				result = getOclValuesFactory().createDateTimeValue(dateFormat.parse(this.stringValue()));	
			}
		} catch (Exception e) {
			result = getOclValuesFactory().createInvalidValue();
		}
//		System.out.println("to datetime - from: " + this.stringValue() + "  to: " + result);
		return	result;
	}

	protected	OclValue	equal(OclValue arg) {
		if (arg instanceof OclStringValue) {
			return getOclValuesFactory().createBooleanValue(this.stringValue().equals(((OclStringValue) arg).stringValue()));
		} else {
			return	super.equal(arg);
		}
	}

	protected	OclBooleanValue	lessThan(OclValue arg) {
		return	getOclValuesFactory().createBooleanValue(this.stringValue().compareTo( ((OclStringValue) arg).stringValue()) < 0);
	}

	protected	OclBooleanValue	startsWith(OclStringValue arg) {
		if (arg.size().intValue() == 0)
			return	getOclValuesFactory().createBooleanValue(false);
		else
			return	getOclValuesFactory().createBooleanValue(this.stringValue().startsWith(arg.stringValue()));
	}

	protected	OclBooleanValue	endsWith(OclStringValue arg) {
		if (arg.size().intValue() == 0)
			return	getOclValuesFactory().createBooleanValue(false);
		else
			return	getOclValuesFactory().createBooleanValue(this.stringValue().endsWith(arg.stringValue()));
	}

	protected	OclIntegerValue	indexOf(OclStringValue arg) {
		if (this.size().intValue() == 0 || arg.size().intValue() == 0)
			return	getOclValuesFactory().createIntegerValue(0);
		else
			return	getOclValuesFactory().createIntegerValue(this.stringValue().indexOf(arg.stringValue()) + 1);
	}

	protected	OclIntegerValue	indexOf(OclStringValue arg, OclIntegerValue fromIndex) {
		if (this.size().intValue() == 0 || arg.size().intValue() == 0)
			return	getOclValuesFactory().createIntegerValue(0);
		else
			return	getOclValuesFactory().createIntegerValue(this.stringValue().indexOf( arg.stringValue(), (int) fromIndex.intValue() - 1) + 1);
	}

	protected	OclIntegerValue	lastIndexOf(OclStringValue arg) {
		if (this.size().intValue() == 0 || arg.size().intValue() == 0)
			return	getOclValuesFactory().createIntegerValue(0);
		else
			return	getOclValuesFactory().createIntegerValue(this.stringValue().lastIndexOf( arg.stringValue()) + 1);
	}

	protected	OclIntegerValue	lastIndexOf(OclStringValue arg, OclIntegerValue fromIndex) {
		if (this.size().intValue() == 0 || arg.size().intValue() == 0)
			return	getOclValuesFactory().createIntegerValue(0);
		else
			return	getOclValuesFactory().createIntegerValue(this.stringValue().lastIndexOf( arg.stringValue(), (int) fromIndex.intValue() - 1) + 1);
	}

	protected	OclStringValue	trim() {
		return	getOclValuesFactory().createStringValue(this.stringValue().trim());
	}

	protected	OclStringValue	toUpper() {
		return	getOclValuesFactory().createStringValue(this.stringValue().toUpperCase());
	}

	protected	OclStringValue	toLower() {
		return	getOclValuesFactory().createStringValue(this.stringValue().toLowerCase());
	}

	protected	OclStringValue	lTrim() {
		StringBuffer	result = new StringBuffer();
		
		int	index = this.stringValue().length();
		
		for (int i = 0; i < this.stringValue().length(); i++)
			if (this.stringValue().charAt(i) != ' ') {
				index = i;		
				break;
			}
		
		for (int i = index; i < this.stringValue().length(); i++) {
			result.append(this.stringValue().charAt(i));				
		}
				
		return	getOclValuesFactory().createStringValue(result.toString());
	}


	protected	OclStringValue	rTrim() {
		StringBuffer	result = new StringBuffer();
		
		int	index = -1;
		
		for (int i = this.stringValue().length() - 1; i >= 0; i--)
			if (this.stringValue().charAt(i) != ' ') {
				index = i;		
				break;
			}
				
		for (int i = 0; i <= index; i++) {
				result.append(this.stringValue().charAt(i));				
		}
				
		return	getOclValuesFactory().createStringValue(result.toString());
	}

	protected	OclBooleanValue	like(OclStringValue pattern) {
		return	getOclValuesFactory().createBooleanValue(Pattern.matches(pattern.stringValue(), this.stringValue()));
	}
	
	public String toString() {
		return this.value;
	}
}
