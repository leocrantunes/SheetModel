package impl.ocl20.constraints;

import ocl20.common.CoreClassifier;

public class OclModifiableTypeDefinition : OclModifiableConstraint {

	private	CoreClassifier	classifier;
	private	OclModifiableDeclarationConstraint	owner;
	
	public OclModifiableTypeDefinition() {
		
	}

	public CoreClassifier getClassifier() {
		return classifier;
	}

	public void setClassifier(CoreClassifier classifier) {
		this.classifier = classifier;
	}
	
	public OclModifiableDeclarationConstraint getOwner() {
		return owner;
	}

	public void setOwner(OclModifiableDeclarationConstraint owner) {
		this.owner = owner;
	}

	@Override
	public Object clone() {
		OclModifiableTypeDefinition theClone = (OclModifiableTypeDefinition) super.clone();
		theClone.classifier = classifier;
		return	theClone;
	}
	
	
}
