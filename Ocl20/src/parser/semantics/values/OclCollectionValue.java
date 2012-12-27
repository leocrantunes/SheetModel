/*
 * Created on Apr 28, 2004
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import impl.ocl20.types.CollectionTypeImpl;
import impl.ocl20.types.TupleTypeImpl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.evaluation.OclValue;
import ocl20.types.CollectionType;


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
abstract public class OclCollectionValue extends OclStaticValue {
    
	public Object clone() {
		return	cloneCollection();
	}

    public OclCollectionValue() {
        createElementsContainer();
    }
    
	public OclCollectionValue(CollectionType	collectionType) {
		this.setCoreType(collectionType); 
		createElementsContainer();
	}
	
	public OclCollectionValue(CollectionType	collectionType, OclCollectionValue value) {
		this(collectionType);
		for (Iterator iter = value.getElements().iterator(); iter.hasNext();) {
			this.add((OclValue) iter.next());
		}
	}

	public OclCollectionValue(CollectionType	collectionType, Iterator iter) {
		this(collectionType);
		while(iter.hasNext()) {
			this.add((OclValue) iter.next());
		}
	}
	
	abstract	protected	void	createElementsContainer();
	
	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValue#executeOperation(java.lang.String, java.util.List)
	 */
	public OclValue executeOperation(String opName, List arguments) {
		try {
			if (isArgumentInvalid(arguments)) {
				   return	(OclValue) arguments.get(0);
			}

			if (opName.equals("size")) {
				return	this.size();
			} else if (opName.equals("includes")) {
				return	this.includes((OclValue) arguments.get(0));
			} else if (opName.equals("excludes")) {
				return	this.excludes((OclValue) arguments.get(0));
			} else if (opName.equals("count")) {
				return	this.count((OclValue) arguments.get(0));
			} else if (opName.equals("includesAll")) {
				return	this.includesAll((OclCollectionValue) arguments.get(0));
			} else if (opName.equals("excludesAll")) {
				return	this.excludesAll((OclCollectionValue) arguments.get(0));
			} else if (opName.equals("isEmpty")) {
				return	this.isEmpty();
			} else if (opName.equals("notEmpty")) {
				return	this.notEmpty();
			} else if (opName.equals("sum")) {
				return	this.sum();
			} else if (opName.equals("avg")) {
				return	this.avg();
			} else if (opName.equals("max")) {
				return	this.max();
			} else if (opName.equals("min")) {
				return	this.min();
			} else if (opName.equals("product")) {
				return	this.product((OclCollectionValue) arguments.get(0));
			} else if (opName.equals("asSet")) {
				return	this.asSet();
			} else if (opName.equals("asBag")) {
				return	this.asBag();
			} else if (opName.equals("asSequence")) {
				return	this.asSequence();
			} else if (opName.equals("asOrderedSet")) {
				return	this.asOrderedSet();
			} else if (opName.equals("flatten")) {
				return	this.flatten();
			} else
				return	super.executeOperation(opName, arguments);	
		} catch (RuntimeException e) {
			return	getOclValuesFactory().createInvalidValue();
		}
	}

	public Iterator iterator() {
		return getElements().iterator();
	}

	public int hashCode() {
		return	getElements().hashCode();
	}

	abstract	public Collection	getElements();
	abstract	public	OclCollectionValue	createEmptyCollection();
	abstract	protected	OclCollectionValue	createEmptyCollection(CoreClassifier elementType);
	
	public CoreClassifier getElementType() {
		return ((CollectionType) this.getType()).getElementType();
	}

	public void add(OclValue value) {
		checkIfElementTypeConformsToCollectionElementType(value);
		if (! value.oclIsInvalid())		// it was commented (oclUndefinedValue)
			getElements().add(value);	
		else
			throw new IllegalArgumentException("can not add invalid value to a collection");
	}

	public	void	checkIfElementTypeConformsToCollectionElementType(OclValue value) {
		if (! value.getType().conformsTo( this.getElementType()) && ! this.getElementType().getName().equals("OclVoid")) {
			IllegalArgumentException e = new IllegalArgumentException("type mismatch - expected: " + ((CollectionType) (this.getType())).getElementType().getName() + " argument: " + value.getType().getName());
			throw e;
		}
	}

	protected	OclCollectionValue	cloneCollection() {
		return	null;
	}	
	
	public	OclIntegerValue	size() {
		return	getOclValuesFactory().createIntegerValue(getElements().size());
	}

	protected	OclBooleanValue	includes(OclValue value) {
		for (Iterator iter = getElements().iterator(); iter.hasNext();) {
			OclValue element = (OclValue) iter.next();
			if (element.equals(value) || (element.oclIsNull() && value.oclIsNull())) {
				return	getOclValuesFactory().createBooleanValue(true);
			}
		}
		return	getOclValuesFactory().createBooleanValue(false);
	}

	protected	OclBooleanValue	excludes(OclValue value) {
		for (Iterator iter = getElements().iterator(); iter.hasNext();) {
			OclValue element = (OclValue) iter.next();
			if (element.equals(value) || (element.oclIsNull() && value.oclIsNull())) {
				return	getOclValuesFactory().createBooleanValue(false);
			}
		}
		return	getOclValuesFactory().createBooleanValue(true);
	}

	protected	OclBooleanValue	includesAll(OclCollectionValue value) {
		for (Iterator iter = value.iterator(); iter.hasNext(); ) {
			OclBooleanValue includesEval = this.includes((OclValue) iter.next());
			if (includesEval.isFalse())
				return	includesEval;
		}
		return	getOclValuesFactory().createBooleanValue(true);
	}

	protected	OclBooleanValue	excludesAll(OclCollectionValue value) {
		for (Iterator iter = value.iterator(); iter.hasNext(); ) {
			OclBooleanValue excludesEval = this.excludes((OclValue) iter.next());
			if (excludesEval.isFalse())
				return	excludesEval;
		}
		return	getOclValuesFactory().createBooleanValue(true);
	}

	protected	OclBooleanValue	isEmpty() {
		return	getOclValuesFactory().createBooleanValue(getElements().size() == 0);
	}

	protected	OclBooleanValue	notEmpty() {
		return	getOclValuesFactory().createBooleanValue(getElements().size() > 0);
	}

	protected	OclValue	count(OclValue value) {
		int	count = 0;
		for (Iterator iter = getElements().iterator(); iter.hasNext();) {
			OclValue element = (OclValue) iter.next();
			if (element.equals(value) || (element.oclIsNull() && value.oclIsNull())) {
				count++;
			}
		}
		return	getOclValuesFactory().createIntegerValue(count);
	}

	protected	OclValue	sum() {
		OclValue	result = this.createInitialValue();
		List		arguments = new ArrayList(); 
		arguments.add(null);
		
		for (Iterator	iter = this.iterator(); iter.hasNext();) {
			OclValue	nextValue = (OclValue) iter.next();
			
			if (! nextValue.oclIsUndefined()) {    // sum only "known" values
				arguments.set(0, nextValue);
				result = result.executeOperation("+", arguments);
			}
		}
		return	result;
	}

	protected	OclValue	avg() {
		OclValue	result = this.createInitialValue();
		List		arguments = new ArrayList();
		arguments.add(null);
		
		int	nElements = 0;
		for (Iterator	iter = this.iterator(); iter.hasNext();) {
			OclValue	nextValue = (OclValue) iter.next();
			
			if (! nextValue.oclIsUndefined()) {    // sum only "known" values
				arguments.set(0, nextValue);
				result = result.executeOperation("+", arguments);
				nElements++;
			}
		}
		
		if (nElements > 0) {
			arguments.set(0, getOclValuesFactory().createIntegerValue(nElements));
			return	result.executeOperation("/", arguments);
		} else {
			if (this.getElements().size() > 0)
				return	getOclValuesFactory().createNullValue();
			else
				return	getOclValuesFactory().createInvalidValue();
		}
	}


	protected	OclValue	max() {
		OclValue	result = "Integer".equals(getElementType().getName()) 
		                                            ? getOclValuesFactory().createIntegerValue(Long.MIN_VALUE)
		                                            : getOclValuesFactory().createRealValue(String.valueOf(Double.MIN_VALUE));
		List	arguments = new ArrayList();
		arguments.add(null);
		int	nElements = 0;
		for (Iterator	iter = this.iterator(); iter.hasNext();) {
			OclValue	nextValue = (OclValue) iter.next();
			
			if (! nextValue.oclIsUndefined()) {    // max considers only "known" values
				arguments.set(0, nextValue);
				OclValue nextValueIsGreater = result.executeOperation("<", arguments);
				if (nextValueIsGreater.isTrue()) {
					result = nextValue;
				}
				nElements++;
			}
		}
		
		if (nElements > 0) {
			return	result;
		} else {
			if (this.getElements().size() > 0)
				return	getOclValuesFactory().createNullValue();
			else
				return	getOclValuesFactory().createInvalidValue();
		}
	}

	protected	OclValue	min() {
		OclValue	result = "Integer".equals(getElementType().getName()) 
														? getOclValuesFactory().createIntegerValue(Long.MAX_VALUE)
														: getOclValuesFactory().createRealValue(String.valueOf(Double.MAX_VALUE));
		List	arguments = new ArrayList();
		arguments.add(null);
		int	nElements = 0;
		for (Iterator	iter = this.iterator(); iter.hasNext();) {
			OclValue	nextValue = (OclValue) iter.next();
			
			if (! nextValue.oclIsUndefined()) {    // max considers only "known" values
				arguments.set(0, nextValue);
				OclValue nextValueIsLess = result.executeOperation(">", arguments);
				if (nextValueIsLess.isTrue()) {
					result = nextValue;
				}
				nElements++;
			}
		}
		
		if (nElements > 0) {
			return	result;
		} else {
			if (this.getElements().size() > 0)
				return	getOclValuesFactory().createNullValue();
			else
				return	getOclValuesFactory().createInvalidValue();
		}
	}

	protected	OclValue	createInitialValue() {
		if ("Integer".equals(this.getElementType().getName()) || "OclVoid".equals(this.getElementType().getName())) {
			return	getOclValuesFactory().createIntegerValue(0);
		} else if ("Real".equals(this.getElementType().getName())) {
			return	getOclValuesFactory().createRealValue("0");
		} else {
			return	getOclValuesFactory().createInvalidValue();
		}
		
	}
	
	protected	OclValue	product(OclCollectionValue otherCollection) {
		TupleTypeImpl  resultElementType = (TupleTypeImpl) OclTypesFactory.createTupleType();
		resultElementType.addElement("first", this.getElementType());
		resultElementType.addElement("second", otherCollection.getElementType());
		
		OclCollectionValue	result = OclValuesFactory.getInstance().createCollectionValue(OclTypesFactory.createSetType(resultElementType));
		
		CoreAttribute firstAttrib = resultElementType.lookupAttribute("first");
		CoreAttribute secondAttrib = resultElementType.lookupAttribute("second");
		
		Map	tupleValues = new HashMap();
		
		for (Iterator	thisIterator   = this.iterator(); thisIterator.hasNext(); ) {
			OclValue	firstValue = (OclValue) thisIterator.next();
			for (Iterator	otherIterator = otherCollection.iterator(); otherIterator.hasNext();) {
				tupleValues.clear();
				tupleValues.put(firstAttrib, firstValue);
				tupleValues.put(secondAttrib, (OclValue) otherIterator.next());
				result.add(new OclTupleValue(resultElementType, tupleValues)); 						
			}
		}
				
		return	result;		
	}

	protected	OclSetValue	asSet() {
		return new OclSetValue(OclTypesFactory.createSetType(this.getElementType()), this.iterator());
	}

	protected	OclBagValue	asBag() {
		return new OclBagValue(OclTypesFactory.createBagType(this.getElementType()), this.iterator());
	}

	protected	OclOrderedSetValue	asOrderedSet() {
		return new OclOrderedSetValue(OclTypesFactory.createOrderedSetType(this.getElementType()), this.iterator());
	}

	protected	OclSequenceValue	asSequence() {
		return	new OclSequenceValue(OclTypesFactory.createSequenceType(this.getElementType()), this.iterator());
	}

	protected	OclCollectionValue	flatten() {
		OclCollectionValue	result = createEmptyCollection(((CollectionTypeImpl) this.getType()).getInnerMostElementType());
		
		for (Iterator iter = this.getElements().iterator(); iter.hasNext();) {
			OclValue element = (OclValue) iter.next();
			if (element instanceof OclCollectionValue) {
				OclCollectionValue collection = (OclCollectionValue) element;
				for (Iterator iter2 = collection.flatten().getElements().iterator(); iter2.hasNext();) {
					result.add((OclValue) iter2.next());
				}
			} else {
				if (! (element.oclIsInvalid())) {
					result.getElements().add(element);
				}
			}
		}

		return	result;
	}


	protected	OclCollectionValue	doExcluding(OclValue arg) {
		OclCollectionValue	result = cloneCollection();
		
		for (Iterator iter = getElements().iterator(); iter.hasNext();) {
			OclValue element = (OclValue) iter.next();
			if (element.equals(arg)) {
				result.getElements().remove(element);
			}
		}
			
		return	result;
	}

	protected	OclCollectionValue	doIncluding(OclValue arg) {
		OclCollectionValue	result = cloneCollection();
		result.add(arg);
			
		return	result;
	}

	protected	OclCollectionValue	doUnion(OclCollectionValue arg) {
		OclCollectionValue	result = cloneCollection();
		result.addAll(arg);	
		return	result;
	}
	
	protected	void	addAll(OclCollectionValue arg) {
		for (Iterator iter = arg.iterator(); iter.hasNext();) {
			add((OclValue) iter.next());
		}
	}
	
	
	protected abstract String getCollectionName();
	
	public String toString() {
		StringBuffer result = new StringBuffer(); 
		
		result.append(getCollectionName());
		result.append("{");
		
		boolean isFirst = true;
		for (Iterator iter = this.getElements().iterator();  iter.hasNext();) {
			OclValue element = (OclValue) iter.next();
			if (! isFirst)
				result.append(",");
			result.append(element.toString());
			isFirst = false;
		}
		
		result.append("}");
		return	result.toString();
	}

	/* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValueImpl#getOwnedElements()
     */
    public Collection getOwnedElements() {
        return this.getElements();
    }
	
}
