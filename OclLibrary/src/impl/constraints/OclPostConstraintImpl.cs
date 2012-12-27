using System;
using OclLibrary.iface.constraints;

namespace OclLibrary.impl.constraints
{
    public class OclPostConstraintImpl : OclOperationConstraintImpl, OclPostConstraint {

        private	OclPrePostConstraint	owner;
        private	String	name;

        /**
	 * @param object
	 */
        public OclPostConstraintImpl() {
        }

        public	String	toString() {
            return	"post" +  (this.getName() != null ? (" " +  this.getName() + ": ") : (": ")) + this.getExpression().ToString(); 
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
	
	
        public override Object Clone() {
            OclPostConstraintImpl theClone = (OclPostConstraintImpl) base.Clone();
		
            theClone.name = name;
		
            return	theClone;
        }

    }
}
