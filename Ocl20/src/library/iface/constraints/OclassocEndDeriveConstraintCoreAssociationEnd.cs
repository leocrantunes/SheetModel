/**
 * OCLAssocEndDeriveConstraintCoreAssociationEnd association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.iface.constraints
{
    public interface OclassocEndDeriveConstraintCoreAssociationEnd {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param deriveConstraint Value of the first association end.
     * @param derivedAssocEnd Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OclAssocEndDeriveConstraint deriveConstraint, CoreAssociationEnd derivedAssocEnd);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param deriveConstraint Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getDeriveConstraint(CoreAssociationEnd derivedAssocEnd);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param derivedAssocEnd Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreAssociationEnd getDerivedAssocEnd(OclAssocEndDeriveConstraint deriveConstraint);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param deriveConstraint Value of the first association end.
     * @param derivedAssocEnd Value of the second association end.
     */
        bool add(OclAssocEndDeriveConstraint deriveConstraint, CoreAssociationEnd derivedAssocEnd);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param deriveConstraint Value of the first association end.
     * @param derivedAssocEnd Value of the second association end.
     */
        bool remove(OclAssocEndDeriveConstraint deriveConstraint, CoreAssociationEnd derivedAssocEnd);
    }
}
