using System.Collections.Generic;
using OclLibrary.iface.common;
using OclLibrary.iface.environment;

namespace OclLibrary.impl.common
{
    public abstract class CoreAssociationClassImpl : CoreClassifierImpl, CoreAssociationClass {
	
        public CoreAssociationEnd lookupAssociationEnd(CoreClassifier c) {
            Environment env = getEnvironmentWithoutParents();

            foreach (CoreAssociationEnd assocEnd in getTheAssociationEnds()) {
                if (assocEnd.getTheParticipant() != c) {
                    return assocEnd;
                }
            }

            return null;
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#getAssociationEnd(java.lang.String)
	 */
        public CoreAssociationEnd getAssociationEnd(string roleName) {
            return	CoreAssociationHelper.getAssociationEnd(this, roleName);
        }
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#getTheAssociationEnds(ocl20.CoreClassifier)
	 */
        public List<object> getTheAssociationEnds(CoreClassifier classifier) {
            return	CoreAssociationHelper.getTheAssociationEnds(this, classifier);
        }
    
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#isClassifierInAssociation(ocl20.CoreClassifier[])
	 */
        public bool isClassifierInAssociation(CoreClassifier classifier) {
            return	CoreAssociationHelper.isClassifierInAssociation(this, classifier);
        }
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#getTheAssociationEnds()
	 */
        public List<object> getTheAssociationEnds() {
            return	adjustListResult(getSpecificAssociationEnds());
        }

        public override abstract List<object> getSpecificAssociationEnds();
    }
}
