/**
 * AssociationClassCallExpCoreAssociationClass association proxy interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.common;

namespace OclLibrary.iface.expressions
{
    public interface AssociationClassCallExpCoreAssociationClass {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param associationClassExp Value of the first association end.
     * @param referredAssociationClass Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(AssociationClassCallExp associationClassExp, CoreAssociationClass referredAssociationClass);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param associationClassExp Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getAssociationClassExp(CoreAssociationClass referredAssociationClass);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param referredAssociationClass Required value of the second association 
     * end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAssociationClass getReferredAssociationClass(AssociationClassCallExp associationClassExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param associationClassExp Value of the first association end.
     * @param referredAssociationClass Value of the second association end.
     */
        bool add(AssociationClassCallExp associationClassExp, CoreAssociationClass referredAssociationClass);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param associationClassExp Value of the first association end.
     * @param referredAssociationClass Value of the second association end.
     */
        bool remove(AssociationClassCallExp associationClassExp, CoreAssociationClass referredAssociationClass);
    }
}
