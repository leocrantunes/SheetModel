using System;
using OclLibrary.iface.common;

namespace OclLibrary.impl.constraints
{
    public abstract class OclClassifierConstraintImpl : OclConstraintImpl {

	
        private	CoreClassifier	contextualClassifier;
	
        /**
	 * 
	 */
        public OclClassifierConstraintImpl() {
            // TODO Auto-generated constructor stub
        }

	
	
        /**
	 * @return Returns the contextualClassifier.
	 */
        public CoreClassifier getContextualClassifier() {
            return contextualClassifier;
        }
        /**
	 * @param contextualClassifier The contextualClassifier to set.
	 */
        public void setContextualClassifier(CoreClassifier contextualClassifier) {
            this.contextualClassifier = contextualClassifier;
        }
	
        public override Object Clone() {
            OclClassifierConstraintImpl theClone = (OclClassifierConstraintImpl) base.Clone();
		
            theClone.contextualClassifier = contextualClassifier;
		
            return	theClone;
        }

    }
}
