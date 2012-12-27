/*
 * Created on May 14, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import impl.ocl20.expressions.OclExpressionImpl;

import java.math.BigDecimal;
import java.util.Collection;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreEnumLiteral;
import ocl20.common.CoreEnumeration;
import ocl20.evaluation.OclValue;
import ocl20.expressions.VariableDeclaration;
import ocl20.types.BagType;
import ocl20.types.CollectionType;
import ocl20.types.OrderedSetType;
import ocl20.types.SequenceType;
import ocl20.types.SetType;
import ocl20.types.TupleType;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclValuesFactory {
	static	private	OclValuesFactory		oclValuesFactory = new OclValuesFactory();
	
	private	static	OclBooleanValue		trueValue = new OclBooleanValue(true);
	private	static	OclBooleanValue		falseValue = new OclBooleanValue(false);
	
	private	static	OclNullValue				nullValue = new OclNullValue();
	private	static	OclInvalidValue		invalidValue = new OclInvalidValue();

	static	public	OclValuesFactory	getInstance() {
		return	oclValuesFactory;
	}
	
	public	OclIntegerValue	createIntegerValue(long	value) {
		return	new OclIntegerValue(value);
	}

	public	OclRealValue	createRealValue(String value) {
		return	new OclRealValue(value);
	}

	public	OclRealValue	createRealValue(BigDecimal value) {
		return	new OclRealValue(value);
	}

	public	OclBooleanValue	createBooleanValue(boolean	value) {
		if (value){
			return	trueValue;
		} else {
			return	falseValue;
		}
	}

	public	OclStringValue	createStringValue(String	value) {
		return	new OclStringValue(value);
	}

	public 	OclDateValue	createDateValue(Date value) {
		return	new OclDateValue(value);
	}

	public 	OclDateTimeValue	createDateTimeValue(Date value) {
		return	new OclDateTimeValue(value);
	}

	public	OclEnumValue	createEnumValue(CoreEnumLiteral	value) {
		return	new OclEnumValue(value);
	}

	public	OclNullValue createNullValue() {
		return	nullValue;
	}

	public	OclInvalidValue createInvalidValue() {
		return	invalidValue;
	}

	
	public	OclCollectionValue	createCollectionValue(CollectionType type) {
		if (type instanceof SetType) {
			return	new OclSetValue((SetType) type);
		} else	if (type instanceof BagType) {
			return	new OclBagValue((BagType) type);
		} else	if (type instanceof SequenceType) {
			return	new OclSequenceValue((SequenceType) type);
		} else	if (type instanceof OrderedSetType) {
			return	new OclOrderedSetValue((OrderedSetType) type);
		} else {
			return	null;
		}
	}

	public	OclTupleValue	createTupleValue(CoreClassifier type, Collection tupleParts) {
		Map	tupleValues = new HashMap();
		
		TupleType  tupleType = (TupleType) type;
		
		for (Iterator iter = tupleParts.iterator(); iter.hasNext(); ) {
			VariableDeclaration tuplePart = (VariableDeclaration) iter.next(); 
			CoreAttribute attrib = tupleType.lookupAttribute(tuplePart.getName());
			if (attrib != null) {
				tupleValues.put(attrib, ((OclExpressionImpl) tuplePart.getInitExpression()).getValue());
			}
		}
		
		return	new OclTupleValue(tupleType, tupleValues);
	}
	
	public  OclValue  createOclValueFromString(CoreAttribute attribute, String newValueAsString) {
		try {
			if (newValueAsString == null || newValueAsString.length() == 0)
				return	createNullValue();
			if ("Boolean".equals(attribute.getFeatureType().getName())) {
				return	createBooleanValue(Boolean.parseBoolean(newValueAsString));
			} else if ("Integer".equals(attribute.getFeatureType().getName())) {
				return createIntegerValue(Long.parseLong(newValueAsString));
			} else if ("Real".equals(attribute.getFeatureType().getName())) {
				return createRealValue(newValueAsString);
			} else if ("String".equals(attribute.getFeatureType().getName())) {
				return createStringValue(newValueAsString);
			} else if ("Date".equals(attribute.getFeatureType().getName())) {
				return	createStringValue(newValueAsString).toDate();
			} else if ("DateTime".equals(attribute.getFeatureType().getName())) {
				return	createStringValue(newValueAsString).toDateTime();
			} else if (attribute.getFeatureType().isEnumeration()) {
				CoreEnumeration enumeration = (CoreEnumeration) attribute.getFeatureType();
				CoreEnumLiteral enumLiteral = enumeration.lookupEnumLiteral(newValueAsString);
				if (enumLiteral != null)
					return createEnumValue(enumeration.lookupEnumLiteral(newValueAsString));
				else
					return	createInvalidValue();
			} else {
				return createInvalidValue();
			}
		} catch (NumberFormatException e) {
			return createInvalidValue();
		}	
	}
	
	public  Object	getJavaValueFromOclValue(Class javaClass, OclValue value) {
			if (value.oclIsUndefined()) {
				if (isInteger(javaClass))
					return		new Long(0);
				else if (isReal(javaClass))
					return	 	new	Double(0);
				else if (isBoolean(javaClass))
					return		new	Boolean(false);
				else
					return		null;
			} else	if (value instanceof OclBooleanValue) {
				return	 new Boolean(((OclBooleanValue) value).booleanValue());
			} else	if (value instanceof OclIntegerValue) {
				return	 new Long(((OclIntegerValue) value).intValue());
			} else	if (value instanceof OclRealValue) {
				return	 new Double(((OclRealValue) value).doubleValue().doubleValue());
			} else	if (value instanceof OclStringValue) {
				return	 new String(((OclStringValue) value).stringValue());
			} else {
				return null;
			} 
	
			// enumeration values??
	}

	protected boolean	isInteger(Class javaClass) {
		return		"BYTE".equals(javaClass.getName().toUpperCase())	  ||
						"SHORT".equals(javaClass.getName().toUpperCase()) ||	
						"INT".equals(javaClass.getName().toUpperCase()) 	||
						"LONG".equals(javaClass.getName().toUpperCase());
	}

	protected boolean	isReal(Class javaClass) {
		return		"FLOAT".equals(javaClass.getName().toUpperCase())	  ||
						"DOUBLE".equals(javaClass.getName().toUpperCase());	
	}

	protected boolean	isBoolean(Class javaClass) {
		return		"BOOL".equals(javaClass.getName().toUpperCase())	  ||
						"BOOLEAN".equals(javaClass.getName().toUpperCase());	
	}
}
