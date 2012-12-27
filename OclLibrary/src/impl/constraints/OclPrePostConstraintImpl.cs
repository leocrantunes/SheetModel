using System;
using System.Collections.Generic;
using System.Linq;
using OclLibrary.iface.constraints;
using OclLibrary.utils;

namespace OclLibrary.impl.constraints
{
    public class OclPrePostConstraintImpl : OclOperationConstraintImpl, OclPrePostConstraint {

        private List<OclPreConstraint> preConditions;
        private List<OclPostConstraint> postConditions;
	
        /**
	 * @param object
	 */
        public OclPrePostConstraintImpl() {
            preConditions = new List<OclPreConstraint>();
            postConditions = new List<OclPostConstraint>();
        }
	
        /**
	 * @return Returns the postConditions.
	 */
        public List<OclPostConstraint> getPostConditions()
        {
            return postConditions;
        }
        /**
	 * @param postConditions The postConditions to set.
	 */
        public void setPostConditions(List<OclPostConstraint> postConditions)
        {
            this.postConditions = postConditions;
        }
        /**
	 * @return Returns the preConditions.
	 */
        public List<OclPreConstraint> getPreConditions()
        {
            return preConditions;
        }
        /**
	 * @param preConditions The preConditions to set.
	 */
        public void setPreConditions(List<OclPreConstraint> preConditions)
        {
            this.preConditions = preConditions;
        }
	
        public void addPreCondition(OclPreConstraint constraint) {
            this.preConditions.Add(constraint);
        }
	
        public void addPostCondition(OclPostConstraint constraint) {
            this.postConditions.Add(constraint);
        }

        public override Object Clone() {
            OclPrePostConstraintImpl theClone = (OclPrePostConstraintImpl) base.Clone();
		
            if (preConditions != null) {
                theClone.preConditions = new List<OclPreConstraint>(postConditions.Cast<OclPreConstraintImpl>().ToList().Clone());
                foreach (OclPreConstraint preConstraint in theClone.preConditions) {
                    preConstraint.setOwner(theClone);
                }
            }
		
            if (postConditions != null) {
                theClone.postConditions = new List<OclPostConstraint>(postConditions.Cast<OclPostConstraintImpl>().ToList().Clone());
                foreach (OclPostConstraint postConstraint in theClone.postConditions) {
                    postConstraint.setOwner(theClone);
                }
            }
		
            return	theClone;
        }
	
    }
}
