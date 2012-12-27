using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.impl.constraints
{
    public class OclAttributeDeriveConstraintImpl : OclClassifierConstraintImpl, OclAttributeDeriveConstraint {

        private	CoreAttribute		derivedAttribute;
	
        /**
	 * @param object
	 */
        public OclAttributeDeriveConstraintImpl() {
        }

        public override String	ToString() {
            return	"derive: " +  this.getExpression().ToString(); 
        }
	
	
	
        /**
	 * @return Returns the derivedAttribute.
	 */
        public CoreAttribute getDerivedAttribute() {
            return derivedAttribute;
        }
        /**
	 * @param derivedAttribute The derivedAttribute to set.
	 */
        public void setDerivedAttribute(CoreAttribute derivedAttribute) {
            this.derivedAttribute = derivedAttribute;
        }
	
        public override Object Clone() {
            OclAttributeDeriveConstraintImpl theClone = (OclAttributeDeriveConstraintImpl) base.Clone();
		
            theClone.derivedAttribute = derivedAttribute;
		
            return	theClone;
        }

    }
}
