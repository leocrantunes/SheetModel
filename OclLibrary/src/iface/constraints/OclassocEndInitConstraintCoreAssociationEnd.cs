/**
 * OCLAssocEndInitConstraintCoreAssociationEnd association proxy interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.common;
using CoreAssociationEnd = OclLibrary.iface.expressions.CoreAssociationEnd;

namespace OclLibrary.iface.constraints
{
    public interface OclassocEndInitConstraintCoreAssociationEnd {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param initConstraint Value of the first association end.
     * @param initializedAssocEnd Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OclAssocEndInitConstraint initConstraint, CoreAssociationEnd initializedAssocEnd);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param initConstraint Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getInitConstraint(CoreAssociationEnd initializedAssocEnd);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param initializedAssocEnd Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAssociationEnd getInitializedAssocEnd(OclAssocEndInitConstraint initConstraint);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param initConstraint Value of the first association end.
     * @param initializedAssocEnd Value of the second association end.
     */
        bool add(OclAssocEndInitConstraint initConstraint, CoreAssociationEnd initializedAssocEnd);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param initConstraint Value of the first association end.
     * @param initializedAssocEnd Value of the second association end.
     */
        bool remove(OclAssocEndInitConstraint initConstraint, CoreAssociationEnd initializedAssocEnd);
    }
}
