

/**
 * NavigationCallExpCoreAssociationEnd association proxy interface.
 */

using System.Collections.Generic;

namespace OclLibrary.iface.expressions
{
    public interface NavigationCallExpCoreAssociationEnd {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param navigationCallExp Value of the first association end.
     * @param navigationSource Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(NavigationCallExp navigationCallExp, CoreAssociationEnd navigationSource);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param navigationCallExp Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getNavigationCallExp(CoreAssociationEnd navigationSource);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param navigationSource Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAssociationEnd getNavigationSource(NavigationCallExp navigationCallExp);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param navigationCallExp Value of the first association end.
     * @param navigationSource Value of the second association end.
     */
        bool add(NavigationCallExp navigationCallExp, CoreAssociationEnd navigationSource);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param navigationCallExp Value of the first association end.
     * @param navigationSource Value of the second association end.
     */
        bool remove(NavigationCallExp navigationCallExp, CoreAssociationEnd navigationSource);
    }
}
