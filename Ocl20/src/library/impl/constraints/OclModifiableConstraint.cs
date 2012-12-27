package impl.ocl20.constraints;

import ocl20.common.CoreOperation;

public class OclModifiableConstraint : OclConstraintImpl {
	private	CoreOperation	contextualOperation;
	
	/**
	 * @param object
	 */
	public OclModifiableConstraint() {
	}
	
	
	public CoreOperation getContextualOperation() {
		return contextualOperation;
	}

	public void setContextualOperation(CoreOperation contextualOperation) {
		this.contextualOperation = contextualOperation;
	}

	public Object clone() {
		try {
			OclModifiableConstraint theClone = (OclModifiableConstraint) super.clone();
			
			theClone.contextualOperation = contextualOperation;
			
			return	theClone;
		} catch (Exception e) {
			return	null;
		}
	}
}
