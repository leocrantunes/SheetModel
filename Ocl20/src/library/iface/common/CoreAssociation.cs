/**
 * CoreAssociation object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface CoreAssociation : CoreModelElement {

        CoreAssociationEnd getAssociationEnd(string roleName);
        string getFullPathName();
        List<object> getTheAssociationEnds(CoreClassifier classifier);
        bool isClassifierInAssociation(CoreClassifier classifier);
        List<object> getTheAssociationEnds();
    }
}
