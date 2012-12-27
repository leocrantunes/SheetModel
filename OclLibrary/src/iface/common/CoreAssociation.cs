/**
 * CoreAssociation object instance interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OclLibrary.iface.common
{
    public interface CoreAssociation : CoreModelElement {
        /**
     * @param roleName 
     * @return 
     */
        CoreAssociationEnd getAssociationEnd(string roleName);
        /**
     * @return 
     */
        string getFullPathName();
        /**
     * @param classifier 
     * @return 
     */
        List<object> getTheAssociationEnds(CoreClassifier classifier);
        /**
     * @param classifier 
     * @return 
     */
        bool isClassifierInAssociation(CoreClassifier classifier);
        /**
     * Returns the value of reference theAssociationEnds.
     * @return Value of reference theAssociationEnds.
     */
        List<object> getTheAssociationEnds();
    }
}
