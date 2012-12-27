using System;
using System.Collections.Generic;
using Ocl20.library.iface.constraints;
using Ocl20.library.utils;

namespace Ocl20.library.impl.constraints
{
    public class OclBodyConstraintImpl : OclOperationConstraintImpl, OclBodyConstraint {

        private	List<string> parameterNames;
	
        /**
	 * @param object
	 */
        public OclBodyConstraintImpl() {
        }

        public override String	ToString() {
            return	"body: " +  this.getExpression().ToString(); 
        }

        /**
	 * @return Returns the parameterNames.
	 */
        public List<string> getParameterNames() {
            return parameterNames;
        }
        /**
	 * @param parameterNames The parameterNames to set.
	 */
        public void setParameterNames(List<string> parameterNames) {
            this.parameterNames = parameterNames;
        }
	
        public override Object Clone() {
            OclBodyConstraintImpl theClone = (OclBodyConstraintImpl) base.Clone();

            if (parameterNames != null)
                theClone.parameterNames = (List<string>) parameterNames.Clone();
        
            return	theClone;
        }

    }
}
