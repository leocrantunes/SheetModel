using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.impl.constraints
{
    public class OclAssocEndDeriveConstraintImpl : OclClassifierConstraintImpl, OclAssocEndDeriveConstraint {

        private	CoreAssociationEnd	derivedAssocEnd;
	
        /**
	 * @param object
	 */
        public OclAssocEndDeriveConstraintImpl() {
        }
	
        public override String ToString() {
            return	"derive: " +  this.getExpression().ToString(); 
        }
	
	
	
        /**
	 * @return Returns the derivedAssocEnd.
	 */
        public CoreAssociationEnd getDerivedAssocEnd() {
            return derivedAssocEnd;
        }
        /**
	 * @param derivedAssocEnd The derivedAssocEnd to set.
	 */
        public void setDerivedAssocEnd(CoreAssociationEnd derivedAssocEnd) {
            this.derivedAssocEnd = derivedAssocEnd;
        }
	
        public override Object Clone() {
            OclAssocEndDeriveConstraintImpl theClone = (OclAssocEndDeriveConstraintImpl) base.Clone();
		
            theClone.derivedAssocEnd = derivedAssocEnd;
		
            return	theClone;
        }
    }
}
