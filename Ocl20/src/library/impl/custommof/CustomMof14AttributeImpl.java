/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;

import impl.ocl20.common.CoreAttributeImpl;

import java.util.ArrayList;
import java.util.Collection;

import javax.jmi.model.Attribute;
import javax.jmi.model.ModelElement;

import org.netbeans.mdr.storagemodel.StorableObject;

import ocl20.common.CoreAssociationEnd;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.custommof.CustomMof14Attribute;



/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public abstract class CustomMof14AttributeImpl extends CoreAttributeImpl implements CustomMof14Attribute {

    protected CustomMof14AttributeImpl(StorableObject object) {
    	super(object);
    }

	protected CoreModel model;
	protected Attribute mdrModelElement;
	protected Mof14JmiFactory factory;
	
	public void setFactory(Mof14JmiFactory factory) {
		this.factory = factory;
	}
	
	public void setMdrModelElement(ModelElement element) {
		this.mdrModelElement = (Attribute) element;
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
    
	public boolean getSpecificIsDerived() {
		return	mdrModelElement.isDerived();
	}

	protected CoreAssociationEnd getSpecificAssociationEnd() {
		return	null;
	}

	protected CoreClassifier getSpecificType() {
		return	(CoreClassifier) factory.makeModelElement(mdrModelElement.getType());
	}

	public boolean getSpecificIsInstanceScope() {
		return	mdrModelElement.getScope() == javax.jmi.model.ScopeKindEnum.INSTANCE_LEVEL;
	}
}
