package impl.ocl20.constraints;

import ocl20.common.CoreAssociationEnd;
import ocl20.expressions.OclExpression;

public class OclModifiableLinksDefinition : OclModifiableConstraint {

	private	OclExpression	sourceInstancesExpression;;
	private	CoreAssociationEnd	associationEnd;
	private	OclExpression	targetInstancesExpression;
	private	OclModifiableDeclarationConstraint	owner;

	
	public	OclModifiableLinksDefinition() {
		
	}


	public CoreAssociationEnd getAssociationEnd() {
		return associationEnd;
	}


	public void setAssociationEnd(CoreAssociationEnd associationEnd) {
		this.associationEnd = associationEnd;
	}


	public OclExpression getSourceInstancesExpression() {
		return sourceInstancesExpression;
	}


	public void setSourceInstancesExpression(OclExpression sourceInstancesExpression) {
		this.sourceInstancesExpression = sourceInstancesExpression;
	}


	public OclExpression getTargetInstancesExpression() {
		return targetInstancesExpression;
	}


	public void setTargetInstancesExpression(OclExpression targetInstancesExpression) {
		this.targetInstancesExpression = targetInstancesExpression;
	}
	
	public OclModifiableDeclarationConstraint getOwner() {
		return owner;
	}

	public void setOwner(OclModifiableDeclarationConstraint owner) {
		this.owner = owner;
	}
	
@Override
	public Object clone() {
		OclModifiableLinksDefinition theClone = (OclModifiableLinksDefinition) super.clone();
		theClone.sourceInstancesExpression = sourceInstancesExpression != null ? (OclExpression) sourceInstancesExpression.clone() : null;
		theClone.associationEnd = associationEnd;
		theClone.targetInstancesExpression = targetInstancesExpression != null ? (OclExpression) targetInstancesExpression.clone() : null;
		return	theClone;
	}	
}
