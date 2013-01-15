using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.impl.common;

namespace Ocl20.library.impl.constraints
{
    public abstract class OclDefinedAttributeImpl : CoreAttributeImpl, OclDefinedAttribute {

        private CoreClassifier type;
        private String name;
        private CoreClassifier owner;
        private String source;
        
        public override bool isOclDefined() {
            return	true;
        }

        public override CoreClassifier getFeatureType() {
            return this.type;
        }

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

        public override int CompareTo(Object arg0) {
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

        public override void setFeatureType(CoreClassifier newValue) {
            this.type = newValue;

        }

        public override void setFeatureOwner(CoreClassifier newValue) {
            this.owner = newValue;

        }

        public override void setElemOwner(CoreModelElement newValue) {
            this.owner = (CoreClassifier) newValue;
        }

        public override void setName(String newValue) {
            this.name = newValue;
        }

        public void setSource(String source) {
            this.source = source;
        }
    }
}
