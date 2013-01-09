using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public abstract class CoreAssociationEndImpl : CoreModelElementImpl, CoreAssociationEnd {
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociationEnd#getFullPathName()
	 */
        public string getFullPathName() {
            return	getTheAssociation().getName() + "::" + this.getName();
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreModelElement#getName()
	 */
        public override string getName() {
            string name = base.getName();
		
            if (!string.IsNullOrEmpty(name)) {
                return	name;
            }
            else {
                return	getTheParticipant().getName();
            }
        }
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociationEnd#getQualifiers()
	 */
        public abstract void setTheParticipant(CoreClassifier newValue);

        public List<object> getTheQualifiers() {
            return adjustListResult(getSpecificQualifiers());
        }
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociationEnd#getTheAssociation()
	 */
        public CoreAssociation getTheAssociation() {
            return	getSpecificAssociation();
        }

        public abstract void setTheAssociation(CoreAssociation newValue);

        /* (non-Javadoc)
	 * @see ocl20.CoreAssociationEnd#getTheParticipant()
	 */
        public CoreClassifier getTheParticipant() {
            return	getSpecificParticipant();
        }
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociationEnd#isMandatory()
	 */
        public bool isMandatory() {
            return	getSpecificIsMandatory();
        }
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociationEnd#isOneMultiplicity()
	 */
        public bool isOneMultiplicity() {
            return getSpecificIsOneMultiplicity();
        }
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociationEnd#isOrdered()
	 */
        public bool isOrdered() {
            return getSpecificIsOrdered();
        }

        protected virtual CoreAssociation getSpecificAssociation() {
            return	null;
        }
        protected virtual List<object>	getSpecificQualifiers() {
            return	new List<object>();
        }
        protected virtual CoreClassifier getSpecificParticipant() {
            return	null;
        }
        protected virtual bool getSpecificIsMandatory() {
            return	false;
        }
        protected virtual bool getSpecificIsOneMultiplicity() {
            return	false;
        }
        protected virtual bool getSpecificIsOrdered() {
            return	false;
        }
	
        /* (non-Javadoc)
	 * @see ocl20.common.CoreAssociationEnd#getDeriveConstraint()
	 */
        public List<object> getDeriveConstraint() {
            // TODO Auto-generated method stub
            return null;
        }
        /* (non-Javadoc)
	 * @see ocl20.common.CoreAssociationEnd#getInitConstraint()
	 */
        public List<object> getInitConstraint() {
            // TODO Auto-generated method stub
            return null;
        }

    }
}
