/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;


import impl.ocl20.common.CoreEnumerationImpl;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;

import javax.jmi.model.Classifier;
import javax.jmi.model.EnumerationType;
import javax.jmi.model.Feature;
import javax.jmi.model.ModelElement;
import javax.jmi.model.Namespace;

import org.netbeans.mdr.storagemodel.StorableObject;

import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.custommof.CustomMof14Enumeration;



/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public abstract class CustomMof14EnumerationImpl extends CoreEnumerationImpl implements CustomMof14Enumeration {

	/**
	 * @param storable
	 */
	public CustomMof14EnumerationImpl(StorableObject object) {
		super(object);
	}

	protected CoreModel model;
	protected EnumerationType mdrModelElement;
	protected Mof14JmiFactory factory;
	
	public void setFactory(Mof14JmiFactory factory) {
		this.factory = factory;
	}
	
	public void setMdrModelElement(ModelElement element) {
		this.mdrModelElement = (EnumerationType) element;
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

	
	public boolean isEnumeration() {
		return	true;
	}
	

    protected List getSpecificEnumLiterals() {
		EnumerationType		enumeration = (EnumerationType) mdrModelElement;
    	List result = new ArrayList();
		
		for (Iterator iter = enumeration.getLabels().iterator(); iter.hasNext();) {
			result.add(factory.makeEnumLiteral(this, (String) iter.next()));
		}

		return	result;
    }

    protected boolean getSpecificIsEnumeration() {
    	return	false;
    }
    protected Collection getSpecificClassifierAncestors() {
    	List result = new ArrayList();
    	for (Iterator iter = ((Classifier) mdrModelElement).getSupertypes().iterator(); iter.hasNext();) {
    		result.add(factory.makeModelElement((ModelElement) iter.next()));
    	}
    	return	result;
    }
    
    protected Collection getSpecificClassifierInterfaces() {
    	return	new ArrayList();
    }
    protected Collection getSpecificClassifierFeatures() {
    	List result = new ArrayList();
    	for (Iterator iter = ((Namespace) mdrModelElement).getContents().iterator(); iter.hasNext();) {
    		ModelElement elem = (ModelElement) iter.next();
    		if (elem instanceof Feature) {
    			CoreModelElement element = factory.makeModelElement(elem);
    			if (element != null)
    				result.add(element);
    		}
    	}
    	return	result;
    }
	protected boolean getSpecificIsConcrete() {
		return ! ((Classifier) mdrModelElement).isAbstract();
	}
	protected Collection getSpecificSubClasses() {
    	Collection result = new HashSet();
    	
    	Classifier cls = (Classifier) mdrModelElement;
    	Collection allClassifiers = ((CustomMof14ModelImpl) this.getModel()).getSpecificAllClassifier();
    	
    	for (Iterator it = allClassifiers.iterator(); it.hasNext();) {
    		Classifier aClass = (Classifier) it.next();
    		if (aClass.getSupertypes().contains(cls)) {
    			result.add(factory.makeModelElement(aClass));
    		}
    	}
    	return	result;
	}
	protected Collection getSpecificAssociationEnds() {
		return	new ArrayList();
	}

}
