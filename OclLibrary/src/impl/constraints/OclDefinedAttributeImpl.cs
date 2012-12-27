using System;
using System.Collections.Generic;
using OclLibrary.iface.common;
using OclLibrary.iface.constraints;
using OclLibrary.impl.common;

namespace OclLibrary.impl.constraints
{
    public abstract class OclDefinedAttributeImpl : CoreAttributeImpl, OclDefinedAttribute {

        private CoreClassifier type;
        private String name;
        private CoreClassifier owner;
        private String source;
        
        public override bool isOclDefined() {
            return	true;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreStructuralFeature#getType()
     */
        public override CoreClassifier getFeatureType() {
            return this.type;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreModelElement#getName()
     */
        public override String getName() {
            return this.name;
        }

        public override CoreClassifier getFeatureOwner() {
            return this.owner;
        }

        public override bool isInstanceScope() {
            return true;
        }

        public bool isClassifierScope() {
            return true;
        }

        public String getSource() {
            return this.source;
        }
    
        /* (non-Javadoc)
	 * @see java.lang.Comparable#compareTo(java.lang.Object)
	 */
        public int compareTo(Object arg0) {
            return getName().ToUpper().CompareTo( ((CoreModelElement) arg0).getName().ToUpper());
        }

        public	Object[] getOwnedElements() {
            List<object>	allOwnedElements = new List<object>();
            OclConstraint constraint;
		
            constraint = owner.getInitConstraint(this.getName());
            if (constraint != null)
                allOwnedElements.Add(constraint);
			
            constraint = owner.getDeriveConstraint(this.getName());
            if (constraint != null)
                allOwnedElements.Add(constraint);

            return 	allOwnedElements.ToArray();
        }

	
	
        /* (non-Javadoc)
	 * @see ocl20.common.CoreStructuralFeature#setFeatureType(ocl20.common.CoreClassifier)
	 */
        public override void setFeatureType(CoreClassifier newValue) {
            this.type = newValue;

        }
        /* (non-Javadoc)
	 * @see ocl20.common.CoreFeature#setFeatureOwner(ocl20.common.CoreClassifier)
	 */
        public override void setFeatureOwner(CoreClassifier newValue) {
            this.owner = newValue;

        }
        /* (non-Javadoc)
	 * @see ocl20.common.CoreModelElement#getTheStereotypes()
	 */
        public override List<object> getTheStereotypes() {
            return new List<object>();
        }
        /* (non-Javadoc)
	 * @see ocl20.common.CoreModelElement#setElemOwner(ocl20.common.CoreModelElement)
	 */
        public override void setElemOwner(CoreModelElement newValue) {
            this.owner = (CoreClassifier) newValue;
        }
        /* (non-Javadoc)
	 * @see ocl20.common.CoreModelElement#setName(java.lang.String)
	 */
        public override void setName(String newValue) {
            this.name = newValue;
        }

        /**
	 * @param source The source to set.
	 */
        public void setSource(String source) {
            this.source = source;
        }
	

    }
}
