/*
 * Created on 29/04/2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

import ocl20.common.CoreClassifier;
import ocl20.evaluation.OclValue;
import ocl20.types.OrderedSetType;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclOrderedSetValue extends OclCollectionValue implements OclOrderedCollection {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	List	orderedElements;	

	public OclOrderedSetValue() {
	}
	
	public OclOrderedSetValue(OrderedSetType	sequenceType) {
		super(sequenceType);
	}

	public OclOrderedSetValue(OrderedSetType	sequenceType, OclOrderedSetValue value) {
		super(sequenceType, value);
	}

	public OclOrderedSetValue(OrderedSetType	sequenceType, Iterator iter) {
		super(sequenceType, iter);
	}

	public	void	createElementsContainer() {
		orderedElements =	new ArrayList();
	}

	public void add(OclValue value) {
		checkIfElementTypeConformsToCollectionElementType(value);
		
		if (value.oclIsInvalid()) {
			throw new IllegalArgumentException("can not add invalid value to a collection");
		}
		
		if (! this.orderedElements.contains(value) ) // && ! (value instanceof OclUndefinedValue))
			this.orderedElements.add(value);
	}
	
	public	Collection	getElements() {
		return	orderedElements;
	}
	
	public List  getOrderedElements() {
	    return	this.orderedElements;
	}

	public void setOrderedElements(List l) {
	    this.orderedElements = l;
	}
	
	public OclValue executeOperation(String opName, List arguments) {
		try {
			if (isArgumentInvalid(arguments)) {
				   return	(OclValue) arguments.get(0);
			}

			if (opName.equals("union") && (arguments.get(0) instanceof OclOrderedSetValue)) {
				return	this.union((OclOrderedSetValue) arguments.get(0));
			} else if (opName.equals("union") && (arguments.get(0) instanceof OclSequenceValue)) {
					return	this.union((OclSequenceValue) arguments.get(0));
			} else if (opName.equals("-") && (arguments.get(0) instanceof OclCollectionValue)) {
				return	this.difference((OclCollectionValue) arguments.get(0));
			} else if (opName.equals("=") && (arguments.get(0) instanceof OclOrderedSetValue)) {
				return	this.equal((OclOrderedSetValue) arguments.get(0));
			} else if (opName.equals("<>") && (arguments.get(0) instanceof OclOrderedSetValue)) {
				return	this.different((OclOrderedSetValue) arguments.get(0));
			} else if (opName.equals("including")) {
				return	this.including((OclValue) arguments.get(0));
			} else if (opName.equals("excluding")) {
				return	this.excluding((OclValue) arguments.get(0));
			} else if (opName.equals("append")) {
				return	this.append((OclValue) arguments.get(0));
			} else if (opName.equals("prepend")) {
				return	this.prepend((OclValue) arguments.get(0));
			} else if (opName.equals("insertAt")) {
				return	(OclOrderedSetValue) this.insertAt((OclIntegerValue) arguments.get(0), (OclValue) arguments.get(1));
			} else if (opName.equals("subOrderedSet")) {
				return	this.subOrderedSet((OclIntegerValue) arguments.get(0), (OclIntegerValue) arguments.get(1));
			} else if (opName.equals("at")) {
				return	this.at((OclIntegerValue) arguments.get(0));
			} else if (opName.equals("indexOf")) {
				return	this.indexOf((OclValue) arguments.get(0));
			} else if (opName.equals("first")) {
				return	this.first();
			} else if (opName.equals("last")) {
				return	this.last();
			} else {
				return	super.executeOperation(opName, arguments); 
			}
		} catch (RuntimeException e) {
			return	getOclValuesFactory().createInvalidValue();
		}
	}

	protected	OclBooleanValue	equal(OclOrderedSetValue arg) {
		if (this == arg) {
			return	getOclValuesFactory().createBooleanValue(true);
		}
		if (this.orderedElements.size() != arg.orderedElements.size()) {
			return	getOclValuesFactory().createBooleanValue(false);
		}
		for (int i = 0; i < orderedElements.size(); i++) {
			OclValue element = (OclValue) orderedElements.get(i);
			if (! element.equals(arg.orderedElements.get(i))) {
				return	getOclValuesFactory().createBooleanValue(false);
			}
		}

		return	getOclValuesFactory().createBooleanValue(true);
	}

	protected	OclBooleanValue	different(OclOrderedSetValue arg) {
		return	getOclValuesFactory().createBooleanValue(! this.equal(arg).booleanValue());
	}


	protected OclOrderedSetValue append(OclValue value) {
		return	(OclOrderedSetValue) insertAt(new OclIntegerValue(orderedElements.size() + 1), value);
	}

	protected OclOrderedSetValue prepend(OclValue value) {
		return	(OclOrderedSetValue) insertAt(new OclIntegerValue(1), value);	
	}

	public OclOrderedCollection insertAt(OclIntegerValue index, OclValue value) {
		if (this.orderedElements.contains(value))
			return	this;

		checkIfElementTypeConformsToCollectionElementType(value);
		OclOrderedSetValue	result = new OclOrderedSetValue((OrderedSetType) this.getType(), this);
//		if (! (value instanceof OclUndefinedValue)) 
			result.orderedElements.add((int) index.intValue() - 1, value);					
		return	result;
	}

	protected	OclOrderedSetValue	union(OclCollectionValue arg) {
		return	(OclOrderedSetValue) super.doUnion(arg);
	}

	protected	OclOrderedSetValue	difference(OclCollectionValue arg) {
		OclOrderedSetValue	result = (OclOrderedSetValue) cloneCollection();
			
		Iterator	iter = arg.iterator();
		while (iter.hasNext()) {
			OclValue	element = (OclValue) iter.next();
			if (result.orderedElements.contains(element)) {
				result.orderedElements.remove(element);
			}
		}
			
		return	result;
	}

	protected	OclOrderedSetValue	subOrderedSet(OclIntegerValue lower, OclIntegerValue upper) {
		OclOrderedSetValue	result = (OclOrderedSetValue) createEmptyCollection();

		for (long i = lower.intValue() - 1; i < upper.intValue(); i++) {
			result.add((OclValue) this.orderedElements.get((int) i));
		}

		return	result;
	}

	public OclValue  at(OclIntegerValue i) {
		return	(OclValue) this.orderedElements.get((int) i.intValue() - 1);
	}

	protected OclValue	indexOf(OclValue obj) {
		if (this.orderedElements.indexOf(obj) >= 0)
			return	new OclIntegerValue(this.orderedElements.indexOf(obj) + 1);
		else
			return	getOclValuesFactory().createInvalidValue();
	}
	
	protected	OclValue	first() {
		return	(OclValue) this.orderedElements.get(0);
	}

	protected	OclValue	last() {
		return	(OclValue) this.orderedElements.get(this.orderedElements.size() - 1);
	}

	protected	OclOrderedSetValue	including(OclValue arg) {
		return (OclOrderedSetValue) super.doIncluding(arg);
	}

	protected	OclOrderedSetValue	excluding(OclValue arg) {
		return (OclOrderedSetValue) super.doExcluding(arg);
	}

	protected	OclCollectionValue	cloneCollection() {
		return	new OclOrderedSetValue((OrderedSetType) this.getType(), this);
	}
	
	public OclCollectionValue	createEmptyCollection() {
		return	new OclOrderedSetValue((OrderedSetType) this.getType());
	}

	protected	OclCollectionValue	createEmptyCollection(CoreClassifier elementType) {
		return	new OclOrderedSetValue((OrderedSetType) OclTypesFactory.createOrderedSetType(elementType));
	}

//	protected	OclCollectionValue	flatten() {
//		OclOrderedSetValue	result = new OclOrderedSetValue((OrderedSetType) this.getType());
//		
//		if (this.getElementType() instanceof CollectionType) {
//			for (Iterator iter = this.orderedElements.iterator(); iter.hasNext();) {
//				OclCollectionValue element = (OclCollectionValue) iter.next();
//				OclCollectionValue flattenedElement = element.flatten();
//				for (Iterator iteratorFlattened = flattenedElement.iterator(); iteratorFlattened.hasNext();) {
//					OclValue	value = (OclValue) iteratorFlattened.next();
//					if (! result.orderedElements.contains(value))
//						result.orderedElements.add(value);
//				}
//			}
//		}
//		else
//			result.orderedElements.addAll(this.orderedElements);
//			
//		return	result;
//	}
	
	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclCollectionValue#getCollectionName()
	 */
	protected String getCollectionName() {
		return "OrderedSet";
	}
}
