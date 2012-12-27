/*
 * Created on 12/01/2005
 *
 * TODO To change the template for this generated file go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
package br.ufrj.cos.lens.odyssey.tools.psw.semantics.values;

import java.util.Collection;

import ocl20.evaluation.OclValue;

/**
 * @author Administrator
 *
 * TODO To change the template for this generated type comment go to
 * Window - Preferences - Java - Code Style - Code Templates
 */
public class AttributeValueDTO  extends OclValueImpl implements Comparable {
    /**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private	String	name;
    private	OclValue	oclValue;
    
	public Object clone() {
		AttributeValueDTO  theClone = (AttributeValueDTO) super.clone();
		if (name != null)
			theClone.name = new String(name);
		theClone.oclValue = (OclValue) oclValue.clone();
		return	theClone;
	}

    /**
     * @param name
     * @param oclValue
     */
    public AttributeValueDTO(String name, OclValue oclValue)  {
        this.name = name;
        this.oclValue = oclValue;
    }
    /**
     * @return Returns the name.
     */
    public String getName() {
        return name;
    }
    /**
     * @param name The name to set.
     */
    public void setName(String name) {
        this.name = name;
    }
    /**
     * @return Returns the oclValue.
     */
    public OclValue getOclValue() {
        return oclValue;
    }
    /**
     * @param oclValue The oclValue to set.
     */
    public void setOclValue(OclValue oclValue) {
        this.oclValue = oclValue;
    }
    
    /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.semantics.values.OclValueImpl#getOwnedElements()
     */
    public Collection getOwnedElements() {
        return oclValue.getOwnedElements();
    }
    
    /* (non-Javadoc)
     * @see java.lang.Comparable#compareTo(java.lang.Object)
     */
    public int compareTo(Object arg0) {
        return	 this.name.compareTo(((AttributeValueDTO) arg0).getName());
    }
}
