/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;


import impl.ocl20.common.CoreAssociationEndImpl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import javax.jmi.model.AssociationEnd;
import javax.jmi.model.ModelElement;

import org.netbeans.mdr.storagemodel.StorableObject;

import ocl20.common.CoreAssociation;
import ocl20.common.CoreClassifier;
import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.custommof.CustomMof14AssociationEnd;


/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public abstract class CustomMof14AssociationEndImpl extends CoreAssociationEndImpl implements CustomMof14AssociationEnd {

	public CustomMof14AssociationEndImpl(StorableObject object) {
		super(object);
	}

	protected CoreModel model;
	protected AssociationEnd mdrModelElement;
	protected Mof14JmiFactory factory;
	
	public void setFactory(Mof14JmiFactory factory) {
		this.factory = factory;
	}
	
	public void setMdrModelElement(ModelElement element) {
		this.mdrModelElement = (AssociationEnd) element;
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
	
	protected CoreAssociation getSpecificAssociation() {
		return	(CoreAssociation) factory.makeModelElement(mdrModelElement.getContainer());
	}
	
	protected List	getSpecificQualifiers() {
		return	new ArrayList();
	}
	
	protected CoreClassifier getSpecificParticipant() {
		return	(CoreClassifier) factory.makeModelElement(mdrModelElement.getType());
	}
	
	protected boolean getSpecificIsMandatory() {
		return  mdrModelElement.getMultiplicity().getLower() != 0;
	}
	
	protected boolean getSpecificIsOneMultiplicity() {
		return mdrModelElement.getMultiplicity().getUpper() == 1;
	}
	
	protected boolean getSpecificIsOrdered() {
		return	false;
	}
}
