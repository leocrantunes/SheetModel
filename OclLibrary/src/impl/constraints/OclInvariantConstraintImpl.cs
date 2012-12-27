using System;

namespace OclLibrary.impl.constraints
{
    public class OclInvariantConstraintImpl : OclClassifierConstraintImpl, OclInvariantConstraint {

        private	String	name;
	
        /**
	 * @param object
	 */
        public OclInvariantConstraintImpl() {
        }
	
        public String toString() {
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
