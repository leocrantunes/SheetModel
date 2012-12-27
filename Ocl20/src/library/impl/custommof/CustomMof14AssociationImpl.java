/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;

import impl.ocl20.common.CoreAssociationImpl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import javax.jmi.model.Association;
import javax.jmi.model.AssociationEnd;
import javax.jmi.model.ModelElement;

import org.netbeans.mdr.storagemodel.StorableObject;

import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.custommof.CustomMof14Association;



/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public abstract class CustomMof14AssociationImpl extends CoreAssociationImpl implements CustomMof14Association {

	protected CustomMof14AssociationImpl(StorableObject object) {
		super(object);
	}
	
	protected CoreModel model;
	protected Association mdrModelElement;
	protected Mof14JmiFactory factory;
	
	public void setFactory(Mof14JmiFactory factory) {
		this.factory = factory;
	}
	
	public void setMdrModelElement(ModelElement element) {
		this.mdrModelElement = (Association) element;
	}

	public void setModel(CoreModel model) {
		this.model = model;
	}

	public ModelElement getMdrModelElement() {
		return	mdrModelElement;
	}

	public CoreModel getModel() {
		return	model;
	}

	public String	getName() {
		return	this.mdrModelElement.getName();
	}
	
	
	protected CoreModelElement getSpecificOwnerElement() {
		return	factory.makeModelElement(mdrModelElement.getContainer());
	}

	protected Collection getSpecificOwnedElements() {
		return	new ArrayList();
	}
	
	protected boolean getSpecificHasDirectStereotype() {
		return	true;
	}
	
	protected Collection getSpecificStereotypes() {
		return	new ArrayList();
	}
	
	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return mdrModelElement.toString();
	}

	public	Collection	 getSpecificAssociationEnds() {
    	List result = new ArrayList();
    	
    	for (Iterator iter = mdrModelElement.getContents().iterator(); iter.hasNext();) {
    		ModelElement elem = (ModelElement) iter.next();
    		if (elem instanceof AssociationEnd)
    			result.add(factory.makeModelElement(elem));
    	}
    	
    	return	result;
	}
}
