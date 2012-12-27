/*
 * Created on Jun 28, 2004
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import ocl20.common.CoreAttribute;
import ocl20.common.CoreClassifier;
import ocl20.evaluation.OclValue;

import br.ufrj.cos.lens.odyssey.tools.psw.semantics.types.OclTypesFactory;

/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
public class OclTupleValue extends OclValueImpl {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	Map	attributeNameValues;
	private	Map	attributeNameAttributeTypeNames;
	private	Map	attributeNameAttributeTypes;

	
	public Object clone() {
		OclTupleValue theClone = (OclTupleValue) super.clone();
		
		theClone.attributeNameAttributeTypeNames = attributeNameAttributeTypeNames;
		theClone.attributeNameValues = attributeNameAttributeTypes;

		theClone.attributeNameValues = new HashMap();
		for (Iterator iterValues = this.attributeNameValues.keySet().iterator(); iterValues.hasNext(); ) {
			String	attName = (String) iterValues.next();
			theClone.attributeNameValues.put(attName, (OclValue) ((OclValue) this.attributeNameValues.get(attName)).clone());
		}

		return	theClone;
		
	}

	public	OclTupleValue(CoreClassifier  classifier, Map tupleValues) {
		this.setCoreType(classifier);
		initAttributeValues(classifier, tupleValues);
	}

	public OclTupleValue() {
	}

    /**
     * @return Returns the attributeNameAttributeTypeNames.
     */
    public Map getAttributeNameAttributeTypeNames() {
        return attributeNameAttributeTypeNames;
    }
    
    /**
     * @param attributeNameAttributeTypeNames The attributeNameAttributeTypeNames to set.
     */
    public void setAttributeNameAttributeTypeNames(
            Map attributeNameAttributeTypeNames) {
        this.attributeNameAttributeTypeNames = attributeNameAttributeTypeNames;
		attributeNameAttributeTypes  = new HashMap();
        for (Iterator iter = attributeNameAttributeTypeNames.keySet().iterator(); iter.hasNext();) {
            String	attributeName = (String) iter.next();
            CoreClassifier attributeType = OclTypesFactory.createTypeFromString((String) attributeNameAttributeTypeNames.get(attributeName));
            this.attributeNameAttributeTypes.put(attributeName, attributeType);
        }
    }
    /**
     * @return Returns the attributeNameValues.
     */
    public Map getAttributeNameValues() {
        return attributeNameValues;
    }
    /**
     * @param attributeNameValues The attributeNameValues to set.
     */
    public void setAttributeNameValues(Map attributeNameValues) {
        this.attributeNameValues = attributeNameValues;
    }
	
	private	void	initAttributeValues(CoreClassifier classifier, Map tupleValues) {
		attributeNameValues = new HashMap();
		attributeNameAttributeTypeNames = new HashMap();
		attributeNameAttributeTypes  = new HashMap();
		
		Collection allAttributes = this.getType().getAllAttributes();
		
		if (allAttributes.size() != tupleValues.size()) {
			throw new IllegalArgumentException("tuple values size does not match tuple type size");
		}
		
		for (Iterator iter = allAttributes.iterator(); iter.hasNext();) {
			CoreAttribute	attribute = (CoreAttribute) iter.next();
			attributeNameValues.put(attribute.getName(), getOclValuesFactory().createNullValue());
			attributeNameAttributeTypeNames.put(attribute.getName(), attribute.getFeatureType().getFullPathName());
			attributeNameAttributeTypes.put(attribute.getName(), attribute);
		}
		
		for (Iterator iterValues = tupleValues.keySet().iterator(); iterValues.hasNext(); ) {
			CoreAttribute attribute = (CoreAttribute) iterValues.next();
			this.setValueOf(attribute.getName(), (OclValue) tupleValues.get(attribute));
		}
		
	}

	public OclValue getValueOf(String attributeName) {
		for (Iterator iter = this.getType().getAllAttributes().iterator(); iter.hasNext(); ) {
			CoreAttribute attrib = (CoreAttribute) iter.next();
			if (attrib.getName().equals(attributeName)) {
				return	this.getValueOf(attrib);
			}
		}
		
		throw new IllegalArgumentException("attribute " + attributeName + " is not defined in: " + this.getType().getName());
	}

	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue#getValueOf(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreAttribute)
	 */
	private OclValue getValueOf(CoreAttribute attribute) {
		if (attribute != null) {
			Object		value = attributeNameValues.get(attribute.getName());
			
			if (value != null) {
				return	(OclValue) value;
			} else {
				throw new IllegalArgumentException("attribute " + attribute.getName() + " is not defined in: " + this.getType().getName());
			}
			
		} else {
			throw new IllegalArgumentException("invalid attribute: null");
		}
	}


	/* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclObjectValue#setValueOf(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreAttribute, br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValue)
	 */
	private void setValueOf(String attrName, OclValue newValue) {
	    CoreAttribute attribute = (CoreAttribute) attributeNameAttributeTypes.get(attrName);
	    
	    if (attribute != null) {
			if (newValue.getType().conformsTo(attribute.getFeatureType())) {
				attributeNameValues.put(attribute.getName(), newValue);
			}
			else {
				throw new IllegalArgumentException("type mismatch in attribute " + attribute.getName() + " expected: " + attribute.getFeatureType().getName() + " value: " + newValue.getType().getName());			
			}
	    } else {
	        throw new IllegalArgumentException("attribute " + attrName + " is not defined in: " + this.getType().getName()); 
	    }
	    
	}

	public OclValue executeOperation(
			String opName,
			List arguments) {
			try {
				if (opName.equals("=")) {
				   return	this.equal((OclValue) arguments.get(0));
				} else if (opName.equals("<>")) {
				   return	this.notEqual((OclValue) arguments.get(0));
				} else
					return	super.executeOperation(opName, arguments);
			} catch (RuntimeException e) {
				return	getOclValuesFactory().createInvalidValue();
			}
	}

	protected	OclValue	equal(OclValue arg) {
		if (this == arg) {
			return	getOclValuesFactory().createBooleanValue(true);
		}
		if (arg instanceof OclTupleValue) {
		    OclTupleValue	otherTuple = (OclTupleValue) arg;
		    
		    if (this.getAttributeNameValues().keySet().size() != otherTuple.getAttributeNameValues().keySet().size() ) 
		       return	getOclValuesFactory().createBooleanValue(false);

		    for (Iterator iter = this.getAttributeNameValues().keySet().iterator(); iter.hasNext();) {
		        String attribute = (String) iter.next();
		        OclValue value = (OclValue) this.getAttributeNameValues().get(attribute);
		        OclValue otherValue = (OclValue) otherTuple.getValueOf(attribute);
		        
		        if (! value.equals(otherValue))
		            return	getOclValuesFactory().createBooleanValue(false);
		    }
	        return	getOclValuesFactory().createBooleanValue(true);
		} else {
		    return	super.equal(arg);
		}    
	}
	

	public String toString() {
		StringBuffer result = new StringBuffer();
		
		result.append("Tuple{");

		boolean isFirst = true;
		for (Iterator iter = this.getType().getAllAttributes().iterator(); iter.hasNext();) {
		    CoreAttribute	attribute = (CoreAttribute) iter.next();
		    String attributeName = attribute.getName();
			OclValue value = (OclValue) attributeNameValues.get(attributeName);

			if (! isFirst)
				result.append(", ");
				
			result.append(attributeName);
			result.append("=");
			result.append(value.toString());
			isFirst = false;
		}
		result.append("}");
		return	result.toString();
	}
	
	/* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValueImpl#getOwnedElements()
     */
    public Collection getOwnedElements() {
        List	elements = new ArrayList();
		for (Iterator iter = this.getType().getAllAttributes().iterator(); iter.hasNext();) {
		    CoreAttribute	attribute = (CoreAttribute) iter.next();
			elements.add(new AttributeValueDTO(attribute.getName(), (OclValue) attributeNameValues.get(attribute.getName())));
		}
		return	 elements;
    }
}


