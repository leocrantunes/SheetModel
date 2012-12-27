/*
 * Created on 27/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class ValuesFactory {
	private	static	OclBooleanValue		trueValue = new OclBooleanValue(true);
	private	static	OclBooleanValue		falseValue = new OclBooleanValue(false);
	
	private	static	OclUndefinedValue	undefinedValue = new OclUndefinedValue();
	private	static	OclNullValue		nullValue = new OclNullValue();
	private	static	OclInvalidValue		invalidValue = new OclInvalidValue();

	public 	static OclIntegerValue createIntegerValue(long value) {
		return	new OclIntegerValue(value);
	}
	
	public 	static OclRealValue createRealValue(String value) {
		return	new OclRealValue(value);
	}

	public 	static OclBooleanValue createBooleanValue(boolean value) {
		if (value){
			return	trueValue;
		} else {
			return	falseValue;
		}
	}

	public 	static OclStringValue createStringValue(String value) {
		return	new OclStringValue(value);
	}

	public	static	OclUndefinedValue createUndefinedValue() {
		return	undefinedValue;
	}
	
	public	static	OclNullValue createNullValue() {
		return	nullValue;
	}

	public	static	OclInvalidValue createInvalidValue() {
		return	invalidValue;
	}

}
