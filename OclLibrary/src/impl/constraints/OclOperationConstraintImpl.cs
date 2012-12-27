using System;
using OclLibrary.iface.common;

namespace OclLibrary.impl.constraints
{
    public abstract class OclOperationConstraintImpl : OclConstraintImpl {

        private	CoreOperation contextualOperation;
	
        /**
	 * 
	 */
        public OclOperationConstraintImpl() : base() {
            // TODO Auto-generated constructor stub
        }
	
        /**
	 * @return Returns the contextualOperation.
	 */
        public CoreOperation getContextualOperation() {
            return contextualOperation;
        }
        /**
	 * @param contextualOperation The contextualOperation to set.
	 */
        public void setContextualOperation(CoreOperation contextualOperation) {
            this.contextualOperation = contextualOperation;
        }
	
        public override Object Clone() {
            OclOperationConstraintImpl theClone = (OclOperationConstraintImpl) base.Clone();
		
            theClone.contextualOperation = contextualOperation;
		
            return	theClone;
        }
	
    }
}
