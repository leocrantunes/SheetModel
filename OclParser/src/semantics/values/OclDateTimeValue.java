package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.text.DateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;

import ocl20.evaluation.OclValue;
import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

public class OclDateTimeValue extends OclDateValue {
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	public OclDateTimeValue() {
		setCoreType(OclTypesFactory.createOclDateTimeType());
	}

	public OclDateTimeValue(Date value) {
		super(value);
		setCoreType(OclTypesFactory.createOclDateTimeType());
	}

	public OclDateTimeValue(OclIntegerValue day, OclIntegerValue month, OclIntegerValue year, OclIntegerValue hour, OclIntegerValue minute, OclIntegerValue second) {
		setCoreType(OclTypesFactory.createOclDateTimeType());
		Calendar calendar = new GregorianCalendar();
		calendar.set((int) day.intValue(), (int) month.intValue(), (int) year.intValue(), (int) hour.intValue(), (int) minute.intValue(), (int) second.intValue());
		this.setValue(new Date(calendar.getTimeInMillis()));
	}	

	public OclValue executeOperation(
		String opName,
		List arguments) {
		try {
			if (isArgumentUndefined(arguments)) {
				return	(OclValue) arguments.get(0);
			}

			if (opName.equals("getHour")) {
			   return	this.getHour();
			} else if (opName.equals("getMinute")) {
			   return	this.getMinute();
			} else if (opName.equals("getSecond")) {
			   return	this.getSecond();
			} else if (opName.equals("=")) {
			   return	this.equal((OclValue) arguments.get(0));
			} else if (opName.equals("<>")) {
			   return	this.notEqual((OclValue) arguments.get(0));
			} else if (opName.equals("addHour")) {
				   return	this.addHour((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("addMinute")) {
				return	this.addMinute((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("addSecond")) {
				return	this.addSecond((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("toString")) {
				return	this.toStringValue();
			} else
				return	super.executeOperation(opName, arguments);
		} catch (RuntimeException e) {
			return	getOclValuesFactory().createInvalidValue();
		}
	}
	
	protected	OclIntegerValue	getHour() {
		return	getOclValuesFactory().createIntegerValue(getCalendar(this.getValue()).get(Calendar.HOUR_OF_DAY));
	}

	protected	OclIntegerValue	getMinute() {
		return	getOclValuesFactory().createIntegerValue(getCalendar(this.getValue()).get(Calendar.MINUTE));
	}

	protected	OclIntegerValue	getSecond() {
		return	getOclValuesFactory().createIntegerValue(getCalendar(this.getValue()).get(Calendar.SECOND));
	}

	protected	OclDateValue	addHour(OclIntegerValue value) {
		return	this.add(Calendar.HOUR, value);
	}

	protected	OclDateValue	addMinute(OclIntegerValue value) {
		return	this.add(Calendar.MINUTE, value);	}

	protected	OclDateValue	addSecond(OclIntegerValue value) {
		return	this.add(Calendar.SECOND, value);
	}
	
	protected	OclStringValue toStringValue() {
		return	new OclStringValue(this.toString());
	}
	
	public String toString() {
		return DateFormat.getDateTimeInstance().format(this.getValue());
	}
}
