using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public abstract class CoreAssociationImpl : CoreModelElementImpl, CoreAssociation {

        private	string	fullName = null;
	
        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#getAssociationEnd(java.lang.String)
	 */
        public CoreAssociationEnd getAssociationEnd(String roleName) {
            return	CoreAssociationHelper.getAssociationEnd(this, roleName);
        }
	

        /* (non-Javadoc)
	 * @see ocl20.CoreAssociation#getFullPathName()
	 */
        public String getFullPathName() {
            if (fullName == null)
                this.fullName = buildName();
            
            return	this.fullName;
        }
    
        private String buildName() {
            return	CoreAssociationHelper.buildName(this);
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

        public	abstract List<object> getSpecificAssociationEnds();
    }
}
