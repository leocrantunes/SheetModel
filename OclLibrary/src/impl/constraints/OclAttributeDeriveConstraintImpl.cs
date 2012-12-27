using System;
using OclLibrary.iface.common;
using OclLibrary.iface.constraints;

namespace OclLibrary.impl.constraints
{
    public class OclAttributeDeriveConstraintImpl : OclClassifierConstraintImpl, OclAttributeDeriveConstraint {

        private	CoreAttribute		derivedAttribute;
	
        /**
	 * @param object
	 */
        public OclAttributeDeriveConstraintImpl() {
        }

        public	String	toString() {
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
