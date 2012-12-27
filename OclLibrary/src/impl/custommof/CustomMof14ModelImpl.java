/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;

import impl.ocl20.common.CoreModelImpl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import javax.jmi.model.ModelElement;
import javax.jmi.model.ModelPackage;

import org.netbeans.mdr.storagemodel.StorableObject;

import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.custommof.CustomMof14Model;



/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public abstract class CustomMof14ModelImpl extends CoreModelImpl implements CustomMof14Model {

	private	ModelPackage	mdrModelPackage;
	private	Map associations = new HashMap();
	private	String name;

	protected CustomMof14ModelImpl(StorableObject object) {
		super(object);
	}
	
	public void setMdrModelPackage(ModelPackage modelPackage) {
		this.mdrModelPackage = modelPackage;
	}
	
	protected Mof14JmiFactory factory;
	
	public void setFactory(Mof14JmiFactory factory) {
		this.factory = factory;
	}
	

	public CoreModel getModel() {
		return	this;
	}

	
	protected CoreModelElement getSpecificOwnerElement() {
		return	null;
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
		return name;
	}
	
    protected Collection getSpecificOwnedElements() {
		List result = new ArrayList();
		
		for (Iterator it = mdrModelPackage.getModelElement().refAllOfType().iterator(); it.hasNext(); ) {
			ModelElement e = (ModelElement) it.next();
			CoreModelElement element = factory.makeModelElement(e);
//			String elemAsString = "null";
//
//			if (element != null) {
//				elemAsString = element.getName() + " - " + element.getClass().getName();	
//			}
//			System.out.println("element = " + e.getName() + " - " + e.getClass().getName() + "  ***  " + elemAsString);
			if (element != null)
				result.add(element);
		}
		return	result;
    }

    public void setName(String name) {
    	this.name = name;
    }
	
	public String getName() {
		return	name;
	}
	
	public Collection getSpecificAllClassifier() {
		return	mdrModelPackage.getClassifier().refAllOfType();
	}
	

}
