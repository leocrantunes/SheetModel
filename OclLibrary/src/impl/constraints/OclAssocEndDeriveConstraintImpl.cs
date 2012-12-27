using System;
using OclLibrary.iface.common;
using OclLibrary.iface.constraints;

namespace OclLibrary.impl.constraints
{
    public class OclAssocEndDeriveConstraintImpl : OclClassifierConstraintImpl, OclAssocEndDeriveConstraint {

        private	CoreAssociationEnd	derivedAssocEnd;
	
        /**
	 * @param object
	 */
        public OclAssocEndDeriveConstraintImpl() {
        }
	
        public	String	toString() {
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
