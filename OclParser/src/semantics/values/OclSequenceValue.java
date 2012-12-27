/*
 * Created on Apr 29, 2004
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
import ocl20.types.SequenceType;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclSequenceValue extends OclCollectionValue implements OclOrderedCollection {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	List	seqElements;	
	

	public OclSequenceValue() {
	}

	public OclSequenceValue(SequenceType	sequenceType) {
		super(sequenceType);
	}

	public OclSequenceValue(SequenceType	sequenceType, OclSequenceValue value) {
		super(sequenceType, value);
	}

	public OclSequenceValue(SequenceType	sequenceType, Iterator iter) {
		super(sequenceType, iter);
	}

	public	void	createElementsContainer() {
		seqElements =	new ArrayList();
	}
	
	public	Collection	getElements() {
		return	seqElements;
	}
	
	public List  getSeqElements() {
	    return	this.seqElements;
	}

	public void setSeqElements(List l) {
	    this.seqElements = l;
	}
	
	public OclValue executeOperation(String opName, List arguments) {
		try {
			if (isArgumentInvalid(arguments)) {
				   return	(OclValue) arguments.get(0);
			}

			if (opName.equals("union") && (arguments.get(0) instanceof OclSequenceValue)) {
				return	this.union((OclSequenceValue) arguments.get(0));
			} else if (opName.equals("union") && (arguments.get(0) instanceof OclOrderedSetValue)) {
					return	this.union((OclSequenceValue) arguments.get(0));
			} else if (opName.equals("=") && (arguments.get(0) instanceof OclSequenceValue)) {
				return	this.equal((OclSequenceValue) arguments.get(0));
			} else if (opName.equals("<>") && (arguments.get(0) instanceof OclSequenceValue)) {
				return	this.equal((OclSequenceValue) arguments.get(0));
			} else if (opName.equals("including")) {
				return	this.including((OclValue) arguments.get(0));
			} else if (opName.equals("excluding")) {
				return	this.excluding((OclValue) arguments.get(0));
			} else if (opName.equals("append")) {
				return	this.append((OclValue) arguments.get(0));
			} else if (opName.equals("prepend")) {
				return	this.prepend((OclValue) arguments.get(0));
			} else if (opName.equals("insertAt")) {
				return	(OclSequenceValue) this.insertAt((OclIntegerValue) arguments.get(0), (OclValue) arguments.get(1));
			} else if (opName.equals("subSequence")) {
				return	this.subSequence((OclIntegerValue) arguments.get(0), (OclIntegerValue) arguments.get(1));
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

	protected	OclBooleanValue	equal(OclSequenceValue arg) {
		if (this == arg) {
			return	getOclValuesFactory().createBooleanValue(true);
		}
		if (this.seqElements.size() != arg.seqElements.size()) {
			return	getOclValuesFactory().createBooleanValue(false);
		}
		for (int i = 0; i < seqElements.size(); i++) {
			OclValue element = (OclValue) seqElements.get(i);
			if (! element.equals(arg.seqElements.get(i))) {
				return	getOclValuesFactory().createBooleanValue(false);
			}
		}

		return	getOclValuesFactory().createBooleanValue(true);
	}

	protected	OclBooleanValue	different(OclSequenceValue arg) {
		return	getOclValuesFactory().createBooleanValue(! this.equal(arg).booleanValue());
	}

	protected OclSequenceValue append(OclValue value) {
		return	(OclSequenceValue) insertAt(new OclIntegerValue(seqElements.size() + 1), value);
	}

	protected OclSequenceValue prepend(OclValue value) {
		return	(OclSequenceValue) insertAt(new OclIntegerValue(1), value);
	}

	public OclOrderedCollection insertAt(OclIntegerValue index, OclValue value) {
		checkIfElementTypeConformsToCollectionElementType(value);
		OclSequenceValue	result = (OclSequenceValue) cloneCollection();
//		if (! (value instanceof OclUndefinedValue)) 
		result.seqElements.add((int) index.intValue() - 1, value);					
		return	result;
	}


	protected	OclSequenceValue	union(OclCollectionValue arg) {
		return	(OclSequenceValue) super.doUnion(arg);
	}

	protected	OclSequenceValue	subSequence(OclIntegerValue lower, OclIntegerValue upper) {
		OclSequenceValue	result = (OclSequenceValue) createEmptyCollection();

		for (long i = lower.intValue() - 1; i < upper.intValue(); i++) {
			result.add((OclValue) this.seqElements.get((int) i));
		}

		return	result;
	}

	public OclValue  at(OclIntegerValue i) {
		return	(OclValue) this.seqElements.get((int) i.intValue() - 1);
	}

	protected OclValue	indexOf(OclValue obj) {
		if (this.seqElements.indexOf(obj) >= 0)
			return	new OclIntegerValue(this.seqElements.indexOf(obj) + 1);
		else
			return	new OclIntegerValue(-1);
	}
	
	protected	OclValue	first() {
		return	(OclValue) this.seqElements.get(0);
	}

	protected	OclValue	last() {
		return	(OclValue) this.seqElements.get(this.seqElements.size() - 1);
	}

	protected	OclSequenceValue	including(OclValue arg) {
		return (OclSequenceValue) super.doIncluding(arg);
	}

	protected	OclSequenceValue	excluding(OclValue arg) {
		return (OclSequenceValue) super.doExcluding(arg);
	}

	protected	OclCollectionValue	cloneCollection() {
		return	new OclSequenceValue((SequenceType) this.getType(), this);
	}

	public OclCollectionValue	createEmptyCollection() {
		return	new OclSequenceValue((SequenceType) this.getType());
	}
	
	protected	OclCollectionValue	createEmptyCollection(CoreClassifier elementType) {
		return	new OclSequenceValue((SequenceType) OclTypesFactory.createSequenceType(elementType));
	}

	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclCollectionValue#getCollectionName()
	 */
	protected String getCollectionName() {
		return "Sequence";
	}
}
