using System;
using System.Collections.Generic;
using System.Linq;
using OclLibrary.iface.constraints;
using OclLibrary.utils;

namespace OclLibrary.impl.constraints
{
    public class OclBodyConstraintImpl : OclOperationConstraintImpl, OclBodyConstraint {

        private	List<string> parameterNames;
	
        /**
	 * @param object
	 */
        public OclBodyConstraintImpl() {
        }

        public	String	toString() {
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
