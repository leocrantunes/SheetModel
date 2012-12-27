/**
 * AssociationConnections association proxy interface.
 */

using System.Collections.Generic;

namespace Ocl20.library.iface.common
{
    public interface AssociationConnections {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param theAssociation Value of the first association end.
     * @param theAssociationEnds Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(CoreAssociation theAssociation, expressions.CoreAssociationEnd theAssociationEnds);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param theAssociation Required value of the first association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAssociation getTheAssociation(expressions.CoreAssociationEnd theAssociationEnds);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param theAssociationEnds Required value of the second association end.
     * @return List of related objects.
     */
        List<object> getTheAssociationEnds(CoreAssociation theAssociation);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param theAssociation Value of the first association end.
     * @param theAssociationEnds Value of the second association end.
     */
        bool add(CoreAssociation theAssociation, expressions.CoreAssociationEnd theAssociationEnds);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param theAssociation Value of the first association end.
     * @param theAssociationEnds Value of the second association end.
     */
        bool remove(CoreAssociation theAssociation, expressions.CoreAssociationEnd theAssociationEnds);
    }
}
