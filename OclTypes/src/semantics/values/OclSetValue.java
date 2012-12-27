/*
 * Created on Apr 28, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;


import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Set;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

import ocl20.common.CoreClassifier;
import ocl20.evaluation.OclValue;
import ocl20.types.SetType;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclSetValue extends OclCollectionValue {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	Set	setElements;	

	
	public OclSetValue() {
	}
	
	public OclSetValue(SetType	setType) {
		super(setType);
	}

	public OclSetValue(SetType	setType, OclSetValue value) {
		super(setType, value);
	}

	public OclSetValue(SetType	setType, Iterator iter) {
		super(setType, iter);
	}

	public	void	createElementsContainer() {
		setElements =	new HashSet();
	}
	
	public Collection getElements() {
		return	setElements;
	}

	public Set getSetElements() {
	    return	this.setElements;
	}

	public void setSetElements(Set s) {
	    this.setElements = s;
	}
	
	public OclValue executeOperation(String opName, List arguments) {
		try {
			if (isArgumentInvalid(arguments)) {
				   return	(OclValue) arguments.get(0);
			}

			if (opName.equals("union") && (arguments.get(0) instanceof OclSetValue)) {
					return	this.union((OclSetValue) arguments.get(0));
			} else if (opName.equals("union") && (arguments.get(0) instanceof OclBagValue)) {
						return	this.union((OclBagValue) arguments.get(0));
			} else if (opName.equals("=") && (arguments.get(0) instanceof OclSetValue)) {
					return	this.equal((OclSetValue) arguments.get(0));
			} else if (opName.equals("<>") && (arguments.get(0) instanceof OclSetValue)) {
					return	this.different((OclSetValue) arguments.get(0));
			} else if (opName.equals("intersection") && (arguments.get(0) instanceof OclSetValue)) {
					return	this.intersection((OclSetValue) arguments.get(0));
			} else if (opName.equals("intersection") && (arguments.get(0) instanceof OclBagValue)) {
					return	this.intersection((OclBagValue) arguments.get(0));
			} else if (opName.equals("-") && (arguments.get(0) instanceof OclSetValue)) {
					return	this.difference((OclSetValue) arguments.get(0));
			} else if (opName.equals("symmetricDifference") && (arguments.get(0) instanceof OclSetValue)) {
					return	this.symmetricDifference((OclSetValue) arguments.get(0));
			} else if (opName.equals("including")) {
				return	this.including((OclValue) arguments.get(0));
			} else if (opName.equals("excluding")) {
				return	this.excluding((OclValue) arguments.get(0));
			} else {
				return	super.executeOperation(opName, arguments); 
			}
		} catch (RuntimeException e) {
			return	getOclValuesFactory().createInvalidValue();
		}
	}

	protected	OclValue	count(OclValue value) {
		// return	new OclIntegerValue(setElements.contains(value) ? 1 : 0);
	    return	super.count(value);
	}

	protected	OclBooleanValue	equal(OclSetValue arg) {
		if (this == arg) {
			return	getOclValuesFactory().createBooleanValue(true);
		}

		if (this.size().intValue() == arg.size().intValue()) {
		    for (Iterator iter = setElements.iterator(); iter.hasNext();) {
		        OclValue element = (OclValue) iter.next();
		        if (arg.excludes(element).isTrue()) {
		            return	getOclValuesFactory().createBooleanValue(false);
		        }
		    }
	        return	getOclValuesFactory().createBooleanValue(true);
		}  else {
	        return	getOclValuesFactory().createBooleanValue(false);
		}
	}

	protected	OclBooleanValue	different(OclSetValue arg) {
		return	getOclValuesFactory().createBooleanValue(! this.equal(arg).booleanValue());
	}

	protected	OclSetValue	union(OclSetValue arg) {
		return	(OclSetValue) super.doUnion(arg);
	}

	protected	OclBagValue	union(OclBagValue arg) {
		OclBagValue result = this.asBag();
		result.addAll(arg);
		return	result;
	}

	protected	OclSetValue	intersection(OclSetValue arg) {
		return	this.intersection(arg.getElements());
	}

	protected	OclSetValue	intersection(OclBagValue arg) {
		return	this.intersection(arg.getElements());
	}

	protected	OclSetValue	intersection(Collection arg) {
		OclSetValue	result = (OclSetValue) cloneCollection();
		result.setElements.retainAll(arg);
		return	result;
	}
	
	protected	OclSetValue	difference(OclSetValue arg) {
		OclSetValue	result = (OclSetValue) cloneCollection();
		result.setElements.removeAll(arg.setElements);
		return	result;
	}

	protected	OclSetValue	symmetricDifference(OclSetValue arg) {
		return 	this.union(arg).difference(this.intersection(arg));	
	}

	protected	OclSetValue	including(OclValue arg) {
		return (OclSetValue) super.doIncluding(arg);
	}

	protected	OclSetValue	excluding(OclValue arg) {
		OclSetValue	result = (OclSetValue) cloneCollection();
		result.setElements.remove(arg);
		return	result;
	}

	protected	OclCollectionValue	cloneCollection() {
		return	new OclSetValue((SetType) this.getType(), this);
	}

	public OclCollectionValue	createEmptyCollection() {
		return	new OclSetValue((SetType) this.getType());
	}
	
	protected	OclCollectionValue	createEmptyCollection(CoreClassifier elementType) {
		return	new OclSetValue((SetType) OclTypesFactory.createSetType(elementType));
	}

	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclCollectionValue#getCollectionName()
	 */
	protected String getCollectionName() {
		return "Set";
	}
}
