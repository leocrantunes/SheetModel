/*
 * Created on Apr 28, 2004
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
import ocl20.types.BagType;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */
public class OclBagValue extends OclCollectionValue {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	List	bagElements;	


	public OclBagValue() {
	}

	public OclBagValue(BagType	bagType) {
		super(bagType);
	}

	public OclBagValue(BagType	bagType, OclBagValue value) {
		super(bagType, value);
	}

	public OclBagValue(BagType	bagType, Iterator iter) {
		super(bagType, iter);
	}
	
	public	void	createElementsContainer() {
		bagElements =	new ArrayList();
	}

	public	Collection	getElements() {
		return	bagElements;
	}
	
	public List  getBagElements() {
	    return	this.bagElements;
	}

	public void setBagElements(List l) {
	    this.bagElements = l;
	}
	public OclValue executeOperation(String opName, List arguments) {
		try {
			if (isArgumentInvalid(arguments)) {
				   return	(OclValue) arguments.get(0);
			}

			if (opName.equals("union") && (arguments.get(0) instanceof OclBagValue)) {
				return	this.union((OclBagValue) arguments.get(0));
			} else if (opName.equals("union") && (arguments.get(0) instanceof OclSetValue)) {
				return	this.union((OclSetValue) arguments.get(0));
			} else if (opName.equals("=") && (arguments.get(0) instanceof OclBagValue)) {
				return	this.equal((OclBagValue) arguments.get(0));
			} else if (opName.equals("<>") && (arguments.get(0) instanceof OclBagValue)) {
				return	this.different((OclBagValue) arguments.get(0));
			} else if (opName.equals("intersection") && (arguments.get(0) instanceof OclBagValue)) {
				return	this.intersection((OclBagValue) arguments.get(0));
			} else if (opName.equals("intersection") && (arguments.get(0) instanceof OclSetValue)) {
				return	this.intersection((OclSetValue) arguments.get(0));
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


	protected	OclBagValue	union(OclCollectionValue arg) {
		return	(OclBagValue) super.doUnion(arg);
	}


	protected	OclBooleanValue	equal(OclBagValue arg) {
		if (this == arg) {
			return	getOclValuesFactory().createBooleanValue(true);
		}
		
		for (Iterator iter = bagElements.iterator(); iter.hasNext();) {
			OclValue element = (OclValue) iter.next();
			if (((OclIntegerValue) arg.count(element)).intValue() != ((OclIntegerValue) this.count(element)).intValue()) {
				return	getOclValuesFactory().createBooleanValue(false);
			}
		}

		return	getOclValuesFactory().createBooleanValue(this.size().intValue() == arg.size().intValue());
	}

	protected	OclBooleanValue	different(OclBagValue arg) {
		return	getOclValuesFactory().createBooleanValue(! this.equal(arg).booleanValue());
	}

	protected	OclCollectionValue	doIntersection(OclCollectionValue result, OclCollectionValue arg) {
		for (Iterator iter = this.bagElements.iterator(); iter.hasNext();) {
			OclValue element = (OclValue) iter.next();
			if (result.includes(element).isFalse()) {
				int intersection = Math.min((int) ((OclIntegerValue) arg.count(element)).intValue(), (int) ((OclIntegerValue) this.count(element)).intValue());
				for (int i = 0; i < intersection; i++) {
					result.add(element);
				}
			}
		}	

		return	result;
	}

	protected	OclSetValue	intersection(OclSetValue arg) {
		return (OclSetValue) doIntersection(new OclSetValue(OclTypesFactory.createSetType(this.getElementType())), arg);
	}

	protected	OclBagValue	intersection(OclBagValue arg) {
		return (OclBagValue) doIntersection(createEmptyCollection(), arg);
	}



	protected	OclBagValue	including(OclValue arg) {
		return (OclBagValue) super.doIncluding(arg);
	}

	protected	OclBagValue	excluding(OclValue arg) {
		return (OclBagValue) super.doExcluding(arg);
	}

	protected	OclCollectionValue	cloneCollection() {
		return	new OclBagValue((BagType) this.getType(), this);
	}

	public OclCollectionValue	createEmptyCollection() {
		return	new OclBagValue((BagType) this.getType());
	}
	
	protected	OclCollectionValue	createEmptyCollection(CoreClassifier elementType) {
		return	new OclBagValue((BagType) OclTypesFactory.createBagType(elementType));
	}

	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclCollectionValue#getCollectionName()
	 */
	protected String getCollectionName() {
		return "Bag";
	}
}
