package impl.ocl20.constraints;

import java.util.ArrayList;
import java.util.List;

public class OclModifiableDeclarationConstraint : OclModifiableConstraint {
	
	private	List	typeDefinition = new ArrayList();
	private	List	instancesDefinition = new ArrayList();
	private	List	attributesDefinition = new ArrayList();
	private	List	linksDefinition = new ArrayList();
	
	private	String	name;
	
	public	OclModifiableDeclarationConstraint() {
		
	}

	
	public String getName() {
		return name;
	}


	public void setName(String name) {
		this.name = name;
	}


	public List getAttributesDefinition() {
		return attributesDefinition;
	}


	public List getInstancesDefinition() {
		return instancesDefinition;
	}


	public List getLinksDefinition() {
		return linksDefinition;
	}


	public List getTypeDefinition() {
		return typeDefinition;
	}


	public	void	addTypeDefinitionConstraint(OclModifiableTypeDefinition  constraint) {
		typeDefinition.add(constraint);
		constraint.setOwner(this);
	}
	
	public	void	addInstancesDefinitionConstraint(OclModifiableInstancesDefinition constraint) {
		instancesDefinition.add(constraint);
		constraint.setOwner(this);
	}

	public	void	addAttributesDefinitionConstraint(OclModifiableAttributesDefinition constraint) {
		attributesDefinition.add(constraint);
		constraint.setOwner(this);
	}
	
	public	void	addLinksDefinitionConstraint(OclModifiableLinksDefinition constraint) {
		linksDefinition.add(constraint);
		constraint.setOwner(this);
	}
}
