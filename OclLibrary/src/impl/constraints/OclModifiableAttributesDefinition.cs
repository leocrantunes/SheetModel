using OclLibrary.iface.common;

public class OclModifiableAttributesDefinition : OclModifiableInstancesDefinition {

	private	CoreAttribute		attribute;
	private	OclModifiableDeclarationConstraint	owner;
	
	public OclModifiableAttributesDefinition() {
	}

	public CoreAttribute getAttribute() {
		return attribute;
	}

	public void setAttribute(CoreAttribute attribute) {
		this.attribute = attribute;
	}

	public OclModifiableDeclarationConstraint getOwner() {
		return owner;
	}

	public void setOwner(OclModifiableDeclarationConstraint owner) {
		this.owner = owner;
	}
	
	@Override
	public Object clone() {
		OclModifiableAttributesDefinition theClone = (OclModifiableAttributesDefinition) super.clone();
		theClone.attribute = attribute;
		return	theClone;
	}
	
	
}
