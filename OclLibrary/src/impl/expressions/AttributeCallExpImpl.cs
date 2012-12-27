using System;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class AttributeCallExpImpl : ModelPropertyCallExpImpl, AttributeCallExp {

        private	CoreAttribute	referredAttribute;
	
        /**
	 * @param object
	 */
        public AttributeCallExpImpl() {
        }
	
        public override void accept(IASTOclVisitor visitor) {
            base.accept(visitor);
            visitor.visitAttributeCallExp(this);
        }
	
        protected override String getSpecificString() {
            return	this.getReferredAttribute().getName();
        }
	
	
        /**
	 * @return Returns the referredAttribute.
	 */
        public CoreAttribute getReferredAttribute() {
            return referredAttribute;
        }
        /**
	 * @param referredAttribute The referredAttribute to set.
	 */
        public void setReferredAttribute(CoreAttribute referredAttribute) {
            this.referredAttribute = referredAttribute;
        }
	
        public override Object Clone() {
            AttributeCallExpImpl theClone = (AttributeCallExpImpl) base.Clone();
            theClone.referredAttribute = referredAttribute;
            return	theClone;
        }

    }
}
