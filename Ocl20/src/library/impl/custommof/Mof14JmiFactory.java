/*
 * Created on 02/03/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package impl.ocl20.custommof;


import java.util.HashMap;
import java.util.Map;

import javax.jmi.model.Association;
import javax.jmi.model.AssociationEnd;
import javax.jmi.model.Attribute;
import javax.jmi.model.Classifier;
import javax.jmi.model.DataType;
import javax.jmi.model.EnumerationType;
import javax.jmi.model.ModelElement;
import javax.jmi.model.ModelPackage;
import javax.jmi.model.MofPackage;
import javax.jmi.model.Operation;

import ocl20.Ocl20Package;
import ocl20.common.CoreEnumLiteral;
import ocl20.common.CoreEnumeration;
import ocl20.common.CoreModel;
import ocl20.common.CoreModelElement;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class Mof14JmiFactory  {

	private Ocl20Package oclPackage;
	private	Map	elementsCreated = new HashMap();
 	private	CoreModel currentModel = null;

 	public Mof14JmiFactory(Ocl20Package oclPackage) {
 		this.oclPackage = oclPackage;
 	}
 	
	public	void releaseAllElements() {
		elementsCreated = new HashMap();
	}

	public CoreModelElement	makeModel(String modelName, ModelPackage model) {
		CustomMof14ModelImpl jmiElement = (CustomMof14ModelImpl) getElementInMap(model);
		if (jmiElement == null) {
			jmiElement = (CustomMof14ModelImpl) oclPackage.getCustomMof().getCustomMof14Model().createCustomMof14Model();
			jmiElement.setMdrModelPackage(model);
			jmiElement.setName(modelName);
			jmiElement.setFactory(this);
			
			elementsCreated.put(model, jmiElement);
			currentModel = (CoreModel) jmiElement;
		}
		return	jmiElement;
	}

	public CoreModelElement	makeModelElement(ModelElement element) {
		CoreModelElement jmiElement = getElementInMap(element);
		if (jmiElement == null) {
			jmiElement = createNewElement(element);
			elementsCreated.put(element, jmiElement);
		}
		return	jmiElement;
	}

	public CoreEnumLiteral makeEnumLiteral(CoreEnumeration enumeration, String literal) {
		CustomMof14EnumLiteralImpl enumLiteral= (CustomMof14EnumLiteralImpl) getElementInMap(enumeration.getName() + literal);
		if (enumLiteral == null) {
			enumLiteral = (CustomMof14EnumLiteralImpl) oclPackage.getCustomMof().getCustomMof14EnumLiteral().createCustomMof14EnumLiteral();
			enumLiteral.setTheEnumeration(enumeration);
			enumLiteral.setName(literal);
			elementsCreated.put(enumeration.getName() + literal, enumLiteral);
		}
		return	enumLiteral;
	}

	private CoreModelElement	createNewElement(ModelElement element) {
			
			if (element instanceof MofPackage) {
				CustomMof14PackageImpl createdElement = (CustomMof14PackageImpl) oclPackage.getCustomMof().getCustomMof14Package().createCustomMof14Package();
				createdElement.setMdrModelElement(element);
				createdElement.setModel(currentModel);
				createdElement.setFactory(this);
				return	createdElement;
			}

			else if (element instanceof Association) {
				CustomMof14AssociationImpl createdElement = (CustomMof14AssociationImpl) oclPackage.getCustomMof().getCustomMof14Association().createCustomMof14Association();
				createdElement.setMdrModelElement(element);
				createdElement.setModel(currentModel);
				createdElement.setFactory(this);
				return	createdElement;
			}
			
			else if (element instanceof AssociationEnd) {
				CustomMof14AssociationEndImpl createdElement = (CustomMof14AssociationEndImpl) oclPackage.getCustomMof().getCustomMof14AssociationEnd().createCustomMof14AssociationEnd();
				createdElement.setMdrModelElement(element);
				createdElement.setModel(currentModel);
				createdElement.setFactory(this);
				return	createdElement;
			}
			
			else if (element instanceof EnumerationType) {
				CustomMof14EnumerationImpl createdElement = (CustomMof14EnumerationImpl) oclPackage.getCustomMof().getCustomMof14Enumeration().createCustomMof14Enumeration();
				createdElement.setMdrModelElement(element);
				createdElement.setModel(currentModel);
				createdElement.setFactory(this);
				return	createdElement;
			}
			
			else if (element instanceof DataType) {
				CustomMof14DataTypeImpl createdElement = (CustomMof14DataTypeImpl) oclPackage.getCustomMof().getCustomMof14DataType().createCustomMof14DataType();
				createdElement.setMdrModelElement(element);
				createdElement.setModel(currentModel);
				createdElement.setFactory(this);
				return	createdElement;
			}
				
			else if (element instanceof Classifier) {
				CustomMof14ClassifierImpl createdElement = (CustomMof14ClassifierImpl) oclPackage.getCustomMof().getCustomMof14Classifier().createCustomMof14Classifier();
				createdElement.setMdrModelElement(element);
				createdElement.setModel(currentModel);
				createdElement.setFactory(this);
				return	createdElement;
			}
			
			else if (element instanceof Attribute) {
				CustomMof14AttributeImpl createdElement = (CustomMof14AttributeImpl) oclPackage.getCustomMof().getCustomMof14Attribute().createCustomMof14Attribute();
				createdElement.setMdrModelElement(element);
				createdElement.setModel(currentModel);
				createdElement.setFactory(this);
				return	createdElement;
			}
				
			else if (element instanceof Operation) {
				CustomMof14OperationImpl createdElement = (CustomMof14OperationImpl) oclPackage.getCustomMof().getCustomMof14Operation().createCustomMof14Operation();
				createdElement.setMdrModelElement(element);
				createdElement.setModel(currentModel);
				createdElement.setFactory(this);
				return	createdElement;
			}

			return	null;
	}
	
	private	CoreModelElement getElementInMap(Object element) {
		return (CoreModelElement) elementsCreated.get(element);
	}
}
