/**
 * CoreModel object instance interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.environment;
using Ocl20.library.impl.environment;

namespace Ocl20.library.iface.common
{
    public interface CoreModel : CorePackage {
        /**
     * @param classifier 
     * @return 
     */
        List<object> getAssociationEndsForClassifier(CoreClassifier classifier);
        /**
     * @param classifier 
     * @return 
     */
        List<object> getAssociationClassesForClassifier(CoreClassifier classifier);
        /**
     * @param associationName 
     * @return 
     */
        CoreAssociation getAssociationForName(string associationName);
        /**
     * @return 
     */
        List<object> getAllStereotypes();
        /**
     * @param association 
     */
        void addAssociation(CoreAssociation association);
        /**
     * @param classifier 
     * @return 
     */
        CoreClassifier toOclType(CoreClassifier classifier);
        /**
     * @param environment 
     */
        void populateEnvironment(Environment environment);
    
        void setMainPackage(object mainPackage);
        object getMainPackage();
        void setOclPackage(Ocl20Package oclPackage);
        Ocl20Package getOclPackage();
    }
}
