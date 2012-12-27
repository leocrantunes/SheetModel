using System;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.impl.constraints
{
    public class OclDefConstraintImpl : OclClassifierConstraintImpl, OclDefConstraint {

        /**
	 * 
	 */
        public OclDefConstraintImpl() {
        }

        public override String ToString() {
            return	"def: " + this.getExpression().ToString();
        }
    }
}

