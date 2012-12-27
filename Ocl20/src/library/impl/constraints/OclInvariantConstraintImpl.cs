using System;
using Ocl20.library.iface.constraints;

namespace Ocl20.library.impl.constraints
{
    public class OclInvariantConstraintImpl : OclClassifierConstraintImpl, OclInvariantConstraint {

        private	String	name;
	
        /**
	 * @param object
	 */
        public OclInvariantConstraintImpl() {
        }
	
        public override String ToString() {
            return	"inv" + (this.getName() != null ? (" " +  this.getName() + ": ") : (": ")) + this.getExpression().ToString();
        }
	
	

        /**
	 * @return Returns the name.
	 */
        public String getName() {
            return name;
        }
        /**
	 * @param name The name to set.
	 */
        public void setName(String name) {
            this.name = name;
        }
	
        public override Object Clone() {
            OclInvariantConstraintImpl theClone = (OclInvariantConstraintImpl) base.Clone();
		
            theClone.name = name;
		
            return	theClone;
        }
	
    }
}
