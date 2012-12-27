/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;

import java.util.ArrayList;
import java.util.Collection;


import impl.ocl20.common.CoreEnumLiteralImpl;

import org.netbeans.mdr.storagemodel.StorableObject;

import ocl20.common.CoreEnumeration;
import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;
import ocl20.custommof.CustomMof14EnumLiteral;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public abstract class CustomMof14EnumLiteralImpl extends CoreEnumLiteralImpl implements CustomMof14EnumLiteral {

	private	CoreEnumeration	enumeration;
	private	String	literal;
	
	public CustomMof14EnumLiteralImpl(StorableObject object) {
		super(object);
	}

	public CoreModel getModel() {
		return	enumeration.getModel();
	}
	
	protected CoreModelElement getSpecificOwnerElement() {
		return	enumeration;
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
	
	public String toString() {
		return getName();
	}
	
	/* (non-Javadoc)
	 * @see ocl20.CoreEnumLiteral#getTheEnumeration()
	 */
	public CoreEnumeration getTheEnumeration() {
		return this.enumeration;
	}
	
	/* (non-Javadoc)
	 * @see ocl20.CoreEnumLiteral#setTheEnumeration(ocl20.CoreEnumeration)
	 */
	public void setTheEnumeration(CoreEnumeration newValue) {
		this.enumeration = newValue;
	}
	
	/* (non-Javadoc)
	 * @see customMof.CustomMof14ModelElement#getName()
	 */
	public String getName() {
		return	literal;
	}
	
	public void setName(String name) {
		this.literal = name;
	}
	
	/* (non-Javadoc)
	 * @see customMof.CustomMof14ModelElement#getElemOwner()
	 */
	public CoreModelElement getElemOwner() {
		return	getTheEnumeration();
	}
}
