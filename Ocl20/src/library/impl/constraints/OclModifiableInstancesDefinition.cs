package impl.ocl20.constraints;

import ocl20.expressions.OclExpression;

public class OclModifiableInstancesDefinition : OclModifiableConstraint {

	private	OclExpression	instancesExpression;
	private	OclModifiableDeclarationConstraint	owner;
	
	public	OclModifiableInstancesDefinition() {
	}

	public OclExpression getInstancesExpression() {
		return instancesExpression;
	}

	public void setInstancesExpression(OclExpression instancesExpression) {
		this.instancesExpression = instancesExpression;
	}

	public OclModifiableDeclarationConstraint getOwner() {
		return owner;
	}

	public void setOwner(OclModifiableDeclarationConstraint owner) {
		this.owner = owner;
	}

	@Override
	public Object clone() {
		OclModifiableInstancesDefinition theClone = (OclModifiableInstancesDefinition) super.clone();
		theClone.instancesExpression = instancesExpression;
		return	theClone;
	}

	
}
