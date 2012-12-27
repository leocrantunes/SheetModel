using System;
using OclLibrary.iface.constraints;

namespace OclLibrary.impl.constraints
{
    public class OclPreConstraintImpl : OclOperationConstraintImpl, OclPreConstraint {

        private	OclPrePostConstraint	owner;
        private	String	name;
	
        /**
	 * @param object
	 */
        public OclPreConstraintImpl() {
        }

        public	String	toString() {
            return	"pre" +  (this.getName() != null ? (" " +  this.getName() + ": ") : (": ")) + this.getExpression().ToString(); 
        }

	
        /**
	 * @return Returns the owner.
	 */
        public OclPrePostConstraint getOwner() {
            return owner;
        }
        /**
	 * @param owner The owner to set.
	 */
        public void setOwner(OclPrePostConstraint owner) {
            this.owner = owner;
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
            OclPreConstraintImpl theClone = (OclPreConstraintImpl) base.Clone();
		
            theClone.name = name;
		
            return	theClone;
        }
	
    }
}
