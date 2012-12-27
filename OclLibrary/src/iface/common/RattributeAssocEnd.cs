/**
 * RAttributeAssocEnd association proxy interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.common
{
    public interface RattributeAssocEnd {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theQualifiers Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CoreAssociationEnd theAssociationEnd, CoreAttribute theQualifiers);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param theAssociationEnd Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAssociationEnd getTheAssociationEnd(CoreAttribute theQualifiers);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param theQualifiers Required value of the second association end.
     * @return List of related objects.
     */
        List<object> getTheQualifiers(CoreAssociationEnd theAssociationEnd);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theQualifiers Value of the second association end.
     */
        bool add(CoreAssociationEnd theAssociationEnd, CoreAttribute theQualifiers);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param theAssociationEnd Value of the first association end.
     * @param theQualifiers Value of the second association end.
     */
        bool remove(CoreAssociationEnd theAssociationEnd, CoreAttribute theQualifiers);
    }
}
