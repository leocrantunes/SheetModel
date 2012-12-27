/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;

import impl.ocl20.common.CoreOperationImpl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;

import javax.jmi.model.DirectionKindEnum;
import javax.jmi.model.Feature;
import javax.jmi.model.ModelElement;
import javax.jmi.model.Operation;
import javax.jmi.model.Parameter;

import org.netbeans.mdr.storagemodel.StorableObject;

import ocl20.common.CoreClassifier;
import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.custommof.CustomMof14Operation;



/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public abstract class CustomMof14OperationImpl extends CoreOperationImpl implements CustomMof14Operation {

    public CustomMof14OperationImpl(StorableObject object) {
    	super(object);
    }
	
	protected CoreModel model;
	protected Operation mdrModelElement;
	protected Mof14JmiFactory factory;
	
	public void setFactory(Mof14JmiFactory factory) {
		this.factory = factory;
	}
	
	public void setMdrModelElement(ModelElement element) {
		this.mdrModelElement = (Operation) element;
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
	
	public boolean getSpecificIsQuery() {
		return	this.mdrModelElement.isQuery();
	}
	
	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return mdrModelElement.toString();
	}

	
	protected boolean getSpecificHasStereotype(String name) {
		return	false;
	}
	
	public boolean getSpecificIsInstanceScope() {
		return	((Feature) mdrModelElement).getScope() == javax.jmi.model.ScopeKindEnum.INSTANCE_LEVEL;
	}

    protected CoreClassifier getSpecificReturnParameterType() {
    	for (Iterator iter = getAllParameters().iterator(); iter.hasNext();) {
  			Parameter p = (Parameter) iter.next();
   			if (p.getDirection() == DirectionKindEnum.RETURN_DIR)
   				return	(CoreClassifier) factory.makeModelElement(p.getType());
    	}
    	
    	return	null;
    }
    
    protected List getSpecificParameterTypesExceptReturn() {
    	List	result = new ArrayList();
    	
 	    for (Iterator iter = getAllParameters().iterator(); iter.hasNext();) {
   	   		Parameter p = (Parameter) iter.next();

    		if (p.getDirection() != DirectionKindEnum.RETURN_DIR)
    			result.add((CoreClassifier) factory.makeModelElement(p.getType()));
 	    }	
 	    
    	return	result;
    }
    
    protected List getSpecificParameterNamesExceptReturn() {
    	List	result = new ArrayList();

    	for (Iterator iter = getAllParameters().iterator(); iter.hasNext();) {
   			Parameter p = (Parameter) iter.next();
   			if (p.getDirection() != DirectionKindEnum.RETURN_DIR)
   				result.add(p.getName());
    	}
    	
    	return	result;
    }

    protected List	getAllParameters() {
    	List result = new ArrayList();
    	
    	Operation		theOperation = (Operation) mdrModelElement;
    	for (Iterator iter = theOperation.getContents().iterator(); iter.hasNext();) {
    		ModelElement	element = (ModelElement) iter.next();
    		if (element instanceof Parameter) {
    			result.add(element);
    		}
    	}    	
    	
    	return	result;
    }
}
