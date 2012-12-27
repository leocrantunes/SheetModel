/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;


import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;

import javax.jmi.model.ModelElement;
import javax.jmi.model.Namespace;

import impl.ocl20.common.CorePackageImpl;

import org.netbeans.mdr.storagemodel.StorableObject;

import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.custommof.CustomMof14Package;



/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public abstract class CustomMof14PackageImpl extends CorePackageImpl implements CustomMof14Package {

	public CustomMof14PackageImpl(StorableObject object) {
		super(object);
	}
	
	protected CoreModel model;
	protected Namespace mdrModelElement;
	protected Mof14JmiFactory factory;
	
	public void setFactory(Mof14JmiFactory factory) {
		this.factory = factory;
	}
	
	public void setMdrModelElement(ModelElement element) {
		this.mdrModelElement = (Namespace) element;
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
	
    protected Collection getSpecificOwnedElements() {
    	Collection result = new ArrayList();
    	
    	for (Iterator iter = ((Namespace) mdrModelElement).getContents().iterator(); iter.hasNext();) {
    		CoreModelElement element = factory.makeModelElement((ModelElement) iter.next());
    		if (element != null)
    			result.add(element);
    	}
    	return	result;
    }

	
}
