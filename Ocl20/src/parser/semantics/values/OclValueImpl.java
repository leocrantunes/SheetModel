/*
 * Created on 27/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;


import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;


import ocl20.common.CoreClassifier;
import ocl20.common.CoreOperation;
import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author Administrador
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
abstract public class OclValueImpl implements Serializable, OclValue {
	private String classifierName;
	transient private	CoreClassifier	valueType;

	public Object	clone() {
		try {
			OclValueImpl theClone = (OclValueImpl) this.getClass().newInstance();
			if (classifierName!= null)
				theClone.classifierName = new String(classifierName);
			else 
				theClone.classifierName = null;
			theClone.valueType = valueType;
			return	theClone;
		} catch (Exception e) {
			e.printStackTrace();
			return	null;
		}
	}
	
	public CoreClassifier getType() {
		return	valueType;
	}
	
	public String	getClassifierName() {
	    return	this.classifierName;
	}
	
	public void setClassifierName(String classifierName) {
	    valueType = OclTypesFactory.createTypeFromString(classifierName);
	    this.classifierName = valueType != null ? valueType.getFullPathName() : classifierName;
	}
	
	public void setCoreType(CoreClassifier type) {
		this.valueType = type;
		this.classifierName = type.getFullPathName();  // set String name for serialization.  
	}
	
	public	boolean	oclIsUndefined() {
		return	((OclBooleanValue) executeOperation("oclIsUndefined", null)).booleanValue();
	}

	public	boolean	oclIsInvalid() {
		return	((OclBooleanValue) executeOperation("oclIsInvalid", null)).booleanValue();
	}

	public	boolean	oclIsNull() {
		return	((OclBooleanValue) executeOperation("oclIsNull", null)).booleanValue();
	}

	public boolean isTrue() {
		return (this instanceof OclBooleanValue && ((OclBooleanValue) this).booleanValue());
	}

	public boolean isFalse() {
		return (this instanceof OclBooleanValue && !((OclBooleanValue) this).booleanValue());
	}

	public OclValue   executeOperation(CoreOperation operation, List args) {
		return	executeOperation(operation.getName(), args);
	}
	
	public OclValue	executeOperation(String opName, List args) {
		if (opName.equals("oclIsUndefined")) {
		   	return	getOclValuesFactory().createBooleanValue(false);
		} else if (opName.equals("oclIsNull")) {
		   	return	getOclValuesFactory().createBooleanValue(false);
		} else if (opName.equals("oclIsInvalid")) {
		   	return	getOclValuesFactory().createBooleanValue(false);
		} else if (opName.equals("asSet")) {
			OclSetValue setValue = (OclSetValue) getOclValuesFactory().createCollectionValue(OclTypesFactory.createSetType(valueType));
			setValue.add(this);
			return	setValue;
		} else if (opName.equals("oclIsTypeOf")) {
			return	this.oclIsTypeOf((OclStringValue) args.get(0));
		} else if (opName.equals("oclIsKindOf")) {
			return	this.oclIsKindOf((OclStringValue) args.get(0)); 
		} else if (opName.equals("oclAsType")) {
			return	this.oclAsType((OclStringValue) args.get(0)); 
		} else if (opName.equals("=")) {
		   return	this.equal((OclValue) args.get(0));
		} else if (opName.equals("<>")) {
		   return	this.notEqual((OclValue) args.get(0));
		} else	{
			if (this.oclIsNull())
				return	getOclValuesFactory().createNullValue();
			else
				return 	getOclValuesFactory().createInvalidValue();
		}
	}

	public boolean equals(Object obj) {
		if (obj instanceof OclValue) {
			OclValue value = (OclValue) obj;
			if (value.oclIsNull() && this.oclIsNull())
				return	true;
			else {
				List arg = new ArrayList();
				arg.add(obj);
				OclValue isEqualEval = this.executeOperation("=", arg);
				if (isEqualEval instanceof OclBooleanValue) {
					return 	((OclBooleanValue) isEqualEval).booleanValue();
				} else {
					return	false;
				}
			}
		}	
		else
			return	false;			
	}

	protected	OclValue	equal(OclValue arg) {
		if (this == arg) {
			return	getOclValuesFactory().createBooleanValue(true);
		} else {
			if (arg.oclIsInvalid() || this.oclIsInvalid())
				return	getOclValuesFactory().createInvalidValue();
			else if (arg.oclIsNull() || this.oclIsNull())
				return	getOclValuesFactory().createNullValue();
			else
				return	getOclValuesFactory().createBooleanValue(false);
			
		}
	}
	
	protected	OclValue	notEqual(OclValue arg) {
		OclValue isEqualEval = equal(arg);
		if (isEqualEval instanceof OclBooleanValue) {
			return	getOclValuesFactory().createBooleanValue(! ((OclBooleanValue) isEqualEval).booleanValue());	
		} else {
			return	isEqualEval;
		}
	}

	protected	boolean	hasArguments(List arguments) {
		return	arguments != null && arguments.size() > 0;
	}
	

	public	OclValue	oclIsTypeOf(OclStringValue typeName) {
		return createOclBooleanValue(valueType.getName().equals(typeName.stringValue()));
	}

	public	OclValue	oclIsKindOf(OclStringValue typeName) {
		if (valueType.getName().equals(typeName.stringValue())) {
			return createOclBooleanValue(true);
		} else {
			List	allPossibleTypes = new ArrayList();
			allPossibleTypes.addAll(this.getType().getAllAncestors());
			allPossibleTypes.addAll(this.getType().getAllImplementedInterfaces());
			
			for (Iterator iter = allPossibleTypes.iterator(); iter.hasNext(); ) {
				CoreClassifier	ancestor = (CoreClassifier) iter.next();
				if (ancestor.getName().equals(typeName.stringValue())) {
					return createOclBooleanValue(true);
				}	
			}
			
			return createOclBooleanValue(false);
		}
	}

	public	OclValue	oclAsType(OclStringValue typeName) {
		boolean	isTypeCompatible;
		
		isTypeCompatible = ((OclBooleanValue) this.oclIsKindOf(typeName)).booleanValue();
		if (isTypeCompatible) {
			return this;
		} else {
			return getOclValuesFactory().createInvalidValue();
		}
	}
	
	/* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValue#getOwnedElements()
     */
    public Collection getOwnedElements() {
        return new ArrayList();
    }
    
    protected	OclBooleanValue	createOclBooleanValue(boolean value) {
    	return	getOclValuesFactory().createBooleanValue(value);
    }
    
	protected	OclValuesFactory getOclValuesFactory() {
		return	OclValuesFactory.getInstance();
	}

	protected boolean isArgumentUndefined(List arguments) {
		return	hasArguments(arguments) && (arguments.get(0) instanceof OclUndefinedValue);
	}
	
	protected boolean isArgumentInvalid(List arguments) {
		return	hasArguments(arguments) && (arguments.get(0) instanceof OclInvalidValue);
	}

}
