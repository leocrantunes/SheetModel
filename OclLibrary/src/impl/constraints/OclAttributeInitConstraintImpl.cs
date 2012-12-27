using System;
using OclLibrary.iface.common;
using OclLibrary.iface.constraints;
using OclLibrary.impl.constraints;

public class OclAttributeInitConstraintImpl : OclClassifierConstraintImpl, OclAttributeInitConstraint {

	private	CoreAttribute	initializedAttribute;
	
	/**
	 * @param object
	 */
	public OclAttributeInitConstraintImpl() {
	}

	public	String	toString() {
		return	"init: " +  this.getExpression().ToString(); 
	}

	
	
	/**
	 * @return Returns the initializedAttribute.
	 */
	public CoreAttribute getInitializedAttribute() {
		return initializedAttribute;
	}
	/**
	 * @param initializedAttribute The initializedAttribute to set.
	 */
	public void setInitializedAttribute(CoreAttribute initializedAttribute) {
		this.initializedAttribute = initializedAttribute;
	}
	
	public override Object Clone() {
		OclAttributeInitConstraintImpl theClone = (OclAttributeInitConstraintImpl) base.Clone();
		
		theClone.initializedAttribute = initializedAttribute;
		
		return	theClone;
	}

}
