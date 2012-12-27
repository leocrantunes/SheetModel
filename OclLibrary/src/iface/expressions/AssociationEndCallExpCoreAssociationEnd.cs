/**
 * AssociationEndCallExpCoreAssociationEnd association proxy interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.expressions
{
    public interface AssociationEndCallExpCoreAssociationEnd {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param associationEndCallExp Value of the first association end.
     * @param referredAssociationEnd Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(AssociationEndCallExp associationEndCallExp, CoreAssociationEnd referredAssociationEnd);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param associationEndCallExp Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getAssociationEndCallExp(CoreAssociationEnd referredAssociationEnd);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param referredAssociationEnd Required value of the second association 
     * end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAssociationEnd getReferredAssociationEnd(AssociationEndCallExp associationEndCallExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param associationEndCallExp Value of the first association end.
     * @param referredAssociationEnd Value of the second association end.
     */
        bool add(AssociationEndCallExp associationEndCallExp, CoreAssociationEnd referredAssociationEnd);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param associationEndCallExp Value of the first association end.
     * @param referredAssociationEnd Value of the second association end.
     */
        bool remove(AssociationEndCallExp associationEndCallExp, CoreAssociationEnd referredAssociationEnd);
    }
}
