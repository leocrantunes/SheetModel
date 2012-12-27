/*
 * Created on 04/10/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.text.DateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;

import ocl20.evaluation.OclValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author alexcorr
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class OclDateValue extends OclPrimitiveValue {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	Date	value;

	public Object clone() {
		OclDateValue theClone = (OclDateValue) super.clone();
		theClone.value = (Date) value.clone();
		return	theClone;
	}

	public OclDateValue() {
		setCoreType(OclTypesFactory.createOclDateType());
		value = new Date();
	}

	public OclDateValue(Date value) {
		setCoreType(OclTypesFactory.createOclDateType());
		if (value != null)
			this.value = value;
		else
			this.value = new Date();
	}

	public OclDateValue(OclIntegerValue day, OclIntegerValue month, OclIntegerValue year) {
		setCoreType(OclTypesFactory.createOclDateType());
		Calendar calendar = new GregorianCalendar();
		calendar.set((int) day.intValue(), (int) month.intValue(), (int) year.intValue());
		this.value = new Date(calendar.getTimeInMillis());
	}	
		
	public Date	getValue() {
	    return	this.value;
	}
	
	public void setValue(Date	value) {
	    this.value = value;
	}
	
	public Date dateValue() {
		return	getValue();
	}
	
	public int hashCode() {
		return value.hashCode();
	}

	protected	GregorianCalendar	getCalendar(Date t){
		GregorianCalendar calendar = new GregorianCalendar();
		calendar.setTime(t);
		return	calendar;
	}

	public OclValue executeOperation(
		String opName,
		List arguments) {
		try {
			if (isArgumentUndefined(arguments)) {
			   return	(OclValue) arguments.get(0);
			}

			if (opName.equals("getDay")) {
			   return	this.getDay();
			} else if (opName.equals("getMonth")) {
			   return	this.getMonth();
			} else if (opName.equals("getYear")) {
			   return	this.getYear();
			} else if (opName.equals("getDow")) {
			   return	this.getDow();
			} else if (opName.equals("getInMilis")) {
			   return	this.getInMilis();
			} else if (opName.equals("=")) {
			   return	this.equal((OclValue) arguments.get(0));
			} else if (opName.equals("<>")) {
			   return	this.notEqual((OclValue) arguments.get(0));
			} else if (opName.equals("isBefore") || opName.equals("<")) {
			   return	this.isBefore((OclDateValue) arguments.get(0));
			} else if (opName.equals("isAfter") || opName.equals(">")) {
			   return	this.isAfter((OclDateValue) arguments.get(0));
			} else if (opName.equals("isLeapYear")) {
			   return	this.isLeapYear();
			} else if (opName.equals("addDay")) {
				   return	this.addDay((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("addMonth")) {
				return	this.addMonth((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("addYear")) {
				return	this.addYear((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("toString")) {
				return	this.toStringValue();
			} else
				return	super.executeOperation(opName, arguments);
		} catch (RuntimeException e) {
			return	getOclValuesFactory().createInvalidValue();
		}
	}
	
	protected	OclIntegerValue	getDay() {
		return	getOclValuesFactory().createIntegerValue(getCalendar(this.value).get(Calendar.DAY_OF_MONTH));
	}

	protected	OclIntegerValue	getMonth() {
		return	getOclValuesFactory().createIntegerValue(getCalendar(this.value).get(Calendar.MONTH) + 1);
	}

	protected	OclIntegerValue	getYear() {
		return	getOclValuesFactory().createIntegerValue(getCalendar(this.value).get(Calendar.YEAR));
	}

	protected	OclIntegerValue	getDow() {
		return	getOclValuesFactory().createIntegerValue(getCalendar(this.value).get(Calendar.DAY_OF_WEEK));
	}

	protected	OclIntegerValue	getInMilis() {
		return	getOclValuesFactory().createIntegerValue(getCalendar(this.value).getTimeInMillis());
	}

	protected	OclBooleanValue	isBefore(OclDateValue other) {
		return	getOclValuesFactory().createBooleanValue(getCalendar(this.value).before(getCalendar(other.value)));
	}
	
	protected	OclBooleanValue	isAfter(OclDateValue other) {
		return	getOclValuesFactory().createBooleanValue(getCalendar(this.value).after(getCalendar(other.value)));
	}
	
	protected	OclBooleanValue	isLeapYear() {
		return	getOclValuesFactory().createBooleanValue((new GregorianCalendar()).isLeapYear(getCalendar(this.value).get(Calendar.YEAR)));
	}

	protected	OclDateValue	add(int field, OclIntegerValue value) {
		Calendar calendar = getCalendar(this.value);
		calendar.add(field, (int) value.intValue());
		return	getOclValuesFactory().createDateValue(calendar.getTime());
	}
	
	protected	OclDateValue	addDay(OclIntegerValue value) {
		return	this.add(Calendar.DAY_OF_MONTH, value);
	}

	protected	OclDateValue	addMonth(OclIntegerValue value) {
		return	this.add(Calendar.MONTH, value);
	}

	protected	OclDateValue	addYear(OclIntegerValue value) {
		return	this.add(Calendar.YEAR, value);
	}

	protected	OclValue	equal(OclValue arg) {
		if (arg instanceof OclDateValue) {
			return getOclValuesFactory().createBooleanValue(this.dateValue().equals(((OclDateValue) arg).dateValue()));
		} else {
			return	super.equal(arg);
		}
	}

	protected	OclStringValue toStringValue() {
		return	new OclStringValue(this.toString());
	}
	
	public String toString() {
		return DateFormat.getDateInstance().format(value);
	}
}
