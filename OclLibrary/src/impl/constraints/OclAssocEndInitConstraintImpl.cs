using System;
using OclLibrary.iface.common;
using OclLibrary.iface.constraints;

namespace OclLibrary.impl.constraints
{
    public class OclAssocEndInitConstraintImpl : OclClassifierConstraintImpl, OclAssocEndInitConstraint {

        private	CoreAssociationEnd	initializedAssocEnd;
	
        /**
	 * @param object
	 */
        public OclAssocEndInitConstraintImpl() {
        }

        public	String	toString() {
            return	"init: " +  this.getExpression().ToString(); 
        }

	
	
        /**
	 * @return Returns the initializedAssocEnd.
	 */
        public CoreAssociationEnd getInitializedAssocEnd() {
            return initializedAssocEnd;
        }
        /**
	 * @param initializedAssocEnd The initializedAssocEnd to set.
	 */
        public void setInitializedAssocEnd(CoreAssociationEnd initializedAssocEnd) {
            this.initializedAssocEnd = initializedAssocEnd;
        }
	
        public override Object Clone() {
            OclAssocEndInitConstraintImpl theClone = (OclAssocEndInitConstraintImpl) base.Clone();
		
            theClone.initializedAssocEnd = initializedAssocEnd;
		
            return	theClone;
        }

    }
}
